using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Permissions;
using System.Xml.Serialization;
using FileManagement.FilesChangeManagers;
using FileManagement.FilesChangeManagers.FileChangeDescriptions;

namespace FileManagement
{
    public class FilesObserver : IFilesObserver
    {
        private const string InitialBackupFileName = "Initial save.txt";
        private const string ChangesStorageFileName = "Changes.txt";

        private DirectoryObject _observableDirectoryObject;
        private readonly string _backupDirectoryPath;

        public FilesObserver(string path, string backupDirectoryPath)
        {
            if (!Directory.Exists(path))
                throw new ArgumentException("Directory does not exist", nameof(path));

            _backupDirectoryPath = backupDirectoryPath;

            if (!Directory.Exists(backupDirectoryPath))
            {
                Init(path);
                return;
            }

            try
            {
                LoadLastBackup();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Error of load last backup. File not found.");
                Init(path);
            }
            catch (Exception)
            {
                Console.WriteLine($"Error of load last backup.");
                Init(path);
            }
        }

        public void StartObserving()
        {
            
        }

        private void Observe(DirectoryObject directoryObject, DateTime dateTime)
        {
            CheckDeleteFileSystemObjects(directoryObject, dateTime);

            CheckAddFileSystemObjects(directoryObject, dateTime);

            foreach (var innerFileObject in directoryObject.InnerFileObjects)
            {
                var fi = new FileInfo(innerFileObject.FullPath);
                byte[] data = null;

                try
                {
                    data = File.ReadAllBytes(fi.FullName);
                }
                catch (Exception)
                {
                    Console.WriteLine("Something went wrong with files comparing...");
                    continue;
                }

                if (innerFileObject.LastData != null && Utils.GetHash(innerFileObject.LastData) == Utils.GetHash(data))
                    continue;

                if (innerFileObject.LastData == null)
                {
                    innerFileObject.LastData = data;

                    var temp = new byte[data.Length];
                    Array.Copy(data, temp, data.Length);

                    innerFileObject.FileChangeDescriptions.Add(new AddDataDescription(dateTime, 0, temp));
                    continue;
                }
            }
        }

        private static void CheckAddFileSystemObjects(DirectoryObject directoryObject, DateTime dateTime)
        {
            DirectoryInfo currentDirectoryInfo = new DirectoryInfo(directoryObject.FullPath);

            foreach (var directoryInfo in currentDirectoryInfo.GetDirectories())
            {
                if (directoryObject.InnerDirectoryObjects.All(dirObj => dirObj.FullPath != directoryInfo.FullName))
                {
                    var dirObj = new DirectoryObject(directoryInfo.FullName, dateTime);
                    directoryObject.InnerDirectoryObjects.Add(dirObj);
                    dirObj.FileChangeDescriptions.Add(new CreateFileSystemObjectDescription(dateTime));
                }
            }

            foreach (var fileInfo in currentDirectoryInfo.GetFiles())
            {
                if (directoryObject.InnerFileObjects.All(fileObj => fileObj.FullPath != fileInfo.FullName))
                {
                    var fileObject = new FileObject(fileInfo.FullName, dateTime);
                    directoryObject.InnerFileObjects.Add(fileObject);
                    fileObject.FileChangeDescriptions.Add(new CreateFileSystemObjectDescription(dateTime));
                }
                else if (directoryObject.InnerFileObjects.Find(fileObj =>
                    fileObj.FullPath == fileInfo.FullName && fileObj.LastData == null) != null)
                {
                    var obj = directoryObject.InnerFileObjects.Find(fileObj =>
                        fileObj.FullPath == fileInfo.FullName && fileObj.LastData == null);

                    obj?.FileChangeDescriptions.Add(new CreateFileSystemObjectDescription());
                }
            }
        }

        private static void CheckDeleteFileSystemObjects(DirectoryObject directoryObject, DateTime dateTime)
        {
            foreach (var innerDirectoryObject in directoryObject.InnerDirectoryObjects)
            {
                if (!Directory.Exists(innerDirectoryObject.FullPath))
                {
                    innerDirectoryObject.FileChangeDescriptions.Add(new DeleteFileSystemObjectDescription(dateTime));
                }
            }

            foreach (var innerFileObject in directoryObject.InnerFileObjects)
            {
                if (innerFileObject.LastData != null && !File.Exists(innerFileObject.FullPath))
                {
                    innerFileObject.FileChangeDescriptions.Add(new DeleteFileSystemObjectDescription(dateTime));
                    innerFileObject.LastData = null;
                }
            }
        }

        private void Init(string path)
        {
            Directory.CreateDirectory(_backupDirectoryPath);
            _observableDirectoryObject = new DirectoryObject(path, DateTime.Now);

            var serializer = new XmlSerializer(typeof(DirectoryObject), new Type[]
            {
                typeof(FileObject),
                typeof(AddDataDescription),
                typeof(CreateFileSystemObjectDescription),
                typeof(DeleteFileSystemObjectDescription),
                typeof(RemoveDataDescription)
            });

            serializer.Serialize(new FileStream(
                MakeInitialBackupFileFullName(), FileMode.OpenOrCreate), _observableDirectoryObject);
        }

        private void LoadLastBackup()
        {
            var serializer = new XmlSerializer(typeof(DirectoryObject), new[]
            {
                typeof(FileObject),
                typeof(AddDataDescription),
                typeof(CreateFileSystemObjectDescription),
                typeof(DeleteFileSystemObjectDescription),
                typeof(RemoveDataDescription)
            });

            _observableDirectoryObject = (DirectoryObject)serializer.Deserialize(
                new FileStream(MakeInitialBackupFileFullName(), FileMode.Open));
        }

        private string MakeInitialBackupFileFullName() => $@"{_backupDirectoryPath}\{InitialBackupFileName}";
        private string MakeChangesStorageFileFullName() => $@"{_backupDirectoryPath}\{ChangesStorageFileName}";
    }
}

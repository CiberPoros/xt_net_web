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

        private bool _wasChangeOnLastCheck = false;

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
            while (true)
            {
                Observe(_observableDirectoryObject, DateTime.Now);

                if (_wasChangeOnLastCheck)
                    Save();

                _wasChangeOnLastCheck = false;
            }
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
                    _wasChangeOnLastCheck = true;
                    continue;
                }

                // TODO: подумать
                innerFileObject.FileChangeDescriptions.Add(new RemoveDataDescription(dateTime, 0, innerFileObject.LastData.Length));
                innerFileObject.FileChangeDescriptions.Add(new AddDataDescription(dateTime, 0, data.ToArray()));

                innerFileObject.LastData = data;

                _wasChangeOnLastCheck = true;
            }
        }

        private void CheckAddFileSystemObjects(DirectoryObject directoryObject, DateTime dateTime)
        {
            DirectoryInfo currentDirectoryInfo = new DirectoryInfo(directoryObject.FullPath);

            foreach (var directoryInfo in currentDirectoryInfo.GetDirectories())
            {
                if (directoryObject.InnerDirectoryObjects.All(dirObj => dirObj.FullPath != directoryInfo.FullName))
                {
                    var dirObj = new DirectoryObject(directoryInfo.FullName, dateTime);
                    directoryObject.InnerDirectoryObjects.Add(dirObj);
                    dirObj.FileChangeDescriptions.Add(new CreateFileSystemObjectDescription(dateTime));
                    _wasChangeOnLastCheck = true;

                    Observe(dirObj, dateTime);
                }
            }

            foreach (var fileInfo in currentDirectoryInfo.GetFiles())
            {
                if (directoryObject.InnerFileObjects.All(fileObj => fileObj.FullPath != fileInfo.FullName))
                {
                    var fileObject = new FileObject(fileInfo.FullName, dateTime);
                    directoryObject.InnerFileObjects.Add(fileObject);
                    fileObject.FileChangeDescriptions.Add(new CreateFileSystemObjectDescription(dateTime));
                    _wasChangeOnLastCheck = true;
                }
                else if (directoryObject.InnerFileObjects.Find(fileObj =>
                    fileObj.FullPath == fileInfo.FullName && fileObj.LastData == null) != null)
                {
                    var obj = directoryObject.InnerFileObjects.Find(fileObj =>
                        fileObj.FullPath == fileInfo.FullName && fileObj.LastData == null);

                    if (obj != null)
                        _wasChangeOnLastCheck = true;

                    obj?.FileChangeDescriptions.Add(new CreateFileSystemObjectDescription());
                }
            }
        }

        private void CheckDeleteFileSystemObjects(DirectoryObject directoryObject, DateTime dateTime)
        {
            foreach (var innerDirectoryObject in directoryObject.InnerDirectoryObjects)
            {
                if (!Directory.Exists(innerDirectoryObject.FullPath))
                {
                    innerDirectoryObject.FileChangeDescriptions.Add(new DeleteFileSystemObjectDescription(dateTime));
                    _wasChangeOnLastCheck = true;
                }
                else
                {
                    Observe(innerDirectoryObject, dateTime);
                }
            }

            foreach (var innerFileObject in directoryObject.InnerFileObjects)
            {
                if (innerFileObject.LastData != null && !File.Exists(innerFileObject.FullPath))
                {
                    innerFileObject.FileChangeDescriptions.Add(new DeleteFileSystemObjectDescription(dateTime));
                    innerFileObject.LastData = null;
                    _wasChangeOnLastCheck = true;
                }
            }
        }

        private void Init(string path)
        {
            Directory.CreateDirectory(_backupDirectoryPath);
            _observableDirectoryObject = new DirectoryObject(path, DateTime.Now);

            Save();
        }

        private void Save()
        {
            var serializer = new XmlSerializer(typeof(DirectoryObject), new Type[]
            {
                typeof(FileObject),
                typeof(AddDataDescription),
                typeof(CreateFileSystemObjectDescription),
                typeof(DeleteFileSystemObjectDescription),
                typeof(RemoveDataDescription)
            });

            if (File.Exists(MakeInitialBackupFileFullName()))
                File.Delete(MakeInitialBackupFileFullName());

            using (var fs = new FileStream(MakeInitialBackupFileFullName(), FileMode.OpenOrCreate))
            {
                serializer.Serialize(fs, _observableDirectoryObject);
            }
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

            using (var fs = new FileStream(MakeInitialBackupFileFullName(), FileMode.Open))
            {
                _observableDirectoryObject = (DirectoryObject)serializer.Deserialize(fs);
            }
        }

        private string MakeInitialBackupFileFullName() => $@"{_backupDirectoryPath}\{InitialBackupFileName}";
    }
}

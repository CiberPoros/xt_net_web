using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using FileManagement.FileChangeDescriptions;
using FileManagement.FileSystemObjects;
using FileManagement.Interfaces;

namespace FileManagement.FilesObservers
{
    public class FilesObserver : IFilesObserver
    {
        private bool _wasChangeOnLastCheck = false;

        private DirectoryObject _observableDirectoryObject;

        public FilesObserver(string path)
        {
            if (!Directory.Exists(path))
                throw new ArgumentException("Directory does not exist", nameof(path));

            if (!Directory.Exists(Utils.BackupDirectoryPath))
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
                Debug.WriteLine($"Error of load last backup. File not found.");
                Init(path);
            }
            catch (Exception)
            {
                Debug.WriteLine($"Error of load last backup.");
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

            CheckFilesChanges(directoryObject, dateTime);
        }

        private void CheckFilesChanges(DirectoryObject directoryObject, DateTime dateTime)
        {
            foreach (var innerFileObject in directoryObject.InnerFileObjects)
            {
                var fi = new FileInfo(innerFileObject.FullPath);

                if (!fi.Exists)
                    continue;

                byte[] data;

                try
                {
                    data = File.ReadAllBytes(fi.FullName);
                }
                catch (Exception)
                {
                    Debug.WriteLine("Something went wrong with files comparing...");
                    continue;
                }

                if (innerFileObject.LastData != null && Utils.GetHash(innerFileObject.LastData) == Utils.GetHash(data))
                    continue;

                innerFileObject.LastData = data;

                var temp = new byte[data.Length];
                Array.Copy(data, temp, data.Length);

                innerFileObject.FileChangeDescriptions.Add(new DataChangeFileSystemObjectDescription(dateTime, temp));
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
            Directory.CreateDirectory(Utils.BackupDirectoryPath);
            _observableDirectoryObject = new DirectoryObject(path, DateTime.Now);

            Save();
        }

        private void Save()
        {
            var serializer = new XmlSerializer(typeof(DirectoryObject), Utils.UsableTypesForSerialization);

            if (File.Exists(Utils.BackupFileFullName))
                File.Delete(Utils.BackupFileFullName);

            using (var fs = new FileStream(Utils.BackupFileFullName, FileMode.OpenOrCreate))
            {
                serializer.Serialize(fs, _observableDirectoryObject);
            }
        }

        private void LoadLastBackup()
        {
            var serializer = new XmlSerializer(typeof(DirectoryObject), Utils.UsableTypesForSerialization);

            using (var fs = new FileStream(Utils.BackupFileFullName, FileMode.Open))
            {
                _observableDirectoryObject = (DirectoryObject)serializer.Deserialize(fs);
            }
        }
    }
}

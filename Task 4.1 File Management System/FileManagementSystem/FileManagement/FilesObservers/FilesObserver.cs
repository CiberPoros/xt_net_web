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
        private const string DirectoryDoesNotExistError = "Directory does not exist";
        private const string LastBackupNotFountError = "Error of load last backup. File not found.";
        private const string LastBackupLoadError = "Error of load last backup.";
        private const string LoadFileError = "Error of load file.";
        private const string FileNotFoundError = "Error of load file. File not found.";

        private bool _wasChangeOnLastCheck = false;

        private DirectoryObject _observableDirectoryObject;

        public FilesObserver(string path)
        {
            if (!Directory.Exists(path))
                throw new ArgumentException(DirectoryDoesNotExistError, nameof(path));

            if (!Directory.Exists(Utils.BackupDirectoryPath))
            {
                Init(path);
                return;
            }

            try
            {
                LoadLastBackup();
            }
            catch (FileNotFoundException e)
            {
                Debug.WriteLine(LastBackupNotFountError);
                Debug.WriteLine(e);
                Init(path);
            }
            catch (IOException e)
            {
                Debug.WriteLine(LastBackupLoadError);
                Debug.WriteLine(e);
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

        private static bool TryReadData(FileObject fileObject, out byte[] data)
        {
            data = null;
            var fi = new FileInfo(fileObject.FullPath);

            if (!fi.Exists)
                return false;

            try
            {
                data = File.ReadAllBytes(fileObject.FullPath);
                return true;
            }
            catch (FileNotFoundException e)
            {
                Debug.WriteLine(FileNotFoundError);
                Debug.WriteLine(e);
                return false;
            }
            catch (IOException e)
            {
                Debug.WriteLine(LoadFileError);
                Debug.WriteLine(e);
                return false;
            }
        }

        private void Init(string path)
        {
            Directory.CreateDirectory(Utils.BackupDirectoryPath);
            _observableDirectoryObject = new DirectoryObject(path, DateTime.Now);

            Save();
        }

        private void Observe(DirectoryObject directoryObject, DateTime dateTime)
        {
            CheckDeleteFileSystemObjects(directoryObject, dateTime);

            CheckAddFileSystemObjects(directoryObject, dateTime);

            CheckFilesChanges(directoryObject, dateTime);
        }

        #region CHECK_INNER_FILES_CHANGES
        private void CheckFilesChanges(DirectoryObject directoryObject, DateTime dateTime)
        {
            foreach (var innerFileObject in directoryObject.InnerFileObjects)
                CheckFileChange(innerFileObject, dateTime);
        }

        private void CheckFileChange(FileObject fileObject, DateTime dateTime)
        {
            if (!TryReadData(fileObject, out var data))
                return;

            if (fileObject.LastData != null && Utils.GetHash(fileObject.LastData) == Utils.GetHash(data))
                return;

            fileObject.LastData = data;

            var temp = new byte[data.Length];
            Array.Copy(data, temp, data.Length);

            fileObject.FileChangeDescriptions.Add(new DataChangeFileSystemObjectDescription(dateTime, temp));
            _wasChangeOnLastCheck = true;
        } 
        #endregion

        #region CHECK_ADD_TO_DIRECTORY_OBJECT_AND_INNER_OBJECTS
        private void CheckAddFileSystemObjects(DirectoryObject directoryObject, DateTime dateTime)
        {
            DirectoryInfo currentDirectoryInfo = new DirectoryInfo(directoryObject.FullPath);

            if (!currentDirectoryInfo.Exists)
                return;

            foreach (var directoryInfo in currentDirectoryInfo.GetDirectories())
                CheckAddDirectory(directoryObject, directoryInfo, dateTime);

            if (!currentDirectoryInfo.Exists)
                return;

            foreach (var fileInfo in currentDirectoryInfo.GetFiles())
                CheckAddFile(directoryObject, fileInfo, dateTime);
        }

        private void CheckAddDirectory(DirectoryObject directoryObject, DirectoryInfo innerDirectoryInfo, DateTime dateTime)
        {
            if (directoryObject.InnerDirectoryObjects.Any(dirObj => dirObj.FullPath == innerDirectoryInfo.FullName))
                return;

            var dirObject = new DirectoryObject(innerDirectoryInfo.FullName, dateTime);
            directoryObject.InnerDirectoryObjects.Add(dirObject);
            dirObject.FileChangeDescriptions.Add(new CreateFileSystemObjectDescription(dateTime));
            _wasChangeOnLastCheck = true;

            Observe(dirObject, dateTime);
        }

        private void CheckAddFile(DirectoryObject directoryObject, FileInfo innerFileInfo, DateTime dateTime)
        {
            if (directoryObject.InnerFileObjects.All(fileObj => fileObj.FullPath != innerFileInfo.FullName))
            {
                var fileObject = new FileObject(innerFileInfo.FullName, dateTime);
                directoryObject.InnerFileObjects.Add(fileObject);
                fileObject.FileChangeDescriptions.Add(new CreateFileSystemObjectDescription(dateTime));
                _wasChangeOnLastCheck = true;
                return;
            }

            var deletedFileObject = directoryObject.InnerFileObjects.Find(fileObj =>
                fileObj.FullPath == innerFileInfo.FullName && fileObj.LastData == null);

            if (deletedFileObject == null)
                return;

            _wasChangeOnLastCheck = true;
            deletedFileObject.FileChangeDescriptions.Add(new CreateFileSystemObjectDescription());
        }
        #endregion

        #region CHECK_DELETE_FYLE_SYSTEM_OBJECTS_AND_INNER_OBJECTS
        private void CheckDeleteFileSystemObjects(DirectoryObject directoryObject, DateTime dateTime)
        {
            foreach (var innerDirectoryObject in directoryObject.InnerDirectoryObjects)
                CheckDeleteDirectory(innerDirectoryObject, dateTime);

            foreach (var innerFileObject in directoryObject.InnerFileObjects)
                CheckDeleteFile(innerFileObject, dateTime);
        }

        private void CheckDeleteDirectory(DirectoryObject directoryObject, DateTime dateTime)
        {
            if (directoryObject.FileChangeDescriptions.Any() && directoryObject.FileChangeDescriptions.Last() is DeleteFileSystemObjectDescription)
                return;

            if (!Directory.Exists(directoryObject.FullPath))
            {
                directoryObject.FileChangeDescriptions.Add(new DeleteFileSystemObjectDescription(dateTime));
                _wasChangeOnLastCheck = true;
            }

            Observe(directoryObject, dateTime);
        }

        private void CheckDeleteFile(FileObject fileObject, DateTime dateTime)
        {
            if (fileObject.LastData == null || File.Exists(fileObject.FullPath))
                return;

            fileObject.FileChangeDescriptions.Add(new DeleteFileSystemObjectDescription(dateTime));
            fileObject.LastData = null;
            _wasChangeOnLastCheck = true;
        } 
        #endregion

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

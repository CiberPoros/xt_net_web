using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;
using FileManagement.FileChangeDescriptions;
using FileManagement.FileSystemObjects;
using FileManagement.Interfaces;

namespace FileManagement.FilesRestorers
{
    public class FilesRestorer : IFilesRestorer
    {
        private const string CanNotLoadLastBackupError = "Can't load last backup.";

        private static string RestoreCatalogName =>
            $"Restore_{DateTime.Now.ToString("G", CultureInfo.CreateSpecificCulture("de-DE")).Replace(':', '-')}";

        private string _restoreCatalog;
        private DirectoryObject _observableDirectoryObject;
        private string _restorableDirectoryName;
        private DateTime _restoreTime;

        public FilesRestorer()
        {
            try
            {
                LoadLastBackup();
            }
            catch (Exception)
            {
                Debug.WriteLine(CanNotLoadLastBackupError);
            }
        }

        public void Restore(DateTime dateTime, string restorableDirectoryName)
        {
            _restorableDirectoryName = restorableDirectoryName ?? throw new ArgumentNullException(nameof(restorableDirectoryName));

            if (_observableDirectoryObject == null)
                LoadLastBackup();

            _restoreCatalog = $@"{Environment.CurrentDirectory}\{RestoreCatalogName}";
            if (!Directory.Exists(_restoreCatalog))
                Directory.CreateDirectory(_restoreCatalog);

            _restoreTime = dateTime;

            RestoreDirectory(_observableDirectoryObject);
        }

        private void RestoreDirectoryObject(DirectoryObject directoryObject)
        {
            foreach (var innerDirectoryObject in directoryObject.InnerDirectoryObjects)
            {
                RestoreDirectory(innerDirectoryObject);

                RestoreDirectoryObject(innerDirectoryObject);
            }

            foreach (var innerFileObject in directoryObject.InnerFileObjects)
            {
                RestoreFile(innerFileObject);
            }
        }

        private void RestoreDirectory(DirectoryObject directoryObject)
        {
            directoryObject.FileChangeDescriptions.Sort((
                val1, val2) => val2.ChangeDateTime.CompareTo(val1.ChangeDateTime));

            bool existed = directoryObject.StartMonitoringTime < _restoreTime;
            foreach (var fileChangeDescription in directoryObject.FileChangeDescriptions)
            {
                if (fileChangeDescription.ChangeDateTime > _restoreTime)
                    break;

                if (fileChangeDescription is DeleteFileSystemObjectDescription)
                    existed = false;

                if (fileChangeDescription is CreateFileSystemObjectDescription)
                    existed = true;
            }

            if (existed)
            {
                try
                {
                    CreateRestoredDirection(directoryObject);
                }
                catch (Exception)
                {
                    Console.WriteLine($"Can't restore direction: {directoryObject.FullPath}");
                    return;
                }
            }

            RestoreDirectoryObject(directoryObject);
        }

        private void RestoreFile(FileObject fileObject)
        {
            fileObject.FileChangeDescriptions.Sort((
                val1, val2) => val2.ChangeDateTime.CompareTo(val1.ChangeDateTime));

            bool existed = fileObject.StartMonitoringTime < _restoreTime;
            byte[] data = fileObject.InitData;

            foreach (var fileChangeDescription in fileObject.FileChangeDescriptions)
            {
                if (fileChangeDescription.ChangeDateTime > _restoreTime)
                    break;

                if (fileChangeDescription is DeleteFileSystemObjectDescription)
                    existed = false;

                if (fileChangeDescription is CreateFileSystemObjectDescription)
                    existed = true;

                if (fileChangeDescription is DataChangeFileSystemObjectDescription description)
                {
                    existed = true;
                    data = description.NewData;
                }
            }

            if (existed)
            {
                try
                {
                    CreateRestoredFile(fileObject, data);
                }
                catch (Exception)
                {
                    Console.WriteLine($"Can't restore file: {fileObject.FullPath}");
                }
            }
        }

        private void CreateRestoredDirection(DirectoryObject directoryObject) =>
            Directory.CreateDirectory($@"{_restoreCatalog}\{directoryObject.FullPath.Substring(_restorableDirectoryName.Length)
                .TrimStart('\\', ':')}");

        private void CreateRestoredFile(FileObject fileObject, byte[] data) =>
            File.WriteAllBytes($@"{_restoreCatalog}\{fileObject.FullPath.Substring(_restorableDirectoryName.Length)
                .TrimStart('\\', ':')}", data);

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

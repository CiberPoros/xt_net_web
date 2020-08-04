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
            catch (FileNotFoundException e)
            {
                Debug.WriteLine($"Couldn't load last backup: file not found. Details: {e}");
            }
            catch (IOException e)
            {
                Debug.WriteLine($"Couldn't load last backup. Details: {e}");
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

            RestoreFileSystemObject(_observableDirectoryObject);
        }

        private static string RestoreCatalogName =>
            $"Restore_{DateTime.Now.ToString("G", CultureInfo.CreateSpecificCulture("de-DE")).Replace(':', '-')}";

        private static void ShowRestoreErrorMessageToConsole(string fullPath) =>
            Console.WriteLine($"Couldn't restore direction: {fullPath}");

        private static void ShowRestorePathToLongErrorMessageToConsole(string fullPath) =>
            Console.WriteLine($"Couldn't restore direction (Path too long): {fullPath}");

        private void RestoreDirectoryObject(DirectoryObject directoryObject)
        {
            foreach (var innerDirectoryObject in directoryObject.InnerDirectoryObjects)
            {
                RestoreFileSystemObject(innerDirectoryObject);

                RestoreDirectoryObject(innerDirectoryObject);
            }

            foreach (var innerFileObject in directoryObject.InnerFileObjects)
            {
                RestoreFileSystemObject(innerFileObject);
            }
        }

        private void RestoreFileSystemObject(FileSystemObject fileSystemObject)
        {
            fileSystemObject.FileChangeDescriptions.Sort((
                val1, val2) => val2.ChangeDateTime.CompareTo(val1.ChangeDateTime));

            var (willExist, data) = WillFileSystemObjectExistAfterRestore(fileSystemObject);

            if (willExist)
            {
                try
                {
                    CreateRestoredFileSystemObject(fileSystemObject, data);
                }
                catch (PathTooLongException e)
                {
                    ShowRestorePathToLongErrorMessageToConsole(fileSystemObject.FullPath);
                    Debug.WriteLine(e);
                }
                catch (IOException e)
                {
                    ShowRestoreErrorMessageToConsole(fileSystemObject.FullPath);
                    Debug.WriteLine(e);
                }
            }

            if (fileSystemObject is DirectoryObject directoryObject)
                RestoreDirectoryObject(directoryObject);
        }

        private (bool willExist, byte[] data) WillFileSystemObjectExistAfterRestore(FileSystemObject fileSystemObject)
        {
            byte[] data = fileSystemObject is FileObject fo ? fo.InitData : null;
            bool willExist = fileSystemObject.StartMonitoringTime < _restoreTime;

            foreach (var fileChangeDescription in fileSystemObject.FileChangeDescriptions)
            {
                if (fileChangeDescription.ChangeDateTime > _restoreTime)
                    break;

                willExist = fileChangeDescription.WillExistAfterRestore;

                if (fileChangeDescription is DataChangeFileSystemObjectDescription description)
                    data = description.NewData;
            }

            return (willExist, data);
        }

        private void CreateRestoredFileSystemObject(FileSystemObject fileSystemObject, byte[] data)
        {
            switch (fileSystemObject)
            {
                case DirectoryObject directoryObject:
                    Directory.CreateDirectory(GetRestoredFileSystemObjectName(directoryObject));
                    break;
                case FileObject fileObject:
                    File.WriteAllBytes(GetRestoredFileSystemObjectName(fileObject), data);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fileSystemObject));
            }
        }

        private string GetRestoredFileSystemObjectName(FileSystemObject fileSystemObject)
        {
            var fsoName = fileSystemObject.FullPath.
                                                Substring(_restorableDirectoryName.Length).
                                                TrimStart('\\', ':');

            return $@"{_restoreCatalog}\{fsoName}";
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

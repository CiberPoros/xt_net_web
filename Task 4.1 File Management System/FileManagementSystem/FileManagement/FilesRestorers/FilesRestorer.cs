using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using FileManagement.FileChangeDescriptions;
using FileManagement.FileSystemObjects;
using FileManagement.Interfaces;

namespace FileManagement.FilesRestorers
{
    public class FilesRestorer : IFilesRestorer
    {
        private static readonly string RestoreCatalogName = 
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

            RestoreDirectory(_observableDirectoryObject);
        }

        private static void ShowRestoreErrorMessageToConsole(string fullPath) =>
            Console.WriteLine($"Couldn't restore direction: {fullPath}");

        private static void ShowRestorePathToLongErrorMessageToConsole(string fullPath) =>
            Console.WriteLine($"Couldn't restore direction (Path too long): {fullPath}");

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

            bool willExist = WillDirectoryExistAfterRestore(directoryObject);

            if (willExist)
            {
                try
                {
                    CreateRestoredDirection(directoryObject);
                }
                catch (PathTooLongException e)
                {
                    ShowRestorePathToLongErrorMessageToConsole(directoryObject.FullPath);
                    Debug.WriteLine(e);
                    return;
                }
                catch (IOException e)
                {
                    ShowRestoreErrorMessageToConsole(directoryObject.FullPath);
                    Debug.WriteLine(e);
                    return;
                }
            }

            RestoreDirectoryObject(directoryObject);
        }

        private void RestoreFile(FileObject fileObject)
        {
            fileObject.FileChangeDescriptions.Sort((
                val1, val2) => val2.ChangeDateTime.CompareTo(val1.ChangeDateTime));

            var (willExist, data) = WillFileObjectExistAfterRestore(fileObject);

            if (willExist)
            {
                try
                {
                    CreateRestoredFile(fileObject, data);
                }
                catch (PathTooLongException e)
                {
                    ShowRestorePathToLongErrorMessageToConsole(fileObject.FullPath);
                    Debug.WriteLine(e);
                }
                catch (IOException e)
                {
                    ShowRestoreErrorMessageToConsole(fileObject.FullPath);
                    Debug.WriteLine(e);
                }
            }
        }

        private (bool willExist, byte[] data) WillFileObjectExistAfterRestore(FileObject fileObject)
        {
            byte[] data = fileObject.InitData;
            bool willExist = fileObject.StartMonitoringTime < _restoreTime;

            foreach (var fileChangeDescription in fileObject.FileChangeDescriptions)
            {
                if (fileChangeDescription.ChangeDateTime > _restoreTime)
                    break;

                if (fileChangeDescription is DeleteFileSystemObjectDescription)
                    willExist = false;

                if (fileChangeDescription is CreateFileSystemObjectDescription)
                    willExist = true;

                if (fileChangeDescription is DataChangeFileSystemObjectDescription description)
                {
                    willExist = true;
                    data = description.NewData;
                }
            }

            return (willExist, data);
        }

        private bool WillDirectoryExistAfterRestore(DirectoryObject directoryObject)
        {
            bool willExist = directoryObject.StartMonitoringTime < _restoreTime;

            foreach (var fileChangeDescription in directoryObject.FileChangeDescriptions)
            {
                if (fileChangeDescription.ChangeDateTime > _restoreTime)
                    break;

                if (fileChangeDescription is DeleteFileSystemObjectDescription)
                    willExist = false;

                if (fileChangeDescription is CreateFileSystemObjectDescription)
                    willExist = true;
            }

            return willExist;
        }

        private void CreateRestoredDirection(DirectoryObject directoryObject) =>
            Directory.CreateDirectory(GetRestoredFileSystemObjectName(directoryObject));

        private void CreateRestoredFile(FileObject fileObject, byte[] data) =>
            File.WriteAllBytes(GetRestoredFileSystemObjectName(fileObject), data);

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

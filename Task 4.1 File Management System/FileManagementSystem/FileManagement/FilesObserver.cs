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
        private FilesChangeManager _filesChangeManager;
        private readonly string _backupDirectoryPath;

        public FilesObserver(string path, string backupDirectoryPath)
        {
            if (!Directory.Exists(path))
                throw new ArgumentException("Directory does not exist", nameof(path));

            _backupDirectoryPath = backupDirectoryPath;
            _filesChangeManager = new FilesChangeManager();

            if (Directory.Exists(backupDirectoryPath))
            {
                try
                {
                    LoadLastBackup();
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine($"Error of load last backup. File not found.");
                }
                catch (Exception)
                {
                    Console.WriteLine($"Error of load last backup.");
                }
            }
            else
            {
                Directory.CreateDirectory(backupDirectoryPath);
                _observableDirectoryObject = new DirectoryObject(path, DateTime.Now);
                _filesChangeManager = new FilesChangeManager(_observableDirectoryObject);

                var serializer = new XmlSerializer(typeof(DirectoryObject), new Type[] {typeof(FileObject)});
                serializer.Serialize(new FileStream(
                    MakeInitialBackupFileFullName(), FileMode.OpenOrCreate), _observableDirectoryObject);

                serializer = new XmlSerializer(typeof(FilesChangeManager), new[]
                {
                    typeof(FileObject),
                    typeof(FileChangesObject),
                    typeof(AddDataDescription),
                    typeof(CreateFileDescription),
                    typeof(DeleteFileDescription),
                    typeof(RemoveDataDescription)
                });

                serializer.Serialize(new FileStream(
                    MakeChangesStorageFileFullName(), FileMode.OpenOrCreate), _filesChangeManager);
            }
        }

        public void StartObserving()
        {
            throw new NotImplementedException();
        }

        private void LoadLastBackup()
        {
            var serializer = new XmlSerializer(typeof(DirectoryObject), new[] {typeof(FileObject)});

            _observableDirectoryObject = (DirectoryObject)serializer.Deserialize(
                new FileStream(MakeInitialBackupFileFullName(), FileMode.Open));

            serializer = new XmlSerializer(typeof(FilesChangeManager), new[] { typeof(FileObject) });

            _filesChangeManager = (FilesChangeManager)serializer.Deserialize(
                new FileStream(MakeChangesStorageFileFullName(), FileMode.Open));
        }

        private string MakeInitialBackupFileFullName() => $@"{_backupDirectoryPath}\{InitialBackupFileName}";
        private string MakeChangesStorageFileFullName() => $@"{_backupDirectoryPath}\{ChangesStorageFileName}";
    }
}

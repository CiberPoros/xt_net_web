using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Permissions;
using System.Xml.Serialization;

namespace FileManagement
{
    public class FilesObserver : IFilesObserver
    {
        private const string InitialBackupFileName = "Initial save.txt";

        private DirectoryObject _observableDirectoryObject;
        private readonly string _backupDirectoryPath;

        public FilesObserver(string path, string backupDirectoryPath)
        {
            if (!Directory.Exists(path))
                throw new ArgumentException("Directory does not exist", nameof(path));

            _backupDirectoryPath = backupDirectoryPath;

            if (Directory.Exists(backupDirectoryPath))
            {
                try
                {
                    LoadLastBackup();
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine($"Error of load last backup. File not found: {e}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error of load last backup. Details: {e}");
                }
            }
            else
            {
                _observableDirectoryObject = new DirectoryObject(path, DateTime.Now);
            }
        }

        public void StartObserving()
        {
            throw new NotImplementedException();
        }

        private void LoadLastBackup()
        {
            var serializer = new XmlSerializer(typeof(DirectoryObject));

            try
            {
                _observableDirectoryObject = (DirectoryObject)serializer.Deserialize(
                    new FileStream(_backupDirectoryPath + InitialBackupFileName, FileMode.Open));
            }
            catch
            {
                throw;
            }
        }
    }
}

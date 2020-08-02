using System;
using System.IO;

namespace FileManagement.FileSystemObjects
{
    [Serializable]
    public class FileObject : FileSystemObject
    {
        private const string FileDoesNotExistsError = "File does not exists.";

        public FileObject()
        {

        }

        public FileObject(string fullName, DateTime startMonitoringTime) : base(fullName, startMonitoringTime)
        {
            if (!File.Exists(fullName))
                throw new ArgumentException(FileDoesNotExistsError, nameof(fullName));

            InitData = File.ReadAllBytes(fullName);
            LastData = new byte[InitData.Length];
            Array.Copy(InitData, LastData, InitData.Length);
        }

        public byte[] InitData { get; set; }
        public byte[] LastData { get; set; }
    }
}

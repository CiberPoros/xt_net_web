using System;
using System.IO;

namespace FileManagement.FileSystemObjects
{
    [Serializable]
    public class FileObject : FileSystemObject
    {
        public FileObject()
        {

        }

        public FileObject(string fullName, DateTime startMonitoringTime) : base(fullName, startMonitoringTime)
        {
            if (!File.Exists(fullName))
                throw new ArgumentException("File does not exists.", nameof(fullName));

            InitData = File.ReadAllBytes(fullName);
            LastData = new byte[InitData.Length];
            Array.Copy(InitData, LastData, InitData.Length);
        }

        public byte[] InitData { get; set; }
        public byte[] LastData { get; set; }
    }
}

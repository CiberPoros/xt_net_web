using System;
using System.Collections.Generic;
using System.IO;

namespace FileManagement.FileSystemObjects
{
    [Serializable]
    public class DirectoryObject : FileSystemObject
    {
        public DirectoryObject()
        {

        }

        public DirectoryObject(string fullName, DateTime startMonitoringTime) : base(fullName, startMonitoringTime)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(fullName);

            foreach (var fileInfo in directoryInfo.GetFiles())
            {
                InnerFileObjects.Add(new FileObject(fileInfo.FullName, startMonitoringTime));
            }

            foreach (var directory in directoryInfo.GetDirectories())
            {
                InnerDirectoryObjects.Add(new DirectoryObject(directory.FullName, startMonitoringTime));
            }
        }

        public List<DirectoryObject> InnerDirectoryObjects { get; set; } = new List<DirectoryObject>();
        public List<FileObject> InnerFileObjects { get; set; } = new List<FileObject>();
    }
}

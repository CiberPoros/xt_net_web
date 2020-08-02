using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement
{
    public class DirectoryObject : FileSystemObject
    {
        public DirectoryObject(string fullName, DateTime startMonitoringTime) : base(fullName, startMonitoringTime)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(fullName);

            foreach (var fileInfo in directoryInfo.GetFiles())
            {
                InnerFileSystemObjects.Add(new FileObject(fileInfo.FullName, startMonitoringTime));
            }

            foreach (var directory in directoryInfo.GetDirectories())
            {
                InnerFileSystemObjects.Add(new DirectoryObject(directory.FullName, startMonitoringTime));
            }
        }

        public List<FileSystemObject> InnerFileSystemObjects { get; } = new List<FileSystemObject>();
    }
}

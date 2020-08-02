using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement
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
                InnerFileSystemObjects.Add(new FileObject(fileInfo.FullName, startMonitoringTime));
            }

            foreach (var directory in directoryInfo.GetDirectories())
            {
                InnerFileSystemObjects.Add(new DirectoryObject(directory.FullName, startMonitoringTime));
            }
        }

        public List<FileSystemObject> InnerFileSystemObjects { get; set; } = new List<FileSystemObject>();
    }
}

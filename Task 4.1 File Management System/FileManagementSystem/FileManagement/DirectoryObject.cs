using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement
{
    public class DirectoryObject : FileSystemObject
    {
        public DirectoryObject(string fullPath, DateTime startMonitoringTime) : base(fullPath, startMonitoringTime)
        {
        }

        public List<FileSystemObject> InnerFileSystemObjects { get; } = new List<FileSystemObject>();
    }
}

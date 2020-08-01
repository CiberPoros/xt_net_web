using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement
{
    public class FileObject : FileSystemObject
    {
        public FileObject(string fullPath, DateTime startMonitoringTime, byte[] data) : base(fullPath, startMonitoringTime)
        {
            Data = data;
        }

        public byte[] Data { get; }
    }
}

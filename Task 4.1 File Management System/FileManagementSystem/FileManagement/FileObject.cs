using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement
{
    public class FileObject : FileSystemObject
    {
        public FileObject(string fullName, DateTime startMonitoringTime) : base(fullName, startMonitoringTime)
        {
            if (!File.Exists(fullName))
                throw new ArgumentException("File does not exists.", nameof(fullName));

            Data = File.ReadAllBytes(fullName);
        }

        public byte[] Data { get; }
    }
}

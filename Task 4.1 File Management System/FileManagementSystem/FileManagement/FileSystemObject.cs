using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement
{
    public abstract class FileSystemObject
    {
        private string _fullPath;

        protected FileSystemObject(string fullPath, DateTime startMonitoringTime)
        {
            FullPath = fullPath ?? throw new ArgumentNullException(nameof(fullPath));
            StartMonitoringTime = startMonitoringTime;
        }

        public string FullPath
        {
            get => _fullPath;
            set => _fullPath = value ?? throw new ArgumentNullException(nameof(value));
        }

        public DateTime StartMonitoringTime { get; }
    }
}

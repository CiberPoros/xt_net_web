using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement
{
    [Serializable]
    public abstract class FileSystemObject
    {
        private string _fullPath;

        protected FileSystemObject()
        {

        }

        protected FileSystemObject(string fullName, DateTime startMonitoringTime)
        {
            FullPath = fullName ?? throw new ArgumentNullException(nameof(fullName));
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

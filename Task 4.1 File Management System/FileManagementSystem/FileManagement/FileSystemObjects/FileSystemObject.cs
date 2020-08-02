using System;
using System.Collections.Generic;
using FileManagement.FileChangeDescriptions;

namespace FileManagement.FileSystemObjects
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

        public List<FileSystemObjectChangeDescription> FileChangeDescriptions { get; set; } = new List<FileSystemObjectChangeDescription>();

        public string FullPath
        {
            get => _fullPath;
            set => _fullPath = value ?? throw new ArgumentNullException(nameof(value));
        }

        public DateTime StartMonitoringTime { get; set; }
    }
}

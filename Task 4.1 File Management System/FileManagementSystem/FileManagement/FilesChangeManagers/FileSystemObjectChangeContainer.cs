using System;
using System.Collections.Generic;
using FileManagement.FilesChangeManagers.FileChangeDescriptions;

namespace FileManagement.FilesChangeManagers
{
    [Serializable]
    public class FileSystemObjectChangeContainer
    {
        public FileSystemObjectChangeContainer()
        {
        }

        public FileSystemObjectChangeContainer(string fullPath, List<FileSystemObjectChangeDescription> fileChangeDescriptions)
        {
            FullPath = fullPath;
            FileChangeDescriptions = fileChangeDescriptions;
        }

        public string FullPath { get; set; }
        public List<FileSystemObjectChangeDescription> FileChangeDescriptions { get; set; } = new List<FileSystemObjectChangeDescription>();
    }
}

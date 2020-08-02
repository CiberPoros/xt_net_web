using System;
using System.Collections.Generic;
using FileManagement.FilesChangeManagers.FileChangeDescriptions;

namespace FileManagement.FilesChangeManagers
{
    [Serializable]
    public class FileChangesObject
    {
        public FileChangesObject()
        {
        }

        public FileChangesObject(string fullPath, List<FileChangeDescription> fileChangeDescriptions)
        {
            FullPath = fullPath;
            FileChangeDescriptions = fileChangeDescriptions;
        }

        public string FullPath { get; set; }
        public List<FileChangeDescription> FileChangeDescriptions { get; set; } = new List<FileChangeDescription>();
    }
}

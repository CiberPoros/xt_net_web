using System;
using System.Collections.Generic;
using FileManagement.FilesChangeManagers.FileChangeDescriptions;

namespace FileManagement.FilesChangeManagers
{
    [Serializable]
    public class FileSystemChangeManager
    {
        public FileSystemChangeManager()
        {
        }

        public FileSystemChangeManager(DirectoryObject directoryObject)
        {
            InitFilesChanges(directoryObject);
        }

        public SerializableDictionary<string, FileSystemObjectChangeContainer> FileSystemObjectChanges { get; set; } = new SerializableDictionary<string, FileSystemObjectChangeContainer>();

        private void InitFilesChanges(DirectoryObject directoryObject)
        {
            foreach (var directoryObj in directoryObject.InnerDirectoryObjects)
            {
                FileSystemObjectChanges.Add(directoryObj.FullPath, new FileSystemObjectChangeContainer(
                    directoryObj.FullPath, new List<FileSystemObjectChangeDescription>()));
                InitFilesChanges(directoryObj);
                break;
            }

            foreach (var fileObj in directoryObject.InnerFileObjects)
            {
                FileSystemObjectChanges.Add(fileObj.FullPath, new FileSystemObjectChangeContainer(
                    fileObj.FullPath, new List<FileSystemObjectChangeDescription>()));
                break;
            }
        }
    }
}

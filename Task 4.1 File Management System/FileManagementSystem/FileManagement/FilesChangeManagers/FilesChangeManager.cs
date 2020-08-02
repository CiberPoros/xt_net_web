using System;
using System.Collections.Generic;
using FileManagement.FilesChangeManagers.FileChangeDescriptions;

namespace FileManagement.FilesChangeManagers
{
    [Serializable]
    public class FilesChangeManager
    {
        public FilesChangeManager()
        {
        }

        public FilesChangeManager(DirectoryObject directoryObject)
        {
            InitFilesChanges(directoryObject);
        }

        public SerializableDictionary<string, FileChangesObject> FilesChanges { get; set; } = new SerializableDictionary<string, FileChangesObject>();

        private void InitFilesChanges(DirectoryObject directoryObject)
        {
            foreach (var directoryObjectInnerFileSystemObject in directoryObject.InnerFileSystemObjects)
            {
                switch (directoryObjectInnerFileSystemObject)
                {
                    case DirectoryObject directoryObj:
                        InitFilesChanges(directoryObj);
                        break;
                    case FileObject fileObj:
                        FilesChanges.Add(fileObj.FullPath, 
                            new FileChangesObject(fileObj.FullPath, new List<FileChangeDescription>()));
                        break;
                    default:
                        throw new ArgumentException("Unknown object type.", nameof(directoryObject));
                }
            }
        }
    }
}

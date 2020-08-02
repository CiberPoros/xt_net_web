using System;

namespace FileManagement.FilesChangeManagers.FileChangeDescriptions
{
    public abstract class FileSystemObjectChangeDescription
    {
        protected FileSystemObjectChangeDescription()
        {

        }

        protected FileSystemObjectChangeDescription(DateTime changeDateTime)
        {
            ChangeDateTime = changeDateTime;
        }

        public DateTime ChangeDateTime { get; set; }
    }
}

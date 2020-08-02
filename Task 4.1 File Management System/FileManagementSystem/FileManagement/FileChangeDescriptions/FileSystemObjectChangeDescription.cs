using System;

namespace FileManagement.FileChangeDescriptions
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

using System;

namespace FileManagement.FilesChangeManagers.FileChangeDescriptions
{
    public abstract class FileChangeDescription
    {
        protected FileChangeDescription()
        {

        }

        protected FileChangeDescription(DateTime changeDateTime)
        {
            ChangeDateTime = changeDateTime;
        }

        public DateTime ChangeDateTime { get; set; }
    }
}

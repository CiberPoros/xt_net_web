using System;

namespace FileManagement.FileChangeDescriptions
{
    [Serializable]
    public class CreateFileSystemObjectDescription : FileSystemObjectChangeDescription
    {
        public CreateFileSystemObjectDescription()
        {

        }

        public CreateFileSystemObjectDescription(DateTime changeDateTime) : base(changeDateTime)
        {

        }
    }
}

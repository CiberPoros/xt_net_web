using System;

namespace FileManagement.FileChangeDescriptions
{
    [Serializable]
    public class DeleteFileSystemObjectDescription : FileSystemObjectChangeDescription
    {
        public DeleteFileSystemObjectDescription()
        {

        }

        public DeleteFileSystemObjectDescription(DateTime changeDateTime) : base(changeDateTime)
        {
        }

        public override bool WillExistAfterRestore => false;
    }
}

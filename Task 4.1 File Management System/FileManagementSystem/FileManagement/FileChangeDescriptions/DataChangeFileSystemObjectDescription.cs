using System;

namespace FileManagement.FileChangeDescriptions
{
    [Serializable]
    public class DataChangeFileSystemObjectDescription : FileSystemObjectChangeDescription
    {
        public DataChangeFileSystemObjectDescription()
        {

        }

        public DataChangeFileSystemObjectDescription(DateTime changeDateTime, byte[] newData) : base(changeDateTime)
        {
            NewData = newData;
        }

        public byte[] NewData { get; set; }
    }
}

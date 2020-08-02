using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement.FilesChangeManagers.FileChangeDescriptions
{
    [Serializable]
    public class AddDataDescription : FileSystemObjectChangeDescription
    {
        public AddDataDescription()
        {

        }

        public AddDataDescription(DateTime changeDateTime, int startIndex, byte[] data) : base(changeDateTime)
        {
            StartIndex = startIndex;
            Data = data;
        }

        public int StartIndex { get; set; }
        public byte[] Data { get; set; }
    }
}

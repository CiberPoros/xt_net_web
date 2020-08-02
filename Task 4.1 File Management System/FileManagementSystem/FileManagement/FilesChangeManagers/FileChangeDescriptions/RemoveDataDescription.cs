using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement.FilesChangeManagers.FileChangeDescriptions
{
    [Serializable]
    public class RemoveDataDescription : FileSystemObjectChangeDescription
    {
        public RemoveDataDescription()
        {

        }

        public RemoveDataDescription(DateTime changeDateTime, int startIndex, int countOfByte) : base(changeDateTime)
        {
            StartIndex = startIndex;
            CountOfByte = countOfByte;
        }

        public int StartIndex { get; set; }
        public int CountOfByte { get; set; }
    }
}

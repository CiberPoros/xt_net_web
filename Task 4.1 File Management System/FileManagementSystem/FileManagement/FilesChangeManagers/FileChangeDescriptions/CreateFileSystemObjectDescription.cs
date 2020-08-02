using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement.FilesChangeManagers.FileChangeDescriptions
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

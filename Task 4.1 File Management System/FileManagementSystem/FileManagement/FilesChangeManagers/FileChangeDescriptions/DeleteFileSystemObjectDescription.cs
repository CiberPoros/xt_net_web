using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement.FilesChangeManagers.FileChangeDescriptions
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
    }
}

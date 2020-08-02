using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement.FilesChangeManagers.FileChangeDescriptions
{
    [Serializable]
    public class DeleteFileDescription : FileChangeDescription
    {
        public DeleteFileDescription()
        {

        }

        public DeleteFileDescription(DateTime changeDateTime, string fullName) : base(changeDateTime)
        {
            FullName = fullName;
        }

        public string FullName { get; set; }
    }
}

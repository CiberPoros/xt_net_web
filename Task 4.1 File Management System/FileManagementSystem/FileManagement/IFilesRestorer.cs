using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement
{
    public interface IFilesRestorer
    {
        void Restore(DateTime dateTime);
    }
}

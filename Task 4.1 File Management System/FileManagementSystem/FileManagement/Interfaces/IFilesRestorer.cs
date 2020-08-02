using System;

namespace FileManagement.Interfaces
{
    public interface IFilesRestorer
    {
        void Restore(DateTime dateTime, string restorableDirectionName);
    }
}

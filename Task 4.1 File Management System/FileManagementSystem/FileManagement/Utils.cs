using System;
using System.Linq;
using FileManagement.FileChangeDescriptions;
using FileManagement.FileSystemObjects;

namespace FileManagement
{
    internal static class Utils
    {
        public const string InitialBackupFileName = "Backup.txt";

        static Utils()
        {
            BackupDirectoryPath = $@"{Environment.CurrentDirectory}\BackupDirectory";
            BackupFileFullName = $@"{BackupDirectoryPath}\{InitialBackupFileName}";

            UsableTypesForSerialization = new[]
            {
                typeof(FileObject),
                typeof(DataChangeFileSystemObjectDescription),
                typeof(CreateFileSystemObjectDescription),
                typeof(DeleteFileSystemObjectDescription)
            };
        }

        public static readonly string BackupDirectoryPath;
        public static readonly string BackupFileFullName;

        public static readonly Type[] UsableTypesForSerialization;

        public static int GetHash(byte[] data)
        {
            unchecked
            {
                return data.Aggregate(0, (current, b) => (current * 31) ^ b);
            }
        }
    }
}

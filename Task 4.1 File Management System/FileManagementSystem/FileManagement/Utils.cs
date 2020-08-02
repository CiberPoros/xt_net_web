using System;
using System.Linq;
using FileManagement.FileChangeDescriptions;
using FileManagement.FileSystemObjects;

namespace FileManagement
{
    internal static class Utils
    {
        public const string InitialBackupFileName = "Backup.txt";

        public static readonly string BackupDirectoryPath = $@"{Environment.CurrentDirectory}\BackupDirectory";

        public static readonly Type[] UsableTypesForSerialization = new Type[]
        {
            typeof(FileObject),
            typeof(DataChangeFileSystemObjectDescription),
            typeof(CreateFileSystemObjectDescription),
            typeof(DeleteFileSystemObjectDescription)
        };

        public static string BackupFileFullName => $@"{Utils.BackupDirectoryPath}\{Utils.InitialBackupFileName}";

        public static int GetHash(byte[] data)
        {
            unchecked
            {
                return data.Aggregate(0, (current, b) => (current * 31) ^ b);
            }
        }
    }
}

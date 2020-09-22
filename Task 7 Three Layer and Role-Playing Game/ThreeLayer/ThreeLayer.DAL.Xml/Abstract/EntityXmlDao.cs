using System.Configuration;
using System.IO;
using System.Xml.Linq;

namespace ThreeLayer.DAL.Xml.Abstract
{
    public abstract class EntityXmlDao<T>
    {
        protected readonly FileInfo _storageFileInfo;

        public EntityXmlDao()
        {
            StoragePathAppSettingsKey = $@"{typeof(T).Name}Path";
            DefaultStoragePath = $@"Storage\{typeof(T).Name}List.xml";
            _storageFileInfo = new FileInfo(ConfigurationManager.AppSettings[StoragePathAppSettingsKey] ?? DefaultStoragePath);
            XmlStorageRootName = $"{typeof(T).Name}List";

            if (!_storageFileInfo.Exists)
            {
                var directoryInfo = new DirectoryInfo(_storageFileInfo.DirectoryName);
                if (!directoryInfo.Exists)
                    directoryInfo.Create();

                using (var stream = _storageFileInfo.Create())
                {
                    var root = new XElement(XmlStorageRootName);
                    var document = new XDocument();

                    document.Add(root);
                    document.Save(stream);
                }
            }
        }

        protected string StoragePathAppSettingsKey { get; }
        protected string DefaultStoragePath { get; }
        protected string XmlStorageRootName { get; }
    }
}

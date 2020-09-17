using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ThreeLayer.DAL.Xml.Extensions;

namespace ThreeLayer.DAL.Xml.Abstract
{
    public abstract class EntityXmlDao<T> where T : class
    {
        protected readonly FileInfo _storageFileInfo;

        public EntityXmlDao()
        {
            StoragePathAppSettingsKey = $@"{GetEntityName()}Path";
            DefaultStoragePath = $@"Storage\{GetEntityName()}List.xml";
            _storageFileInfo = new FileInfo(ConfigurationManager.AppSettings[StoragePathAppSettingsKey] ?? DefaultStoragePath);
            XmlStorageRootName = GetEntityName();

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

        protected abstract string GetEntityName();

        protected string StoragePathAppSettingsKey { get; }
        protected string DefaultStoragePath { get; }
        protected string XmlStorageRootName { get; }

        public virtual void Add(T obj) =>
            XDocument.Load(_storageFileInfo.FullName)
                     .AddToRoot(obj.ToXElement())
                     .Save(_storageFileInfo.FullName);

        public IEnumerable<T> GetAll() =>
            XDocument.Load(_storageFileInfo.FullName).Root
                     .Elements()
                     .Select(xElement => xElement.FromXElement<T>());
    }
}

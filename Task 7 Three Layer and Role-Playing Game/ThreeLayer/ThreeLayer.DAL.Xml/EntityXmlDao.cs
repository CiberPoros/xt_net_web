using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ThreeLayer.DAL.Xml.Extensions;

namespace ThreeLayer.DAL.Xml
{
    public abstract class EntityXmlDao<T> where T : class
    {
        protected readonly FileInfo _storageFileInfo;

        public EntityXmlDao()
        {
            _storageFileInfo = new FileInfo(ConfigurationManager.AppSettings[StoragePathAppSettingsKey] ?? DefaultStoragePath);
            XmlStorageRootName = nameof(T);

            if (!_storageFileInfo.Exists)
            {
                using (var stream = _storageFileInfo.Create())
                {
                    var root = new XElement(XmlStorageRootName);
                    var document = new XDocument();

                    document.Add(root);
                    document.Save(stream);
                }
            }
        }

        protected abstract string StoragePathAppSettingsKey { get; }
        protected abstract string DefaultStoragePath { get; }

        protected string XmlStorageRootName { get; }

        public void Add(T obj) => 
            XDocument.Load(_storageFileInfo.FullName)
                     .AddToRoot(obj.ToXElement())
                     .Save(_storageFileInfo.FullName);

        public IEnumerable<T> GetAll() => 
            XDocument.Load(_storageFileInfo.FullName).Root
                     .Elements()
                     .Select(xElement => xElement.FromXElement<T>());
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ThreeLayer.Common.Entities;
using ThreeLayer.DAL.Contracts;
using ThreeLayer.DAL.Xml.Abstract;
using ThreeLayer.DAL.Xml.Extensions;

namespace ThreeLayer.DAL.Xml
{
    public class EntityWithIdXmlDao<T> : EntityXmlDao<T>, IEntityWithIdDao<T> where T : class, IEntityWithId
    {
        public EntityWithIdXmlDao()
        {
        }

        public event EventHandler<T> Removed;

        public int Add(T entity)
        {
            entity.Id = EntityIdManager.GetNextId<T>(_storageFileInfo);

            XDocument.Load(_storageFileInfo.FullName)
                     .AddToRoot(entity.ToXElement())
                     .Save(_storageFileInfo.FullName);

            return entity.Id;
        }

        public bool RemoveById(int id)
        {
            var document = XDocument.Load(_storageFileInfo.FullName);
            var match = document.Root.Elements()
                                     .FirstOrDefault(xElement => xElement.FromXElement<T>().Id == id);

            if (match == null)
                return false;

            var entity = match.FromXElement<T>();
            match.Remove();
            document.Save(_storageFileInfo.FullName);
            Removed?.Invoke(this, entity);

            return true;
        }

        public IEnumerable<T> GetAll() =>
            XDocument.Load(_storageFileInfo.FullName).Root
                     .Elements()
                     .Select(xElement => xElement.FromXElement<T>());

        public void Update(T entity)
        {
            var document = XDocument.Load(_storageFileInfo.FullName);
            var match = document.Root.Elements()
                                     .FirstOrDefault(xElement => xElement.FromXElement<T>().Id == entity.Id);

            match?.Remove();

            document.AddToRoot(entity.ToXElement())
                    .Save(_storageFileInfo.FullName);
        }
    }
}

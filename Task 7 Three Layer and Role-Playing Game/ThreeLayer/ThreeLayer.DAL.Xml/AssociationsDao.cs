using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using ThreeLayer.Common.Entities;
using ThreeLayer.DAL.Contracts;
using ThreeLayer.Common.Utils;
using System.Xml.Linq;
using ThreeLayer.DAL.Xml.Extensions;

namespace ThreeLayer.DAL.Xml
{
    public class AssociationsDao<TEntity, TAssociatedEntity> : IAssociationsDao<TEntity, TAssociatedEntity>, IDisposable
        where TEntity : class, IEntityWithId
        where TAssociatedEntity : class, IEntityWithId
    {
        private readonly IEntityWithIdDao<TEntity> _entityWithIdDao;
        private readonly IEntityWithIdDao<TAssociatedEntity> _associatedEntityWithIdDao;

        private readonly string _storagePathAppSettingsKey;
        private readonly string _defaultStoragePath;
        private readonly string _xmlStorageRootName;

        private readonly FileInfo _storageFileInfo;

        private readonly Type _firstType;
        private readonly Type _secondType;

        private readonly bool _isInverted;

        public AssociationsDao(IEntityWithIdDao<TEntity> entityWithIdDao, IEntityWithIdDao<TAssociatedEntity> associatedEntityWithIdDao)
        {
            _entityWithIdDao = entityWithIdDao;
            _associatedEntityWithIdDao = associatedEntityWithIdDao;

            (_firstType, _secondType) = Utilites.SortTypesByName(typeof(TEntity), typeof(TAssociatedEntity));

            _isInverted = _firstType.FullName != typeof(TEntity).FullName;

            _storagePathAppSettingsKey = $@"{_firstType.Name}{_secondType.Name}AssociationsPath";
            _defaultStoragePath = $@"Storage\{_firstType.Name}{_secondType.Name}AssociationsList.xml";
            _storageFileInfo = new FileInfo(ConfigurationManager.AppSettings[_storagePathAppSettingsKey] ?? _defaultStoragePath);
            _xmlStorageRootName = $"{_firstType.Name}{_secondType.Name}AssociationsList";

            CheckStorageExistence();

            Subscribe();
        }

        public void Dispose() => Describe();

        public bool Bind(int entityId, int associatedEntityId)
        {
            var document = XDocument.Load(_storageFileInfo.FullName);

            if (_isInverted)
                (entityId, associatedEntityId) = (associatedEntityId, entityId);

            var match = document.Root.Elements().FirstOrDefault(xElement => xElement.FromXElement<XmlAssociationEntity>().FirstEntityId == entityId
                                                                            && xElement.FromXElement<XmlAssociationEntity>().SecondEntityId == associatedEntityId);

            if (match != null)
                return false;

            document.AddToRoot(new XmlAssociationEntity() { FirstEntityId = entityId, SecondEntityId = associatedEntityId }.ToXElement())
                    .Save(_storageFileInfo.FullName);

            return true;
        }

        public bool UnBind(int entityId, int associatedEntityId)
        {
            var document = XDocument.Load(_storageFileInfo.FullName);

            if (_isInverted)
                (entityId, associatedEntityId) = (associatedEntityId, entityId);

            var match = document.Root.Elements().FirstOrDefault(xElement => xElement.FromXElement<XmlAssociationEntity>().FirstEntityId == entityId
                                                                            && xElement.FromXElement<XmlAssociationEntity>().SecondEntityId == associatedEntityId);

            if (match == null)
                return false;

            match.Remove();

            document.Save(_storageFileInfo.FullName);

            return true;
        }

        public IEnumerable<TAssociatedEntity> GetAssociatedEntities(int entityId)
        {
            var document = XDocument.Load(_storageFileInfo.FullName);

            var associationsIdSet = (from element in document.Root.Elements()
                                     let association = element.FromXElement<XmlAssociationEntity>()
                                     let match = _isInverted ? association.SecondEntityId : association.FirstEntityId
                                     let associatedId = _isInverted ? association.FirstEntityId : association.SecondEntityId
                                     where match == entityId
                                     select associatedId).ToHashSet();

            return _associatedEntityWithIdDao.GetAll()
                                             .Where(entity => associationsIdSet.Contains(entity.Id));
        }

        private void Subscribe()
        {
            _entityWithIdDao.Removed += OnRemoved;
            _associatedEntityWithIdDao.Removed += OnRemoved;
        }

        private void Describe()
        {
            _entityWithIdDao.Removed -= OnRemoved;
            _associatedEntityWithIdDao.Removed -= OnRemoved;
        }

        private void OnRemoved(object sender, TEntity entity)
        {
            var document = XDocument.Load(_storageFileInfo.FullName);

            var deleted = from element in document.Root.Elements()
                          let association = element.FromXElement<XmlAssociationEntity>()
                          where (_isInverted ? association.SecondEntityId : association.FirstEntityId) == entity.Id
                          select element;

            foreach (var element in deleted)
                element.Remove();

            document.Save(_storageFileInfo.FullName);
        }

        private void OnRemoved(object sender, TAssociatedEntity associatedEntity)
        {
            var document = XDocument.Load(_storageFileInfo.FullName);

            var deleted = from element in document.Root.Elements()
                          let association = element.FromXElement<XmlAssociationEntity>()
                          where (_isInverted ? association.FirstEntityId : association.SecondEntityId) == associatedEntity.Id
                          select element;

            foreach (var element in deleted)
                element.Remove();

            document.Save(_storageFileInfo.FullName);
        }

        private void CheckStorageExistence()
        {
            if (!_storageFileInfo.Exists)
            {
                var directoryInfo = new DirectoryInfo(_storageFileInfo.DirectoryName);
                if (!directoryInfo.Exists)
                    directoryInfo.Create();

                using (var stream = _storageFileInfo.Create())
                {
                    var root = new XElement(_xmlStorageRootName);
                    var document = new XDocument();

                    document.Add(root);
                    document.Save(stream);
                }
            }
        }
    }
}

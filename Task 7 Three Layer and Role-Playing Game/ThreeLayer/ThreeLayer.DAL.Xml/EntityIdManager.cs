using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ThreeLayer.Common.Entities;
using ThreeLayer.DAL.Xml.Extensions;

namespace ThreeLayer.DAL.Xml
{
    internal static class EntityIdManager
    {
        private static readonly Dictionary<Type, int> _lastIdOfEntity;

        static EntityIdManager()
        {
            _lastIdOfEntity = new Dictionary<Type, int>();
        }

        public static int GetNextId<T>(FileInfo storageFileInfo) where T : class, IEntityWithId
        {
            var type = typeof(T);
            if (_lastIdOfEntity.TryGetValue(type, out int id))
            {
                _lastIdOfEntity[type]++;
                return id + 1;
            }

            var lastId = XDocument.Load(storageFileInfo.FullName).Root
                                  .Elements()
                                  .Max(xElement => xElement.FromXElement<T>().Id);

            _lastIdOfEntity.Add(type, lastId + 1);
            return lastId + 1;
        }
    }
}

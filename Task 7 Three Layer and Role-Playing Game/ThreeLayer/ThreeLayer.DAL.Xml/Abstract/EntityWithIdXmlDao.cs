using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeLayer.Common.Entities;

namespace ThreeLayer.DAL.Xml.Abstract
{
    public abstract class EntityWithIdXmlDao<T> : EntityXmlDao<T> where T : class, IEntityWithId
    {
        public override void Add(T obj)
        {
            obj.Id = EntityIdManager.GetNextId<Award>(_storageFileInfo);
            base.Add(obj);
        }
    }
}

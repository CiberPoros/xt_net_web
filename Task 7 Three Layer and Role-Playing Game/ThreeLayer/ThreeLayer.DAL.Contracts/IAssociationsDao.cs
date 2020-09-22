using System.Collections.Generic;
using ThreeLayer.Common.Entities;

namespace ThreeLayer.DAL.Contracts
{
    public interface IAssociationsDao<TEntity, TAssociatedEntity>
        where TEntity : class, IEntityWithId
        where TAssociatedEntity : class, IEntityWithId
    {
        bool Bind(int entityId, int associatedEntityId);

        bool UnBind(int entityId, int associatedEntityId);

        IEnumerable<TAssociatedEntity> GetAssociatedEntities(int entityId);
    }
}

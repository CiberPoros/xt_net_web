using System;
using System.Collections.Generic;
using ThreeLayer.Common.Utils;

namespace ThreeLayer.Common.Entities
{
    public static class AssociationsInfo
    {
        private static HashSet<(Type, Type)> Associations { get; }

        static AssociationsInfo()
        {
            Associations = new HashSet<(Type, Type)>
            {
                { Utilites.SortTypesByName(typeof(User), typeof(Award)) },
                { Utilites.SortTypesByName(typeof(User), typeof(Role)) }
            };
        }

        public static bool IsAssociatedWith(Type entityType, Type otherEntityType) => Associations.Contains(Utilites.SortTypesByName(entityType, otherEntityType));
        public static bool IsAssociatedWith<TEntity, TOtherEntity>() 
            where TEntity : IEntityWithId 
            where TOtherEntity : IEntityWithId
            => Associations.Contains(Utilites.SortTypesByName(typeof(TEntity), typeof(TOtherEntity)));
    }
}

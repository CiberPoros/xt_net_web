using System;
using System.Collections.Generic;
using ThreeLayer.Common.Entities;

namespace ThreeLayer.DAL.Contracts
{
    public interface IEntityWithIdDao<T> where T : class, IEntityWithId
    {
        event EventHandler<T> Removed;

        /// <summary>
        /// Returns entity ID
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Add(T entity);

        bool RemoveById(int id);
        IEnumerable<T> GetAll();
        void Update(T entity);
    }
}

using System;
using System.Collections.Generic;
using ThreeLayer.BLL.UsersLogicContracts;
using ThreeLayer.Common.Entities;
using ThreeLayer.DAL.Contracts;

namespace ThreeLayer.BLL.UsersLogic
{
    public class UsersManager : IUsersManager
    {
        private readonly IEntityWithIdDao<User> _usersDao;
        private readonly IAssociationsDao<User, Award> _associationsDao;

        public UsersManager(IEntityWithIdDao<User> usersDao, IAssociationsDao<User, Award> associationsDao)
        {
            _usersDao = usersDao ?? throw new ArgumentNullException(nameof(usersDao));
            _associationsDao = associationsDao ?? throw new ArgumentNullException(nameof(associationsDao));
        }

        public void AddUser(User user)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            _usersDao.Add(user);
        }

        public bool BindToAward(int userId, int awardId) => _associationsDao.Bind(userId, awardId);

        public IEnumerable<User> GetAllUsers() => _usersDao.GetAll();

        public IEnumerable<Award> GetAwards(int userId) => _associationsDao.GetAssociatedEntities(userId);

        public bool RemoveUserById(int id) => _usersDao.RemoveById(id);
        public bool UnBindFromAward(int userId, int awardId) => _associationsDao.UnBind(userId, awardId);
    }
}

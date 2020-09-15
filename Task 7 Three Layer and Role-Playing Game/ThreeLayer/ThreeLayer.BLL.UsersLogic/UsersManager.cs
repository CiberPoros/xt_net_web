using System;
using System.Collections.Generic;
using ThreeLayer.BLL.UsersLogicContracts;
using ThreeLayer.Common.Entities;
using ThreeLayer.DAL.Contracts;

namespace ThreeLayer.BLL.UsersLogic
{
    public class UsersManager : IUsersManager
    {
        private readonly IUsersDao _usersDao;

        public UsersManager(IUsersDao usersDao)
        {
            _usersDao = usersDao ?? throw new ArgumentNullException(nameof(usersDao));
        }

        public void AddUser(User user)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            _usersDao.AddUser(user);
        }

        public IEnumerable<User> GetAllUsers() => _usersDao.GetAllUsers();
        public void RemoveUserById(int id) => _usersDao.RemoveUserById(id);
    }
}

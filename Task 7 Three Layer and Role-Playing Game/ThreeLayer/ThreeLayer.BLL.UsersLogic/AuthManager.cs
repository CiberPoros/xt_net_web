using System;
using System.Linq;
using ThreeLayer.DAL.Contracts;
using ThreeLayer.Common.Entities;
using ThreeLayer.BLL.UsersLogicContracts;

namespace ThreeLayer.BLL.UsersLogic
{
    public class AuthManager : IAuthManager
    {
        private readonly IEntityWithIdDao<AuthUserData> _authUserDataDao;
        private readonly IEntityWithIdDao<User> _userDao;
        private readonly IRolesManager _rolesManager;

        public AuthManager(IEntityWithIdDao<AuthUserData> authUserDataDao, IEntityWithIdDao<User> userDao, IRolesManager rolesManager)
        {
            _authUserDataDao = authUserDataDao ?? throw new ArgumentNullException(nameof(authUserDataDao));
            _userDao = userDao ?? throw new ArgumentNullException(nameof(userDao));
            _rolesManager = rolesManager ?? throw new ArgumentNullException(nameof(rolesManager));
        }

        public bool SignIn(string login, string password)
        {
            var user = _userDao.GetAll().FirstOrDefault(item => item.Name == login);

            if (user is null)
                return false;

            return password == _authUserDataDao.GetAll()
                                               .FirstOrDefault(item => item.UserId == user.Id).Password;
        }

        public bool Register(User user, string password, Role role)
        {
            if (_userDao.GetAll().Any(item => item.Name == user.Name))
                return false;

            var userId = _userDao.Add(user);

            var authUserData = new AuthUserData { Password = password, UserId = userId };
            var authUserDataId = _authUserDataDao.Add(authUserData);

            user.AuthUserDataId = authUserDataId;
            _userDao.Update(user);

            _rolesManager.AddRoleToUser(user.Id, role.Id);

            return true;
        }
    }
}

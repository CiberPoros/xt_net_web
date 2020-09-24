using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeLayer.BLL.UsersLogicContracts;
using ThreeLayer.Common.Entities;
using ThreeLayer.DAL.Contracts;

namespace ThreeLayer.BLL.UsersLogic
{
    public class RolesManager : IRolesManager
    {
        private readonly IAssociationsDao<User, Role> _userRoleAssociationsDao;
        private readonly IEntityWithIdDao<Role> _rolesDao;

        public RolesManager(IAssociationsDao<User, Role> userRoleAssociationsDao, IEntityWithIdDao<Role> rolesDao)
        {
            _userRoleAssociationsDao = userRoleAssociationsDao ?? throw new ArgumentNullException(nameof(userRoleAssociationsDao));
            _rolesDao = rolesDao ?? throw new ArgumentNullException(nameof(rolesDao));
        }

        public bool AddNewRole(Role role)
        {
            if (_rolesDao.GetAll().Any(item => item.Title == role.Title))
                return false;

            _rolesDao.Add(role);
            return true;
        }

        public bool AddRoleToUser(int userId, int roleId) => _userRoleAssociationsDao.Bind(userId, roleId);
        public Role GetRoleByTitle(string title) => _rolesDao.GetAll().FirstOrDefault(role => role.Title == title);
        public IEnumerable<Role> GetRolesByUserId(int userId) => _userRoleAssociationsDao.GetAssociatedEntities(userId);
        public bool RemoveRoleFromUser(int userId, int roleId) => _userRoleAssociationsDao.UnBind(userId, roleId);
    }
}

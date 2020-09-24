using System.Collections.Generic;
using ThreeLayer.Common.Entities;

namespace ThreeLayer.BLL.UsersLogicContracts
{
    public interface IRolesManager
    {
        bool AddNewRole(Role role);
        IEnumerable<Role> GetRolesByUserId(int userId);
        Role GetRoleByTitle(string title);
        bool AddRoleToUser(int userId, int roleId);
        bool RemoveRoleFromUser(int userId, int roleId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Ninject;
using ThreeLayer.BLL.UsersLogicContracts;

namespace ThreeLayer.PL.WebPages.Models
{
    public class UsersRoleProvider : RoleProvider
    {
        private readonly IRolesManager _rolesManager;
        private readonly IUsersManager _usersManager;

        public UsersRoleProvider()
        {
            _rolesManager = DependencesContainer.Resolver.Get<IRolesManager>();
            _usersManager = DependencesContainer.Resolver.Get<IUsersManager>();
        }

        public override string[] GetRolesForUser(string username)
        {
            var user = _usersManager.GetAllUsers().FirstOrDefault(item => item.Name == username);

            if (user == null)
                return null;

            return _rolesManager.GetRolesByUserId(user.Id)
                                .Select(item => item.Title)
                                .ToArray();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var user = _usersManager.GetAllUsers().FirstOrDefault(item => item.Name == username);

            if (user == null)
                return false;

            return _rolesManager.GetRolesByUserId(user.Id).Any(role => role.Title == roleName);
        }

        #region NOT_IMPLEMENTED
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames) => throw new NotImplementedException();
        public override void CreateRole(string roleName) => throw new NotImplementedException();
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole) => throw new NotImplementedException();
        public override string[] FindUsersInRole(string roleName, string usernameToMatch) => throw new NotImplementedException();
        public override string[] GetAllRoles() => throw new NotImplementedException();

        public override string[] GetUsersInRole(string roleName) => throw new NotImplementedException();

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames) => throw new NotImplementedException();
        public override bool RoleExists(string roleName) => throw new NotImplementedException(); 
        #endregion
    }
}
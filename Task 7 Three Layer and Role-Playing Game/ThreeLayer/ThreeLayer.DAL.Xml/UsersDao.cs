using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ThreeLayer.Common.Entities;
using ThreeLayer.DAL.Contracts;
using ThreeLayer.DAL.Xml.Extensions;

namespace ThreeLayer.DAL.Xml
{
    public class UsersDao : EntityXmlDao<User>, IUsersDao
    {
        public UsersDao()
        {
            StoragePathAppSettingsKey = "UsersStoragePath";
            DefaultStoragePath = @"Storage\UsersList.xml";
        }

        protected override string StoragePathAppSettingsKey { get; }
        protected override string DefaultStoragePath { get; }

        public event EventHandler<User> UserRemoved;

        public void AddUser(User user) => Add(user);
        public IEnumerable<User> GetAllUsers() => GetAll();

        public void RemoveUserById(int id)
        {
            var document = XDocument.Load(_storageFileInfo.FullName);
            var match = document.Root.Elements()
                                     .FirstOrDefault(xElement => xElement.FromXElement<User>().Id == id);

            if (match == null)
                return;

            match.Remove();
            document.Save(_storageFileInfo.FullName);

            UserRemoved(this, match.FromXElement<User>());
        }
    }
}

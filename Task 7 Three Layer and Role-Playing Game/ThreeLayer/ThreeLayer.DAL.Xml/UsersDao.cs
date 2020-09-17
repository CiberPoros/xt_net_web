﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ThreeLayer.Common.Entities;
using ThreeLayer.DAL.Contracts;
using ThreeLayer.DAL.Xml.Abstract;
using ThreeLayer.DAL.Xml.Extensions;

namespace ThreeLayer.DAL.Xml
{
    public class UsersDao : EntityWithIdXmlDao<User>, IUsersDao
    {
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

        protected override string GetEntityName() => nameof(User);
    }
}

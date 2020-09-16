using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ThreeLayer.Common.Entities;
using ThreeLayer.DAL.Contracts;
using ThreeLayer.DAL.Xml.Extensions;
using ThreeLayers.DAL.Entities;

namespace ThreeLayer.DAL.Xml
{
    public class UserAwardBondsDao : EntityXmlDao<UserAwardBondsDao>, IUserAwardBondsDao, IDisposable
    {
        private readonly IUsersDao _usersDao;
        private readonly IAwardsDao _awardsDao;

        public UserAwardBondsDao(IUsersDao usersDao, IAwardsDao awardsDao)
        {
            _usersDao = usersDao ?? throw new ArgumentNullException(nameof(usersDao));
            _awardsDao = awardsDao ?? throw new ArgumentNullException(nameof(awardsDao));

            Subscribe();
        }

        public bool Bind(int userId, int awardId)
        {
            var document = XDocument.Load(_storageFileInfo.FullName);

            var match = document.Root.Elements().FirstOrDefault(xElement => xElement.FromXElement<UserAwardBond>().AwardId == awardId
                                                                            && xElement.FromXElement<UserAwardBond>().UserId == userId);

            if (match != null)
                return false;

            document.AddToRoot(new UserAwardBond() { UserId = userId, AwardId = awardId }.ToXElement());

            return true;
        }

        public IEnumerable<Award> GetAwardsOfUserById(int userId)
        {
            var document = XDocument.Load(_storageFileInfo.FullName);
            var awardIdSet = document.Root.Elements()
                                          .Where(xElement => xElement.FromXElement<UserAwardBond>().UserId == userId)
                                          .Select(xElement => xElement.FromXElement<UserAwardBond>().AwardId)
                                          .ToHashSet();

            return _awardsDao.GetAllAwards().Where(currentAward => awardIdSet.Contains(currentAward.Id));
        }

        public IEnumerable<User> GetOwnersOfAwardById(int awardId)
        {
            var document = XDocument.Load(_storageFileInfo.FullName);
            var userIdSet = document.Root.Elements()
                                         .Where(xElement => xElement.FromXElement<UserAwardBond>().AwardId == awardId)
                                         .Select(xElement => xElement.FromXElement<UserAwardBond>().UserId)
                                         .ToHashSet();

            return _usersDao.GetAllUsers().Where(currentUser => userIdSet.Contains(currentUser.Id));
        }

        public bool UnBind(int userId, int awardId)
        {
            var document = XDocument.Load(_storageFileInfo.FullName);

            var match = document.Root.Elements().FirstOrDefault(xElement => xElement.FromXElement<UserAwardBond>().AwardId == awardId
                                                                            && xElement.FromXElement<UserAwardBond>().UserId == userId);

            if (match == null)
                return false;

            match.Remove();
            document.Save(_storageFileInfo.FullName);

            return true;
        }

        public void Dispose() => Unsubscribe();

        private void UnBindAllAwards(int userId)
        {
            var document = XDocument.Load(_storageFileInfo.FullName);

            var bindedElements = document.Root.Elements()
                                              .Where(xElement => xElement.FromXElement<UserAwardBond>().UserId == userId)
                                              .ToList();

            foreach (var element in bindedElements)
                element.Remove();

            document.Save(_storageFileInfo.FullName);
        }

        private void UnBindAllOwners(int awardId)
        {
            var document = XDocument.Load(_storageFileInfo.FullName);

            var bindedElements = document.Root.Elements()
                                              .Where(xElement => xElement.FromXElement<UserAwardBond>().AwardId == awardId)
                                              .ToList();

            foreach (var element in bindedElements)
                element.Remove();

            document.Save(_storageFileInfo.FullName);
        }

        private void OnUserRemoved(object sender, User user) => UnBindAllAwards(user.Id);
        private void OnAwardRemoved(object sender, Award award) => UnBindAllOwners(award.Id);

        private void Subscribe()
        {
            _usersDao.UserRemoved += OnUserRemoved;
            _awardsDao.AwardRemoved += OnAwardRemoved;
        }

        private void Unsubscribe()
        {
            _usersDao.UserRemoved -= OnUserRemoved;
            _awardsDao.AwardRemoved -= OnAwardRemoved;
        }

        protected override string GetEntityName() => nameof(UserAwardBond);
    }
}

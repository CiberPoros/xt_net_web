using System;
using System.Collections.Generic;
using ThreeLayer.BLL.UsersLogicContracts;
using ThreeLayer.Common.Entities;
using ThreeLayer.DAL.Contracts;

namespace ThreeLayer.BLL.UsersLogic
{
    public class AwardsManager : IAwardsManager
    {
        private readonly IEntityWithIdDao<Award> _awardsDao;
        private readonly IAssociationsDao<Award, User> _associationsDao;

        public AwardsManager(IEntityWithIdDao<Award> awardsDao, IAssociationsDao<Award, User> associationsDao)
        {
            _awardsDao = awardsDao ?? throw new ArgumentNullException(nameof(awardsDao));
            _associationsDao = associationsDao ?? throw new ArgumentNullException(nameof(associationsDao));
        }

        public void AddAward(Award award)
        {
            if (award is null)
                throw new ArgumentNullException(nameof(award));

            _awardsDao.Add(award);
        }

        public bool BindToUser(int awardId, int userId) => _associationsDao.Bind(awardId, userId);

        public IEnumerable<Award> GetAllAwards() => _awardsDao.GetAll();

        public IEnumerable<User> GetUsers(int awardId) => _associationsDao.GetAssociatedEntities(awardId);

        public bool RemoveAwardById(int id) => _awardsDao.RemoveById(id);
        public bool UnBindFromUser(int awardId, int userId) => _associationsDao.UnBind(awardId, userId);
    }
}

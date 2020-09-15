using System;
using System.Collections.Generic;
using ThreeLayer.BLL.UsersLogicContracts;
using ThreeLayer.Common.Entities;
using ThreeLayer.DAL.Contracts;

namespace ThreeLayer.BLL.UsersLogic
{
    public class AwardsManager : IAwardsManager
    {
        private readonly IAwardsDao _awardsDao;

        public AwardsManager(IAwardsDao awardsDao)
        {
            _awardsDao = awardsDao ?? throw new ArgumentNullException(nameof(awardsDao));
        }

        public void AddAward(Award award)
        {
            if (award is null)
                throw new ArgumentNullException(nameof(award));

            _awardsDao.AddAward(award);
        }

        public IEnumerable<Award> GetAllAwards() => _awardsDao.GetAllAwards();
        public void RemoveAwardById(int id) => _awardsDao.RemoveAwardById(id);
    }
}

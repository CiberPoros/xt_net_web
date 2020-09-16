using System;
using System.Collections.Generic;
using ThreeLayer.Common.Entities;
using ThreeLayer.DAL.Contracts;
using ThreeLayer.BLL.UsersLogicContracts;

namespace ThreeLayer.BLL.UsersLogic
{
    public class UserAwardBondsManager : IUserAwardBondsManager
    {
        private readonly IUserAwardBondsDao _userAwardBondsDao;

        public UserAwardBondsManager(IUserAwardBondsDao userAwardBondsDao)
        {
            _userAwardBondsDao = userAwardBondsDao ?? throw new ArgumentNullException(nameof(userAwardBondsDao));
        }

        public bool Bind(int userId, int awardId) => _userAwardBondsDao.Bind(userId, awardId);
        public bool UnBind(int userId, int awardId) => _userAwardBondsDao.UnBind(userId, awardId);
    }
}

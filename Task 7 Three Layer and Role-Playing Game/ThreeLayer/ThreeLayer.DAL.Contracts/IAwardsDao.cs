using System;
using System.Collections.Generic;
using ThreeLayer.Common.Entities;

namespace ThreeLayer.DAL.Contracts
{
    public interface IAwardsDao
    {
        event EventHandler<Award> AwardRemoved;

        void AddAward(Award award);
        void RemoveAwardById(int id);
        IEnumerable<Award> GetAllAwards();
    }
}

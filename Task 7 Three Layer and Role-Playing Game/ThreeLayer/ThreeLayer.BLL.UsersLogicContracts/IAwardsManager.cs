using System.Collections.Generic;
using ThreeLayer.Common.Entities;

namespace ThreeLayer.BLL.UsersLogicContracts
{
    public interface IAwardsManager
    {
        void AddAward(Award award);
        void RemoveAwardById(int id);
        IEnumerable<Award> GetAllAwards();
    }
}

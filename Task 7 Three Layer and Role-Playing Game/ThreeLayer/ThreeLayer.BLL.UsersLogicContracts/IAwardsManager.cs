using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

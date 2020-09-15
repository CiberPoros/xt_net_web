using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeLayer.Common.Entities;

namespace ThreeLayer.BLL.UsersLogicContracts
{
    public interface IUserAwardBondsManager
    {
        bool Bind(User user, Award award);
        bool UnBind(User user, Award award);
    }
}

using ThreeLayer.Common.Entities;

namespace ThreeLayer.BLL.UsersLogicContracts
{
    public interface IUserAwardBondsManager
    {
        bool Bind(int userId, int awardId);
        bool UnBind(int userId, int awardId);
    }
}

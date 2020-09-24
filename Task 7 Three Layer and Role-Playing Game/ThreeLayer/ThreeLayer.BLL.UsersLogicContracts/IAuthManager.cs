using ThreeLayer.Common.Entities;

namespace ThreeLayer.BLL.UsersLogicContracts
{
    public interface IAuthManager
    {
        bool Register(User user, string password, Role role);
        bool SignIn(string login, string password);
    }
}

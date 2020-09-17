using Ninject.Modules;
using ThreeLayer.BLL.UsersLogic;
using ThreeLayer.BLL.UsersLogicContracts;

namespace ThreeLayer.Common.Dependences
{
    public class NinjectRegistrationsBLL : NinjectModule
    {
        public override void Load()
        {
            Bind<IAwardsManager>().To<AwardsManager>().InSingletonScope();
            Bind<IUsersManager>().To<UsersManager>().InSingletonScope();
            Bind<IUserAwardBondsManager>().To<UserAwardBondsManager>().InSingletonScope();
        }
    }
}

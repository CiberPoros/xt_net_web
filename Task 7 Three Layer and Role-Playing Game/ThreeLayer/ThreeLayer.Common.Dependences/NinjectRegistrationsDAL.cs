using Ninject.Modules;
using ThreeLayer.DAL.Contracts;
using ThreeLayer.DAL.Xml;

namespace ThreeLayer.Common.Dependences
{
    public class NinjectRegistrationsDAL : NinjectModule
    {
        public override void Load()
        {
            Bind<IUsersDao>().To<UsersDao>().InSingletonScope();
            Bind<IAwardsDao>().To<AwardsDao>().InSingletonScope();
            Bind<IUserAwardBondsDao>().To<UserAwardBondsDao>().InSingletonScope();
        }
    }
}

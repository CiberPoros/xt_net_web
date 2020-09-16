using Ninject.Modules;
using ThreeLayer.DAL.Contracts;
using ThreeLayer.DAL.Xml;

namespace ThreeLayer.Common.Dependences
{
    public class NinjectRegistrationsDAL : NinjectModule
    {
        public override void Load()
        {
            Bind<IUsersDao>().To<UsersDao>();
            Bind<IAwardsDao>().To<AwardsDao>();
            Bind<IUserAwardBondsDao>().To<UserAwardBondsDao>();
        }
    }
}

using Ninject.Modules;
using ThreeLayer.Common.Entities;
using ThreeLayer.DAL.Contracts;
using ThreeLayer.DAL.Xml;
using ThreeLayer.DAL.Sql;

namespace ThreeLayer.Common.Dependences
{
    public class NinjectRegistrationsDAL : NinjectModule
    {
        public override void Load()
        {
            // XML
            //Bind(typeof(IEntityWithIdDao<>)).To(typeof(EntityWithIdXmlDao<>)).InSingletonScope();
            //Bind(typeof(IAssociationsDao<,>)).To(typeof(AssociationsDao<,>)).InSingletonScope();

            // SQL 
            Bind<IEntityWithIdDao<User>>().To<UsersDao>().InSingletonScope();
            Bind<IEntityWithIdDao<Award>>().To<AwardsDao>().InSingletonScope();
            Bind<IEntityWithIdDao<Role>>().To<RolesDao>().InSingletonScope();
            Bind<IEntityWithIdDao<AuthUserData>>().To<AuthUserDataDao>().InSingletonScope();

            Bind<IAssociationsDao<User, Award>>().To<UserAwardAssociationsDao>().InSingletonScope();
            Bind<IAssociationsDao<Award, User>>().To<AwardUserAssociationsDao>().InSingletonScope();
            Bind<IAssociationsDao<User, Role>>().To<UserRoleAssociationsDao>().InSingletonScope();
        }
    }
}

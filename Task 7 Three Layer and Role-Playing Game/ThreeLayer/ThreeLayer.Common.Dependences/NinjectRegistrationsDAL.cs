using Ninject.Modules;
using ThreeLayer.DAL.Contracts;
using ThreeLayer.DAL.Xml;

namespace ThreeLayer.Common.Dependences
{
    public class NinjectRegistrationsDAL : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IEntityWithIdDao<>)).To(typeof(EntityWithIdXmlDao<>)).InSingletonScope();
            Bind(typeof(IAssociationsDao<,>)).To(typeof(AssociationsDao<,>)).InSingletonScope();
        }
    }
}

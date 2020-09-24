using Ninject;
using ThreeLayer.Common.Dependences;

namespace ThreeLayer.PL.WebPages
{
    public static class DependencesContainer
    {
        public static readonly IKernel Resolver;

        static DependencesContainer()
        {
            Resolver = new StandardKernel(new NinjectRegistrationsDAL(), new NinjectRegistrationsBLL());
        }
    }
}
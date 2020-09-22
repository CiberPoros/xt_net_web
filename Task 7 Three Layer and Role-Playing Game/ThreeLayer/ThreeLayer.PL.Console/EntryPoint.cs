using Ninject;
using ThreeLayer.Common.Dependences;
using ThreeLayer.PL.Console.Dependences;

namespace ThreeLayer.PL.Console
{
    internal class EntryPoint
    {
        public static void Main()
        {
            IKernel resolver = new StandardKernel(new NinjectRegistrationsBLL(), new NinjectRegistrationsDAL(), new NinjectRegistrationsPL());

            var startedMenuManager = resolver.Get<StartedMenuManager>();

            startedMenuManager.StartShowingMenu();
        }
    }
}

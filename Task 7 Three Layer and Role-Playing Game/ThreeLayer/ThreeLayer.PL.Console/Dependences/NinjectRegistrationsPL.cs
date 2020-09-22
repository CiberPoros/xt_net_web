using Ninject.Modules;
using ThreeLayer.PL.Console.ActionManagers;

namespace ThreeLayer.PL.Console.Dependences
{
    internal class NinjectRegistrationsPL : NinjectModule
    {
        public override void Load()
        {
            Bind<UsersActionsManager>().ToSelf().InSingletonScope();
            Bind<AwardsActionsManager>().ToSelf().InSingletonScope();
            Bind<StartedMenuManager>().ToSelf().InSingletonScope();
        }
    }
}

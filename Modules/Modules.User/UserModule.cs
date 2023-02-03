using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Modules.User.Views;
using PrismRoles.Core;

namespace Modules.User
{
    [Roles("User")]
    public class UserModule : IModule
    {
        IRegionManager _regionManager;
        public UserModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("MainRegion", typeof(UserView));
        }
    }
}

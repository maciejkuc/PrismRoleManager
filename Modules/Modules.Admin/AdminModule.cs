using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Modules.Admin.Views;
using PrismRoles.Core;

namespace Modules.Admin
{
    [Roles("Admin")]
    public class AdminModule : IModule
    {
        IRegionManager _regionManager;
        public AdminModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("MainRegion", typeof(AdminView));
        }
    }
}

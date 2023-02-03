using System.Windows;
using Microsoft.Practices.Unity;
using PrismRoles.Views;
using System.Security.Principal;
using System.Threading;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Prism.Modularity;
using Modules.Admin;
using Modules.User;
using System.Collections.Generic;

namespace PrismRoles
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            //TODO: get your login/role information from your source and set the IIdentity/IPrincipal
            MessageBoxResult boxResult = MessageBox.Show("Rodzaj uzytkownika", "Wybierz rodzaj", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            var ident = WindowsIdentity.GetCurrent();
            List<string> roles = new List<string>();
            if (boxResult == MessageBoxResult.Yes)
                roles.Add("User");
            else if (boxResult == MessageBoxResult.No)
                roles.Add("Admin");
            else if(boxResult == MessageBoxResult.Cancel)
                roles.AddRange(new List<string>() { "Admin", "User" });

            var p = new GenericPrincipal(ident, roles.ToArray());
            Thread.CurrentPrincipal = p;

            Application.Current.MainWindow.Show();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return base.CreateModuleCatalog();
            //return new ConfigurationModuleCatalog();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            moduleCatalog.AddModule(typeof(UserModule));
            moduleCatalog.AddModule(typeof(AdminModule));
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterType<IModuleInitializer, PrismRoles.Core.Prism.RoleBasedModuleInitializer>(new ContainerControlledLifetimeManager());
        }
    }
}

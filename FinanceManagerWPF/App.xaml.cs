using FinanceManager.BLL.Repositories.Concreate.DataBaseMCSQLFinanceManager;
using FinanceManager.DAL.Data;
using FinanceManager.DAL.Repositories.Abstract.DataBaseMCSQLFinanceManager;
using FinanceManager.DAL.Repositories.Concreate.DataBaseMCSQLFinanceManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using WPF.ViewModels;
using WPF.Views;

namespace FinanceManagerWPF
{
    public partial class App : Application
    {
        public IUnityContainer Container;

        protected override void OnStartup(StartupEventArgs e)
        {
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            base.OnStartup(e);
            RegisterUnity();
            LoginView lf = Container.Resolve<LoginView>();
            bool? result = lf.ShowDialog();

            if (result.HasValue && result.Value)
            {
                OrderListView al = Container.Resolve<OrderListView>();
                Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                Current.MainWindow = al;
                al.Show();
            }
        }

        private void RegisterUnity()
        {
            Container = new UnityContainer();
            var configuration = new ConfigurationBuilder()
    .AddJsonFile("D:\\University\\Ivan_pz\\FinanceManager.DAL\\Data\\appsettings.json", optional: false, reloadOnChange: true)
    .Build();

            var connectionString = configuration.GetConnectionString("BloggingDatabase");
            // Register DbContext
            Container.RegisterType<FinanceManagerContext>(
               new HierarchicalLifetimeManager(),
               new InjectionConstructor(
                   new DbContextOptionsBuilder<FinanceManagerContext>()
                       .UseSqlServer(connectionString)
            .Options
            )
           );


            // Register repositories
            Container.RegisterType<IOrdersRepository, FinanceManagerWPFOrdersRepository>(new HierarchicalLifetimeManager());
            Container.RegisterType<IProductsRepository, FinanceManagerWPFProductsRepository>(new HierarchicalLifetimeManager());

            // Register business logic services
            Container.RegisterType<IUsersRepository, FinanceManagerWPFUserRepository>(new HierarchicalLifetimeManager());

            // Register view models
            Container.RegisterType<OrderAddViewModel>(new HierarchicalLifetimeManager());
            Container.RegisterType<OrderChangeViewModel>(new HierarchicalLifetimeManager());
            Container.RegisterType<OrderListViewModel>(new HierarchicalLifetimeManager());
            Container.RegisterType<LoginViewModel>(new HierarchicalLifetimeManager());
            // Add other registrations as needed
        }

        private void OnExit(object sender, ExitEventArgs e)
        {
            Container?.Dispose();
        }
    }
}

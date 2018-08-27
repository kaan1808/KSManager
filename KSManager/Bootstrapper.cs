using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using KSManager.Api;
using KSManager.ViewModels;

namespace KSManager
{
    public class Bootstrapper : BootstrapperBase
    {
        private readonly SimpleContainer _container;

        public Bootstrapper()
        {
            _container = new SimpleContainer();
            Initialize();
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override void Configure()
        {
            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>()

                .Singleton<MainViewModel>()
                .Singleton<LoginViewModel>()
                .Singleton<ManagerViewModel>()
                .Singleton<PasswordManagerViewModel>()

                .Singleton<IKsManagerApi, KsManagerApi>();
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainViewModel>();
        }
    }
}

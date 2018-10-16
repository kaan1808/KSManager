using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using KSManager.Api;

namespace KSManager.ViewModels
{
    public class ManagerViewModel
        : Conductor<Screen>.Collection.OneActive
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _windowManager;
        private readonly IKsManagerApi _ksManagerApi;

        private BindableCollection<Screen> _modules;
        private Screen _selectedModule;

        public ManagerViewModel(IEventAggregator eventAggregator, IWindowManager windowManager, IKsManagerApi ksManagerApi)
        {
            _eventAggregator = eventAggregator;
            _windowManager = windowManager;
            _ksManagerApi = ksManagerApi;

            Modules = new BindableCollection<Screen>
            {
                IoC.Get<PasswordManagerViewModel>(),
                IoC.Get<ContactManagerViewModel>()
            };

            SelectedModule = Modules.First();
        }

        public BindableCollection<Screen> Modules
        {
            get => _modules;
            set { Set(ref _modules, value); }
        }

        public Screen SelectedModule
        {
            get => _selectedModule;
            set
            {
                Set(ref _selectedModule, value);
                ActivateItem(SelectedModule);
            }
        }

        public void Logout()
        {
            _eventAggregator.PublishOnUIThread(new NavigateMessage()
            {
                Screen = new LoginViewModel(_ksManagerApi, _eventAggregator , true)
            });

            TryClose();
        }

        public void Settings()
        {
            var view = new SettingsViewModel(_windowManager,_eventAggregator);
            _windowManager.ShowDialog(view);
        }
    }
}

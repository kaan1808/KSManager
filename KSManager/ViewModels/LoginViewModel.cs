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
    public class LoginViewModel : Screen
    {
        private readonly IKsManagerApi _ksManagerApi;
        private readonly IEventAggregator _eventAggregator;
    
        private string _username;
        private string _password;

        public LoginViewModel(IKsManagerApi ksManagerApi, IEventAggregator eventAggregator, bool accessTokenIsGenerated)
        {
            _eventAggregator = eventAggregator;
            if(accessTokenIsGenerated == false)
                _ksManagerApi = ksManagerApi;
            else
            {
                ksManagerApi.ClearProperties();
                _ksManagerApi = ksManagerApi;
            }
        }


        public RelayCommand<object> LoginCommand => new RelayCommand<object>(LoginCommandExecute, LoginCommandCanExecute);

        public string Username
        {
            get => _username;
            set
            {
                Set(ref _username, value);
                NotifyOfPropertyChange();
            }
        }

        public string Password
        {
            get => _password;
            set
            {

                Set(ref _password, value);
                NotifyOfPropertyChange(nameof(LoginCommand));
            }
        }

        private bool LoginCommandCanExecute(object obj) => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);

        private async void LoginCommandExecute(object obj)
        {
            try
            {
                await _ksManagerApi.Authenticate(Username, Password);

                _eventAggregator.PublishOnUIThread(new NavigateMessage
                {
                    Screen = IoC.Get<ManagerViewModel>()
                });

            }
            catch (KsManagerApiException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Register()
        {
            try
            {
                 _eventAggregator.PublishOnUIThread(new NavigateMessage
                {
                    Screen =  IoC.Get<RegisterViewModel>()
                });
            }
            catch (KsManagerApiException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

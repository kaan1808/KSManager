using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using KSManager.Api;
using MaterialDesignThemes.Wpf;

namespace KSManager.ViewModels
{
    public class RegisterViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IKsManagerApi _ksManagerApi;

        private string _username;
        private string _password;
        private string _repeatPassword;

        public RegisterViewModel(IEventAggregator eventAggregator, IKsManagerApi ksManagerApi)
        {
            _eventAggregator = eventAggregator;
            _ksManagerApi = ksManagerApi;
        }
        
        public string Username
        {
            get => _username;
            set
            {
                Set(ref _username, value);
                NotifyOfPropertyChange(() => CanRegister);
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                Set(ref _password, value);
                NotifyOfPropertyChange(() => CanRegister);
            }
        }

        public string RepeatPassword
        {
            get => _repeatPassword;
            set
            {
                Set(ref _repeatPassword, value);
                NotifyOfPropertyChange(() => CanRegister);
            }
        }



        public void Login()
        {
            Username = string.Empty;
            Password = string.Empty;
            RepeatPassword = string.Empty;
            _eventAggregator.PublishOnUIThread(new NavigateMessage()
            {
                Screen = IoC.Get<LoginViewModel>()
            });
        }

        public bool CanRegister => !string.IsNullOrWhiteSpace(Username) &&
                                    !string.IsNullOrWhiteSpace(Password) && Password.Equals(RepeatPassword);

        public async void Register()
        {
            try
            {
                await _ksManagerApi.Register(new Api.Client.Model.RegisterObject()
                {
                    Username = Username,
                    Password = Password
                });
                if (MessageBox.Show("Register completed", "Register", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    var loginViewModel = IoC.Get < LoginViewModel>();
                    loginViewModel.Username = Username;
                    _eventAggregator.PublishOnUIThread(new NavigateMessage()
                    {
                        Screen = loginViewModel
                    });
                }
            }
            catch (KsManagerApiException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

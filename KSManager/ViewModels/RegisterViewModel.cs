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

        public RelayCommand<object> RegisterCommand =>
            new RelayCommand<object>(RegisterCommandExecute, RegisterCommandCanExecute);

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
                NotifyOfPropertyChange();
            }
        }

        public string RepeatPassword
        {
            get => _repeatPassword;
            set
            {
                Set(ref _repeatPassword, value);
                
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

        private bool RegisterCommandCanExecute(object obj) => !string.IsNullOrWhiteSpace(Username) && 
                                                              !string.IsNullOrWhiteSpace(Password) &&
                                                              !string.IsNullOrWhiteSpace(RepeatPassword) && Password.Equals(RepeatPassword);

        private async void RegisterCommandExecute(object obj)
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
                    _eventAggregator.PublishOnUIThread(new NavigateMessage()
                    {
                        Screen = IoC.Get<LoginViewModel>()
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}

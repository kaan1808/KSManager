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

        private string _username;
        private string _password;

        public LoginViewModel(IKsManagerApi ksManagerApi)
        {
            _ksManagerApi = ksManagerApi;
        }

        public string Username
        {
            get => _username;
            set
            {
                Set(ref _username, value);
                NotifyOfPropertyChange(nameof(CanLogin));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                Set(ref _password, value);
                NotifyOfPropertyChange(nameof(CanLogin));
            }
        }

        public bool CanLogin => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);

        public async void Login()
        {
            try
            {
                await _ksManagerApi.Authenticate(Username, Password);
            }
            catch (KsManagerApiException ex)
            {
                MessageBox.Show(ex.Message);
            }

            MessageBox.Show("Hi");

        }

    }
}

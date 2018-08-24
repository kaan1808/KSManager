using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using KSManager.Api;

namespace KSManager.ViewModels
{
    public class LoginViewModel : Screen
    {
        private readonly IKsManagerApi _ksManagerApi;

        public LoginViewModel(IKsManagerApi ksManagerApi)
        {
            _ksManagerApi = ksManagerApi;
        }

    }
}

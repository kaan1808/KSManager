using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using KSManager.ViewModels;

namespace KSManager.Helper
{
    public class MenuItem : PropertyChangedBase
    {
        private Screen _viewModelBase;

        public MenuItem(Screen viewModel)
        {
            ViewModel = viewModel;
        }

        public Screen ViewModel
        {
            get => _viewModelBase;
            set
            {
                _viewModelBase = value;
                NotifyOfPropertyChange();
            }
        }
    }
}

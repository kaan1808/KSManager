using Caliburn.Micro;
using KSManager.Api;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSManager.ViewModels
{
    public class ContactManagerViewModel : Screen
    {
        private readonly IKsManagerApi _ksManagerApi;
        private readonly IEventAggregator _eventAggregator;

        private PackIconKind _packIconKind;


        public override string DisplayName => "Contact Manager";


        public ContactManagerViewModel(IKsManagerApi ksManagerApi, IEventAggregator eventAggregator)
        {
            _ksManagerApi = ksManagerApi;
            _eventAggregator = eventAggregator;
            PackIconKind = PackIconKind.Account;
        }


        public PackIconKind PackIconKind
        {
            get => _packIconKind;
            set => Set(ref _packIconKind, value);
        }





    }
}

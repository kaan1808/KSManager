using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace KSManager.ViewModels
{
    public class MainViewModel
        : Conductor<Screen>.Collection.OneActive,
            IHandle<NavigateMessage>
    {
        private readonly IEventAggregator _eventAggregator;


        public MainViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
        }

        protected override void OnViewReady(object view)
        {
            ChangeActiveItem(IoC.Get<LoginViewModel>(), false);
        }

        public void Handle(NavigateMessage message)
        {
            ChangeActiveItem(message.Screen, true);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace KSManager.ViewModels
{
    public class MainViewModel : Conductor<Screen>.Collection.OneActive
    {
        protected override void OnViewReady(object view)
        {
            ChangeActiveItem(IoC.Get<LoginViewModel>(), false);
        }
    }
}

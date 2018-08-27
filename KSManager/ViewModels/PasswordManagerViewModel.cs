using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using KSManager.Api;
using KSManager.Model;

namespace KSManager.ViewModels
{
    public class PasswordManagerViewModel
        : Screen
    {
        private readonly IKsManagerApi _ksManagerApi;

        private BindableCollection<SmallPasswordEntry> _entries;

        public PasswordManagerViewModel(IKsManagerApi ksManagerApi)
        {
            _ksManagerApi = ksManagerApi;

        }

        public override string DisplayName => "Password Manager";

        public BindableCollection<SmallPasswordEntry> Entries
        {
            get => _entries;
            set => Set(ref _entries, value);
        }

        protected override async void OnActivate()
        {
            Entries = new BindableCollection<SmallPasswordEntry>((await _ksManagerApi.GetPasswordEntriesSmall())
                .Select(x => new SmallPasswordEntry
                {
                    Id = x.Id,
                    Title = x.Title,
                    Icon = x.Icon,
                    Username = x.Username
                }));
        } 
    }
}

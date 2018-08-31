using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using KSManager.Api;
using KSManager.Model;
using MaterialDesignThemes.Wpf;

namespace KSManager.ViewModels
{
    public class PasswordManagerViewModel
        : Screen
    {
        private readonly IKsManagerApi _ksManagerApi;
        private readonly IEventAggregator _eventAggregator;

        private CancellationTokenSource _cancellationTokenSource;

        private PasswordManagerDetailViewModel _passwordManagerDetail;

        private BindableCollection<SmallPasswordEntry> _entries;
        private SmallPasswordEntry _selecetedListEntry;

        private PackIconKind _packIconKind;

        public PasswordManagerViewModel(IKsManagerApi ksManagerApi, IEventAggregator eventAggregator)
        {
            _ksManagerApi = ksManagerApi;
            _eventAggregator = eventAggregator;
            PackIconKind = PackIconKind.Key;
        }

        public PackIconKind PackIconKind
        {
            get => _packIconKind;
            set => Set(ref _packIconKind, value);
        }

        public SmallPasswordEntry SelectedListEntry
        {
            get => _selecetedListEntry;
            set
            {
                Set(ref _selecetedListEntry, value);
                NotifyOfPropertyChange(() => CanDeleteEntry);

                _cancellationTokenSource?.Cancel();
                _cancellationTokenSource = new CancellationTokenSource();
                PasswordManagerDetail.LoadEntry(_selecetedListEntry.Id, _cancellationTokenSource.Token);
            }
        }

        public PasswordManagerDetailViewModel PasswordManagerDetail
        {
            get
            {
                if (_passwordManagerDetail == null && _selecetedListEntry != null)
                {
                    CreateDetailViewModel();
                }

                return _passwordManagerDetail;
            }
            set => Set(ref _passwordManagerDetail, value);
        }

        public override string DisplayName => "Password Manager";

        public BindableCollection<SmallPasswordEntry> Entries
        {
            get => _entries;
            set => Set(ref _entries, value);
        }

        protected override async void OnActivate()
        {
            await GetEntryList(); 
        }

        public void NewEntry()
        {
            CreateDetailViewModel();
            PasswordManagerDetail.SelectedEntry = new PasswordEntry();
        }

        public bool CanDeleteEntry => SelectedListEntry != null;

        public void DeleteEntry()
        {
            _ksManagerApi.DeletePasswordEntry(SelectedListEntry.Id, CancellationToken.None);
            Entries.Remove(SelectedListEntry);

            if (Entries.Count <= 0)
            {
                _eventAggregator.PublishOnUIThread(new NavigateMessage()
                {
                    Screen = IoC.Get<LoginViewModel>()
                });
            }
        }

        private void CreateDetailViewModel()
        {
            if (_passwordManagerDetail == null)
            {
                var detail = IoC.Get<PasswordManagerDetailViewModel>();
                detail.Parent = this;
                PasswordManagerDetail = detail;   
            }
        }

        public async Task GetEntryList()
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

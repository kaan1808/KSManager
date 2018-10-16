using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using AutoMapper;
using Caliburn.Micro;
using KSManager.Api;
using KSManager.Model;
using MaterialDesignThemes.Wpf;

namespace KSManager.ViewModels
{
    public class PasswordManagerViewModel
        : Screen
    {
        #region fields
        private readonly IKsManagerApi _ksManagerApi;
        private readonly IEventAggregator _eventAggregator;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _isNew;
        private PasswordManagerDetailViewModel _passwordManagerDetail;

        private BindableCollection<SmallPasswordEntry> _entries;
        private SmallPasswordEntry _selecetedListEntry;

        private PackIconKind _packIconKind;
        #endregion fields

        #region constructor
        public PasswordManagerViewModel(IKsManagerApi ksManagerApi, IEventAggregator eventAggregator)
        {
            _ksManagerApi = ksManagerApi;
            _eventAggregator = eventAggregator;
            PackIconKind = PackIconKind.Key;
        }
        #endregion constructor

        #region Properties
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
                NotifyOfPropertyChange(() => CanCopyEntry);

                _cancellationTokenSource?.Cancel();
                _cancellationTokenSource = new CancellationTokenSource();

                if (_isNew)
                {
                    _isNew = false;
                    return;
                }

                if(SelectedListEntry != null)
                    PasswordManagerDetail.LoadEntry(SelectedListEntry.Id, _cancellationTokenSource.Token);
                
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
        #endregion Properties

        protected override async void OnActivate()
        {
            await GetEntryList(); 
        }

        #region public methods
        public bool CanDeleteEntry => SelectedListEntry != null;
        public bool CanCopyEntry => SelectedListEntry != null;
        public void NewEntry()
        {
            CreateDetailViewModel();
            PasswordManagerDetail.SelectedEntry = new PasswordEntry();
            _isNew = true;
            SelectedListEntry = new SmallPasswordEntry();
        }

        public void DeleteEntry()
        {
            _ksManagerApi.DeletePasswordEntry(SelectedListEntry.Id, CancellationToken.None);
            var index = Entries.IndexOf(SelectedListEntry);

            Entries.Remove(SelectedListEntry);
            if (Entries.Count == 0)
                PasswordManagerDetail = null;
            else    
                SelectedListEntry = Entries[index - 1];
        }

        public async void CopyEntry()
        {
            var entry = await _ksManagerApi.GetPasswordEntry(SelectedListEntry.Id, CancellationToken.None);
            var copy = new PasswordEntry
            {
                Email = entry.Email,
                Icon = entry.Icon,
                Title = entry.Title + ("(Copy)"),
                Password = entry.Password,
                SecurityAnswer = entry.SecurityAnswer,
                SecurityQuestion = entry.SecurityQuestion,
                Url = entry.Url,
                Username = entry.Username,
                Note = entry.Note
            };
            var newEntry = await _ksManagerApi.SavePasswordEntry(Mapper.Map<Api.Client.Model.PasswordEntry>(copy), CancellationToken.None);
            
            RefreshEntries(Mapper.Map<Api.Client.Model.PasswordEntry>(newEntry), Mapper.Map<Api.Client.Model.PasswordEntry>(entry));
            SelectedListEntry = Entries.SingleOrDefault(x => x.Id == newEntry.Id);
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

        public void RefreshEntries(Api.Client.Model.PasswordEntry newEntry, Api.Client.Model.PasswordEntry content)
        {
            var listBoxItem = Entries.SingleOrDefault(x => x.Id == newEntry.Id);
            if (listBoxItem != null)
            {
                listBoxItem.Title = content.Title;
                listBoxItem.Username = content.Username;
                listBoxItem.Icon = content.Icon;
            }
            else
            {
                Entries.Add(new SmallPasswordEntry()
                {
                    Icon = newEntry.Icon,
                    Id = newEntry.Id,
                    Title = newEntry.Title,
                    Username = newEntry.Username
                });
            }

        }
        #endregion public methods

        #region private methods
        private void CreateDetailViewModel()
        {
            if (_passwordManagerDetail == null)
            {
                var detail = IoC.Get<PasswordManagerDetailViewModel>();
                detail.Parent = this;
                PasswordManagerDetail = detail;
            }
        }
        #endregion private methods
    }
}

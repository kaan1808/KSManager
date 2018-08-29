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

        private CancellationTokenSource _cancellationTokenSource;

        private BindableCollection<SmallPasswordEntry> _entries;
        private SmallPasswordEntry _selecetedListEntry;

        private PasswordEntry _selectedEntry;

        private PackIconKind _packIconKind;

        public PasswordManagerViewModel(IKsManagerApi ksManagerApi)
        {
            _ksManagerApi = ksManagerApi;
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

                _cancellationTokenSource?.Cancel();
                _cancellationTokenSource = new CancellationTokenSource();
                LoadEntry(SelectedListEntry.Id, _cancellationTokenSource.Token);
            }
        }

        public PasswordEntry SelectedEntry
        {
            get => _selectedEntry;
            set => Set(ref _selectedEntry, value);
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
            SelectedListEntry = Entries.First();
        }

        private async void LoadEntry(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var entry = await _ksManagerApi.GetPasswordEntry(id, cancellationToken);

                SelectedEntry = new PasswordEntry
                {
                    Id = entry.Id,
                    Title = entry.Title,
                    Email = entry.Email,
                    Icon = entry.Icon,
                    LastChanges = entry.LastChanges,
                    Note = entry.Note,
                    Password = entry.Password,
                    Securityanswer = entry.SecurityAnswer,
                    Securityquestion = entry.SecurityQuestion,
                    Url = entry.Url,
                    Username = entry.Username
                };
            }
            catch (KsManagerApiException ex)
            {
                // TODO:
                MessageBox.Show(ex.Message);
            }
            catch (OperationCanceledException)
            {
            }

            
        }

        public async Task SaveEntry()
        {
            var content = new Api.Client.Model.PasswordEntry
            {
                Email = SelectedEntry.Email,
                Title = SelectedEntry.Title,
                SecurityAnswer = SelectedEntry.Securityanswer,
                SecurityQuestion = SelectedEntry.Securityquestion,
                Icon = SelectedEntry.Icon,
                Note = SelectedEntry.Note,
                Id = SelectedEntry.Id,
                LastChanges = SelectedEntry.LastChanges,
                Password = SelectedEntry.Password,
                Url = SelectedEntry.Url,
                Username = SelectedEntry.Username

            };

            await _ksManagerApi.UpdatePasswordEntry(content);
            LoadEntry(content.Id, CancellationToken.None);

            var listBoxItem = Entries.SingleOrDefault(x => x.Id == content.Id);
            if (listBoxItem != null)
            {
                listBoxItem.Title = content.Title;
                listBoxItem.Username = content.Username;
                listBoxItem.Icon = content.Icon;
            }
        }
    }
}

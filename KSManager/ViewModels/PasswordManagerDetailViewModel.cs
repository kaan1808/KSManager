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

namespace KSManager.ViewModels
{
    public class PasswordManagerDetailViewModel
        : Screen
    {
        private readonly IKsManagerApi _ksManagerApi;
        private PasswordEntry _selectedEntry;

        private PasswordManagerViewModel _parent;
      

        public PasswordManagerDetailViewModel(IKsManagerApi ksManagerApi)
        {
            _ksManagerApi = ksManagerApi;
        }

        public PasswordEntry SelectedEntry
        {
            get => _selectedEntry;
            set => Set(ref _selectedEntry, value);
        }

        public new PasswordManagerViewModel Parent
        {
            get => _parent;
            set => Set(ref _parent, value);
        }

        public async void LoadEntry(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var entry = await _ksManagerApi.GetPasswordEntry(id, cancellationToken);
                SelectedEntry = Mapper.Map<PasswordEntry>(entry);
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
            var content = Mapper.Map<Api.Client.Model.PasswordEntry>(SelectedEntry);
            var newEntry = await _ksManagerApi.SavePasswordEntry(content);
            LoadEntry(newEntry.Id, CancellationToken.None);

            var listBoxItem = Parent.Entries.SingleOrDefault(x => x.Id == newEntry.Id);
            if (listBoxItem != null)
            {
                listBoxItem.Title = content.Title;
                listBoxItem.Username = content.Username;
                listBoxItem.Icon = content.Icon;
            }

            await Parent.GetEntryList();
        }
    }

}

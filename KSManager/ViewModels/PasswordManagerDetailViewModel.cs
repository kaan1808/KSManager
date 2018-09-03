using System;
using System.Collections.Generic;
using System.IO;
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
    public class PasswordManagerDetailViewModel
        : Screen
    {
        private readonly IKsManagerApi _ksManagerApi;
        private PasswordEntry _selectedEntry;
        private int _icon;
        private PasswordManagerViewModel _parent;

        private ImageDialogViewModel _imageDialog;

        public PasswordManagerDetailViewModel(IKsManagerApi ksManagerApi)
        {
            _ksManagerApi = ksManagerApi;
        }

        #region Properties
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
        #endregion Properties


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

            Parent.RefreshEntries(newEntry, content);
            await Parent.GetEntryList();
        }

        public async void Icons()
        {
           var result =  await DialogHost.Show(_imageDialog ??(_imageDialog = new ImageDialogViewModel()));
            if (result != null)
            {
                SelectedEntry.Icon = (int)result;
                Parent.SelectedListEntry.Icon = SelectedEntry.Icon;
            }
        }
    }

}

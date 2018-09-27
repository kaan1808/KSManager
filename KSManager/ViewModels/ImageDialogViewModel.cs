using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using Screen = Caliburn.Micro.Screen;

namespace KSManager.ViewModels
{
    public class ImageDialogViewModel : Screen
    {
        private List<int> _items;
        private int _selectedItem;

        public ImageDialogViewModel()
        {
            Items = new List<int>();
            LoadImages();
        }

        public List<int> Items
        {
            get => _items;
            set => Set(ref _items, value);
        }

        public int SelectedItem
        {
            get => _selectedItem;
            set
            {
                Set(ref _selectedItem, value);
                DialogHost.CloseDialogCommand.Execute(SelectedItem, null);
            }
        }

        public void LoadImages()
        {
            for (int i = 0; i < 274; ++i)
            {
                Items.Add(i);
            }
        }
    }

 
}

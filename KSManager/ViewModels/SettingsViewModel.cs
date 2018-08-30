using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;

namespace KSManager.ViewModels
{
    public class SettingsViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _windowManager;

        private IEnumerable<Swatch> _swatches;


        public SettingsViewModel(IWindowManager windowManager, IEventAggregator eventAggregator)
        {
            _windowManager = windowManager;
            _eventAggregator = eventAggregator;

            Swatches = new SwatchesProvider().Swatches.Where(x => x.Name != "bluegrey" && x.Name != "brown" && x.Name != "grey");
        }
        public RelayCommand<string> ApplySwatchCommand => new RelayCommand<string>(ApplySwatchExecute);

        public IEnumerable<Swatch> Swatches
        {
            get => _swatches;
            set => Set(ref _swatches, value);
        }

        private static void ApplySwatchExecute(string color)
        {
            new PaletteHelper().ReplacePrimaryColor(color);
            Properties.Settings.Default.PrimaryColor = color;
            Properties.Settings.Default.Save();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;

namespace KSManager.Views
{
    /// <summary>
    /// Interaktionslogik für SettingsView.xaml
    /// </summary>
    public partial class SettingsView : Window
    {
        public SettingsView()
        {
            InitializeComponent();
            CloseButton.Click += (s, e) => Close();
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            new PaletteHelper().SetLightDark(true);
            Properties.Settings.Default.Save();
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            new PaletteHelper().SetLightDark(false);
            Properties.Settings.Default.Save();
        }
    }
}

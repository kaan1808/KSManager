using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using KSManager.Helper;
using KSManager.Model;
using Newtonsoft.Json;

namespace KSManager
{
    /// <summary>
    /// Interaktionslogik für MainWindow.s
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            var entry = new PasswordStorageData()
            {
                Id = Guid.NewGuid(),
                Title = "Test",
                LastChanges = DateTime.Now,
                User = 
            }

        }
    }
}

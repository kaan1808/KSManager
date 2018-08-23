using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KSManager.Model;
using Newtonsoft.Json;

namespace KSManager
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var requestUri = "http://localhost:51427/api/users/";
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(requestUri);
                string responseBody = await response.Content.ReadAsStringAsync();

                response.EnsureSuccessStatusCode();
                var user = JsonConvert.DeserializeObject<IEnumerable<User>>(responseBody);

                var httpContent = new StringContent(user.Single(x => x.UserName == "XXXX").ToString(), Encoding.UTF8,
                    "application/json");

                await client.PutAsync(requestUri, httpContent);



            }
        }
    }
}

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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KSManager.Controlls
{
    /// <summary>
    /// Interaktionslogik für BindablePasswordBox.xaml
    /// </summary>
    public partial class BindablePasswordBox : UserControl
    {
        public static readonly DependencyProperty SecurePasswordProperty;

        static BindablePasswordBox()
        {
            SecurePasswordProperty = DependencyProperty.Register("SecurePassword", typeof(string),
                typeof(BindablePasswordBox),
                new FrameworkPropertyMetadata(default(string),
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPasswordChanged));
        }

        private static void OnPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = (BindablePasswordBox)d;
            if (!passwordBox.PasswordBox.Password.Equals((string)e.NewValue))
            {
                passwordBox.PasswordBox.PasswordChanged -= passwordBox.PasswordBox_PasswordChanged;
                passwordBox.PasswordBox.Password = (string)e.NewValue;
                passwordBox.PasswordBox.PasswordChanged += passwordBox.PasswordBox_PasswordChanged;
            }
        }

        public BindablePasswordBox()
        {
            InitializeComponent();
        }

        public string SecurePassword
        {
            get => (string)GetValue(SecurePasswordProperty);
            set { SetValue(SecurePasswordProperty, value); }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            SecurePassword = ((PasswordBox)sender).Password;
        }

        private void UserControl_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox.Focus();
        }
    }
}

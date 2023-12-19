using System.Windows;
using System.Windows.Controls;
using WPF.Interfaces;
using WPF.Utilities;
using WPF.ViewModels;

namespace WPF.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView(LoginViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
            Loaded += Login_Loaded;
        }

        private void Login_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ICloseable cviewModel)
            {
                cviewModel.Close += () =>
                {
                    DialogResult = false;
                    Close();
                };
            }

            if (DataContext is LoginViewModel lvm)
            {
                lvm.LoginSuccessful += () =>
                {
                    DialogResult = true;
                    this.Close();
                };
                lvm.LoginFailed += () =>
                {
                    MessageBox.Show("Invalid credentials", "Error");
                };
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password;
            }
        }
    }
}

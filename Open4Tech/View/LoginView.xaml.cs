using Open4Tech.Model;
using Open4Tech.ViewModel;
using System.Windows;

namespace Open4Tech.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            UserModel.Instance.Password = Password.Password;
        }

        private void Click(object sender, RoutedEventArgs e)
        {
            Email.Text = null;
            Password.Password = null;
        }
    }
}

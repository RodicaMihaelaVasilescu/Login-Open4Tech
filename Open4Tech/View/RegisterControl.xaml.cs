using Open4Tech.Model;
using Open4Tech.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Open4Tech.View
{
    /// <summary>
    /// Interaction logic for RegisterControl.xaml
    /// </summary>
    public partial class RegisterControl : UserControl
    {

        public RegisterControl()
        {
            InitializeComponent();
            DataContext = new RegisterViewModel();
        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            UserModel.Instance.Password = Password.Password;
        }
    }
}

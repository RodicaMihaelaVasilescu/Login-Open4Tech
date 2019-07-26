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
            DataContext = new LoginViewModel(this);
        }
    }
}

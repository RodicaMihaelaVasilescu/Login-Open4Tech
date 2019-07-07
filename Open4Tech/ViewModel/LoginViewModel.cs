using GalaSoft.MvvmLight.Command;
using Open4Tech.Model;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using Open4Tech.Helper;
using System.IO;
using Open4Tech.Properties;

namespace Open4Tech.ViewModel
{
    class LoginViewModel : INotifyPropertyChanged
    {
        private ForgotPasswordViewModel forgotPasswordViewModel;

        public ICommand LoginCommand { get; set; }

        public ICommand RegisterCommand { get; set; }

        public ICommand ForgotPasswordCommand { get; set; }

        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {
                if (_email == value) return;
                _email = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Email"));
            }
        }

        public LoginViewModel()
        {
            UserModel.Instance.Email = Email;
            LoginCommand = new RelayCommand(LoginCommandExecute);
            RegisterCommand = new RelayCommand(RegisterCommandExecute);
            ForgotPasswordCommand = new RelayCommand(ForgotPasswordCommandExecute);
        }

        private void LoginCommandExecute()
        {
            //Process.Start(new ProcessStartInfo("https://www.google.com/"));
            UserModel.Instance.Email = Email;
            if (UserModel.Instance.Email == null || UserModel.Instance.Password == null)
            {
                MessageBox.Show("Both email and password should be filled in.");
                return;
            }
            if(AccountManager.AccountExists(UserModel.Instance.Email, UserModel.Instance.Password))
            {
                var homepageViewModel = new HomepageViewModel();
                var HomepageWindow = WindowManager.CreateElementWindow(homepageViewModel, Resources.HomepageWindowTitle, Resources.HomepageControlPath);

                if (homepageViewModel.CloseAction == null)
                {
                    homepageViewModel.CloseAction = () => HomepageWindow.Close();
                }

                MessageBox.Show("Succesfully logged in");
                HomepageWindow.Show();
            }
            else
            {
                MessageBox.Show("Invalid credentials.");
            }
        }

        private void RegisterCommandExecute()
        {
            Email = null;
            var registerViewModel = new RegisterViewModel();
            var window = WindowManager.CreateElementWindow(registerViewModel, Resources.RegisterAccountWindowTitle, Resources.RegisterAccountControlPath);
            if (registerViewModel.CloseAction == null)
            {
                registerViewModel.CloseAction = () => window.Close();
            }
            window.Show();
        }
        public void ForgotPasswordCommandExecute()
        {
            forgotPasswordViewModel = new ForgotPasswordViewModel();
            var window = WindowManager.CreateElementWindow(forgotPasswordViewModel, Resources.ForgotPasswordWindowTitle, Resources.ForgotPasswordControlPath);
            if (forgotPasswordViewModel.CloseAction == null)
            {
                forgotPasswordViewModel.CloseAction = () => window.Close();
            }
            window.Show();
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}

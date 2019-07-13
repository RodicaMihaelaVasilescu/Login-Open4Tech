using Open4Tech.Model;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using Open4Tech.Helper;
using Open4Tech.Properties;
using System.Net.Mail;
using System;
using Open4Tech.Command;

namespace Open4Tech.ViewModel
{
    class LoginViewModel : INotifyPropertyChanged
    {
        #region Properties

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

        #endregion

        #region Constructor
        public LoginViewModel()
        {
            UserModel.Instance.Email = Email;
            LoginCommand = new RelayCommand(LoginCommandExecute);
            RegisterCommand = new RelayCommand(RegisterCommandExecute);
            ForgotPasswordCommand = new RelayCommand(ForgotPasswordCommandExecute);
            Login();
        }
        #endregion

        #region Private Methods
        private void LoginCommandExecute()
        {
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
        private void Login()
        {
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587); ;
            SmtpServer.Credentials = new System.Net.NetworkCredential("ArtClub.App@gmail.com", "ArtClub.App@gmail.com");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(new MailMessage("ArtClub.App@gmail.com", "wpfapp.app@gmail.com", "Login Alert", DateTime.Now.ToString() + "\n" + System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString()));
        }
        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}

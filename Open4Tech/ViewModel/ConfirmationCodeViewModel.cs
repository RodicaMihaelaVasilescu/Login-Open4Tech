using Open4Tech.Command;
using Open4Tech.Helper;
using Open4Tech.Model;
using Open4Tech.Properties;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Open4Tech.ViewModel
{
    class ConfirmationCodeViewModel : INotifyPropertyChanged
    {
        #region Properties

        public Action CloseAction { get; set; }

        public ICommand ContinueCommand { get; set; }
        public ICommand LoginCommand { get; set; }

        private ResetPasswordViewModel resetPasswordViewModel;

        private Window window;

        private string SentConfirmationCode;

        private string _confirmationCode;

        public string ConfirmationCode
        {
            get { return _confirmationCode; }
            set
            {
                if (_confirmationCode == value) return;
                _confirmationCode = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ConfirmationCode"));
            }
        }

    #endregion

        #region Constructor
        public ConfirmationCodeViewModel(Window window, string code)
        {
            this.window = window;
            SentConfirmationCode = code;
            ContinueCommand = new RelayCommand(ContinueCommandExecute);
            LoginCommand = new RelayCommand(LoginCommandExecute);
        }

        #endregion

        #region Private Methods
        private void LoginCommandExecute()
        {
            var loginViewModel = new LoginViewModel(window);
            WindowManager.ChangeWindowContent(window, loginViewModel, Resources.LoginWindowTitle, Resources.LoginControlPath);
            if (loginViewModel.CloseAction == null)
            {
                loginViewModel.CloseAction = () => window.Close();
            }
        }

        private void ContinueCommandExecute()
        {

            if (ConfirmationCode == SentConfirmationCode)
            {
                if (!AccountManager.EmailExists(UserModel.Instance.Email))
                {
                    AccountManager.RegisterAccount(UserModel.Instance.Email, UserModel.Instance.Password);
                    var loginViewModel = new LoginViewModel(window);
                    WindowManager.ChangeWindowContent(window, loginViewModel, Resources.LoginWindowTitle, Resources.LoginControlPath);
                    if (loginViewModel.CloseAction == null)
                    {
                        loginViewModel.CloseAction = () => window.Close();
                    }
                    return;
                }

                resetPasswordViewModel = new ResetPasswordViewModel(window);
                WindowManager.ChangeWindowContent(window, resetPasswordViewModel, Resources.ResetPasswordWindowTitle, Resources.ResetPasswordControlPath);
                if (resetPasswordViewModel.CloseAction == null)
                {
                    resetPasswordViewModel.CloseAction = () => window.Close();
                }
                //CloseAction?.Invoke();
            }
            else
            {
                MessageBox.Show("Incorrect code. Try again!");
            }
        }
        #endregion
    
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}

using GalaSoft.MvvmLight.Command;
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
        public Action CloseAction { get; set; }

        public ICommand ContinueCommand { get; set; }

        private ResetPasswordViewModel resetPasswordViewModel;

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

        public ConfirmationCodeViewModel(string code)
        {
            SentConfirmationCode = code;
            ContinueCommand = new RelayCommand(ContinueCommandExecute);
        }

        private void ContinueCommandExecute()
        {

            if (ConfirmationCode == SentConfirmationCode)
            {
                if (!AccountManager.EmailExists(UserModel.Instance.Email))
                {
                    AccountManager.RegisterAccount(UserModel.Instance.Email, UserModel.Instance.Password);
                    CloseAction?.Invoke();
                    return;
                }

                resetPasswordViewModel = new ResetPasswordViewModel();
                var window = WindowManager.CreateElementWindow(resetPasswordViewModel, Resources.ResetPasswordWindowTitle, Resources.ResetPasswordControlPath);
                if (resetPasswordViewModel.CloseAction == null)
                {
                    resetPasswordViewModel.CloseAction = () => window.Close();
                }
                window.Show();
                CloseAction?.Invoke();
            }
            else
            {
                MessageBox.Show("Incorrect code. Try again!");
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}

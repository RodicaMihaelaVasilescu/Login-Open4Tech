using Open4Tech.Command;
using Open4Tech.Helper;
using Open4Tech.Model;
using Open4Tech.Properties;
using Open4Tech.ViewModel.BaseClass;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Open4Tech.ViewModel
{
    class ForgotPasswordViewModel : EmailNotification, INotifyPropertyChanged
    {
        #region Properties

        public Action CloseAction { get; set; }

        public ICommand SendCommand { get; set; }

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
        public ForgotPasswordViewModel()
        {
            SendCommand = new RelayCommand(SendCommandExecute);
        }

    #endregion

        #region Private Methods
        private void SendCommandExecute()
        {
            UserModel.Instance.Email = Email;
            if(Email == null)
            {
                MessageBox.Show(string.Format("The email must be filled in.", Email), "Warning");
                return;
            }
            if (!AccountManager.EmailExists(Email))
            {
                MessageBox.Show( string.Format("The email {0} does not match any existing account. You must create an account.", Email), "Warning");
                return;
            }
            if (SendEmailCode(Resources.RegisterAccountEmailSubject, Resources.GenericEmailContent))
            {
                CloseAction?.Invoke();
            }
        }
        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}

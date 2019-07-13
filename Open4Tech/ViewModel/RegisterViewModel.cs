using Open4Tech.Command;
using Open4Tech.Helper;
using Open4Tech.Model;
using Open4Tech.Properties;
using Open4Tech.Validator;
using Open4Tech.ViewModel.BaseClass;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Open4Tech.ViewModel
{
    class RegisterViewModel : EmailNotification, INotifyPropertyChanged
    {
        #region Properties
        private string _email;

        public ICommand RegisterCommand { get; set; }

        public Action CloseAction { get; set; }

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
        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand(RegisterCommandExecute);
        }
    #endregion

        #region Private Methods
        private void RegisterCommandExecute()
        {
            UserModel.Instance.Email = Email;
            if(UserModel.Instance.Email == null || UserModel.Instance.Password == null)
            {
                MessageBox.Show("Both email and password should be filled in.");
                return;
            }
            if (AccountManager.EmailExists(Email))
            {
                MessageBox.Show(string.Format("The email {0} already exists", Email));
                return;
            }
            var Validator = new PersonalAccountValidator().ValidatePassword(UserModel.Instance.Password);
            if(!Validator.IsValid)
            {
                MessageBox.Show(Validator.ValidationMessage);
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

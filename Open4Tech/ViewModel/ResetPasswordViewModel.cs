using GalaSoft.MvvmLight.Command;
using Open4Tech.Helper;
using Open4Tech.Model;
using Open4Tech.Validator;
using System;
using System.Windows;
using System.Windows.Input;

namespace Open4Tech.ViewModel
{
    class ResetPasswordViewModel
    {
        public Action CloseAction { get; set; }

        public ICommand ResetCommand { get; set; }

        public ResetPasswordViewModel()
        {
            ResetCommand = new RelayCommand(ResetCommandExecute);
        }

        private void ResetCommandExecute()
        {
            if(UserModel.Instance.Password == null)
            {
                return;
            }
            var Validator = new PersonalAccountValidator().ValidatePassword(UserModel.Instance.Password);

            if (!Validator.IsValid)
            {
                MessageBox.Show(Validator.ValidationMessage);
            }
            else
            {
                AccountManager.ChangePassword(UserModel.Instance.Email, UserModel.Instance.Password);
                CloseAction?.Invoke();
            }
        }
    }
}

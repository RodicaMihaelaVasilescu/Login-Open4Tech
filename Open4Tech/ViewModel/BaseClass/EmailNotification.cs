using Open4Tech.Helper;
using Open4Tech.Model;
using Open4Tech.Properties;
using Open4Tech.Validator;
using System;
using System.Windows;

namespace Open4Tech.ViewModel.BaseClass
{
    public class EmailNotification
    {
        public bool SendEmailCode(string emailSubject, string emailContent)
        {
            var confirmationCode = new Random().Next(100000, 999999).ToString();
            emailContent += confirmationCode;
            var Validator = new PersonalAccountValidator().ValidateEmail(UserModel.Instance.Email, emailSubject, emailContent);
            if (UserModel.Instance.Email == null || !Validator.IsValid)
            {
                MessageBox.Show(Validator.ValidationMessage);
                return false;
            }
            var confirmationCodeViewModel = new ConfirmationCodeViewModel(confirmationCode);
            var confirmationCodeWindow = WindowManager.CreateElementWindow(confirmationCodeViewModel, Resources.ConfirmationCodeWindowTitle, Resources.ConfirmationCodeControlPath);

            if (confirmationCodeViewModel.CloseAction == null)
            {
                confirmationCodeViewModel.CloseAction = () => confirmationCodeWindow.Close();
            }
            confirmationCodeWindow.Show();
            return true;
        }
    }
}

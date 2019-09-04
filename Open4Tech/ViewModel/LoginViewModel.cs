﻿using Open4Tech.Model;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using Open4Tech.Helper;
using Open4Tech.Properties;
using System.Net.Mail;
using System;
using Open4Tech.Command;
using System.IO;
using System.Threading.Tasks;

namespace Open4Tech.ViewModel
{
    class LoginViewModel : INotifyPropertyChanged
    {
        #region Properties

        private ForgotPasswordViewModel forgotPasswordViewModel;

        private Window window;

        public ICommand LoginCommand { get; set; }

        public ICommand RegisterCommand { get; set; }

        public ICommand ForgotPasswordCommand { get; set; }

        public Action CloseAction { get; set; }

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
        public LoginViewModel(Window window)
        {
            this.window = window;
            UserModel.Instance.Email = Email;
            LoginCommand = new RelayCommand(LoginCommandExecute);
            RegisterCommand = new RelayCommand(RegisterCommandExecute);
            ForgotPasswordCommand = new RelayCommand(ForgotPasswordCommandExecute);
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
            if (AccountManager.AccountExists(UserModel.Instance.Email, UserModel.Instance.Password))
            {
                var homepageViewModel = new HomepageViewModel(window);
                WindowManager.ChangeWindowContent(window, homepageViewModel, Resources.HomepageWindowTitle, Resources.HomepageControlPath);

                if (homepageViewModel.CloseAction == null)
                {
                    homepageViewModel.CloseAction = () => window.Close();
                }
            }
            else
            {
                MessageBox.Show("Invalid credentials.");
            }
        }

        private void RegisterCommandExecute()
        {
            Email = null;
            var registerViewModel = new RegisterViewModel(window);
            WindowManager.ChangeWindowContent(window, registerViewModel, Resources.RegisterAccountWindowTitle, Resources.RegisterAccountControlPath);
            if (registerViewModel.CloseAction == null)
            {
                registerViewModel.CloseAction = () => window.Close();
            }
        }
        public void ForgotPasswordCommandExecute()
        {
            forgotPasswordViewModel = new ForgotPasswordViewModel(window);
            WindowManager.ChangeWindowContent(window, forgotPasswordViewModel, Resources.ForgotPasswordWindowTitle, Resources.ForgotPasswordControlPath);
            if (forgotPasswordViewModel.CloseAction == null)
            {
                forgotPasswordViewModel.CloseAction = () => window.Close();
            }
            window.Show();
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}

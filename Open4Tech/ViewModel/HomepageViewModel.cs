using GalaSoft.MvvmLight.Command;
using Open4Tech.Model;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Input;

namespace Open4Tech.ViewModel
{
    class HomepageViewModel : INotifyPropertyChanged
    {
        public Action CloseAction { get; set; }

        public ICommand LogoutCommand { get; set; }

        private string _welcomeText;

        public string WelcomeText
        {
            get { return _welcomeText; }
            set
            {
                if (_welcomeText == value) return;
                _welcomeText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WelcomeText"));
            }
        }

        public HomepageViewModel()
        {
            LogoutCommand = new RelayCommand(LogoutCommandExecute);
            WelcomeText = UserModel.Instance.Email;
        }

        private void LogoutCommandExecute()
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to log out?", "Log out", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                CloseAction?.Invoke();
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}

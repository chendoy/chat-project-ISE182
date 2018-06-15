using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ISE182_PROJECT_G8.presentationLayer
{
    public class LoginObserver : INotifyPropertyChanged
    {
        public LoginObserver()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string username;
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }

        private string groupID;
        public string GroupID
        {
            get
            {
                return groupID;
            }
            set
            {
                groupID = value;
                OnPropertyChanged("GroupID");
            }
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
            set
            {
                errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }

        private bool rememberMe;
        public bool RememberMe
        {
            get
            {
                return rememberMe;
            }
            set
            {
                rememberMe = value;
                OnPropertyChanged("RememberMe");
            }
        }

        private string password = "";
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        private bool passwordEmpty = true;
        public bool PasswordEmpty
        {
            get
            {
                return passwordEmpty;
            }
            set
            {
                passwordEmpty = value;
                OnPropertyChanged("PasswordEmpty");
            }
        }
    }
}

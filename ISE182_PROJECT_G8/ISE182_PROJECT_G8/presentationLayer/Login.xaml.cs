using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using ISE182_PROJECT_G8.logicLayer;
using System.Text.RegularExpressions;
using System.Windows.Input;
using ISE182_PROJECT_G8.persistantLayer;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace ISE182_PROJECT_G8.presentationLayer
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        private Chatroom chatroom;
        private LoginObserver loginObserver;
        private const string SALT = "1337";

        public Login(Chatroom chatroom)
        {
            this.chatroom = chatroom;
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("en-US"); //used to display .net error messages in english (my windows is in hebrew)//
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            Logger.Instance.Info("log-in window started successfully");
            loginObserver = new LoginObserver();
            this.DataContext = loginObserver;
            LoadRememberedUser();

        }
        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            string password = loginObserver.Password;
            if (password != null)
            {
                String username = loginObserver.Username;
                int groupID = int.Parse(loginObserver.GroupID);

                if (chatroom.Login(username, groupID, password))
                {
                    //*
                    if (loginObserver.RememberMe) //remember me was ticked - save the user//
                    {
                        SaveRememberedUser(new User(username, groupID, "", -1));

                    }
                    else //wasn't ticked - save a "dummy" user
                    {
                        SaveRememberedUser(new User("", -1, "", -1));
                    }
                    Logger.Instance.Info("User: " + username + " logged-in successfully, starting chat window");
                    chat_window chat_window = new chat_window(chatroom);
                    chat_window.Show();
                    this.Close();
                }
                else
                {
                    loginObserver.ErrorMessage = "User not found, Please try again or register";
                    Logger.Instance.Error("User not found when trying to log-in");
                }

            }
        }
        
        private void Remember_Me_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Register_Button_Click(object sender, RoutedEventArgs e) { 

            string password = loginObserver.Password;
            if (password != null) {

                if (chatroom.Register(loginObserver.Username, Int32.Parse(loginObserver.GroupID),loginObserver.Password))
                {
                    Logger.Instance.Info("User registered successfully");
                    loginObserver.ErrorMessage = "User registered successfully!";
                }
            
            else
            {
                Logger.Instance.Error("Incorrect input was entered while trying to register");
                loginObserver.ErrorMessage = "Incorrect input...please try again";
            }
}

        }

        private void LoadRememberedUser()
        {
            UserPL user =  chatroom.GetRememberedUser();
            if (user != null)
            {
                loginObserver.Username = user.GetNickname();
                loginObserver.GroupID = user.GetGroupID().ToString();
                if (loginObserver.GroupID == "0")
                {
                    loginObserver.Username="";
                    loginObserver.GroupID = "";
                }
                else
                {
                    loginObserver.RememberMe = true;
                }
            }
        }

        private void SaveRememberedUser(User userToBeRemembered)
        {
            chatroom.saveRememberedUser(userToBeRemembered); //delegates to BL layer
        }


        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            Logger.Instance.Info("Group if textBox content validated successfully");
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox pb = sender as PasswordBox;
            if (Hashing.isValid(pb.Password))
            {
                loginObserver.Password = Hashing.GetHashString(pb.Password + SALT);  
            }
            else
                loginObserver.Password = null;
            
        }
    }


}

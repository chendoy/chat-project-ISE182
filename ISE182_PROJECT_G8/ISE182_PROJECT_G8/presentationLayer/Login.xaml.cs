using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using ISE182_PROJECT_G8.logicLayer;

namespace ISE182_PROJECT_G8.presentationLayer
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        private Chatroom chatroom;
        private LoginObserver loginObserver;

        public Login(Chatroom chatroom)
        {
            this.chatroom = chatroom;
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("en-US"); //used to display .net errors messages in english (my windows is in hebrew)//
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;

            loginObserver = new LoginObserver();
            this.DataContext = loginObserver;

            LoadRememberedUser();

        }
        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            string username = loginObserver.Username;
            int groupID = Int32.Parse(loginObserver.GroupID);
            if ((bool)chatroom.Login(username, groupID)) {

                if (loginObserver.RememberMe) //"remember me was ticked - save the user//
                {
                    chatroom.getSaver().SaveRememberMe(new User(username, groupID));

                }
                else //wasn't ticked - save a "dummy" user
                {
                    chatroom.getSaver().SaveRememberMe(new User("", 0));
                }

                chat_window chat_window = new chat_window(chatroom);
                chat_window.Show();
                this.Close();

            }
            else
            {
                //message_notify.Visibility = Visibility.Visible;
                loginObserver.ErrorMessage = "User not found";
            }
        }

        private void Remember_Me_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if ((bool)chatroom.Register(loginObserver.Username, Int32.Parse(loginObserver.GroupID)))
                {
                   //message_notify.Visibility = Visibility.Visible;
                   loginObserver.ErrorMessage = "User registered successfully!";
                }
            }
            catch
            {
                //message_notify.Visibility = Visibility.Visible;
                loginObserver.ErrorMessage = "Incorrect input...please try again";
            }
        }

        private void LoadRememberedUser()
        {
            User user =  chatroom.getRememberedUser();
            if (user != null)
            {
                loginObserver.Username = user.getNickname();
                loginObserver.GroupID = user.getGroupID().ToString();
                if (loginObserver.GroupID == "0")
                {
                    loginObserver.Username = "";
                    loginObserver.GroupID = "";
                }
            }
        }

    }


}

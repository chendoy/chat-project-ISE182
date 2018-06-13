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
            try
            {
                //Sundy code
               string username = loginObserver.Username;
               int groupID = Int32.Parse(loginObserver.GroupID);
               //Protect us from pass the hash attack
               if(checkuser(username, groupID))
                {
                    //Need checn to add thw password field
                    //String hash = logicLayer.Hashing(password);
                    //String where = "Nickname =" + username + " AND Group_Id = " + groupID+"NPK = "+hash;
                    //dataAccessLayer.Query Cuser = new dataAccessLayer.Query("Nickname,Group_ID", "Users", where);
                    //ObservableCollection<User> objects = Cuser.Excute<User>()
                    //if (objects != null) { Go to * }
                }

                if ((bool)chatroom.Login(username, groupID)) {
                    //*
                    if (loginObserver.RememberMe) //remember me was ticked - save the user//
                    {
                        SaveRememberedUser(new User(username, groupID));

                    }
                    else //wasn't ticked - save a "dummy" user
                    {
                        SaveRememberedUser(new User("", -1));
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
            catch
            {
                Logger.Instance.Error("User not found when trying to log-in");
                loginObserver.ErrorMessage = "User not found, Please try again or register";
            }
        }
        //Sundy code
        private Boolean checkuser(String name,int id)
        {
            String where = "Nickname =" + name + " AND Group_Id = " + id;
            dataAccessLayer.Query_old Cuser = new dataAccessLayer.Query_old("Nickname,Group_ID", "Users",where);
            //Lets say naor return observablecollection from query.execute
            //ObservableCollection<User> objects = Cuser.Excute<User>()
            //if (objects != null) { return true; }
            
            return false;
        }
        private void Remember_Me_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                //Sundy code
                String name = loginObserver.Username;
                int gid = Int32.Parse(loginObserver.GroupID);
                //If user exsist
                if (checkuser(name, gid))
                {
                    loginObserver.ErrorMessage = "User already registered";
                }
                else
                {
                    string pass = "";
                    string table = "Users";
                    String col = "Group_Id,Nickname,Password";
                    String values = gid + "," + name + "," + pass;
                    dataAccessLayer.Insert command = new dataAccessLayer.Insert(table, col, values);
                    //return command.Excute();
                }
                if ((bool)chatroom.Register(loginObserver.Username, Int32.Parse(loginObserver.GroupID)))
                {
                    Logger.Instance.Info("User registered successfully");
                    loginObserver.ErrorMessage = "User registered successfully!";
                }
            }
            catch
            {
                Logger.Instance.Error("Incorrect input was entered while trying to register");
                loginObserver.ErrorMessage = "Incorrect input...please try again";
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

    }


}

﻿using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using ISE182_PROJECT_G8.logicLayer;
using System.Text.RegularExpressions;
using System.Windows.Input;
using ISE182_PROJECT_G8.persistantLayer;

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
               string username = loginObserver.Username;
               int groupID = Int32.Parse(loginObserver.GroupID);
                if((bool)chatroom.Login(username, groupID)) {

                    if (loginObserver.RememberMe) //remember me was ticked - save the user//
                    {
                        chatroom.getSaver().SaveRememberMe(new User(username, groupID));

                    }
                    else //wasn't ticked - save a "dummy" user
                    {
                        chatroom.getSaver().SaveRememberMe(new User("", -1));
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

        private void Remember_Me_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
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
            User user =  chatroom.getRememberedUser();
            if (user != null)
            {
                loginObserver.Username = user.getNickname();
                loginObserver.GroupID = user.getGroupID().ToString();
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

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            Logger.Instance.Info("Group if textBox content validated successfully");
        }

    }


}

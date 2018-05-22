﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using ISE182_PROJECT_G8.logicLayer;
using ISE182_PROJECT_G8.persistantLayer;

namespace ISE182_PROJECT_G8.presentationLayer
{
    /// <summary>
    /// Interaction logic for chat_window.xaml
    /// </summary>
    public partial class chat_window : Window
    {

        private Chatroom chatroom;
        private ChatroomObserver chatroomObserver;
        DispatcherTimer dispatcherTimer = new DispatcherTimer();  // member 

        public chat_window(Chatroom chatroom)
        {
            this.chatroom = chatroom;
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            chatroomObserver = new ChatroomObserver();
            this.DataContext = chatroomObserver;
            send_button.IsDefault = true;

            
            Logger.Instance.Info("chat window started successfully");
            UpadateMessageList();
            //DispatcherTimer init//
            dispatcherTimer.Tick += DispatcherTimer_Tick;   
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 60);
            dispatcherTimer.Start();


        }

        private void UpadateMessageList()
        {
            if (chatroom.RetreiveMessages())
            {
                chatroomObserver.Messages = chatroom.getMessageList();
                Sorter(null, null);
                Filter(null, null);
            }

        }

        private void Send_Button_Click(object sender, RoutedEventArgs e)
        {
            string content = chatroomObserver.Message;
            if ((bool)chatroom.Send(content))
            {
                Logger.Instance.Info("Message was sent successfully");
                chatroomObserver.Message = "";
                UpadateMessageList();
            }
        }
        private void Logout_Button_Click(object sender, RoutedEventArgs e)
        {
            chatroom.LogOut();
            Logger.Instance.Info("log-out button pressed");
            Login login_window = new Login(chatroom);
            login_window.Show();
            this.Close();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Send_Button_Click(sender, e);
            }
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            Logger.Instance.Info("DispatcherTimer ticked");
            UpadateMessageList();
        }

        private void Sorter(object sender, EventArgs e)
        {
            Sorter sorter = null;
            ObservableCollection<Message> list = chatroomObserver.Messages;
            ObservableCollection<Message> nlist = null;
            if (chatroomObserver.ByAll)
            {
                //Sort by all radio button clicked
                if (chatroomObserver.AscType)
                {
                    sorter = new SortByAll(list, true);  
                }
                else
                {
                    sorter = new SortByAll(list, false);
                }

            }
            //Srot by name radio button clicked
            else if(chatroomObserver.ByName)
            {
                if (chatroomObserver.AscType)
                {
                    sorter = new SortByNickname(list, true);
                }
                else
                {
                    sorter = new SortByNickname(list, false);
                }
            }
            // Else sort by time
            else if(chatroomObserver.ByTime)
            {
                if (chatroomObserver.AscType)
                {
                    sorter = new SortByTime(list, true);
                }
                else
                {
                    sorter = new SortByTime(list, false);
                }
            }

            nlist = sorter.Sort();
            chatroomObserver.Messages = nlist;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //chatroomObserver.GidEnable =true;
        }
        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            //if(chatroomObserver.GidEnable)
            //{
            //    chatroomObserver.NicknameEnable = true;
            //}
            //else
            //{
            //    chatroomObserver.NicknameEnable = false;
            //}
            
        }

        private void Filter(object sender, TextChangedEventArgs e)
        {
            ObservableCollection<Message> list = chatroomObserver.Messages;
            ObservableCollection<Message> nlist = null;
            Filter filter = null;

            if (chatroomObserver.GidEnable)
            {
                String gid = chatroomObserver.GroupID;
                if (!String.IsNullOrWhiteSpace(gid))
                {
                    int gidnum = 0;
                    int.TryParse(gid, out gidnum);

                    String name = chatroomObserver.Name;
                    if (chatroomObserver.NicknameEnable & !String.IsNullOrWhiteSpace(name))
                    {
                        filter = new FilterByNickname(list, name, gidnum);
                    }
                    else
                    {
                        filter = new FilterByGroupID(list, gidnum);
                    }
                    nlist = filter.filter();
                    chatroomObserver.Messages = nlist;
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

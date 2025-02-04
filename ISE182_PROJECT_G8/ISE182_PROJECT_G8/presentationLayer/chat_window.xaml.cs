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
        //private ListBoxItem selectedItemInListBox;

        private const string EDITABLE = "Images/edit.jpg";
        private const string NOTEDITABLE = "Images/edit_disabled.jpeg";
        private const string CANCELEDIT = "Images/cancel_edit.jpg";

        public chat_window(Chatroom chatroom)
        {
            chatroomObserver = new ChatroomObserver();
            this.DataContext = chatroomObserver;
            this.chatroom = chatroom;
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            send_button.IsDefault = true;


            Logger.Instance.Info("chat window started successfully");
            UpdateMessageList();
            //DispatcherTimer init//
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 2);
            dispatcherTimer.Start();


        }

        private void UpdateMessageList()
        {
            //Filter(null, null);
            if (chatroom.RetreiveMessages(out IList<Message> addMsgs))
            {
                chatroomObserver.Messages.Clear();

            }

            foreach (Message msg in addMsgs)
            {
                chatroomObserver.Messages.Add(msg);
            }

            if (addMsgs.Count > 0)
            {
                Sorter(null, null);
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (((ListBox)sender).SelectedItem != null)
            {
                var data = ((ListBox)sender).SelectedItem as Message;
                if (chatroom.GetLoggedInUser().GetNickname().Equals(data.getUserName()) & chatroom.GetLoggedInUser().GetGroupID().Equals(data.getGroupId()))
                    chatroomObserver.MessageToEdit = data;
                else
                    chatroomObserver.MessageToEdit = null;

            }

            UpdateEditButtonImage();
        }

        private void Send_Button_Click(object sender, RoutedEventArgs e)
        {
            string content = chatroomObserver.Message;
            if (chatroomObserver.EditMode) //we're on edit mode
            {
                Guid guidOfEdit = chatroomObserver.MessageToEdit.getGuid();

                if (chatroom.UpdateMessage(guidOfEdit, content)) //sundy needs to complete
                {
                    Logger.Instance.Info("Message was edited successfully");
                    chatroomObserver.Message = "";
                    chatroomObserver.MessageToEdit = null;
                    UpdateMessageList();
                }
                else
                {
                    chatroomObserver.Message = "";
                }

                chatroomObserver.EditMode = false;
                UpdateEditButtonImage();
            }

            else //not on edit mode
            {
                if (chatroom.Send(content))
                {
                    Logger.Instance.Info("Message was sent successfully");
                    chatroomObserver.Message = "";
                    UpdateMessageList();
                }
                else
                {
                    chatroomObserver.Message = "";
                }
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

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (chatroomObserver.EditMode == false)
            {
                if (chatroomObserver.MessageToEdit != null) //there is message to edit
                {
                    chatroomObserver.EditMode = true; //then enable edit mode
                    chatroomObserver.Message = chatroomObserver.MessageToEdit.getContent();
                }
            }
            else
            {
                chatroomObserver.EditMode = false;
                chatroomObserver.Message = "";
            }

            UpdateEditButtonImage();
        }

        private void UpdateEditButtonImage()
        {
            if (chatroomObserver.MessageToEdit != null)
            {
                if (chatroomObserver.EditMode)
                {
                    chatroomObserver.EditImage = CANCELEDIT;
                }
                else
                {
                    chatroomObserver.EditImage = EDITABLE;
                }
            }
            else
            {
                chatroomObserver.EditImage = NOTEDITABLE;
            }
            

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
            UpdateMessageList();
        }

        private void Sorter(object sender, EventArgs e)
        {
            
            ObservableCollection<Message> list = chatroomObserver.Messages;
            ObservableCollection<Message> nlist = null;
            Sorter sorter = new SortByTime(list, true);
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
            else if (chatroomObserver.ByName)
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
            else if (chatroomObserver.ByTime)
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
            //chatroomObserver.Messages = nlist;
            chatroomObserver.Messages.Clear();
            foreach (Message msg in nlist)
            {
                chatroomObserver.Messages.Add(msg);
            }
        }


        private void Filter(object sender, TextChangedEventArgs e)
        {
            String gid = chatroomObserver.GroupID;
            if (!String.IsNullOrWhiteSpace(gid))
            {
                int gidnum = 0;
                int.TryParse(gid, out gidnum);
                chatroom.SetGroupFilter(gidnum);

                String name = chatroomObserver.Name;
                if (chatroomObserver.NicknameEnable & !String.IsNullOrWhiteSpace(name))
                {
                    chatroom.SetNicknameFilter(name);
                }
                else
                {
                    chatroom.SetNicknameFilter(null);
                }
            }
            else
            {
                chatroom.SetGroupFilter(null);
                chatroom.SetNicknameFilter(null);
            }

            UpdateMessageList();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            Logger.Instance.Info("Group if textBox content validated successfully");
        }

        private void TextBox_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!((TextBox)sender).IsEnabled)
            {
                chatroomObserver.Name = "";
            }

            Filter(sender, null);
        }

        private void TextBox_IsEnabledChanged_1(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!((TextBox)sender).IsEnabled)
            {
                chatroomObserver.GroupID = "";
                Filter(sender, null);
            }
        }

        private void CheckBox_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!((CheckBox)sender).IsEnabled)
            {
                chatroomObserver.NicknameEnable = false;
            }
        }
    }
}

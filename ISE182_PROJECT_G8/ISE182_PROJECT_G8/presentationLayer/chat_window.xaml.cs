using System;
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
            chatroom.RetreiveMessages();
            //Add sort filter here for testing
            UpdateMessageList();
            Logger.Instance.Info("chat window started successfully");

            //DispatcherTimer init//
            dispatcherTimer.Tick += dispatcherTimer_Tick;   
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 2);  
            //dispatcherTimer.Start();  


        }

        private void Send_Button_Click(object sender, RoutedEventArgs e)
        {
            string content = chatroomObserver.Message;
            if ((bool)chatroom.Send(content))
            {
                Logger.Instance.Info("Message was sent successfully");
                chatroomObserver.Message = "";
                UpdateMessageList();
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

        public void UpdateMessageList()
        {
            Logger.Instance.Info("Message list was updated successfully");
            List<Message> list = chatroom.getMessageList();
            chatroomObserver.Messages.Clear();
            foreach (Message message in list)
            {
                chatroomObserver.Messages.Add(message);
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Send_Button_Click(sender, e);
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)

        {
            Logger.Instance.Info("DispatcherTimer ticked");
            chatroom.RetreiveMessages();
            UpdateMessageList();

        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {

        }

        private void Filter(object sender, TextChangedEventArgs e)
        {
            ObservableCollection<Message> list = chatroomObserver.Messages;
            if (chatroomObserver.GidEnable)
            {
                String gid = chatroomObserver.GroupID;
                int gidnum = int.Parse(gid);
                if(chatroomObserver.NicknameEnable)
                {
                    String name = chatroomObserver.Name;
                    Filter filter = new FilterByNickname(list, name, gidnum);
                    ObservableCollection<Message> nlist = filter.filter();
                }
                else
                {
                    Filter filter = new FilterByGroupID(list,gidnum);
                    ObservableCollection<Message> nlist = filter.filter();
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

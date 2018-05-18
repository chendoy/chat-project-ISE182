using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            //send_textbox.Focus();
            chatroomObserver = new ChatroomObserver();
            this.DataContext = chatroomObserver;
            send_button.IsDefault = true;
            //chatroom.RetreiveMessages(); // Need to do timer
            UpdateMessageList();

            dispatcherTimer.Tick += dispatcherTimer_Tick;   
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 2);  
            //dispatcherTimer.Start();  


        }

        private void Send_Button_Click(object sender, RoutedEventArgs e)
        {
            string content = chatroomObserver.Message;
            if ((bool)chatroom.Send(content))
            {
                chatroomObserver.Message = "";
                UpdateMessageList();
            }
        }
        private void Logout_Button_Click(object sender, RoutedEventArgs e)
        {
            chatroom.LogOut();
            Login login_window = new Login(chatroom);
            login_window.Show();
            this.Close();
        }

        public void UpdateMessageList()
        {
            List<Message> list = chatroom.getMessageList();
            chatroomObserver.Messages.Clear();
            foreach (Message message in list)
            {
                chatroomObserver.Messages.Add(message.ToString());
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
            chatroom.RetreiveMessages();
            UpdateMessageList();

        }
    }
}

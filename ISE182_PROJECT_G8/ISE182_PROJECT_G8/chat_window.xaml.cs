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
using ISE182_PROJECT_G8.logicLayer;

namespace ISE182_PROJECT_G8
{
    /// <summary>
    /// Interaction logic for chat_window.xaml
    /// </summary>
    public partial class chat_window : Window
    {

        public Chatroom chatroom;
        public chat_window(Chatroom chatroom)
        {

            this.chatroom = chatroom;
            InitializeComponent();

            this.ResizeMode = ResizeMode.NoResize;
            send_textbox.Focus();
            Uri iconUri = new Uri("StoredData\\chat_icon.ico", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource =
                 new BitmapImage(new Uri("StoredData\\chat_background.jpg", UriKind.Relative));
            this.Background = myBrush;
            chat_panel.ItemsSource = chatroom.getMessageList();
            
            var brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri("StoredData\\send_btn.jpg", UriKind.Relative));
            send_button.Background = brush;

            send_button.IsDefault = true;

            var brush2 = new ImageBrush();
            brush2.ImageSource = new BitmapImage(new Uri("StoredData\\logout_btn.jpg", UriKind.Relative));
            logout_button.Background = brush2;


        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            string content = send_textbox.Text;
            if ((bool)chatroom.Send(content))
            {
                send_textbox.Clear();
            }

        }
        private void logout_button_Click(object sender, RoutedEventArgs e)
        {
            chatroom.LogOut();
            Login login_window = new Login(chatroom);
            login_window.Show();
            this.Close();

        }


        private void chat_panel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

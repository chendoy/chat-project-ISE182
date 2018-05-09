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
        public chat_window()
        {
            InitializeComponent();

            Uri iconUri = new Uri("StoredData\\chat_icon.ico", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource =
                 new BitmapImage(new Uri("StoredData\\chat_background.jpg", UriKind.Relative));
            this.Background = myBrush;
            chat_panel.ItemsSource = Chatroom.Instance.getMessageList();
            send_button.IsDefault = true;

        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            string content = send_textbox.Text;
            if ((bool)Chat_EventHandler.Send(content))
            {

            }

        }


    }
}

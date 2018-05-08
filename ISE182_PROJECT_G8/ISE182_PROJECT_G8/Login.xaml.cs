using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using ISE182_PROJECT_G8.logicLayer;

namespace ISE182_PROJECT_G8
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("en-US"); //used to display .net errors messages in english (my windows is in hebrew)//


            InitializeComponent();

            username_textbox.Text = loadRememberedUser().getNickname();
            groupID_textbox.Text = loadRememberedUser().getGroupID().ToString();
            if (groupID_textbox.Text == "0") groupID_textbox.Text = "";

   
            Uri iconUri = new Uri("StoredData\\chat_icon.ico", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            var brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri("StoredData\\login_btn.jpg", UriKind.Relative));
            login_btn.Background = brush;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = username_textbox.Text;
            int groupID = Int32.Parse(groupID_textbox.Text);
            if ((bool)Chat_EventHandler.Login(username, groupID)) {

                if (remember_checkbox.IsChecked.GetValueOrDefault()) //"remember me was ticked - save the user//
                {
                    Chatroom.Instance.getSaver().SaveRememberMe(new User(username, groupID));

                }
                else //wasn't ticked - save a "dummy" user
                {
                    Chatroom.Instance.getSaver().SaveRememberMe(new User("", -1));
                }

                chat_window chat_window = new chat_window();
                chat_window.Show();
                this.Close();

            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            /*string username = username_textbox.Text;
            int groupID = Int32.Parse(groupID_textbox.Text);*/

            try
            {
                if ((bool)Chat_EventHandler.Register(username_textbox.Text, Int32.Parse(groupID_textbox.Text)))
                {
                    incorrect_register.Visibility = Visibility.Hidden;
                    registered_status.Visibility = Visibility.Visible;
                }
            }
            catch
            {
                registered_status.Visibility = Visibility.Hidden;
                incorrect_register.Visibility = Visibility.Visible;
            }
        }

        private User loadRememberedUser()
        {
            return Chatroom.Instance.getRememberedUser();
        }
    }

 
    }

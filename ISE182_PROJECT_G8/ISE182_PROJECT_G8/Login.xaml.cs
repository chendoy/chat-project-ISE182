using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows;

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
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource =
                 new BitmapImage(new Uri("StoredData\\login_wallpaper.jpg", UriKind.Relative));
            this.Background = myBrush;

            Uri iconUri = new Uri("StoredData\\chat_icon.ico", UriKind.RelativeOrAbsolute);

           
            this.Icon = BitmapFrame.Create(iconUri);

            var brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri("StoredData\\login_btn.jpg", UriKind.Relative));
             login_btn.Background = brush;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
   
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.presentationLayer
{
    class GUI
    {
        private static void displayGui()
        {
            Console.WriteLine("Welcome to ISE_182 chat, please choose one of the options:");
            Console.WriteLine("a. Rigistration");
            Console.WriteLine("b. Login/Logout");
            Console.WriteLine("c. Retrieve last 10 messages from server");
            Console.WriteLine("d. Display last 20 retrieved messages");
            Console.WriteLine("e. Display all retrieved messages");
            Console.WriteLine("f. Write (and send) a new message (max. Length 150 characters)");
            Console.WriteLine("g. Exit");
            Console.WriteLine();
            Console.Write("Your Choice: ");
            char choice = (char)Console.Read();
        }
        public static void Main(String[]args)
        {
            displayGui();
        }
    }
}

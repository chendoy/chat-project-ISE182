using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISE182_PROJECT_G8.logicLayer;

namespace ISE182_PROJECT_G8.presentationLayer
{
    class GUI
    {
        private static void displayGui()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("Welcome to ISE_182 chat, please choose one of the options: \n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("a. Rigistration");
            Console.WriteLine("b. Login/Logoff");
            Console.WriteLine("c. Retrieve last 10 messages from server");
            Console.WriteLine("d. Display last 20 retrieved messages");
            Console.WriteLine("e. Display all retrieved messages");
            Console.WriteLine("f. Write (and send) a new message (max. Length 150 characters)");
            Console.WriteLine("g. Exit");
            Console.WriteLine();
            Console.Write("Your Choice: ");
            char choice = (char)Console.Read();
            agent(choice);
        }
        private static void agent(char choice)
        {
            switch (choice)
            {
                case 'a':
                    //something//
                    break;
                case 'b':
                    //something//
                    break;
                case 'c':
                    //something//
                    break;
                case 'd':
                    //something//
                    break;
                case 'e':
                    //something//
                    break;
                case 'f':
                    //something//
                    break;
                case 'g':
                    
                    break;
            }
        }
        private static void systemInit()
        {
            Chatroom chatRoom = new Chatroom();
            chatRoom.chatroomInit();
        }
        public static void Main(String[]args)
        {
            systemInit();    //initiates the chatroom, loads data from persistant layer to RAM, etc..//
            displayGui();
        }
    }
}

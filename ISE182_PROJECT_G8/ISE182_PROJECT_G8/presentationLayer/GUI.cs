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

        public static void displayGui(Chatroom chatRoom)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("Welcome to ISE_182 chat, please choose one of the options: \n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("a. Registration");
            Console.WriteLine("b. Login/Logoff");
            Console.WriteLine("c. Retrieve last 10 messages from server");
            Console.WriteLine("d. Display last 20 retrieved messages");
            Console.WriteLine("e. Display all retrieved messages");
            Console.WriteLine("f. Write (and send) a new message (max. Length 150 characters)");
            Console.WriteLine("g. Exit");
            Console.WriteLine();
            Console.Write("Your Choice: ");
            String choice = Console.ReadLine();
            Console.WriteLine();
            agent(choice,chatRoom);
        }

        //this agent will take the user wherever he wants to go//
        public static void agent(String choice,Chatroom chatRoom) 
        {
            switch (choice)
            {
                case "a":
                    chatRoom.Register();
                    break;
                case "b":
                    chatRoom.log_inOrOff();
                    break;
                case "c":
                    chatRoom.getLoggedInUser().retreive_n_messages(10);
                    break;
                case "d":
                    //something//
                    break;
                case "e":
                    //something//
                    break;
                case "f":
                    //something//
                    break;
                case "g":
                    //nothing to do-just break//
                    break;
            }
        }
        private static Chatroom systemInit()
        {
           Chatroom chatRoom = new Chatroom(); //initiates a new Chatroom//
            return chatRoom;
        }
        public static void Main(String[]args)
        {
            Chatroom chatRoom=systemInit();    //initiates the chatroom, loads data from persistant layer to RAM, etc..//
            displayGui(chatRoom);
        }
    }
}

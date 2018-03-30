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
            Console.WriteLine("b. Login/Logout");
            Console.WriteLine("c. Retrieve last 10 messages from server");
            Console.WriteLine("d. Display last 20 retrieved messages");
            Console.WriteLine("e. Display all retrieved messages");
            Console.WriteLine("f. Write (and send) a new message (max. Length 150 characters)");
            Console.WriteLine("g. Exit");
            Console.WriteLine("h. Test tools (For us - not for release)");
            Console.WriteLine();
            Console.Write("Your Choice: ");
            String choice = Console.ReadLine();
            Console.WriteLine();

            agent(choice, chatRoom);
        }

        //this agent will take the user wherever he wants to go//
        public static void agent(String choice, Chatroom chatRoom)
        {
            switch (choice)
            {
                case "a":
                    Chat_EventHandler.Register(chatRoom);
                    break;
                case "b":
                    Chat_EventHandler.loginOut(chatRoom);
                    break;
                case "c":
                   // Chat_EventHandler.getLoggedInUser(chatRoom).retreive_n_messages(10);//
                    break;
                case "d":
                    Chat_EventHandler.displayNmessages(chatRoom);
                    break;
                case "e":
                    Chat_EventHandler.displayAllMsg(chatRoom);
                    break;
                case "f":
                    Chat_EventHandler.send(chatRoom);
                    break; 
                case "g":
                    Chat_EventHandler.Exit(chatRoom);
                    break;
                case "h":
                    Chat_EventHandler.test(chatRoom);
                    break;
            }
        }


    }
}


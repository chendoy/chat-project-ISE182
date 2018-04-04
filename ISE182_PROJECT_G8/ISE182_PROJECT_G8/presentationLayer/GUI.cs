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
        public static void DisplayVisitorGUI(Chatroom chatroom)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Blue; //UI tweak//
            Console.WriteLine("Welcome to ISE_182 chat, please choose one of the options: \n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("a. Registration");
            Console.WriteLine("b. Login");
            Console.WriteLine("c. Exit");
            Choice:
            {
                Console.Write("Your Choice:");
                Console.BackgroundColor = ConsoleColor.Black; Console.Write(" ");
                String choice = Console.ReadLine(); Console.WriteLine();

                switch (choice)
                {
                    case "a":
                        GUI_EventHandler.Register(chatroom);
                        Console.WriteLine("What would you like to do next?");
                        goto Choice;
                        break;
                    case "b":
                        if (GUI_EventHandler.Login(chatroom))
                        {
                            DisplayUserGUI(chatroom);
                        }
                        break;
                    case "c":
                        GUI_EventHandler.ExitVisitor(chatroom);
                        break;
                    default:
                        Console.WriteLine("Invalid input, try again");
                        goto Choice;
                }
            }
        }

        public static void DisplayUserGUI(Chatroom chatroom)
        {
            Console.WriteLine("USerGUI");
            Console.ReadKey();
        }
        //public static void displayGui(Chatroom chatRoom)
        //{
        //    Console.BackgroundColor = ConsoleColor.Blue; //UI tweak//
        //    Console.WriteLine("Welcome to ISE_182 chat, please choose one of the options: \n");
        //    Console.BackgroundColor = ConsoleColor.Black;
        //    Console.WriteLine("a. Registration");
        //    Console.WriteLine("b. Login/Logout");
        //    Console.WriteLine("c. Retrieve last 10 messages from server");
        //    Console.WriteLine("d. Display last 20 retrieved messages");
        //    Console.WriteLine("e. Display all retrieved messages (by certain user)");
        //    Console.WriteLine("f. Write (and send) a new message (max. Length 150 characters)");
        //    Console.WriteLine("g. Exit");
        //    Console.WriteLine("h. Test tools (For us - not for release) \n");
        //    Console.BackgroundColor = ConsoleColor.Blue;
        //    Console.Write("Your Choice:");
        //    Console.BackgroundColor = ConsoleColor.Black; Console.Write(" ");
        //    String choice = Console.ReadLine(); Console.WriteLine();

        //    agent(choice, chatRoom);
        //}

        //this agent will take the user wherever he wants to go depends on his 'choice'//
        public static void VisitorAgent(String choice, Chatroom chatRoom)
        {

        }
        public static void agent(String choice, Chatroom chatRoom)
        {
            switch (choice)
            {
                case "a":
                    //Chat_EventHandler.Register(chatRoom);
                    break;
                case "b":
                    Chat_EventHandler.loginOut(chatRoom);
                    break;
                case "c":
                    Chat_EventHandler.retreiveMessages(chatRoom);
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
                case "h": //test functions - not for release//
                    Chat_EventHandler.test(chatRoom);
                    break;
            }
        }

        //makes the chat ready for the next user input//
        private static void ChatReadyVisitor(Chatroom chatroom)
        {
            Console.WriteLine("What would you like to do next?");
            String choice = Console.ReadLine();
            VisitorAgent(choice, chatroom);
        }
    }
}


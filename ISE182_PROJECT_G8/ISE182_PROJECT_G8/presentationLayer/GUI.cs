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
                Console.BackgroundColor = ConsoleColor.Blue;
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
                        if (!GUI_EventHandler.Login(chatroom))
                        {
                            goto Choice;
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

        public static void DisplayUserGUI(Chatroom chatroom, string nickname)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Blue; //UI tweak//
            Console.WriteLine(String.Format("Welcome back {0}, please choose one of the options: \n", nickname));
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("a. Write (and send) a new message (max. Length 150 characters)");
            Console.WriteLine("b. Retrieve last 10 messages from server");
            Console.WriteLine("c. Display last 20 retrieved messages");
            Console.WriteLine("d. Display all retrieved messages (by certain user)");
            Console.WriteLine("e. Logout");
            //Console.WriteLine("f. Change user");
            Console.WriteLine("f. Exit");

            ChoiceUser:
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Write("Your Choice:");
                Console.BackgroundColor = ConsoleColor.Black; Console.Write(" ");
                String choice = Console.ReadLine(); Console.WriteLine();

                switch (choice)
                {
                    case "a":
                        GUI_EventHandler.SendMessage(chatroom);
                        Console.WriteLine("What would you like to do next?");
                        goto ChoiceUser;
                        break;
                    case "b":
                        GUI_EventHandler.RetreiveMessages(chatroom);
                        Console.WriteLine("What would you like to do next?");
                        goto ChoiceUser;
                        break;
                    case "c":
                        GUI_EventHandler.Display20messages(chatroom);
                        Console.WriteLine("What would you like to do next?");
                        goto ChoiceUser;
                        break;
                    case "d":
                        GUI_EventHandler.DisplayMessagesByUser(chatroom);
                        Console.WriteLine("What would you like to do next?");
                        goto ChoiceUser;
                        break;
                    case "e":
                        GUI_EventHandler.Logout(chatroom);
                        break;
                    case "f":
                        GUI_EventHandler.Exit(chatroom);
                        break;
                    case "h": //test functions - not for release//
                        Chat_EventHandler.test(chatroom);
                        break;
                }
            }
        }
    }
}


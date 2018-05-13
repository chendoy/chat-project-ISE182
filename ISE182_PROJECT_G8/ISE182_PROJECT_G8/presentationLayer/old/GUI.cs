using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISE182_PROJECT_G8.logicLayer;


namespace ISE182_PROJECT_G8.presentationLayer
{
    /*This class will handle the GUI
     * This class will show the user the menu
     * get the user choice and forward it to GUI_EventHandler
     * for further processing
     */
    class GUI
    {
        // Showing the visitor menu
        public static void DisplayVisitorGUI()
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
                        GUI_EventHandler.Register();
                        Console.WriteLine("What would you like to do next?");
                        goto Choice;
                    case "b":
                        if (!GUI_EventHandler.Login())
                        {
                            goto Choice;
                        }
                        break;
                    case "c":
                        GUI_EventHandler.ExitVisitor();
                        break;
                    default:
                        Console.WriteLine("Invalid input, try again");
                        goto Choice;
                }
            }
        }

        // Showing the logged in user menu
        public static void DisplayUserGUI(string nickname)
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
                        GUI_EventHandler.SendMessage();
                        Console.WriteLine("What would you like to do next?");
                        goto ChoiceUser;
                    case "b":
                        GUI_EventHandler.RetreiveMessages();
                        Console.WriteLine("What would you like to do next?");
                        goto ChoiceUser;
                    case "c":
                        GUI_EventHandler.DisplayMessages();
                        DisplayUserGUI(nickname);
                        break;
                    case "d":
                        GUI_EventHandler.DisplayMessagesByUser();
                        DisplayUserGUI(nickname);
                        break;
                    case "e":
                        Console.WriteLine("Are you sure you want to logout y/n?");
                        String choise = Console.ReadLine();
                        if (choise == "y")
                        {
                            GUI_EventHandler.Logout();
                        }
                        else
                        {
                            goto ChoiceUser;
                        }
                        break;
                    case "f":
                        Console.WriteLine("Are you sure you want to exit y/n?");
                        choise = Console.ReadLine();
                        if (choise == "y")
                        {
                            GUI_EventHandler.Exit();
                        }
                        else
                        {
                            goto ChoiceUser;
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid input, try again");
                        goto ChoiceUser;
                }
            }
        }

        // Showing information window such as messages
        public static void DisplayInfo(string title, string info)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Green; //UI tweak//
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(String.Format("Displaying {0}:\n", title));
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            if (String.IsNullOrWhiteSpace(info))
            {
                info = "There's nothing to show..";
            }
            Console.WriteLine("\n" + info);
            Console.Write("\n Press any key to go back to menu...");
            Console.ReadKey(true);
        }
    }
}


using ISE182_PROJECT_G8.logicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.presentationLayer
{
    /* This class handle the requests from the user within the PL
     * This class will get the input and send them to BL 
     * for further processing 
     */
    public class GUI_EventHandler
    {
        private static int _nMessagesDisplay = 20;  //magic number//

        // Handle register request
        public static void Register()
        {
            Console.Write("Enter User Name to Register or 'x' to cancel: ");
            Notfound: //will be used in goto statement//
            {
                String nickname = Console.ReadLine();
                if (nickname != "x")
                {
                    if (InputGroupID(out int groupID))
                    {

                        bool? succeeded = Chat_EventHandler.Register(nickname, groupID);
                        if (!succeeded.HasValue)
                        {
                            Console.WriteLine("Error: nickname or Group ID is not valid");
                            Console.WriteLine("Please pick another user name or 'x' to cancel");
                            goto Notfound;
                        }
                        else
                        {
                            if (succeeded.Value)
                            {
                                Console.WriteLine("Registration was Successfull, Welcome to ISE_182 chat!");
                            }
                            else
                            {
                                Console.WriteLine("Error: User name already exist.");
                                Console.WriteLine("Please pick another user name or 'x' to cancel");
                                goto Notfound;
                            }
                        }
                    }
                }
            }
        }

        // Handle login request, get the info and send to BL
        public static bool Login()
        {
            Console.WriteLine("Enter user name for login or 'x' to cancel: ");
            string nickname = Console.ReadLine();
            if (nickname != "x")
            {
                if (InputGroupID(out int groupID))
                {
                    bool logged = Chat_EventHandler.Login(nickname, groupID);
                    if (logged)
                    {
                        Console.WriteLine(nickname + "[" + groupID + "]" + " Logged-in Successfully");
                        System.Threading.Thread.Sleep(1000);
                        GUI.DisplayUserGUI(nickname);
                    }
                    else
                    {
                        Console.WriteLine("Error: user does not exist!");
                    }

                    return logged;
                }
            }

            return false;
        }

        // handle exiting the application for visitor
        public static void ExitVisitor()
        {
            Chat_EventHandler.ExitVisitor();
            Console.Write("Thank you for using ISE_182 chat!");

            //moving dot while "Thank You" message is shown//

            Console.Write(".");
            System.Threading.Thread.Sleep(1000);
            Console.Write(".");
            System.Threading.Thread.Sleep(1000);
            Console.Write(".");
            System.Threading.Thread.Sleep(1000);
        }

        // Handle send message request
        public static void SendMessage()
        {
            Console.WriteLine("Please enter your message or enter 'x' to exit:");
            string msg = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(msg))
            {
                Console.WriteLine("Cannot send empty message");
            }
            else
            {
                if (msg != "x")
                {
                    bool? succeeded = Chat_EventHandler.Send(msg);
                    if (!succeeded.HasValue)
                    {
                        Console.WriteLine("Message length limit exceeded - max. 150 characters!");
                    }
                    else
                    {
                        if (!succeeded.Value)
                        {
                            Console.WriteLine("Server did not respond, please check your internet connection..");
                        }
                    }
                }
            }
        }

        // Retriving messages from the server
        public static void RetreiveMessages()
        {
            if (Chat_EventHandler.RetreiveMessages())
            {
                Console.WriteLine("Messages retreived successfuly!");
            }
            else
            {
                Console.WriteLine("Server did not respond, please check your internet connection..");
            }
        }

        // Display last messages, cap is _nMessagesDisplay
        public static void DisplayMessages()
        {
            string messages = Chat_EventHandler.DisplayNmessages(_nMessagesDisplay);
            GUI.DisplayInfo(String.Format("last {0} messages", _nMessagesDisplay), messages);
        }

        // Display Messages By input user information
        public static void DisplayMessagesByUser()
        {
            Console.WriteLine("Enter user name for filtering or 'x' to cancel: ");
            string nickname = Console.ReadLine();
            if (nickname != "x")
            {
                if (InputGroupID(out int groupID))
                {
                    String msgs = Chat_EventHandler.DisplayMessagesByUser(nickname, groupID);
                    GUI.DisplayInfo("messages of " + nickname + "[" + groupID + "]", msgs);
                }
            }
        }

        // Logout the user from the application and return him to visitor menu
        public static void Logout()
        {
            string nickname = Chat_EventHandler.Logout();
            Console.WriteLine(nickname + " Logged-off Successfully");
            System.Threading.Thread.Sleep(1000);
            GUI.DisplayVisitorGUI();
        }

        // handle exiting the application for loggedin user
        public static void Exit()
        {
            Chat_EventHandler.Exit();
            Console.Write("Thank you for using ISE_182 chat!");
            //moving dot while "Thank You" message is shown//

            Console.Write(".");
            System.Threading.Thread.Sleep(1000);
            Console.Write(".");
            System.Threading.Thread.Sleep(1000);
            Console.Write(".");
            System.Threading.Thread.Sleep(1000);

        }

        // Get Group ID from the user, validate it. return false if the user want to abort
        private static bool InputGroupID(out int groupID)
        {
            Console.WriteLine("Group id (-1 to abort): ");
            string groupTxt = Console.ReadLine();
            if (int.TryParse(groupTxt, out groupID))
            {
                if (groupID == -1)
                {
                    Console.WriteLine("Group ID is -1, aborting...");
                    System.Threading.Thread.Sleep(1000);
                    return false;
                }
                else
                {
                    if (groupID < 1 | groupID > 40)
                    {
                        Console.WriteLine("Group ID must be between 1 to 40\nPlease try again..");
                        return InputGroupID(out groupID);
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            else
            {
                Console.WriteLine("Warning: Group ID is not a number");
                Console.WriteLine("Please try again..");
                return InputGroupID(out groupID);
            }
        }
    }
}


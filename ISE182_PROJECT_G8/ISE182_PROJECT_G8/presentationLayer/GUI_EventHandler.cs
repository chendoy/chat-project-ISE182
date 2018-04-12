using ISE182_PROJECT_G8.logicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.presentationLayer
{
    public class GUI_EventHandler
    {
        private static int _nMessagesDisplay = 20;  //magic number//
        public static void Register()
        {
            Console.WriteLine("Enter User Name to Register or 'x' to cancel: ");
            Notfound: //will be used in goto statement//
            {
                String nickname = Console.ReadLine();
                if (nickname != "x")
                {
                    Console.WriteLine("Group id: ");
                    string groupTxt = Console.ReadLine();
                    if (int.TryParse(groupTxt, out int groupID))
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
                    else
                    {
                        Console.WriteLine("Error: Group ID is not a number");
                        Console.WriteLine("Please pick user name again or 'x' to cancel");
                        goto Notfound;
                    }
                }
            }
        }

        public static bool Login()
        {
            Console.WriteLine("User name: ");
            string nickname = Console.ReadLine();
            Console.WriteLine("Group ID: ");
           int groupId = Convert.ToInt32(Console.ReadLine());

            bool logged = Chat_EventHandler.Login(nickname,groupId);
            if (logged)
            {
                Console.WriteLine(nickname +"["+ groupId+"]" +" Logged-in Successfully");
                System.Threading.Thread.Sleep(1000);
                GUI.DisplayUserGUI(nickname);
            }
            else
            {
                Console.WriteLine("Error: user does not exist!");
            }

            return logged;
        }

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

        public static void SendMessage()
        {
            Console.WriteLine("Please enter your message or press x to exit:");
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

        public static void DisplayMessages()
        {
            string messages = Chat_EventHandler.DisplayNmessages(_nMessagesDisplay);
            GUI.DisplayInfo(String.Format("last {0} messages",_nMessagesDisplay), messages);
        }

        public static void DisplayMessagesByUser()
        {
            Console.WriteLine("Enter user name for filtering or 'x' to cancel: ");
            string nickname = Console.ReadLine();
            if (nickname != "x")
            {
                Console.WriteLine("Enter group ID (-1 to abort): ");
                int groupId = Convert.ToInt32(Console.ReadLine());
                if (groupId >= 1 & groupId <= 40 && groupId != -1)
                {
                    String msgs = Chat_EventHandler.DisplayMessagesByUser(nickname, groupId);
                    GUI.DisplayInfo("messages of " + nickname + "[" + groupId + "]", msgs);
                }
                else //there was a problem with the group id//
                {
                    Console.WriteLine("group ID is -1 or is not valid, aborting...");
                    System.Threading.Thread.Sleep(2000);
                }
            } 
        }

        public static void Logout()
        {
            string nickname = Chat_EventHandler.Logout();
            Console.WriteLine(nickname + " Logged-off Successfully");
            System.Threading.Thread.Sleep(1000);
            GUI.DisplayVisitorGUI();
        }

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
    }
}

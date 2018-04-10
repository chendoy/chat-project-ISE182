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
        public static void Register()
        {
            Console.WriteLine("Enter User Name to Register or 'x' to cancel: ");
            Notfound: //will be used in goto statement//
            {
                String nickname = Console.ReadLine();
                if (nickname != "x")
                {
                    present_handler.output("Group id: ");
                    int groupID = Convert.ToInt32(Console.ReadLine()); // Error if not integer

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

        public static bool Login()
        {
            Console.WriteLine("User name: ");
            string nickname = Console.ReadLine();

            bool logged = Chat_EventHandler.Login(nickname);
            if (logged)
            {
                Console.WriteLine(nickname + " Logged -in Successfully");
                System.Threading.Thread.Sleep(1000);
                GUI.DisplayUserGUI(nickname);
            }
            else
            {
                Console.WriteLine("Error: user not exist.");
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
            if(msg != "x")
            {
                //Console.WriteLine("Please enter your message:");
                //string msg = Console.ReadLine();
                if (!Chat_EventHandler.Send(msg))
                {
                Console.WriteLine("Message length limit exceeded - max. 150 characters!");
                }
            }

        }

        public static void RetreiveMessages()
        {
            try
            {
                Chat_EventHandler.RetreiveMessages();
                Console.WriteLine("Messages retreived successfuly!");
            }
            catch
            {
                Console.WriteLine("Error: Something went wrong");
            }
        }

        public static void Display20messages()
        {
            string messages = Chat_EventHandler.DisplayNmessages(20);
            GUI.DisplayInfo("last 20 messages", messages);
        }

        public static void DisplayMessagesByUser()
        {
            Console.WriteLine("Enter user name for filtering or 'x' to cancel: ");
            string nickname = Console.ReadLine();
            if (nickname != "x")
            {
                String msgs = Chat_EventHandler.DisplayMessagesByUser(nickname);

                GUI.DisplayInfo("messages of " + nickname, msgs);
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

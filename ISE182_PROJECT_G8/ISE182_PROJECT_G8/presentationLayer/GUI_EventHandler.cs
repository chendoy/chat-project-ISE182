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
        public static void Register(Chatroom chatroom)
        {
            Console.WriteLine("Enter User Name to Register or 'x' to cancel: ");
            Notfound: //will be used in goto statement//
            {
                String nickname = Console.ReadLine();
                if (nickname != "x")
                {
                    present_handler.output("Group id: ");
                    int groupID = Convert.ToInt32(Console.ReadLine()); // Error if not integer

                    bool? succeeded = Chat_EventHandler.Register(chatroom, nickname, groupID);
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
                            Console.WriteLine("Registration was Scuccessfull, Welcome to ISE_182 chat!");
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

        public static bool Login(Chatroom chatroom)
        {
            Console.WriteLine("User name: ");
            string nickname = Console.ReadLine();

            bool logged = Chat_EventHandler.Login(chatroom, nickname);
            if (logged)
            {
                Console.WriteLine(nickname + " Logged -in Successfully");
                System.Threading.Thread.Sleep(1000);
            }
            else
            {
                Console.WriteLine("Error: user not exist.");
            }

            return logged;
        }

        public static void ExitVisitor(Chatroom chatroom)
        {
            Chat_EventHandler.ExitVisitor(chatroom);
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

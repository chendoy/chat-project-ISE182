using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISE182_PROJECT_G8.presentationLayer;
using ISE182_PROJECT_G8.persistantLayer;


namespace ISE182_PROJECT_G8.logicLayer
{
    /* 
     * Class of static methods that defines the behaviour of ISE_182 (group #8) chat//
     */

    class Chat_EventHandler
    {
        private static Chatroom chatroom = Chatroom.Instance;
        public static bool? Register(String nickname, int groupID)
        {
            if (!UserHandler.isValid(nickname, groupID))
            {
                return null;
            }
                
            return chatroom.Register(nickname, groupID);
        }
        
        public static bool Login(String nickname)
        {
            return chatroom.Login(nickname);
        }

        public static void ExitVisitor()
        {
            Logger.Instance.Info("System exits [visitor]");
        }

        public static void Exit()
        {
            string nickname = chatroom.getLoggedInUser().getNickname();
            chatroom.LogOut(); //log the current user out//
            ExitVisitor();
            Logger.Instance.Info(String.Format("System exits [{0}]", nickname));
        }

        public static bool Send(string msg)
        {
            if (!MessageHandler.isValid(msg))
            {
                Logger.Instance.Error("Message was not valid");
                return false;
            }

            bool sent = chatroom.Send(msg);
            if (sent)
            {
                Logger.Instance.Info("Message was sent successfully");
                return true;
            }

            return false;
        }

        public static void RetreiveMessages()
        {
            chatroom.RetreiveMessages();
            Logger.Instance.Info("Messages was retreived successfully");
        }

        //displaying s specific number (n) of retreived messages//
        public static string DisplayNmessages(int n)
        {
            return chatroom.DisplayNmessages(n);
        }

        public static string DisplayMessagesByUser(string nickname)
        {
            return chatroom.DisplayMessagesByUser(nickname);
        }

        public static string Logout()
        {
            return chatroom.LogOut();
        }

        



        /*test functions:
            * a: prints all regiestered users//
            * b: clears all registered users
            * c: prints the currently logged in user, if exists
            * */
        public static void test()
        {
            present_handler.output("Choose a test function:");
            string choice = Console.ReadLine();
            switch(choice)
            {
                case "a":
                    chatroom.printAllUsers();
                    //Chat_EventHandler./chat_ready(chatroom);
                    break;
                /*case "b":
                    chatroom.clearUserList();
                    break;*/
                case "c":
                    User loggedIn = chatroom.getLoggedInUser();
                    if (loggedIn != null)
                        present_handler.output("logged in: " + loggedIn.getNickname());
                    else
                        present_handler.output("No logged in User");
                    //Chat_EventHandler.chat_ready(chatroom);
                    break;
            }
        }
    }
}

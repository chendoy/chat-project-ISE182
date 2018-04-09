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
        public static bool? Register(Chatroom chatRoom, String nickname, int groupID)
        {
            if (!UserHandler.isValid(nickname, groupID))
            {
                return null;
            }
                
            return chatRoom.Register(nickname, groupID);
        }
        
        public static bool Login(Chatroom chatRoom, String nickname)
        {
            return chatRoom.Login(nickname);
        }

        public static void ExitVisitor(Chatroom chatRoom)
        {
            chatRoom.saveUsers(); //persisting registered users data//
            chatRoom.saveMessages(); //persisting received messages data//
            Logger.Instance.Info("System exits [visitor]");
        }

        public static void Exit(Chatroom chatRoom)
        {
            string nickname = chatRoom.getLoggedInUser().getNickname();
            chatRoom.LogOut(); //log the current user out//
            ExitVisitor(chatRoom);
            Logger.Instance.Info(String.Format("System exits [{0}]", nickname));
        }

        public static bool Send(Chatroom chatRoom, string msg)
        {
            if (!MessageHandler.isValid(msg))
            {
                Logger.Instance.Error("Message was not valid");
                return false;
            }

            chatRoom.Send(msg);
            Logger.Instance.Info("Message was sent successfully");
            return true;
        }

        public static void RetreiveMessages(Chatroom chatRoom)
        {
            chatRoom.RetreiveMessages();
            Logger.Instance.Info("Messages was retreived successfully");
        }

        //displaying s specific number (n) of retreived messages//
        public static string DisplayNmessages(Chatroom chatRoom, int n)
        {
            return chatRoom.DisplayNmessages(n);
        }

        public static string DisplayMessagesByUser(Chatroom chatRoom, string nickname)
        {
            return chatRoom.DisplayMessagesByUser(nickname);
        }

        public static string Logout(Chatroom chatroom)
        {
            return chatroom.LogOut();
        }

        



        /*test functions:
            * a: prints all regiestered users//
            * b: clears all registered users
            * c: prints the currently logged in user, if exists
            * */
        public static void test(Chatroom chatRoom)
        {
            present_handler.output("Choose a test function:");
            string choice = Console.ReadLine();
            switch(choice)
            {
                case "a":
                    chatRoom.printAllUsers();
                    //Chat_EventHandler./chat_ready(chatRoom);
                    break;
                /*case "b":
                    chatRoom.clearUserList();
                    break;*/
                case "c":
                    User loggedIn = chatRoom.getLoggedInUser();
                    if (loggedIn != null)
                        present_handler.output("logged in: " + loggedIn.getNickname());
                    else
                        present_handler.output("No logged in User");
                    //Chat_EventHandler.chat_ready(chatRoom);
                    break;
            }
        }
    }
}

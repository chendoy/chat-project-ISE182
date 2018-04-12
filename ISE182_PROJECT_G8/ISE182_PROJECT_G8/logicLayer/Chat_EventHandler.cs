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
            if (!UserHandler.isValid(nickname, groupID)) //detailes of registration was not valid - will not register// 
            {
                Logger.Instance.Error("Register info was not valid");
                return null;
            }
                
            return chatroom.Register(nickname, groupID);
        }
        
        public static bool Login(String nickname, int groupId)
        {
            return chatroom.Login(nickname, groupId);
        }

        public static void ExitVisitor()
        {
            Logger.Instance.Info("System exits [visitor]");
        }

        public static void Exit()
        {
<<<<<<< HEAD
            string nickname = chatroom.getLoggedInUser().getNickname();
            chatroom.LogOut(); //changes the chatroom's state//
=======
            string nickname = chatroom.GetLoggedInUser().getNickname();
            chatroom.LogOut(); //log the current user out//
>>>>>>> a99ad0b74778e0971b9e6715fced7e90695e3ae6
            ExitVisitor();
            Logger.Instance.Info(String.Format("System exits [{0}]", nickname));
        }

        public static bool? Send(string msg)
        {
            if (!MessageHandler.isValid(msg))
            {
                Logger.Instance.Error("Message was not valid");
                return null;
            }

            return chatroom.Send(msg);
        }

        public static bool RetreiveMessages()
        {
            return chatroom.RetreiveMessages();
        }

        //displaying s specific number (n) of retreived messages//
        public static string DisplayNmessages(int n)
        {
            return chatroom.DisplayNmessages(n);
        }

        public static string DisplayMessagesByUser(string nickname, int groupId)
        {
            return chatroom.DisplayMessagesByUser(nickname, groupId);
        }

        public static string Logout()
        {
            return chatroom.LogOut(); //changes the chatroom's state//
        }

<<<<<<< HEAD
=======
        



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
                    User loggedIn = chatroom.GetLoggedInUser();
                    if (loggedIn != null)
                        present_handler.output("logged in: " + loggedIn.getNickname());
                    else
                        present_handler.output("No logged in User");
                    //Chat_EventHandler.chat_ready(chatroom);
                    break;
            }
        }
>>>>>>> a99ad0b74778e0971b9e6715fced7e90695e3ae6
    }
}

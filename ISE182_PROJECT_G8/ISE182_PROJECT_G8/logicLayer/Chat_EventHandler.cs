using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISE182_PROJECT_G8.presentationLayer;


namespace ISE182_PROJECT_G8.logicLayer
{
    /* 
     * Class of static methods that define the behaviour of ISE_182 (group #8) chat//
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
        }

        public static void Exit(Chatroom chatRoom)
        {
            chatRoom.logUserOut(); //log the current user out//
            ExitVisitor(chatRoom);
        }

        //makes the chat ready for the next user input//
        public static void chat_ready(Chatroom chatroom)
        {
            present_handler.output("What would you like to do next?");
            String choice = Console.ReadLine();
            presentationLayer.GUI.agent(choice, chatroom);
        }

        public static bool Send(Chatroom chatRoom, string msg)
        {
            if (!MessageHandler.isValid(msg))
            {
                return false;
            }

            chatRoom.Send(msg);
            return true;
        }

        public static void RetreiveMessages(Chatroom chatRoom)
        {
            chatRoom.RetreiveMessages();
        }

        //displaying s specific number (n) of retreived messages//
        public static string DisplayNmessages(Chatroom chatRoom, int n)
        {
            return chatRoom.DisplayNmessages(n);
        }

        public static void loginOut(Chatroom chatRoom)
        {
            chatRoom.loginOut();
            Chat_EventHandler.chat_ready(chatRoom);
        }

        public static void displayAllMsg(Chatroom chatRoom)
        {
            chatRoom.displayAllMsg();
            Chat_EventHandler.chat_ready(chatRoom);
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
                    Chat_EventHandler.chat_ready(chatRoom);
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
                    Chat_EventHandler.chat_ready(chatRoom);
                    break;
            }
        }
    }
}

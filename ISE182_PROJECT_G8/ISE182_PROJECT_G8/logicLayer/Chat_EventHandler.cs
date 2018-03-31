using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.logicLayer
{
    class Chat_EventHandler
    {
        public static void Register(Chatroom chatRoom)
        {
            chatRoom.Register();
        }


        public static void Exit(Chatroom chatRoom)
        {
            chatRoom.saveUsers();
            chatRoom.saveMessages();
            Console.Write("Thank you for using ISE_182 chat!");
            Console.Write(".");
            System.Threading.Thread.Sleep(1000);
            Console.Write(".");
            System.Threading.Thread.Sleep(1000);
            Console.Write(".");
            System.Threading.Thread.Sleep(1000);
        }

        //makes the chat be ready for the next user input//
        public static void chat_prepareNext(Chatroom chatroom)
        {
            Console.WriteLine("What would you like to do next?");
            String choice = Console.ReadLine();
            ISE182_PROJECT_G8.presentationLayer.GUI.agent(choice, chatroom);
        }

        public static void send(Chatroom chatRoom)
        {
            if (chatRoom.getLoggedInUser() == null)
                Console.WriteLine("Error: no logged-in user found");
            else
            {
                chatRoom.send();
                Chat_EventHandler.chat_prepareNext(chatRoom);
            }
        }

        public static void loginOut(Chatroom chatRoom)
        {
            User logeed_in_user = chatRoom.getLoggedInUser();
            if (logeed_in_user != null)
                chatRoom.log_in();
            else
                logeed_in_user.log_out(chatRoom);
            Chat_EventHandler.chat_prepareNext(chatRoom);
        }

        public static void displayAllMsg(Chatroom chatRoom)
        {
            chatRoom.displayAllMsg();
            Chat_EventHandler.chat_prepareNext(chatRoom);
        }

        public static void displayNmessages(Chatroom chatRoom)
        {
            chatRoom.displayNmessages();
            Chat_EventHandler.chat_prepareNext(chatRoom);
        }

        public static void retreiveMessages(Chatroom chatRoom)
        {
            chatRoom.retreive();
            Chat_EventHandler.chat_prepareNext(chatRoom);
        }

        public static void test(Chatroom chatRoom)
        {

            /*test functions:
             * 
             * a: prints all regiestered users//
             * b:clears all registered users
             * c: prints the currently logged in user, if exists
             * */

            Console.WriteLine("Choose a test function:");
            string choice = Console.ReadLine();
            switch(choice)
            {
                case "a":
                    chatRoom.printAllUsers();
                    Chat_EventHandler.chat_prepareNext(chatRoom);
                    break;
                /*case "b":
                    chatRoom.clearUserList();
                    break;*/
                case "c":
                    User loggedIn = chatRoom.getLoggedInUser();
                    if (loggedIn != null)
                        Console.WriteLine("logged in: " + loggedIn.getNickname());
                    else
                        Console.WriteLine("No logged in User");
                    Chat_EventHandler.chat_prepareNext(chatRoom);
                    break;
            }
            

        }

    }


}

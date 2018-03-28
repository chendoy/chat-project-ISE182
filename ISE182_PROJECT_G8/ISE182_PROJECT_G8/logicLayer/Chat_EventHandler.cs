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
           Console.WriteLine("Hope to see you soon!");
           System.Threading.Thread.Sleep(4000);
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
            chatRoom.getLoggedInUser().send();
            Chat_EventHandler.chat_prepareNext(chatRoom);
        }

        public static void test(Chatroom chatRoom)
        {
            Console.WriteLine("Choose a test function:");
            string choice = Console.ReadLine();
            switch(choice)
            {
                case "a":
                    chatRoom.printAllUsers();
                    break;
                /*case "b":
                    chatRoom.clearUserList();
                    break;*/
            }

            
        }

    }


}

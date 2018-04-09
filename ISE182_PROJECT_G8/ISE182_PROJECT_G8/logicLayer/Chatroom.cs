﻿
using ISE182_PROJECT_G8.CommunicationLayer;
using ISE182_PROJECT_G8.presentationLayer;
using ISE182_PROJECT_G8.persistantLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.logicLayer
{
    public class Chatroom
    {
        public static int _nMessagesRetreive = 10; //magic number//
        public static int _nMessagesDisplay = 20;  //magic number//
        public String _url = "http://ise172.ise.bgu.ac.il";//"http://localhost/"; //localhost means non BGU environment, for BGU: http://ise172.ise.bgu.ac.il //
        private int port = 80;
        private User loggedInUser;
        private List<User> userList;  //in RAM, retreived from persistant layer//
        private List<Message> messageList; //in RAM, retreived from persistant layer//

        public Chatroom()
        {
            this.loggedInUser = null;
            this.messageList = new List<Message>();
            this.userList = new List<User>();
        }

        public bool Register(String nickname, int groupID)
        {
            User newUser = new User(nickname, groupID); // If not valid - need to improve 
            bool alreadyExist = UserHandler.existIn(newUser, this.userList);

            if (!alreadyExist)
            {
                this.userList.Add(newUser);
                Logger.Instance.Info("User: "+nickname+ " registered successfully");
                return true;
            }
            Logger.Instance.Info("User: " + nickname + " failed to register - already exists");
            return false;
        }


        public bool Login(string nickname)
        {
            //linq query to find existing user in 'userList'//

            var loggedin = (from user in userList
                            where user.getNickname().Equals(nickname)
                            select user).FirstOrDefault();
            if (loggedin != null)
            {
                this.loggedInUser = loggedin; //changes the 'Chat' object status//
                Logger.Instance.Info("User: "+nickname+" is now logged in to the chat");
                this.loggedInUser.loginOrOff(); //changes the 'User' object status//
                return true;
            }
            else
            {
                Logger.Instance.Error("Failed to log "+nickname+" in - doesn't exist");
                return false; 
            }
        }

        public User getLoggedInUser()
        {
            return this.loggedInUser;
        }

        // next 4 static methods are related to persistant layer: save/load users/messages//

        //1//
        public void saveUsers()
        {
            persistantLayer.Saver.saveUsers(this.userList);
        }
        //2//
        public void loadUsers()
        {
            this.userList = persistantLayer.Saver.LoadUsers();
        }
        //3//
        public void saveMessages()
        {
            persistantLayer.Saver.saveMessages(this.messageList);
        }
        //4//
        public void loadMessages()
        {
            this.messageList = persistantLayer.Saver.LoadMessages();
        }

        // Assume there is logged in user
        public string LogOut()
        {
            this.loggedInUser.loginOrOff();
            string nickname = loggedInUser.getNickname();
            this.loggedInUser = null;
            Logger.Instance.Info("Chatroom: user "+nickname+" logged-of");
            return nickname;
        }

        public void Send(string msg)
        {
            User loggedInUser = getLoggedInUser();

            Message message = loggedInUser.Send(this._url, msg); //asks the logged in user instance to send the message//
            Logger.Instance.Info("Chatroom: asks "+this.loggedInUser.getNickname()+" to send message");
            this.messageList.Add(message); //adds the sent message to the chat's message list (RAM)//
            this.messageList = MessageHandler.sortbytime(this.messageList);
        }
        

        public string DisplayMessagesByUser(string nickname)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var messages = (from msg in messageList where msg.getUserName().Equals(nickname) select msg);
            foreach (Message msg in messages)
            {
                stringBuilder.AppendLine(msg.toString());
            }
            Logger.Instance.Info("Chatroom: messages by user "+nickname+" was built");
            return stringBuilder.ToString();
        }

        //displaying specific number off messages//
        public string DisplayNmessages(int n)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int startIndex = Math.Max(0,this.messageList.Count - n);
            for (int i = startIndex; this.messageList.ElementAtOrDefault(i) != null & i < this.messageList.Count; i++)
            {
                stringBuilder.AppendLine(this.messageList.ElementAt(i).toString());
            }
            Logger.Instance.Info("Chatroom: "+n+" messages "+"list generetad");
            return stringBuilder.ToString();
        }

        public void RetreiveMessages()
        {
            //retreives the messages from the server//
            List<IMessage> imsgRetreived = Communication.Instance.GetTenMessages(this._url); //asks communication layer to retreive messages//

            //generates a list of type: 'Messege' from list of type: 'IMessege'//
            List<Message> msgRetreived = new List<Message>();
            foreach (IMessage imsg in imsgRetreived)
                msgRetreived.Add(new Message(imsg));

            //adds the retreived messages as a whole to the chat's messages list//
            MessageHandler.addUniqueByGuid(this.messageList, msgRetreived);
            this.messageList = MessageHandler.sortbytime(this.messageList);
        }

        public void printAllUsers() //***test function****//
        {
            foreach (User user in userList)
            {
                present_handler.output(user.toString());
            }
            Console.ReadKey();
        }

    }
}

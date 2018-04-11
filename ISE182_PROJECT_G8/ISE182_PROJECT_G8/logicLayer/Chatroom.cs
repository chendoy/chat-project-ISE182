
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
    public sealed class Chatroom
    {
        public static int _nMessagesRetreive = 10; //magic number//
        public static int _nMessagesDisplay = 20;  //magic number//
        public String _url = "http://ise172.ise.bgu.ac.il";// "http://ise172.ise.bgu.ac.il";//
        private int port = 80;
        private User loggedInUser;
        private List<User> userList;  //in RAM, retreived from persistant layer//
        private List<Message> messageList; //in RAM, retreived from persistant layer//
        private Saver saver;

        private static Chatroom instance = null;
        private static readonly object padlock = new object();

        //private constructor for singleton
        private Chatroom()
        {
            this.loggedInUser = null;
            saver = Saver.Instance;
            this.messageList = saver.LoadMessages();
            this.userList = saver.LoadUsers();
        }

        public static Chatroom Instance
        {
            get
            {   //only if there is no instance lock object, otherwise return instance
                if (instance == null)
                {
                    lock (padlock) // senario: n threads in here,
                    {              //locking the first and others going to sleep till the first get new Instance
                        if (instance == null)  // rest n-1 threads no need new instance because its not null anymore.
                        {
                            instance = new Chatroom();
                        }
                    }
                }
                return instance;
            }
        }

        public bool Register(String nickname, int groupID)
        {
            User newUser = new User(nickname, groupID); // If not valid - need to improve 
            bool alreadyExist = UserHandler.existIn(newUser, this.userList);

            if (!alreadyExist)
            {
                this.userList.Add(newUser);
                saver.SaveUsers(this.userList); //persisting registered users data//
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

        // next 4 methods are related to persistant layer: save/load users/messages//

        // Assume there is logged in user
        public string LogOut()
        {
            this.loggedInUser.loginOrOff();
            string nickname = loggedInUser.getNickname();
            this.loggedInUser = null;
            Logger.Instance.Info("Chatroom: user "+nickname+" logged-of");
            return nickname;
        }

        public bool Send(string msg)
        {
            User loggedInUser = getLoggedInUser();
            try
            {
                Message message = loggedInUser.Send(this._url, msg); //asks the logged in user instance to send the message//
                Logger.Instance.Info("Chatroom: asks " + this.loggedInUser.getNickname() + " to send message");
                this.messageList.Add(message); //adds the sent message to the chat's message list (RAM)//
                saver.SaveMessages(this.messageList); //persisting received messages data//
                this.messageList = MessageHandler.sortbytime(this.messageList);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Instance.Fatal(String.Format("Could not reach the server: {0}", _url));
                return false;
            }
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
            saver.SaveMessages(this.messageList); //persisting received messages data//
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

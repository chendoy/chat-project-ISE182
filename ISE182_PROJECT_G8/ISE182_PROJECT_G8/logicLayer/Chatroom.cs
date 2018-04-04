using ISE182_PROJECT_G8.CommunicationLayer;
using ISE182_PROJECT_G8.presentationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.logicLayer
{
    public class Chatroom
    {
        public static String _group = "8";
        public static int _nMessagesRetreive = 10; //magic number//
        public static int _nMessagesDisplay = 20;  //magic number//
        public String _url = "http://localhost/"; //localhost means non BGU environment, for BGU: http://ise172.ise.bgu.ac.il //
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
                return true;
            }

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
                this.loggedInUser.loginOrOff(); //changes the 'User' object status//
                return true;
            }
            else
            {
                return false; //todo: implement an error//
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

        public void loginOut()
        {
            User logged_in_user = getLoggedInUser();
            if (logged_in_user != null) //a user is already logged-in//
            {
                present_handler.output(logged_in_user.getNickname() + " is currently logged-in, do you want to change user? Y/N");
                String choice = present_handler.get();
                switch (choice)
                {
                    case "Y":
                    case "y":
                        loggedInUser.loginOrOff(); //changes the user state: logged in-->logged off//
                        logUserOut(); //log the user from the chatroom//
                        log_in(); //initiates new log in to chatroom//
                        break;
                    case "N":
                    case "n":
                        //does nothing (at the moment/)//
                        break;
                }
            }
            else //no user is logged-in to the chat//
                log_in();

        }

        public void log_in() { /*old*/}


        public void logUserOut()
        {
            if (getLoggedInUser() != null)
            {
                this.loggedInUser.loginOrOff();
                this.loggedInUser = null;
            }
        }

        public void Send(string msg)
        {
            User loggedInUser = getLoggedInUser();

            Message message = loggedInUser.send(this._url, _group, msg); //asks the logged in user instance to send the message//
            this.messageList.Add(message); //adds the sent message to the chat's message list (RAM)//
        }
        

        public void displayAllMsg()
        {

            present_handler.output("Enter user name for filtering: ");
            String nickname = present_handler.get();
            present_handler.output("******************************");
            var messages = (from msg in messageList where msg.getUserName().Equals(nickname) select msg);
            foreach (Message msg in messages)
            {
                present_handler.output(msg.toString());
            }

        }

        //displaying specific number off messages//
        public string DisplayNmessages(int n)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; this.messageList.ElementAtOrDefault(i) != null & i < n; i++)
            {
                stringBuilder.AppendLine(this.messageList.ElementAt(i).toString());
            }

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
            //***ADD SORT BY TIME STAMP HERE***//
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

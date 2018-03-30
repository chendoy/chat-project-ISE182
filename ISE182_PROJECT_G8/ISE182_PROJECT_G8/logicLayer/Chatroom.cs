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
        public static int _nMessagesRetreive = 10;
        public static int _nMessagesDisplay = 20;
        public String _url = "http://localhost/" ; //localhost means non BGU environment//
        private int port=80;
        private User loggedInUser; 
        private List<User> userList;
        private List<Message> messageList;

        public Chatroom()
        {
            this.loggedInUser = new User ("Chato");
            this.messageList = new List<Message>();
            this.userList = new List<User>();
        }

        public void Register()
        {
            Console.WriteLine("Enter User Name to Register:");
            String nickname = Console.ReadLine();
            User newUser = new User(nickname);    //todo: check if it doesn't already exist//
            this.userList.Add(newUser);
            Console.WriteLine("Registration was Scuccessfull, Welcome to ISE_182 chat!");
            Chat_EventHandler.chat_prepareNext(this);
        }

        public void log_in()
        {
            Console.Write("User name:");
            String nickname = Console.ReadLine();
            var loggedin = (from user in userList
                            where user.getNickname().Equals(nickname)
                            select user).FirstOrDefault();
            if (loggedin != null)
            {
                this.loggedInUser = loggedin;
                this.loggedInUser.loginOrOff();
            }
            else
                Console.WriteLine("Error: no user found!");
            Chat_EventHandler.chat_prepareNext(this);
        }


        public User getLoggedInUser()
        {
            if(this.loggedInUser==null)
                Console.WriteLine("No logged-in user, login first with a valid user");
            else
            return this.loggedInUser;
            return null;
        }
     
        public void saveUsers()
        {
            persistantLayer.Saver.saveUsers(this.userList);
        }
        public void loadUsers()
        {
            this.userList = persistantLayer.Saver.LoadUsers();
        }
        public void saveMessages()
        {
            persistantLayer.Saver.saveMessages(this.messageList);
        }
        public void loadMessages()
        {
                this.messageList = persistantLayer.Saver.LoadMessages();
        }

        public void printAllUsers() //***test function****//
        {
            foreach(User user in userList)
            {
                Console.WriteLine(user.toString());
            }
            Console.ReadKey();
        }

        public void logUserOut()
        {
            this.loggedInUser = null;
        }

        public void send()
        {
            this.messageList.Add(this.getLoggedInUser().send(this._url, _group));
        }

        public void displayAllMsg()
        {
            foreach(Message msg in this.messageList)
            {
                Console.WriteLine(msg.toString());
            }
            
        }

        public void displayNmessages()
        {
            for(int i=0;this.messageList.ElementAtOrDefault(i)!=null & i<20;i++)
            {
                Console.WriteLine(this.messageList.ElementAt(i).toString());
            }
        }
    }
}

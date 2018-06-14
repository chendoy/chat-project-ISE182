using ISE182_PROJECT_G8.CommunicationLayer;
using ISE182_PROJECT_G8.persistantLayer;
using ISE182_PROJECT_G8.dataAccessLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.logicLayer
{
    /* This is the main Logic Layer class
     * This class will get the inputs and process them
     * This class will return the result of the process to 
     * the one who called it
     */
    public class Chatroom
    {
        private String _url = "http://ise172.ise.bgu.ac.il"; // "http://localhost/"; //  
        private int port = 80;
        private User loggedInUser;
        private List<User> userList;  //in RAM, retreived from persistant layer//
        private ObservableCollection<Message> messageList; //in RAM, retreived from persistant layer//
        private Saver saver;
        private User rememberedUser;
        public string SALT = "1337";
        private DBA messageRetriever;

        //public constructor for chatroom
        public Chatroom()
        {
            this.loggedInUser = null;
            saver = Saver.Instance;
            this.messageList = saver.LoadMessages(); //un-persisting messages data to RAM//
            this.userList = saver.LoadUsers(); //un-persisting users data to RAM//
            this.rememberedUser = saver.LoadRememberMe();
            this.messageRetriever = new DBA();
        }

        public bool Register(String nickname, int groupID, string password)
        {
            if (!UserHandler.isValid(nickname, groupID)) //details of registration was not valid - will not register// 
            {
                Logger.Instance.Error("Register info was not valid");
                return false;
            }
            else
            {
                DBA conn = new DBA();
                UserPL exsist = conn.GetUser(groupID, nickname);
                if (exsist != null)
                {
                    Logger.Instance.Info("User:" + nickname + " already exsist");
                    return false;
                }
                else
                {
                    Boolean ans = conn.Register(groupID, nickname, password);
                    saver.SaveUsers(this.userList); //persisting registered users data//
                    Logger.Instance.Info("User: " + nickname + " registered successfully");
                    return ans;
                }
            }
        }

        public bool Login(string nickname, int groupId, string password)
        {
            DBA conn = new DBA();
            //linq query to find existing user in 'userList'//
            var loggedin = conn.GetUser(groupId, nickname);
            if (loggedin != null)
            {
                if (password.Equals(loggedin.GetPassword()))
                {
                    this.loggedInUser = new User(loggedin); //changes the 'Chat' object status//
                    Logger.Instance.Info("User: " + nickname + " is now logged in to the chat");
                    this.loggedInUser.loginOrOff(); //changes the 'User' object status//
                    return true;
                }
                else
                {
                    Logger.Instance.Info("User: " + nickname + "typped wrong password");
                    return false;
                }
            }
            else
            {
                Logger.Instance.Error("User: " + nickname + " failed to log-in - doesn't exist");
                return false;
            }
        }

        public User GetLoggedInUser()
        {
            return this.loggedInUser;
        }

        public ObservableCollection<Message> getMessageList()
        {
            return this.messageList;
        }

        // Assume there is logged in user
        public string LogOut()
        {
            this.loggedInUser.loginOrOff();
            string nickname = loggedInUser.GetNickname();
            this.loggedInUser = null;
            Logger.Instance.Info("Chatroom: user " + nickname + " logged-out");
            return nickname;
        }

        public bool Send(string msg)
        {
            if (!MessageHandler.isValid(msg))
            {
                Logger.Instance.Error("Message was not valid");
                return false;
            }
            else
            {
                User loggedInUser = GetLoggedInUser();
                try
                {
                    Message message = loggedInUser.Send(this._url, msg); //asks the logged in user instance to send the message//
                    Logger.Instance.Info("Chatroom: asks " + this.loggedInUser.GetNickname() + " to send message");
                    this.messageList.Add(message); //adds the sent message to the chat's message list (RAM)//
                    saver.SaveMessages(this.messageList); //persisting received messages data//
                    //this.messageList = MessageHandler.sortbytime(this.messageList);
                    Logger.Instance.Info("Message was sent successfully");
                    return true;
                }
                catch
                {
                    Logger.Instance.Fatal(String.Format("Could not reach the server: {0}", _url));
                    return false;
                }
            }
        }

        public string DisplayMessagesByUser(string nickname, int groupId)
        {
            StringBuilder stringBuilder = new StringBuilder();

            //linq query//
            var messages = (from msg in messageList where msg.getUserName().Equals(nickname) & msg.getGroupId() == groupId select msg);

            //appending the messages to a list//
            foreach (Message msg in messages)
                stringBuilder.AppendLine(msg.ToString());
            Logger.Instance.Info("Chatroom: messages by user " + nickname + " was built");
            return stringBuilder.ToString();
        }

        #region Retreive Messages

        public bool RetreiveMessages()
        {
            try
            {
                //retreives the messages from the server//
                List<IMessage> imsgRetreived = Communication.Instance.GetTenMessages(this._url); //asks communication layer to retreive messages//

                //generates a list of type: 'Messege' from list of type: 'IMessege'//
                ObservableCollection<Message> msgRetreived = new ObservableCollection<Message>();
                foreach (IMessage imsg in imsgRetreived)
                    msgRetreived.Add(new Message(imsg));

                //adds the retreived messages as a whole to the chat's messages list//
                MessageHandler.addUniqueByGuid(this.messageList, msgRetreived);
                saver.SaveMessages(this.messageList); //persisting received messages data//
                //this.messageList = MessageHandler.sortbytime(this.messageList);
                Logger.Instance.Info("Messages retreived successfully from server");
                return true;
            }
            catch
            {
                Logger.Instance.Fatal(String.Format("Could not reach the server: {0}", _url));
                return false;
            }
        }

        public bool RetreiveMessages(ObservableCollection<Message> messages)
        {
            return messageRetriever.RetreiveMessages(ref messages);
        }

        public void SetGroupFilter(int? groupId)
        {
            messageRetriever.SetGroupFilter(groupId);
        }

        public void SetNicknameFilter(string nickname)
        {
            messageRetriever.SetNicknameFilter(nickname);
        }

        public void ClearFilters()
        {
            messageRetriever.ClearFilters();
        }

        #endregion

        public UserPL GetRememberedUser()
        {
            this.rememberedUser = Saver.Instance.LoadRememberMe();
            return this.rememberedUser;
        }
        public Saver getSaver()
        {
            return this.saver;
        }

        public void clearUsersList()
        {
            this.userList.Clear();
        }

        public void saveRememberedUser(User userToBeRemembered)
        {
            getSaver().SaveRememberMe(userToBeRemembered);
        }

    }
}
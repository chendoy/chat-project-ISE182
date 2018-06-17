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
        private User loggedInUser;
        private List<User> userList;  //in RAM, retreived from persistant layer//
        private IList<Message> messageList; //in RAM, retreived from persistant layer//
        private Saver saver;
        private User rememberedUser;
        private const string SALT = "1337";
        private const int msgLimit = 200;
        private DBA messageRetriever;

        //public constructor for chatroom
        public Chatroom()
        {
            this.loggedInUser = null;
            saver = Saver.Instance;
            this.messageList = new List<Message>(); //saver.LoadMessages(); //un-persisting messages data to RAM//
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

        public bool UpdateMessage(Guid guidOfEdit, string content)
        {
            DateTime sendTime = DateTime.UtcNow;
            DBA conn = new DBA();
            return conn.UpdateMessage(guidOfEdit, sendTime, content);
        }

        public User GetLoggedInUser()
        {
            return this.loggedInUser;
        }

        public IList<Message> GetMessageList()
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
                DBA conn = new DBA();
                User loggedInUser = GetLoggedInUser();
                if (loggedInUser != null)
                {
                    DateTime time = DateTime.UtcNow;
                    Boolean ans = conn.SendMessage(time, loggedInUser.GetUserId(), msg);
                    if(ans)
                    {
                        Logger.Instance.Info("Chatroom: asks " + this.loggedInUser.GetNickname() + " to send message");
                        Logger.Instance.Info("Message was sent successfully");
                        return true;
                    }
                    else
                    {
                        Logger.Instance.Info("Send Message failed ");
                        return false;
                    }
                }
                else
                {
                    Logger.Instance.Info("Tried to send message without login");
                    return false;
                }
            }
        }
        

        public bool RetreiveMessages(out IList<Message> addMsgs) // Need to check 200
        {
            bool needToReset = false;
            if (!messageRetriever.HasTimeFilter()) // Means the filter changed and need to reload all the messages
            {
                this.messageList = new List<Message>();
                needToReset = true;
            }

            addMsgs = messageRetriever.RetreiveMessages();
            if (addMsgs.Count > 0)
            {
                needToReset = (AddByKeepUniqueGuid(this.messageList, addMsgs) | needToReset);

                if (messageList.Count > msgLimit)
                {
                    for (int i = 0; i < messageList.Count - msgLimit; i++)
                    {
                        this.messageList.RemoveAt(0);
                    }

                    needToReset = true;
                }
            }

            if (needToReset)
            {
                addMsgs = this.messageList;
            }

            messageRetriever.SetTimeFilter(DateTime.UtcNow); // For receiving only new messages

            return needToReset;
        }

        private bool AddByKeepUniqueGuid(IList<Message> mainList, IList<Message> toAddList)
        {
            bool removed = false;
            foreach (Message message in toAddList)
            {
                Message msgWithThisGuid = (from msg in mainList
                                       where msg.getGuid().Equals(message.getGuid())
                                       select msg).FirstOrDefault();

                if (msgWithThisGuid != null) //there is message with this GUID in the main list, so it was updated
                {
                    mainList.Remove(msgWithThisGuid);
                    removed = true;
                }
                mainList.Add(message);
            }

            return removed;
        }

        #region Filters
        public void SetGroupFilter(int? groupId)
        {
            bool filterChanged = messageRetriever.SetGroupFilter(groupId);
            if (filterChanged)
            {
                messageRetriever.SetTimeFilter(null);
            }
        }

        public void SetNicknameFilter(string nickname)
        {
            bool filterChanged = messageRetriever.SetNicknameFilter(nickname);
            if (filterChanged)
            {
                messageRetriever.SetTimeFilter(null);
            }
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
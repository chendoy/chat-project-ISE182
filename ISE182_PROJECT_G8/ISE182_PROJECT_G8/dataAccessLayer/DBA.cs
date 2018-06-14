using ISE182_PROJECT_G8.logicLayer;
using ISE182_PROJECT_G8.persistantLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.dataAccessLayer
{
    class DBA
    {
        // Access via Chatroom
        private readonly string server_address = "ise172.ise.bgu.ac.il,1433\\DB_LAB";
        private readonly string database_name = "MS3";
        private readonly string user_name = "publicUser";
        private readonly string password = "isANerd";

        private string connectionString;
        private QueryMessage query;

        public DBA()
        {
            this.connectionString = $"Data Source={server_address};Initial Catalog={database_name };User ID={user_name};Password={password}";
            query = new QueryMessage(connectionString);
        }

        public UserPL GetUser(int groupId, string nickname)
        {
            QueryUser userquey = new QueryUser(connectionString);
            userquey.SetGroupFilter(groupId);
            userquey.SetNicknameFilter(nickname);
            IList<UserPL> userlist = userquey.Select();
            if(userlist.Count == 1)
            {
                foreach(UserPL user in userlist)
                {
                    return user;
                }
                return null;
            }
            else
            {
                Logger.Instance.Fatal($"Found {userlist.Count.ToString()}  users with same nickname:{nickname} and id:{groupId.ToString()}");
                return null;
            }

        }

        public UserPL Register(int groupId, string nickname, string password)
        {
            return null;
        }

        public bool RetreiveMessages(ref ObservableCollection<Message> messages)
        {
            if (!query.HasTimeFilter())
            {
                messages.Clear();
            }

            bool isRetrieved = false; // query.Excute(this.connectionString, ref messages);
            if (isRetrieved)
            {
                query.SetTimeFilter(DateTime.UtcNow);
            }

            return isRetrieved;
        }

        public IList<Message> RetreiveMessages()
        {
            return null;
        }


        public Message SendMessage(DateTime sendTime, int groupId, string nickname, string body)
        {
            return null;
        }

        public bool UpdateMessage(Guid guid, DateTime updateTime, string body)
        {
            return false;
        }

        public void SetGroupFilter(int? groupId)
        {
            bool filterChanged = query.SetGroupFilter(groupId);
            
            if (filterChanged)
                query.SetTimeFilter(null);
        }

        public void SetNicknameFilter(string nickname)
        {
            bool filterChanged = query.SetNicknameFilter(nickname);

            if (filterChanged)
                query.SetTimeFilter(null);
        }

        public void ClearFilters()
        {
            query.ClearFilters();
            query.SetTimeFilter(null);
        }
    }
}

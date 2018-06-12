using ISE182_PROJECT_G8.logicLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.dataAccessLayer
{
    class MessageRetriever
    {
        // Access via Chatroom
        private readonly string server_address = "ise172.ise.bgu.ac.il,1433\\DB_LAB";
        private readonly string database_name = "MS3";
        private readonly string user_name = "publicUser";
        private readonly string password = "isANerd";

        private string connectionString;
        private QueryMessage query;

        public MessageRetriever()
        {
            this.connectionString = $"Data Source={server_address};Initial Catalog={database_name };User ID={user_name};Password={password}";
            query = new QueryMessage();
        }

        public bool RetreiveMessages(ref ObservableCollection<Message> messages)
        {
            if (!query.HasTimeFilter())
            {
                messages.Clear();
            }

            bool isRetrieved = query.Excute(this.connectionString, ref messages);
            if (isRetrieved)
            {
                query.SetTimeFilter(DateTime.UtcNow);
            }

            return isRetrieved;
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

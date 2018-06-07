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
    class DBA
    {
        private readonly string server_address = "ise172.ise.bgu.ac.il,1433\\DB_LAB";
        private readonly string database_name = "MS3";
        private readonly string user_name = "publicUser";
        private readonly string password = "isANerd";
    
        private SqlConnection connection;

        public DBA()
        {
            string connetion_string = null;
            connetion_string = $"Data Source={server_address};Initial Catalog={database_name };User ID={user_name};Password={password}";
            this.connection = new SqlConnection(connetion_string);
        }

        public ObservableCollection<Message> GetNewMessages(DateTime fromTime)
        {
            string selectFields = "*";
            string fromTable = "Messages";
            string dateToString = fromTime.ToString("yyyy'-'MM'-'dd HH':'mm':'ss'.'ms", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            string where = $"SendTime >= '{dateToString}'";
            Query query = new Query(selectFields, fromTable, where);
            return query.Excute<Message>(this.connection);
        }
    }
}

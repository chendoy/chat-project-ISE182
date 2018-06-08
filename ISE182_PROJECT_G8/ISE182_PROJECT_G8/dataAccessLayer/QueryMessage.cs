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
    class QueryMessage
    {
        // NEED TO ADD OPTION TO CLEAR FILTERS AND ADD THEM
        private readonly int retrieveCap = 200;
        private readonly string selectFields = "[Guid], [Group_Id], [Nickname], [SendTime], [Body]";
        private readonly string fromTable = "[dbo].[Messages] JOIN [dbo].[Users] ON [User_Id] = [Id]";
        
        private IList<IQueryFilter> filters;

        public QueryMessage()
        {
            this.filters = new List<IQueryFilter>();
        }

        private string WhereStatement()
        {
            string where = "";
            foreach (IQueryFilter filter in this.filters)
            {
                where += filter.GenerateWhereClause();
                where += " AND ";
            }

            where = where.Substring(0, where.Length - 5);
            if (String.IsNullOrWhiteSpace(where))
            {
                return "";
            }
            else
            {
                return "Where " + where;
            }
        }

        public void Excute(SqlConnection connection, ref ObservableCollection<Message> messages)
        {
            SqlCommand command;
            SqlDataReader data_reader;

            try
            {
                connection.Open();
                string sql_query = $"SELECT TOP {retrieveCap.ToString()} {selectFields} FROM {fromTable} {WhereStatement()};";
                command = new SqlCommand(sql_query, connection);
                data_reader = command.ExecuteReader();
                
                while (data_reader.Read())
                {
                    Guid.TryParse(data_reader.GetString(0), out Guid guid);
                    int groupId = data_reader.GetInt32(1);
                    string nickname = data_reader.GetString(2);
                    DateTime sendTime = data_reader.GetDateTime(3);
                    string body = data_reader.GetString(4);
                    messages.Add(new Message(guid, nickname, sendTime, body, groupId.ToString()));
                }
                
                data_reader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
                Console.WriteLine(ex.ToString());
            }
        }

    }
}

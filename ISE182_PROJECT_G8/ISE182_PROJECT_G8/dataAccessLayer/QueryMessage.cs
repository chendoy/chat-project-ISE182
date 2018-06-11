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
    class QueryMessage
    {
        private readonly int retrieveCap = 200;
        private readonly string selectFields = "[Guid], [Group_Id], [Nickname], [SendTime], [Body]";
        private readonly string fromTable = "[dbo].[Messages] JOIN [dbo].[Users] ON [User_Id] = [Id]";
        
        private IQueryFilter[] filters;
        private SqlCommand command;

        public QueryMessage()
        {
            this.filters = new IQueryFilter[3];
            ClearFilters();
            command = new SqlCommand();
        }

        private string WhereStatement()
        {
            command.Parameters.Clear();
            string where = "";
            bool first = true;
            foreach (IQueryFilter filter in this.filters)
            {
                if (filter != null)
                {
                    if (!first)
                    {
                        where += " AND ";
                    }

                    where += filter.GenerateWhereClause(command);
                    first = false;
                }                
            }

            if (!String.IsNullOrWhiteSpace(where))
            {
                where = "WHERE" + where;
            }

            return where;            
        }

        public bool Excute(SqlConnection connection, ref ObservableCollection<Message> messages)
        {
            
            SqlDataReader data_reader;

            try
            {
                connection.Open();
                command.Connection = connection;
                string sql_query = $"SELECT TOP {retrieveCap.ToString()} {selectFields} FROM {fromTable} {WhereStatement()};";
                command.CommandText = sql_query;
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
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
                Logger.Instance.Error(ex.ToString());
                return false;
            }
        }

        public void SetTimeFilter(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                this.filters[0] = new TimeFilter(dateTime.Value);
            }
            else
            {
                this.filters[0] = null;
            }
        }

        public bool HasTimeFilter()
        {
            return this.filters[0] != null;
        }

        public void SetGroupFilter(int? groupId)
        {
            if (groupId.HasValue)
            {
                this.filters[1] = new GroupFilter(groupId.Value);
            }
            else
            {
                this.filters[1] = null;
            }
        }

        public void SetNicknameFilter(string nickname)
        {
            if (nickname != null)
            {
                this.filters[2] = new NicknameFilter(nickname);

            }
            else
            {
                this.filters[2] = null;
            }
        }

        public void ClearFilters()
        {
            for (int i = 0; i < this.filters.Length; i++)
            {
                this.filters[i] = null;
            }
        }
    }
}

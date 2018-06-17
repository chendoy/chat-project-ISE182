using ISE182_PROJECT_G8.logicLayer;
using ISE182_PROJECT_G8.persistantLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.dataAccessLayer
{
    class QueryMessage : Query<Message>
    {
        private readonly int retrieveCap = 200;
        private readonly string selectFields = "[Guid], [Group_Id], [Nickname], [SendTime], [Body]";
        private readonly string fromTable = "[MS3].[dbo].[Messages] JOIN [MS3].[dbo].[Users] ON [User_Id] = [Id]";
        private readonly string msgtable = "[MS3].[dbo].[Messages]";


        public QueryMessage(string connectionString) : base(connectionString)
        {
        }

        public override IList<Message> Select()
        {
            SqlDataReader data_reader;
            List<Message> messages = new List<Message>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Logger.Instance.Info("Connected to the database");
                    command.Connection = connection;
                    string sql_query = $"SELECT TOP {retrieveCap.ToString()} {selectFields} FROM {fromTable} {WhereStatement()} ORDER BY [SendTime] DESC;";
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

                    messages.Reverse(); // send the messages via sendTime ASC
                    data_reader.Close();
                    command.Dispose();
                    return messages;
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex.ToString());
                return new List<Message>();
            }
        }

        public bool Update(Guid guid, DateTime sendTime, string body)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Logger.Instance.Info("Connected to the database");
                    command.Connection = connection;

                    SqlParameter body_param = new SqlParameter(@"body", SqlDbType.NChar, 100);
                    body_param.Value = body;
                    command.Parameters.Add(body_param);
                    SqlParameter date_param = new SqlParameter(@"sendTime", SqlDbType.DateTime, 64);
                    date_param.Value = sendTime;
                    command.Parameters.Add(date_param);


                    string sql_query = $"UPDATE  {msgtable} SET SendTime = {sendTime},Body = {body});";
                    command.CommandText = sql_query;
                    int num_rows_changed = command.ExecuteNonQuery();
                    command.Dispose();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex.ToString());
                return false;
            }
        }

        public bool Insert(DateTime sendTime, int userId, string body)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    Guid msgGuid = Guid.NewGuid();
                    connection.Open();
                    Logger.Instance.Info("Connected to the database");
                    command.Connection = connection;
                    SqlParameter guid_param = new SqlParameter(@"msgGuid", SqlDbType.Char, 68);
                    guid_param.Value = msgGuid.ToString();
                    command.Parameters.Add(guid_param);
                    SqlParameter userid_param = new SqlParameter(@"userId", SqlDbType.Int, 32);
                    userid_param.Value = userId;
                    command.Parameters.Add(userid_param);
                    SqlParameter date_param = new SqlParameter(@"sendTime", SqlDbType.DateTime, 8);
                    date_param.Value = sendTime;
                    command.Parameters.Add(date_param);
                    SqlParameter body_param = new SqlParameter(@"body", SqlDbType.NChar, 100);
                    body_param.Value = body;
                    command.Parameters.Add(body_param);
                    string insertfields = "Guid,User_Id,SendTime,Body";
                    string values = "@msgGuid, @userId , @sendTime , @body";
                    string sql_query = $"INSERT INTO {msgtable} ({insertfields}) VALUES ({values});";
                    command.CommandText = sql_query;
                    int num_rows_changed = command.ExecuteNonQuery();
                    command.Dispose();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex.ToString());
                return false;
            }
        }
    }
}

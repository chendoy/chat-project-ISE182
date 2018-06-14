using ISE182_PROJECT_G8.logicLayer;
using ISE182_PROJECT_G8.persistantLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.dataAccessLayer
{
    class QueryUser : Query<UserPL>
    {
        private readonly string selectFields = "[Group_Id], [Nickname], [Password]";
        private readonly string fromTable = "[dbo].[Users]";

        public QueryUser(string connectionString) : base(connectionString)
        {
        }

        public override IList<UserPL> Select()
        {
            SqlDataReader data_reader;
            IList<UserPL> users = new List<UserPL>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Logger.Instance.Info("Connected to the database");
                    command.Connection = connection;
                  
                    string sql_query = $"SELECT {selectFields} FROM {fromTable} {WhereStatement()};";
                    command.CommandText = sql_query;
                    data_reader = command.ExecuteReader();

                    while (data_reader.Read())
                    {
                        int groupId = data_reader.GetInt32(0);
                        string nickname = data_reader.GetString(1).Trim();
                        string password = data_reader.GetString(2).Trim();
                        users.Add(new UserPL(nickname, groupId,password));
                    }

                    data_reader.Close();
                    command.Dispose();
                    return users;
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex.ToString());
                return new List<UserPL>();
            }
        }

        public bool Insert(int groupId, string nickname, string password)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Logger.Instance.Info("Connected to the database");
                    command.Connection = connection;

                    SqlParameter nickname_param = new SqlParameter(@"nickname", SqlDbType.Char, 8);
                    nickname_param.Value = nickname;
                    command.Parameters.Add(nickname_param);
                    SqlParameter password_param = new SqlParameter(@"password", SqlDbType.Char, 64);
                    password_param.Value = password;
                    command.Parameters.Add(password_param);
                    SqlParameter groupid_param = new SqlParameter(@"groupId", SqlDbType.Int, 20);
                    groupid_param.Value = groupId;
                    command.Parameters.Add(groupid_param);
                    string values = "@groupId , @nickname , @password";

                    string sql_query = $"INSERT INTO {fromTable} ({selectFields}) VALUES ({values});";
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

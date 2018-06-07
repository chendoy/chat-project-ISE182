using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.dataAccessLayer
{
    class Query
    {
        private string selectFields;
        private string fromTable;
        private string where;

        public Query(string selectFields, string fromTable, string where)
        {
            this.selectFields = selectFields;
            this.fromTable = fromTable;
            this.where = where;
        }

        public ObservableCollection<T> Excute<T>(SqlConnection connection)
        {
            SqlCommand command;
            SqlDataReader data_reader;

            try
            {
                ObservableCollection<T>  objects = new ObservableCollection<T>();

                connection.Open();
                string sql_query = $"SELECT {selectFields} FROM [dbo].[{fromTable}] WHERE {where};";
                command = new SqlCommand(sql_query, connection);
                data_reader = command.ExecuteReader();
                if (fromTable == "Messages")
                {
                    while (data_reader.Read())
                    {
                        DateTime dateFacturation = new DateTime();
                        if (!data_reader.IsDBNull(2))
                            dateFacturation = data_reader.GetDateTime(2); //2 is the coloumn index of the date. There are such               
                        Console.WriteLine("GUID: " + data_reader.GetValue(0) + "UserID: " + data_reader.GetValue(1) + ", Message date: " + dateFacturation.ToString() + ", Body: " + data_reader.GetValue(3));
                    }
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

            return null;
        }
    }
}

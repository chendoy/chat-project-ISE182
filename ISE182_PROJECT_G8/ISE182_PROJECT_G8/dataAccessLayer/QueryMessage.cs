using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.dataAccessLayer
{
    class QueryMessage
    {
        private string selectFields;
        private string fromTable;
        private IList<IQueryFilter> filters;

        public QueryMessage(string selectFields, string fromTable)
        {
            this.selectFields = selectFields;
            this.fromTable = fromTable;
            this.filters = new List<IQueryFilter>();
        }

        public ObservableCollection<T> Excute<T>(SqlConnection connection)
        {
            SqlCommand command;
            SqlDataReader data_reader;

            try
            {
                ObservableCollection<T> objects = new ObservableCollection<T>();

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

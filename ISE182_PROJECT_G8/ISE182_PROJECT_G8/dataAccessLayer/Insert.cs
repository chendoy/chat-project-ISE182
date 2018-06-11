using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ISE182_PROJECT_G8.dataAccessLayer
{
    class Insert
    {
        private string InsertTable;
        private string Insertcolumns;
        private string InsertValue;

        public Insert(string InsertTable, string Insertcolumns, string InsertValue)
        {
            this.InsertTable = InsertTable;
            this.Insertcolumns = Insertcolumns;
            this.InsertValue = InsertValue;
        }

        public Boolean Excute(SqlConnection connection)
        {
            SqlCommand command;
            SqlDataReader data_reader;

            try
            {
                connection.Open();
                string sql_query = $"INSERT TO [dbo].[{InsertTable}] ({Insertcolumns}) ({InsertValue});";
                command = new SqlCommand(sql_query, connection);
                data_reader = command.ExecuteReader();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
}

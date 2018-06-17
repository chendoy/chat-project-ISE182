using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.dataAccessLayer
{
    class TimeFilter : IQueryFilter
    {
        private readonly string dateField = "SendTime";
        private DateTime dateTime;

        public TimeFilter(DateTime dateTime)
        {
            this.dateTime = dateTime;
        }

        public string GenerateWhereClause(SqlCommand command)
        {
            //string dateToString = this.dateTime.ToString("yyyy'-'MM'-'dd HH':'mm':'ss'.'fff", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            SqlParameter date_param = new SqlParameter(@"dateTime", SqlDbType.DateTime, 8);
            date_param.Value = dateTime;
            command.Parameters.Add(date_param);
            string whereClause = $"{dateField} >= @dateTime";
            return whereClause;
        }
    }
}

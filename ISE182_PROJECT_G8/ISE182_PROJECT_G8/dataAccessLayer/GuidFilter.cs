using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.dataAccessLayer
{
    class GuidFilter : IQueryFilter
    {
        private readonly string guidField = "Guid";
        private Guid guid;

        public GuidFilter(Guid guid)
        {
            this.guid = guid;
        }

        public override bool Equals(object obj)
        {
            var filter = obj as GuidFilter;
            return filter != null &&
                   guid == filter.guid;
        }

        public string GenerateWhereClause(SqlCommand command)
        {
            string where = $"{guidField} = @guid";
            SqlParameter guid_param = new SqlParameter(@"guid", SqlDbType.Char, 68);
            guid_param.Value = this.guid.ToString();
            command.Parameters.Add(guid_param);
            return where;
        }
    }
}

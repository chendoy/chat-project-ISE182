using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.dataAccessLayer
{
    class GroupFilter : IQueryFilter
    {
        private readonly string groupField = "Group_Id";
        private int groupId;

        public GroupFilter(int groupId)
        {
            this.groupId = groupId;
        }

        public override bool Equals(object obj)
        {
            var filter = obj as GroupFilter;
            return filter != null &&
                   groupId == filter.groupId;
        }

        public string GenerateWhereClause(SqlCommand command)
        {
            SqlParameter groupid_param = new SqlParameter(@"groupId", SqlDbType.Int, 20);
            groupid_param.Value = groupId;
            command.Parameters.Add(groupid_param);
            return $"{groupField} = {groupId.ToString()}";
        }
    }
}

using System;
using System.Collections.Generic;
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
            return $"{groupField} = {groupId.ToString()}";
        }

        
    }
}

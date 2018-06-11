using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.dataAccessLayer
{
    class NicknameFilter : IQueryFilter
    {
        private readonly string nicknameField = "Nickname";
        private string nickname;

        public NicknameFilter(string nickname)
        {
            this.nickname = nickname;
        }

        public string GenerateWhereClause(SqlCommand command)
        {
            string where = $"{nicknameField} = @nickname";
            SqlParameter nickname = new SqlParameter(@"nickname", this.nickname);
            command.Parameters.Add(nickname);
            return where;
        }
    }
}

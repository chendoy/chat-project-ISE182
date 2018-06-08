using System;
using System.Collections.Generic;
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

        public string GenerateWhereClause()
        {
            return $"{nicknameField} = {this.nickname}";
        }
    }
}

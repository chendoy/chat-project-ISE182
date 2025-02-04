﻿using System;
using System.Collections.Generic;
using System.Data;
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

        public override bool Equals(object obj)
        {
            var filter = obj as NicknameFilter;
            return filter != null &&
                   nickname == filter.nickname;
        }

        public string GenerateWhereClause(SqlCommand command)
        {
            string where = $"{nicknameField} = @nickname";
            SqlParameter nickname_param = new SqlParameter(@"nickname", SqlDbType.Char, 8);
            nickname_param.Value = this.nickname;
            command.Parameters.Add(nickname_param);
            return where;
        }


    }
}

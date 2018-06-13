using ISE182_PROJECT_G8.logicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.dataAccessLayer
{
    class QueryUser : Query<User>
    {
        public QueryUser(string connectionString) : base(connectionString)
        {
        }

        public override IList<User> Select()
        {
            throw new NotImplementedException();
        }

        public bool Insert(int groupId, string nickname, string password)
        {
            return false;
        }
    }
}

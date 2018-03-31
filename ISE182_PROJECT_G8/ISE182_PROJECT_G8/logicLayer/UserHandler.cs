using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.logicLayer
{
    public static class UserHandler
    {
        public static Boolean existIn(User user, List<User> userList)
        {
            var loggedin = (from user_in_list in userList
                            where user_in_list.getNickname().Equals(user.nickname)
                            select user_in_list).FirstOrDefault();

            return loggedin != null;
        }
    }
}

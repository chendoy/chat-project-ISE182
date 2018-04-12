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
                            where (user_in_list.getNickname().Equals(user.getNickname())) && (user_in_list.getGroupID()==user.getGroupID())
                            select user_in_list).FirstOrDefault();

            return loggedin != null;
        }
        
        public static Boolean isValid(String nickname, int groupID)
        {
            Boolean nicknameVaild = (nickname.Length > 0) && (!nickname.Contains(" "));
            Boolean groupIDValid = groupID >= 1 & groupID <= 40;

            return groupIDValid & nicknameVaild;
        }
    }
}

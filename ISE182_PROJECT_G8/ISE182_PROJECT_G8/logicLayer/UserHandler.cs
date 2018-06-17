using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISE182_PROJECT_G8.persistantLayer;

namespace ISE182_PROJECT_G8.logicLayer
{
    /* This class handle all the processes which
     * need to done on users purely
     */
    public static class UserHandler
    {
        private const int maxNicknameLength = 8; //magic number

        //returns true if 'user' exists in 'userList' using a linq query//
        public static Boolean existIn(User user, List<User> userList)
        {
            var loggedin = (from user_in_list in userList
                            where (user_in_list.GetNickname().Equals(user.GetNickname())) && (user_in_list.GetGroupID()==user.GetGroupID())
                            select user_in_list).FirstOrDefault();

            return loggedin != null;
        }
        
        public static Boolean isValid(String nickname, int groupID)
        {
            Boolean nicknameVaild = (nickname.Length > 0) && nickname.Length<= maxNicknameLength && (!nickname.Contains(" "));
            Boolean groupIDValid = groupID >= 1;
            Logger.Instance.Info("User and group id validation performed");
            return groupIDValid & nicknameVaild;
        }
    }
}

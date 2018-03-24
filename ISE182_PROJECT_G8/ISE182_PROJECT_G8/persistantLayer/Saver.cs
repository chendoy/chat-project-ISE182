using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISE182_PROJECT_G8.logicLayer;

namespace ISE182_PROJECT_G8.persistantLayer
{
    static class Saver
    {
        private static String path= "C:\Users\חן\source\repos\ISE182_PROJECT_G8\ISE182_PROJECT_G8\ISE182_PROJECT_G8\persistantLayer";
        private static List<User> persistantUserList;
        private static List<Message> persistantMessageList;

       /*  static Saver()
        {
            this.persistantMessageList = new List<Message>();
            this.persistantUserList = new List<User>();
        }
        */

        public static void saveUser(User userToSave)
        {
            this.persistantUserList.Add(userToSave);

        }
    }

}

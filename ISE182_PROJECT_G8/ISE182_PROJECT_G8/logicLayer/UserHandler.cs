using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.logicLayer
{
    public static class UserHandler
    {
        public static void log_inOrOff()
        {
            Console.WriteLine("Enter User Name:");
            String nickname = Console.ReadLine();
            User newUser = new User(nickname);    //todo: check if it doesn't already exist//
           

        }
    }
}

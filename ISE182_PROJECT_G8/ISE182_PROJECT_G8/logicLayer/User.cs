using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.logicLayer
{
    public class User
    {
        public String nickname;
        public int status;

        //constructor//
        public User(String nickname)
        {
            this.nickname = nickname;
            this.status = 0;
        }

        public void loginOrOff()
        {
            if (status == 0)

            { status = 1; Console.WriteLine(this.nickname + " Logged-in Successfully"); }

            else
            { status = 0; Console.WriteLine(this.nickname + " Logged-off Successfully"); }
        }
        public void retreive_n_messages(int n)
        {
            Console.WriteLine("complete this method");
            Console.ReadLine();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISE182_PROJECT_G8.CommunicationLayer;

namespace ISE182_PROJECT_G8.logicLayer
{
    [Serializable]
    public class User
    {
        public String nickname;
        public int status;  //0 means logged-of, 1 means logged-in//

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
        public void retreive_n_messages(int n) //todo: implement this method//
        {
            Console.WriteLine("complete this method");
            Console.ReadLine();
        }
        public void send()
        {
            Console.Write("Please Enter Your Message:");
            String content = Console.ReadLine();
            IMessage msg = Communication.Instance.Send(Chatroom._url, "8", this.nickname, content);
            
        }


        public String toString()
        {
            return this.nickname;
        }

    }
}

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
        public int status;  //0 - logged off, 1 - logged-in//

        public User(String nickname)
        {
            this.nickname = nickname;
            this.status = 0; 
        }

        public String getNickname()
        {
            return this.nickname;
        }

        public String toString()
        {
            return this.nickname;
        }

        public void loginOrOff() //flips the user login status - log-in if it was off and vice versa//
        {
            if (status == 0)

            { status = 1; Console.WriteLine(this.nickname + " Logged-in Successfully"); }

            else
            { status = 0; Console.WriteLine(this.nickname + " Logged-off Successfully"); }
        }

        public void log_out(Chatroom chatRoom)
        {
            chatRoom.logUserOut();
            this.loginOrOff();
        }

        public Message send(String url, String _group)
        {
            Console.Write("Please Enter Your Message: ");
            String content = Console.ReadLine();
            return new Message (Communication.Instance.Send(url, _group, this.nickname, content));
        }
    }
}

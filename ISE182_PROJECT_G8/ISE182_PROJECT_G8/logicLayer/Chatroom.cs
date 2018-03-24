using logicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.logicLayer
{
    class Chatroom
    {
        private  static  String _url = "http://ise172.ise.bgu.ac.il" ;
        private int port=80;
        private List<User> loggedInUsers;
        private List<User> userList;
        private List<Message> messageList;

        public Chatroom()
        {
            this.loggedInUsers = new List<User>();
            this.userList = new List<User>();
            this.messageList = new List<Message>();
        }

        public void chatroomInit()
        {

        }

        public void register()
        {
            String nickname;
            Console.WriteLine("Please Enter Nickname:\n");
            nickname = Console.ReadLine();
            User newUser = new User(nickname);
            persistantLayer.Saver.
        }

    }
}

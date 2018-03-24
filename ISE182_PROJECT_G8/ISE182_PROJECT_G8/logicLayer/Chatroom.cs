using ISE182_PROJECT_G8.logicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.logicLayer
{
    public class Chatroom
    {
        private  static  String _url = "http://ise172.ise.bgu.ac.il" ;
        private int port=80;
        private User loggedInUser;
        private List<User> userList;
        private List<Message> messageList;

        public Chatroom()
        {
            this.loggedInUser = null;
            this.userList = new List<User>();
            this.messageList = new List<Message>();
        }

        public void Register()
        {
            Console.WriteLine("Enter User Name to Register:");
            String nickname = Console.ReadLine();
            User newUser = new User(nickname);    //todo: check if it doesn't already exist//
            this.userList.Add(newUser);
            persistantLayer.Saver.saveUser(newUser);
            Console.WriteLine("Registration was Scuccessfull");
        }

        public void log_inOrOff()
        {
            String nickname = Console.ReadLine();
            
        }






    }
}

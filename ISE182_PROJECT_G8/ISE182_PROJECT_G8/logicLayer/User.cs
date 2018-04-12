using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISE182_PROJECT_G8.CommunicationLayer;
using ISE182_PROJECT_G8.presentationLayer;
using ISE182_PROJECT_G8.persistantLayer;

namespace ISE182_PROJECT_G8.logicLayer
{
    [Serializable]
    public class User
    {
        private String nickname;
        private int groupID;
        private int status;  //0 - logged off, 1 - logged-in//

        public User(String nickname, int groupID)
        {
            if (UserHandler.isValid(nickname, groupID))
            {
                this.nickname = nickname;
                this.status = 0;
                this.groupID = groupID;
            }
            else
            {
                Logger.Instance.Error("Failed to create User: "+nickname+" of group "+ groupID+" already exist");
                present_handler.output("Error: nickname or Group ID is not valid");
            }

        }

        public String getNickname()
        {
            return this.nickname;
        }

        public int getGroupID()
        {
            return this.groupID;
        }

        public String toString()
        {
            return this.nickname+"["+this.getGroupID()+"]";
        }

        public void loginOrOff() //flips the user login status - log-in if it was off and vice versa//
        {
            if (status == 0)
            {
                status = 1;
            }
            else
            {
                status = 0;
            }
        }


        public Message Send(String url, String message)
        {
            return new Message (Communication.Instance.Send(url, this.groupID.ToString(), this.nickname, message));
        }
    }
}

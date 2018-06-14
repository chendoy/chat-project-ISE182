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
    /* This class stores a user info
     */
    [Serializable]
    public class User : UserPL
    {
        private int status;  //0 - logged off, 1 - logged-in//

        public User(String nickname, int groupID, string password) : base(nickname, groupID, password)

        {
            this.status = 0; //fresh user is initially logged-off//
        }

        public User(UserPL userpl) : base(userpl.GetNickname(), userpl.GetGroupID(), userpl.GetPassword())
        {
            this.status = 0;
        }
        
        public void loginOrOff() //flips the user login status - log-in if it was off and vice versa//
        {
            if (status == 0) status = 1;
            else             status = 0;
        }
        
        public Message Send(String url, String message)
        {
            Logger.Instance.Info("Message was sent successfully by communication module");
            return new Message (Communication.Instance.Send(url, this.GetGroupID().ToString(), this.GetNickname(), message));
        }
    }
}

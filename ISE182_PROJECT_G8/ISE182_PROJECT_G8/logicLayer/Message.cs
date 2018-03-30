using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISE182_PROJECT_G8.logicLayer;
using ISE182_PROJECT_G8.CommunicationLayer;

namespace ISE182_PROJECT_G8.logicLayer
{
    [Serializable]
    public class Message
    {
        Guid Id { get; }
        string UserName { get; }
        DateTime Date { get; }
        string MessageContent { get; }
        string GroupID { get; }

        public Message (IMessage msg)
        {
            this.Id = msg.Id;
            this.UserName = msg.UserName;
            this.Date = msg.Date;
            this.MessageContent = msg.MessageContent;
            this.GroupID = msg.GroupID;
        }
        private Boolean Check (object obj, int n) {
            throw new NotImplementedException();
        }

        public String getUserName()
        {
            return this.UserName;
        }
        public String getContent()
        {
            return this.MessageContent;
        }
        public DateTime getTime()
        {
            return this.Date;
        }

        public String toString()
        {
            return this.Date + " " + this.UserName + ": " + this.MessageContent;
        }
        /*bool ICheckable.Check(object var, int n)
        {
            throw new NotImplementedException();
        }
        */
    }
}

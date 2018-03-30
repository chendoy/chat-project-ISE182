using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISE182_PROJECT_G8.logicLayer;
using ISE182_PROJECT_G8.CommunicationLayer;

namespace ISE182_PROJECT_G8.logicLayer
{
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

        /*bool ICheckable.Check(object var, int n)
        {
            throw new NotImplementedException();
        }
        */
    }
}

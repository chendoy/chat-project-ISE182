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
            return "Guid: " + this.Id  +"\n"+"Time: " +this.Date  +"\n"+ "User: "+ this.UserName + "[" + this.GroupID + "]" + "\n" +"Message: "+ this.MessageContent+"\n";
        }
        public Guid getGuid()
        {
            return this.Id;
        }
    
    }
}

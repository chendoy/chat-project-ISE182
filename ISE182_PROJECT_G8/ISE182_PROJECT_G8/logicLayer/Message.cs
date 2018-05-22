using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISE182_PROJECT_G8.logicLayer;
using ISE182_PROJECT_G8.CommunicationLayer;

namespace ISE182_PROJECT_G8.logicLayer
{
    /* This class stores a message info
     */

    [Serializable]
    public class Message
    {
        private Guid Id { get; }
        private string UserName { get; }
        private DateTime Date { get; }
        private string MessageContent { get; }
        private string GroupID { get; }

        private static TimeZoneInfo timezone = TimeZoneInfo.Local;

        //IMessage to Message constructor//
        public Message(IMessage msg)
        {
            this.Id = msg.Id;
            this.UserName = msg.UserName;
            this.Date = TimeZoneInfo.ConvertTimeFromUtc(msg.Date, timezone);
            this.MessageContent = msg.MessageContent;
            this.GroupID = msg.GroupID;
        }

        //getters and setters://

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

        public override String ToString()
        {
            String format = "{0} {1} - {2}[{3}] - {4}";
            return String.Format(format, getTime().ToShortTimeString(), getTime().ToShortDateString(), getUserName(), getGroupId(), getContent());
        }
        public Guid getGuid()
        {
            return this.Id;
        }
        public int getGroupId()
        {
            return Convert.ToInt32(this.GroupID);
        }
    
    }
}

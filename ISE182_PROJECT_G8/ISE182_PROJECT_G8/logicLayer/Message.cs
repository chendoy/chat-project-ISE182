using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISE182_PROJECT_G8.logicLayer;
using ISE182_PROJECT_G8.CommunicationLayer;

namespace ISE182_PROJECT_G8.logicLayer
{
    class Message : logicLayer.ICheckable
    {
        Guid Id { get; }
        string UserName { get; }
        DateTime Date { get; }
        string MessageContent { get; }
        string GroupID { get; }

        public Message(String MessageContent, string GroupID, Guid Id)
        {
            this.MessageContent = MessageContent;
            this.GroupID = GroupID;
            this.Id = Id;
        }
        private Boolean Check (object obj, int n) {
            throw new NotImplementedException();
        }

        bool ICheckable.Check(object var, int n)
        {
            throw new NotImplementedException();
        }

    }
}

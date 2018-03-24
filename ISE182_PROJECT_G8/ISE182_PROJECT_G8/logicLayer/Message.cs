using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISE182_PROJECT_G8.logicLayer;
using ISE182_PROJECT_G8.logicLayer;

namespace ISE182_PROJECT_G8.logicLayer
{
    class Message : logicLayer.ICheckable
    {
        private String body;
        private User author;

        public Message(String body, User author)
        {
            this.body = body;
            this.author = author;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.logicLayer
{
    abstract class Sorter
    {
        private List<Message> list;
        public Sorter(List<Message> list)
        {
            this.list = list;
        }
        abstract public List<Message> Sort(List<Message> messages);
    }
}

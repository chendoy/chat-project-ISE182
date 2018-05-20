using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.logicLayer
{
    class SortByTime : Sorter
    {
        public SortByTime(List<Message> list) : base(list) { }


        public override List<Message> Sort(List<Message> messages)
        {
            List<Message> sorted = new List<Message>();
            sorted = getlist().OrderBy(o => o.getTime()).ToList();
            return sorted;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.logicLayer
{
    class SortByTime : Sorter
    {
        private  List<Message> sorttime;
        public SortByTime(List<Message> sorttime)
        {
            this.sorttime = sorttime;
        }
        public override List<Message> Sort(List<Message> messages)
        {
            List<Message> sorted = new List<Message>();
            sorted = sorttime.OrderBy(o => o.getTime()).ToList();
            return sorted;
        }
    }
}

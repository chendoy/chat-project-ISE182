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
        private Boolean isAsc;
        public Sorter(List<Message> list,Boolean Asc)
        {
            this.list = list;
            isAsc = Asc;
        }
        abstract public List<Message> Sort(List<Message> messages);
        public List<Message> getlist()
        {
            return list;
        }
        public Boolean getisAsc()
        {
            return isAsc;
        }
        public class SortByTime : Sorter
        {
            public SortByTime(List<Message> list,Boolean isA) : base(list,isA) { }


            public override List<Message> Sort(List<Message> messages)
            {
                List<Message> sorted = new List<Message>();
                if(getisAsc())
                {
                    sorted = getlist().OrderBy(o => o.getTime()).ToList();
                }
                else
                {
                    sorted = getlist().OrderByDescending(o => o.getTime()).ToList();
                }
               
                return sorted;
            }
        }
        public class SortByNickname : Sorter
        {
            public SortByNickname(List<Message> list, Boolean isA) : base(list, isA) { }


            public override List<Message> Sort(List<Message> messages)
            {
                List<Message> sorted = new List<Message>();
                if (getisAsc())
                {
                    //sorted = getlist().OrderBy(o => o.getUserName()).ToList();
                    sorted = (from msg in getlist() orderby msg.getUserName() select msg).ToList();
                }
                else
                {
                    //sorted = getlist().OrderByDescending(o => o.getTime()).ToList();
                    sorted = (from msg in getlist() orderby msg.getUserName() descending select msg).ToList();
                }

                return sorted;
            }
        }
        public class SortByAll : Sorter
        {
            public SortByAll(List<Message> list, Boolean isA) : base(list, isA) { }


            public override List<Message> Sort(List<Message> messages)
            {
                List<Message> sorted = new List<Message>();
                if (getisAsc())
                {
                    sorted = (from msg in getlist() orderby msg.getGroupId(),msg.getUserName(),msg.getTime()  select msg).ToList();
                }
                else
                {
                    sorted = (from msg in getlist() orderby msg.getGroupId(), msg.getUserName(), msg.getTime() descending select msg).ToList();
                }

                return sorted;
            }
        }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.logicLayer
{
    // Responsible for filtering
    abstract class Filter
    {
        private List<Message> list;
        public Filter (List<Message> list)
        {
            this.list = list;
        }
        abstract public List<Message> filter();
        public List<Message> getlist()
        {
            return list;
        }
        public void setlist(List<Message> newlist)
        {
            this.list = newlist;
        }
        public class FilterByGroupID : Filter
        {
            private int gid;
            public FilterByGroupID(List<Message> list, Boolean isA,int id) : base(list) {
                this.gid = id;
            }
            public int getid()
            {
                return this.gid;
            }
            public void setid(int newid)
            {
                this.gid = newid;
            }
            public override List<Message> filter()
            {
                List<Message> sorted = new List<Message>();
                sorted = (from msg in getlist() where msg.getGroupId()==getid() select msg).ToList();
                return sorted;
            }
        }
        public class FilterByNickname : Filter
        {
            private String nickname;
            public FilterByNickname(List<Message> list, Boolean isA, String name) : base(list)
            {
                this.nickname = name;
            }
            public String getnickname()
            {
                return nickname;
            }
            public void setnickname(String name)
            {
                nickname = name;
            }

            public override List<Message> filter()
            {
                List<Message> sorted = new List<Message>();
                sorted = (from msg in getlist() where msg.getUserName().Equals(getnickname()) select msg).ToList();
                return sorted;
            }
        }
    }
}

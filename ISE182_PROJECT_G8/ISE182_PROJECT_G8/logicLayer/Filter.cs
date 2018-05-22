using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.logicLayer
{
    // Responsible for filtering
    public abstract class Filter
    {
        private ObservableCollection<Message> list;
        int gid;
        public Filter(ObservableCollection<Message> list, int id)
        {
            this.list = list;
            this.gid = id;
        }
        abstract public ObservableCollection<Message> filter();
        public ObservableCollection<Message> getlist()
        {
            return list;
        }
        public void setgid(int id)
        {
            this.gid = id;
        }
        public int getgid()
        {
            return gid;
        }
        public void setlist(ObservableCollection<Message> newlist)
        {
            this.list = newlist;
        }
    }
    public class FilterByGroupID : Filter
    {
        public FilterByGroupID(ObservableCollection<Message> list, int id) : base(list, id) {
        }
        public override ObservableCollection<Message> filter()
        {
            ObservableCollection<Message> sorted = new ObservableCollection<Message>();
            List<Message> list = new List<Message>(getlist());
            list = (from msg in list where msg.getGroupId()==getgid() select msg).ToList();
            foreach (Message message in list)
            {
                sorted.Add(message);
            }
            return sorted;
        }
    }
    public class FilterByNickname : Filter
    {
        private String nickname;
        public FilterByNickname(ObservableCollection<Message> list, String name, int id) : base(list,id)
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
        public override ObservableCollection<Message> filter()
        {
            ObservableCollection<Message> sorted = new ObservableCollection<Message>();
            List<Message> list = new List<Message>(getlist());
            list = (from msg in list where msg.getGroupId() == getgid()& msg.getUserName().Equals(getnickname()) select msg).ToList();
            foreach (Message message in list)
            {
                sorted.Add(message);
            }
            return sorted;
        }
    }
}

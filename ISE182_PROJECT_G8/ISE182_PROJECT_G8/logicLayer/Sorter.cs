using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.logicLayer
{
    public abstract class Sorter
    {
        private ObservableCollection<Message> list;
        private Boolean isAsc;
        public Sorter(ObservableCollection<Message> list,Boolean Asc)
        {
            this.list = list;
            isAsc = Asc;
        }
        abstract public ObservableCollection<Message> Sort();
        public ObservableCollection<Message> getlist()
        {
            return list;
        }
        public Boolean getisAsc()
        {
            return isAsc;
        }
        public void SetAsc(Boolean B)
        {
            this.isAsc = B;
        }
        public void setlist(ObservableCollection<Message> newlist)
        {
            this.list = newlist;
        }
        
    }
    public class SortByTime : Sorter
    {
        public SortByTime(ObservableCollection<Message> list, Boolean isA) : base(list, isA) { }


        public override ObservableCollection<Message> Sort()
        {
            ObservableCollection<Message> sorted = new ObservableCollection<Message>();
            List<Message> list = new List<Message>(getlist());
            if (getisAsc())
            {
                list = getlist().OrderBy(o => o.getTime()).ToList();
            }
            else
            {
                list = getlist().OrderByDescending(o => o.getTime()).ToList();
            }
            
            foreach (Message message in list)
            {
                sorted.Add(message);
            }
            return sorted;
        }
    }
    public class SortByNickname : Sorter
    {
        public SortByNickname(ObservableCollection<Message> list, Boolean isA) : base(list, isA) { }


        public override ObservableCollection<Message> Sort()
        {
            ObservableCollection<Message> sorted = new ObservableCollection<Message>();
            List<Message> list = new List<Message>(getlist());
            if (getisAsc())
            {
                //sorted = getlist().OrderBy(o => o.getUserName()).ToList();
                list = (from msg in getlist() orderby msg.getUserName() select msg).ToList();
            }
            else
            {
                //sorted = getlist().OrderByDescending(o => o.getTime()).ToList();
                list = (from msg in getlist() orderby msg.getUserName() descending select msg).ToList();
            }
            foreach (Message message in list)
            {
                sorted.Add(message);
            }

            return sorted;
        }
    }
    public class SortByAll : Sorter
    {
        public SortByAll(ObservableCollection<Message> list, Boolean isA) : base(list, isA) { }


        public override ObservableCollection<Message> Sort()
        {
            ObservableCollection<Message> sorted = new ObservableCollection<Message>();
            List<Message> list = new List<Message>(getlist());
            if (getisAsc())
            {
                list = (from msg in getlist() orderby msg.getGroupId(), msg.getUserName(), msg.getTime() select msg).ToList();
            }
            else
            {
                list = (from msg in getlist() orderby msg.getGroupId() descending, msg.getUserName() descending, msg.getTime() descending select msg).ToList();
            }
            foreach (Message message in list)
            {
                sorted.Add(message);
            }

            return sorted;
        }
    }
}

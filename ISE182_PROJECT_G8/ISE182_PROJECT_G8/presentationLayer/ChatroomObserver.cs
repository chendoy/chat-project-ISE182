using ISE182_PROJECT_G8.logicLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.presentationLayer
{
    public class ChatroomObserver : INotifyPropertyChanged
    {
        public ChatroomObserver()
        {
            Messages.CollectionChanged += Messages_CollectionChanged;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string message;
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }

        private Boolean gidEnable;
        public Boolean GidEnable
        {
            get
            {
                return gidEnable;
            }
            set
            {
                gidEnable = value;
                OnPropertyChanged("GidEnable");
            }
        }


        private string groupID;
        public string GroupID
        {
            get
            {
                return groupID;
            }
            set
            {
                groupID = value;
                OnPropertyChanged("GroupID");
            }
        }

        private Boolean nicknameenable;
        public Boolean NicknameEnable
        {
            get
            {
                return nicknameenable;
            }
            set
            {
                nicknameenable = value;
                OnPropertyChanged("NicknameEnable");
            }
        }


        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        private Boolean bytime = true;
        public Boolean ByTime
        {
            get
            {
                return bytime;
            }
            set
            {
                bytime = value;
                OnPropertyChanged("ByTime");
            }
        }
        private Boolean byname;
        public Boolean ByName
        {
            get
            {
                return byname;
            }
            set
            {
                byname = value;
                OnPropertyChanged("ByName");
            }
        }
        private Boolean byall;
        public Boolean ByAll
        {
            get
            {
                return byall;
            }
            set
            {
                byall = value;
                OnPropertyChanged("ByAll");
            }
        }
        private Boolean asctype = true;
        public Boolean AscType
        {
            get
            {
                return asctype;
            }
            set
            {
                asctype = value;
                OnPropertyChanged("AscType");
            }
        }
        private Boolean dsctype;
        public Boolean DscType
        {
            get
            {
                return dsctype;
            }
            set
            {
                dsctype = value;
                OnPropertyChanged("DscType");
            }
        }

        private ObservableCollection<Message> messages = new ObservableCollection<Message>();
        public ObservableCollection<Message> Messages
        {
            get
            {
                return messages;
            }
            set
            {
                messages = value;
                OnPropertyChanged("Messages");
            }
        }

        private void Messages_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("Messages");
        }
    }
}

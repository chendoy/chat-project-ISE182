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

        //private string messages;
        //public string Messages
        //{
        //    get
        //    {
        //        return messages;
        //    }
        //    set
        //    {
        //        messages = value;
        //        OnPropertyChanged("Messages");
        //    }
        //}

        //private ObservableCollection<string> messages;

        //public void UpdateMessageList(List<Message> list)
        //{
        //    //messages.Clear();
        //    foreach (Message message in list)
        //    {
        //        messages.Add(message.ToString());
        //    }
        //    OnPropertyChanged("Messages");
        //}

        public ObservableCollection<string> Messages { get; } = new ObservableCollection<string>();

        private void Messages_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("Messages");
        }
    }
}

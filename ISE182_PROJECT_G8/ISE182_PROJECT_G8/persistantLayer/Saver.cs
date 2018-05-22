using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISE182_PROJECT_G8.logicLayer;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.ObjectModel;

namespace ISE182_PROJECT_G8.persistantLayer
{

    /* 
     * this class of static methods handles persisting and reloading serializable objects such as 'User' and 'Message' to RAM
     * and from filesystem in order to maintain quick access of the chat system to its database
     */

    public sealed class Saver // singleton design pattern
    {
        private static string directoryPath = @"StoredData";
        private static string UsersFilename = Path.Combine(directoryPath, "Users.bin");
        private static string MessagesFilename = Path.Combine(directoryPath, "Messages.bin");
        private static string RememberMeFilename = Path.Combine(directoryPath, "RememberMe.bin");

        private static Saver instance = null;
        private static readonly object padlock = new object();

        private Saver()
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        public static Saver Instance
        {
            get
            {   //only if there is no instance lock object, otherwise return instance
                if (instance == null)
                {
                    lock (padlock) // senario: n threads in here,
                    {              //locking the first and others going to sleep till the first get new Instance
                        if (instance == null)  // rest n-1 threads no need new instance because its not null anymore.
                        {
                            instance = new Saver();
                        }
                    }
                }
                return instance;
            }
        }


        public void SaveUsers(List<User> usersToSave)
        {
            Stream Filestream = File.Create(UsersFilename);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(Filestream, usersToSave);
            Filestream.Close();
            Logger.Instance.Info("Users data was persisted successfully");
        }

        public List<User> LoadUsers()
        {
            if (File.Exists(UsersFilename))
            {
                Stream Filestream = File.OpenRead(UsersFilename);
                BinaryFormatter deserializer = new BinaryFormatter();
                List<User> loadedUsers = (List<User>)deserializer.Deserialize(Filestream);
                Filestream.Close();
                Logger.Instance.Info("Users data was loaded successfully");
                return loadedUsers;
            }
            return new List<User>(); //the file doesn't exist - return an empty list//
        }

        public void SaveMessages(ObservableCollection<Message> MessagesToSave)
        {
            Stream Filestream = File.Create(MessagesFilename);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(Filestream, MessagesToSave);
            Filestream.Close();
            Logger.Instance.Info("Messages data was persisted successfully");
        }

        public ObservableCollection<Message> LoadMessages()
        {
            if (File.Exists(MessagesFilename))
            {
                Stream Filestream = File.OpenRead(MessagesFilename);
                BinaryFormatter deserializer = new BinaryFormatter();
                ObservableCollection<Message> loadedMessages = (ObservableCollection<Message>)deserializer.Deserialize(Filestream);
                Filestream.Close();
                Logger.Instance.Info("Messages data was loaded successfully");
                return loadedMessages;
            }
            return new ObservableCollection<Message>();  //the file doesn't exist - return an empty list//


        }

        public void SaveRememberMe(User user)
        {
            Stream Filestream = File.Create(RememberMeFilename);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(Filestream, user);
            Filestream.Close();
            Logger.Instance.Info("Remember me user loaded successfully");
        }

        public User LoadRememberMe()
        {
            if (File.Exists(RememberMeFilename))
            {
                Stream Filestream = File.OpenRead(RememberMeFilename);
                BinaryFormatter deserializer = new BinaryFormatter();
                User rememberMeUser = ((User)deserializer.Deserialize(Filestream));
                Filestream.Close();
                Logger.Instance.Info("Remember me data was loaded successfully");
                return rememberMeUser;
            }
            return new User("",-1); //the file doesn't exist - return a dummy user//
        }
    }

}

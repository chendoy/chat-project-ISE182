using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISE182_PROJECT_G8.logicLayer;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ISE182_PROJECT_G8.persistantLayer
{

    /* 
     * this class of static methods handles persisting and reloading serializable objects such as 'User' and 'Message' to RAM
     * and from filesystem in order to maintain quick access of the chat system to its database
     */

    static class Saver
    {
        public static string UsersFilename = @"C:\chat_database\Users.bin";
        public static string MessagesFilename = @"C:\chat_database\Messages.bin";

        public static void saveUsers(List<User> usersToSave)
        {
            Stream Filestream = File.Create(UsersFilename);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(Filestream, usersToSave);
            Filestream.Close();
            Logger.Instance.Info("Users data was persisted successfully");
        }

        public static List<User> LoadUsers()
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
            return new List<User>(); //todo: implement an error here or something//
        }

        public static void saveMessages(List<Message> MessagesToSave)
        {
            Stream Filestream = File.Create(MessagesFilename);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(Filestream, MessagesToSave);
            Filestream.Close();
            Logger.Instance.Info("Messages data was persisted successfully");
        }

        public static List<Message> LoadMessages()
        {
            if (File.Exists(MessagesFilename))
            {
                Stream Filestream = File.OpenRead(MessagesFilename);
                BinaryFormatter deserializer = new BinaryFormatter();
                List<Message> loadedMessages = (List<Message>)deserializer.Deserialize(Filestream);
                Filestream.Close();
                Logger.Instance.Info("Messages data was loaded successfully");
                return loadedMessages;
            }
            return new List<Message>(); //todo: implement an error here or something//


        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISE182_PROJECT_G8.logicLayer;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ISE182_PROJECT_G8.presenttationLayer
{
    static class Saver
    {
        public static string Filename = @"C:\chat_database\users.bin";

        public static void saveUsers(List<User> usersToSave)
        {
            Stream Filestream = File.Create(Filename);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(Filestream, usersToSave);
            Filestream.Close();
        }

        public static List<User> LoadUsers()
        {
            if(File.Exists(Filename))
            {
                Stream Filestream = File.OpenRead(Filename);
                BinaryFormatter deserializer = new BinaryFormatter();
                List<User> loadedUsers = (List<User>)deserializer.Deserialize(Filestream);
                Filestream.Close();
                return loadedUsers;
            }
            return null; //todo: implement an error here or something//
        }

     
     
    }

}

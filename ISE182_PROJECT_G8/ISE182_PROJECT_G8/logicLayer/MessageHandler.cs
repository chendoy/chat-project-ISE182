using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.logicLayer
{
    class MessageHandler
    {
        public static void addUniqueByGuid(List<Message> mainList, List<Message> toAddList)
        {
            foreach (Message message in toAddList)
            {
                var msgWithThisGuid = (from msg in mainList
                                       where msg.getGuid().Equals(message.getGuid())
                                       select msg).FirstOrDefault();

                if (msgWithThisGuid == null) //there is no message with this GUID in the main list, so it is OK to add//
                    mainList.Add(message);
            }
        }

        public static List<Message> sortbytime(List<Message> list)
        {
            List<Message> sorted = new List<Message>();
            sorted = list.OrderBy(o => o.getTime()).ToList();
            return sorted;
        }

        // Checks the message length
        public static Boolean isValid(String message)
        {
            int length = message.Length;
            if (length > 150)
            {
                return false;
            }
            return true;
        }
    }
}

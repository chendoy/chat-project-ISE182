﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISE182_PROJECT_G8.persistantLayer;

namespace ISE182_PROJECT_G8.logicLayer
{
    /* This class handle all the processes which
     * need to done on messages purely
     */
    public static class MessageHandler
    {

        private static readonly int _maxLength = 100;

        //adds messages from 'toAddList' to 'mainList' if it has unique GUID among the 'mainList' messages//
        public static void addUniqueByGuid(IList<Message> mainList, IList<Message> toAddList)
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

        // Checks the message length for 150 length max limit//
        public static Boolean isValid(String message)
        {
            int length = message.Length;
            if (String.IsNullOrWhiteSpace(message) || message.Length==0 ||  length > _maxLength)
            {
                Logger.Instance.Error("Message content validation returned: Not Valid");
                return false;
            }
            return true;
        }
    }
}

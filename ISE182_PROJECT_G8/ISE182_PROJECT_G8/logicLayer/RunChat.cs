using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISE182_PROJECT_G8.logicLayer;
using ISE182_PROJECT_G8.persistantLayer;
using ISE182_PROJECT_G8.presentationLayer;

namespace ISE182_PROJECT_G8.logicLayer
{
    class RunChat
    {
        private static Chatroom systemInit()
        {
            Chatroom chatRoom = new Chatroom(); //initiates a new Chatroom object//
            chatRoom.loadUsers(); //loades persistant users data to RAM//
            chatRoom.loadMessages(); //loades persistant messages data to RAM//
            log4net.Config.XmlConfigurator.Configure(); //configures the logger//
            Logger.Instance.Info("System initialization was completed, starting GUI...");
            return chatRoom;
        }
        public static void Main(String[] args)
        {
            Logger.Instance.Info("starting system initialization");
            Chatroom chatRoom = systemInit();    //initiates the chatroom, logger, loads persistant data to RAM, etc..//
            GUI.DisplayVisitorGUI(chatRoom);

        }
    }
}

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
        private static void SystemInit()
        {
            Chatroom chatRoom = Chatroom.Instance; //initiates a new Chatroom object//
            log4net.Config.XmlConfigurator.Configure(); //configures the logger//
            Logger.Instance.Info("System initialization was completed, starting GUI...");
        }
        public static void Main(String[] args)
        {
            Logger.Instance.Info("starting system initialization");
            SystemInit();    //initiates the chatroom, logger, loads persistant data to RAM, etc..//
            GUI.DisplayVisitorGUI();
        }
    }
}

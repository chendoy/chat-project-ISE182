using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISE182_PROJECT_G8.logicLayer;
using ISE182_PROJECT_G8.persistantLayer;
//using ISE182_PROJECT_G8.presentationLayer;
using System.Windows;

namespace ISE182_PROJECT_G8.logicLayer
{
    class RunChat
    {
        private static Chatroom SystemInit()
        {
            Chatroom chatRoom = new Chatroom(); //initiates a new Chatroom
            log4net.Config.XmlConfigurator.Configure(); //configures the logger//
            Logger.Instance.Info("System initialization was completed, starting GUI...");
            return chatRoom;
        }
        [STAThread]
        public static void Main(String[] args)
        {
           Logger.Instance.Info("starting system initialization");
            Chatroom chatroom=SystemInit();    //initiates the chatroom, logger, loads persistant data to RAM, etc..//
           Application app = new Application();
           Window CurrentWindow = new Login(chatroom);
            app.Run(CurrentWindow);
        }
    }
}

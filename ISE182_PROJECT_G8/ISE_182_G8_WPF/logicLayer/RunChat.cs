using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISE182_PROJECT_G8.logicLayer;
using ISE182_PROJECT_G8.persistantLayer;
using ISE_182_G8_WPF;

namespace ISE182_PROJECT_G8.logicLayer
{
    class RunChat
    {
        //private static Login CurrentWindow;//

        private static void SystemInit()
        {
            Logger.Instance.Info("starting system initialization");
            Chatroom chatRoom = Chatroom.Instance; //initiates a new Chatroom singleton instance//
           // log4net.Config.XmlConfigurator.Configure(); //configures the logger////
            Logger.Instance.Info("System initialization was completed, starting GUI...");
        }
        [STAThread]
        public static void Main(String[] args)
        {
            SystemInit();    //initiates the chatroom, logger, loads persistant data to RAM, etc..//
            var app = new Application();
            app.Run(new MainWindow());
        }
    }
}

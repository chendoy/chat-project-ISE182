using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISE182_PROJECT_G8.logicLayer;
using ISE182_PROJECT_G8.presenttationLayer;

namespace ISE182_PROJECT_G8.logicLayer
{
    class StartMain
    {
        private static Chatroom systemInit()
        {
            Chatroom chatRoom = new Chatroom(); //initiates a new Chatroom//
            chatRoom.loadUsers(); //loades persistant users data to RAM//
            return chatRoom;
        }
        public static void Main(String[] args)
        {
            Chatroom chatRoom = systemInit();    //initiates the chatroom, loads data from persistant layer to RAM, etc..//
            ISE182_PROJECT_G8.presentationLayer.GUI.displayGui(chatRoom);

        }
    }
}

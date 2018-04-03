using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.presentationLayer
{
    class present_handler
    {
            // this function can get message from user and send it back
            public static String get()
            {
                String content = Console.ReadLine();
                return content;
            }
            // This function print to the screen
            public static void output(String s)
            {
                Console.WriteLine(s);
            }
    }
}

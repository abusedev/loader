using System;
using System.Windows.Forms;

namespace abuseloader.handler
{
    internal class variables
    {

        // configs //
        public static string downloadSettings = Application.StartupPath + @"/downloadkey.cfg";

        // app //
        public static string downloadKey;
        public static decimal niceVersion = 1.0m;
        public static double version = 1.0;
        public static string appdata = @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\@buse";


        // web //
        public static string response;
        public static string keylink;
    }
}

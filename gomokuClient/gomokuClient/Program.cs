using A21ÖÇÄÜÎå×ÓÆå04200923;
using System.Net.Sockets;

namespace gomokuClient
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            string IP = "192.168.31.93";
            int Port = 2019;
            ApplicationConfiguration.Initialize();
            Application.Run(new startForm(IP, Port));
            //Application.Run(new Mainform());
        }
    }
}
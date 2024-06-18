using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Chat_app
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Chat App  (Made By: Pongo_x86)";
            Console.WriteLine("Welcome to the Chat App! Version: 0.01");
            Console.Write("Choose an option: ");
            Console.WriteLine("1. Host a chat server | 2. Connect to a chat server!");
            int option = Convert.ToInt32(Console.ReadLine());
            if (option == 1)
            {
                Thread.Sleep(1000);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("NOTE: Hosting Requires Port Forwarding For Connections Outside The Local Network");
                Console.ResetColor();
                Thread.Sleep(1000);
                Backend.BackendMain.initializeServer();
            }
            else if (option == 2)
            {
                ChatInterface.InterfaceMain.InitializeInterface();
            }
            else
            {
                Console.WriteLine("Invalid option!");
            }
            Console.ReadKey();
        }
    }
}

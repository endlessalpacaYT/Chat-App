using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat_app.Client; 

namespace Chat_app.ChatInterface
{
    internal class InterfaceMain
    {
        public static void InitializeInterface()
        {
            ClientMain.initializeClient();
            Interface();
        }

        public static void Interface()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Chat Interface!");

            // Start a new task to receive messages
            Task.Run(() =>
            {
                while (true)
                {
                    string receivedMessage = ClientMain.ReceiveMessage();
                    Console.WriteLine(receivedMessage);
                }
            });
            string username = ClientMain.GetUsername();
            // Continuously send messages
            while (true)
            {

                Console.Write("Enter a message: ");
                string input = Console.ReadLine();
                input = username + ": " + input;
                ClientMain.SendMessage(input);
            }
        }
    }
}
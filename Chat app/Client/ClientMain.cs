using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Chat_app.Client
{
    internal class ClientMain
    {
        private static TcpClient client;
        private static NetworkStream stream;
        private static string username;

        public static void initializeClient()
        {
            Console.WriteLine("Enter the IP and port of the chat server you want to join eg. IP 127.0.0.1 Port 80");
            Console.Write("Enter the IP of the server: ");
            string ip = Console.ReadLine();
            Console.Write("Enter the port of the server: ");
            int port = Convert.ToInt32(Console.ReadLine());
            ClientConnect(ip, port);
        }

        public static void ClientConnect(string ip, int port)
        {
            Console.WriteLine("Connecting to server: " + ip + " on port: " + port);
            try
            {
                client = new TcpClient(ip, port);
                Thread.Sleep(1000);
                Console.WriteLine("Connected to server: " + ip + " on port: " + port);
                Console.Write("Enter your username: ");
                username = Console.ReadLine();
                string message = "User: " + username + " Connected!";
                byte[] data = Encoding.ASCII.GetBytes(message);

                stream = client.GetStream();

                stream.Write(data, 0, data.Length);

                Console.WriteLine("Sent: {0}", message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error connecting to server: " + e.Message);
            }
        }

        public static void SendMessage(string message)
        {
            byte[] data = Encoding.ASCII.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }

        public static string ReceiveMessage()
        {
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            return Encoding.ASCII.GetString(buffer, 0, bytesRead);
        }

        public static string GetUsername()
        {
            return username; 
        }
    }
}
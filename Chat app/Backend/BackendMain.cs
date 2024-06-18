using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Chat_app.Backend
{
    internal class BackendMain
    {
        private static List<TcpClient> clients = new List<TcpClient>();

        public static void initializeServer()
        {
            Console.Write("Enter the port you want to host the server on: ");
            int port = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Server is starting on port: " + port);
            startTcpServer(port);
        }

        public static void startTcpServer(int port)
        {
            IPAddress localIP = IPAddress.Any; // Listen on all network interfaces
            TcpListener server = new TcpListener(localIP, port);
            server.Start();
            Console.WriteLine("Server started on port: " + port);

            while (true)
            {
                Console.WriteLine("Waiting for a connection...");
                TcpClient client = server.AcceptTcpClient();
                clients.Add(client);
                Console.WriteLine("Connected!");

                Task.Run(() => HandleClient(client));
            }
        }

        private static void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];

            try
            {
                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        break;
                    }

                    string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Received: " + message);

                    BroadcastMessage(buffer, bytesRead);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Connection closed unexpectedly: " + ex.Message);
            }
            finally
            {
                clients.Remove(client);
                client.Close();
            }
        }

        private static void BroadcastMessage(byte[] buffer, int bytesRead)
        {
            foreach (var client in clients)
            {
                NetworkStream stream = client.GetStream();
                stream.Write(buffer, 0, bytesRead);
            }
        }
    }
}
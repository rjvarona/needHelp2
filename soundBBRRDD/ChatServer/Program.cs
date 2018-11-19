using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using ChatLibrary;

namespace ChatServer
{
    class Program
    {
        //This is a dictionary of key,value pairs used to store the names and sockets of each client.
        public static Dictionary<string, TcpClient> ClientList = new Dictionary<string, TcpClient>();
        /// <summary>
        /// Listens for new connections and handles them.
        /// </summary>
        static void Main()
        {
            



        var serverSocket = new TcpListener(IPAddress.Any, 8888);
            serverSocket.Start();
            Console.WriteLine("Chat server started...");
            List<string> playerList = new List<string>();

            while (true)
            {
                string player = "player";
                
                //auto updating players
                playerList.Add(player);
                playerList[playerList.Count - 1] = playerList[playerList.Count - 1] + (playerList.Count - 1).ToString();
                player = playerList[playerList.Count - 1];
                
                var clientSocket = serverSocket.AcceptTcpClient();
               


                Console.WriteLine(player + " joined cat room.");
                //Create a new object to Handle all future incoming messages from this client
                var client = new HandleClient();
                //Start that thread running
                client.StartClient(clientSocket, player);
            }
        }

        /// <summary>
        /// A simple broadcast message function that resides here to allow the clients to broadcast
        /// incomming messages to everyone. 
        /// </summary>
        /// <param name="msg">The message to broadcast</param>
        /// <param name="uname">The user's name who sent it</param>
        /// <param name="flag"></param>
        public static void Broadcast(string msg, string uname, bool flag)
        {
            foreach (var item in ClientList)
            {
                var broadcastSocket = item.Value;
                var m = flag ? uname + " says: " + msg : msg;
                item.Value.WriteString(m);
            }
        }
    }
}

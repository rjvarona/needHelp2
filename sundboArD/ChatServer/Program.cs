﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using ChatClient;

namespace ChatServer
{
    class Program
    {
        //This is a dictionary of key,value pairs used to store the names and sockets of each client.
        public static Dictionary<string, TcpClient> ClientList = new Dictionary<string, TcpClient>();
        public static List<string> playerList = new List<string>();
        /// <summary>
        /// Listens for new connections and handles them.
        /// </summary>
        static void Main()
        {
            var serverSocket = new TcpListener(IPAddress.Any, 8888);
            serverSocket.Start();
            Console.WriteLine("Chat server started...");
           string player = "player";
            while (true)
            {
                //This next line of code actually blocks
                var clientSocket = serverSocket.AcceptTcpClient();


                playerList.Add(player);
                playerList[playerList.Count - 1] = playerList[playerList.Count - 1] + (playerList.Count - 1).ToString();
                player = playerList[playerList.Count - 1];



                //Somebody connected and sent us data... and no clientSocket doesn't have a method called ReadString: See TcpClientExtension.cs
                string dataFromClient = clientSocket.ReadString();


                

                //Add the name and StringSocket to the Dictionary object
                ClientList.Add(player, clientSocket);
                //Tell everyone that someone new joined!
                Broadcast(player + " joined.", player, false);
                //Log the fact to the server console
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

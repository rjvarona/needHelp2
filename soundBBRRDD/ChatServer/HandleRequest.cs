using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class HandleRequest
    {

        private TcpClient _clientSocket;

        /// <summary>
        /// Finding strings to send SoundBoard to play
        /// case statement to play the right sound requested by the client from server.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string RequestSound(string message)
        {
            string soundPacket = @"C:\Users\rjvar\Documents\GitHub\needHelp2\soundBBRRDD\wavs\Wrong.wav";

            return soundPacket;
        }
    }
}

using System.ComponentModel;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Media;
using System.Collections.Generic;



namespace ChatLibrary
{
    public class ClientModel : INotifyPropertyChanged
    {
       
        public bool isConnected = false;
        private TcpClient _socket;

        private SoundPlayer _soundPlayer;
        
        private string _messageBoard;
        public string MessageBoard
        {
            get { return _messageBoard; }
            set { _messageBoard = value; OnPropertyChanged(); }
        }

        private string _currentMessage;
        public string CurrentMessage
        {
       
            get { return _currentMessage; }
            set { _currentMessage = value; OnPropertyChanged(); }
        }

        public bool Connected
        {
            get { return _socket != null && _socket.Connected; }
        }

        public void Connect()
        {
            string player = "player";
            _socket = new TcpClient("127.0.0.1", 8888);
            List<string> playerList = new List<string>();
            //auto updating players

            OnPropertyChanged("Connected");
            


            playerList.Add(player);
            playerList[playerList.Count - 1] = playerList[playerList.Count - 1] + (playerList.Count - 1).ToString();
            player = playerList[playerList.Count - 1];
            //OnPropertyChanged("Connected");
            Send(player);
           
            _messageBoard = "Welcome: " + player;
            var thread = new Thread(GetMessage);
            thread.Start();
        }

        private void GetMessage()
        {
            while(true)
            {
                string msg = _socket.ReadString();
                MessageBoard += "\r\n" + msg;
            }
        }

        public void TSeries()
        {
            _soundPlayer = new SoundPlayer(@"C:\Users\rjvar\Desktop\wavs\Wrong.wav");

            _soundPlayer.Play();

        }

        public void Send(string message)
        {

            _soundPlayer = new SoundPlayer(@"C:\Users\rjvar\Documents\GitHub\needHelp2\soundBBRRDD\wavs\Wrong.wav");

            _soundPlayer.Play();


            _socket.WriteString(message);
        }

            #region INPC
            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                var handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(propertyName));
                }
            }
            #endregion
    }
}

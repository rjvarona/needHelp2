using System.ComponentModel;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Media;
using System.Collections.Generic;
using System.Windows;
using System.Linq;


namespace ChatClient
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
            get {


                return _currentMessage;


            }
            set { _currentMessage = value; OnPropertyChanged(); }
        }

        public bool Connected
        {
            get { return _socket != null && _socket.Connected; }
        }

        public void Connect()
        {
           
            _socket = new TcpClient("127.0.0.1", 8888);
            List<string> playerList = new List<string>();
            //auto updating players

            OnPropertyChanged("Connected");


            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.TstSound.IsEnabled = true;
            mainWindow.China.IsEnabled = true;
            mainWindow.BingBong.IsEnabled = true;
            mainWindow.FakeNews.IsEnabled = true;
            mainWindow.ReallyRich.IsEnabled = true;
            mainWindow.GreatWall.IsEnabled = true;




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

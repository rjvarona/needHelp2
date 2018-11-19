using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;

namespace ChatClient
{
    public class ClientModel : INotifyPropertyChanged
    {
        private TcpClient _socket;

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
            //string player = "player";


            _socket = new TcpClient("127.0.0.1", 8888);

            //auto updating players
            OnPropertyChanged("Connected");
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            mainWindow.China2.IsEnabled = true;
            mainWindow.BingBong.IsEnabled = true;
            mainWindow.Wrong.IsEnabled = true;
            mainWindow.GreatWall.IsEnabled = true;
            mainWindow.ReallyRich.IsEnabled = true;
            mainWindow.FakeNews.IsEnabled = true;
            mainWindow.BuildWall.IsEnabled = true;



            //_currentMessage = player;
            Send(_currentMessage);
            _messageBoard = "Welcome: " + _currentMessage;
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

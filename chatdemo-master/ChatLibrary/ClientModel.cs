using System.ComponentModel;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;

namespace ChatLibrary
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
            get { return _currentMessage; }
            set { _currentMessage = value; OnPropertyChanged(); }
        }

        public bool Connected
        {
            get { return _socket != null && _socket.Connected; }
        }

        public void Connect()
        {
            _socket = new TcpClient("127.0.0.1", 8888);
            OnPropertyChanged("Connected");
            Send();
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

        public void Send()
        {
            _socket.WriteString(_currentMessage);
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

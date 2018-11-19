using System.ComponentModel;
using System.Runtime.CompilerServices;

using ChatServer;
using System.Windows;
using System.Linq;

namespace ChatClient.ViewModels
{
    
    /// <summary>
    /// change the code that needs to be changed so it looks like yours
    /// delete half of the stuff you do not need e.g. message board.
    /// make sure when you can not play sound unless you connected
    /// change it to look like yours
    /// find out what you need and delete and implement it so that it is yours and follow state diagram
    /// 
    /// </summary>
    class ClientViewModel : INotifyPropertyChanged
    {
        #region Properties
        //Elements bound to by the view
        public string Message {
            get { return _clientModel.CurrentMessage; }
            set { _clientModel.CurrentMessage = value; NotifyPropertyChanged(); }
        }

        public string MessageBoard 
        {
            get     { return _clientModel.MessageBoard; }
            set { _clientModel.MessageBoard = value; NotifyPropertyChanged(); }
        }       
    
        public DelegateCommand ConnectCommand { get; set; }
        public DelegateCommand SendCommand { get; set; }
        public DelegateCommand TstSound { get; set; }
        #endregion 

        #region Private and Internal Vars/Props
        private readonly  ChatClient.ClientModel _clientModel;
        private readonly HandleRequest _requestModel;

        #endregion 
         /// <summary>
        /// Constructor creates the Model!
        /// </summary>
        public ClientViewModel()
        {
            //MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            //mainWindow.TstSound.IsEnabled = true;
            //Create ourselves a model
            _clientModel = new ClientModel();

            _requestModel = new HandleRequest();
            //Subscribe to the Model's PropertyChanged event
            _clientModel.PropertyChanged += ClientModelChanged;
            //Create our two Command objects
            ConnectCommand = new DelegateCommand(
                a => _clientModel.Connect(), 
                b => !_clientModel.Connected
            );

            TstSound = new DelegateCommand(
                a => _clientModel.Send(""),
                b => _clientModel.Connected
                );

            SendCommand = new DelegateCommand(
                a => _clientModel.Send("pressF"),
                b => _clientModel.Connected
            );



        }

        #region Event Listeners
        private void ClientModelChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Connected"))
            {
                NotifyPropertyChanged("Connected");
                ConnectCommand.RaiseCanExecuteChanged();
                SendCommand.RaiseCanExecuteChanged();
                

            }

            else if (e.PropertyName.Equals("MessageBoard"))
            {
                NotifyPropertyChanged("MessageBoard");
            }
        }
        #endregion

        #region INPC Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string prop = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
        #endregion NPC Implementation
    }
}

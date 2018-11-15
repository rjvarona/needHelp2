using System.ComponentModel;
using System.Runtime.CompilerServices;
using ChatLibrary;
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
            get { return _clientModel.MessageBoard; }
            set { _clientModel.MessageBoard = value; NotifyPropertyChanged(); }
        }       
    
        public DelegateCommand ConnectCommand { get; set; }
        public DelegateCommand SendCommand { get; set; }
        public DelegateCommand tstSound { get; set; }
        #endregion 

        #region Private and Internal Vars/Props
        private readonly ClientModel _clientModel;
        #endregion 
         /// <summary>
        /// Constructor creates the Model!
        /// </summary>
        public ClientViewModel()
        {
            //Create ourselves a model
            _clientModel = new ClientModel();
            //Subscribe to the Model's PropertyChanged event
            _clientModel.PropertyChanged += ClientModelChanged;
            //Create our two Command objects
            ConnectCommand = new DelegateCommand(
                a => _clientModel.Connect(), 
                b => !_clientModel.Connected
            );

            tstSound = new DelegateCommand(
                a => _clientModel.TSeries()
                );

            SendCommand = new DelegateCommand(
                a => _clientModel.Send(),
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

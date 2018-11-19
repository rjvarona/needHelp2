using System.Collections.Generic;
using System.ComponentModel;
using System.Media;
using System.Runtime.CompilerServices;
namespace ChatClient.ViewModels
{
    /// <summary>
    /// Adaptive code only. You should only see things here that adapt
    /// the Model to the view. This is an abstraction of the Model for 
    /// the express use by the View.
    /// </summary>
    class ClientViewModel : INotifyPropertyChanged
    {

        private SoundPlayer _soundPlayer;

        #region Properties
        //Elements bound to by the view
        public string Message
        {
            get { return _clientModel.CurrentMessage; }
            set { _clientModel.CurrentMessage = value; NotifyPropertyChanged(); }
        }

        public string MessageBoard
        { //put case statement here to play the sound

            get
            {

                //readonly SoundPlayer _alertBeep = new SoundPlayer("FilePath");
                string[] words = _clientModel.MessageBoard.Split(':');
                string checker = words[words.Length - 1];
           



                switch (checker)
                {
                    case " China":
                        _soundPlayer = new SoundPlayer(@"C:\Users\rjvar\Desktop\needHelp2-master\sundboArD\Wavs\China.wav");
                        _soundPlayer.Play();
                        return _clientModel.MessageBoard;

                    case " Wrong":
                        _soundPlayer = new SoundPlayer(@"C:\Users\rjvar\Desktop\needHelp2-master\sundboArD\Wavs\Wrong.wav");
                        _soundPlayer.Play();
                        return _clientModel.MessageBoard;
                    case " GreatWall":
                        _soundPlayer = new SoundPlayer(@"C:\Users\rjvar\Desktop\needHelp2-master\sundboArD\Wavs\GreatWall.wav");
                        _soundPlayer.Play();
                        return _clientModel.MessageBoard;
                    case " ReallyRich":
                        _soundPlayer = new SoundPlayer(@"C:\Users\rjvar\Desktop\needHelp2-master\sundboArD\Wavs\ReallyRich.wav");
                        _soundPlayer.Play();
                        return _clientModel.MessageBoard;
                    case " BingBong":
                        _soundPlayer = new SoundPlayer(@"C:\Users\rjvar\Desktop\needHelp2-master\sundboArD\Wavs\BingBong.mp4");

                        _soundPlayer.Play();
                        return _clientModel.MessageBoard;
                    case " FakeNews":
                        _soundPlayer = new SoundPlayer(@"C:\Users\rjvar\Desktop\needHelp2-master\sundboArD\Wavs\FakeNews.wav");

                        _soundPlayer.Play();
                        return _clientModel.MessageBoard;
                    case " BuildWall":
                        _soundPlayer = new SoundPlayer(@"C:\Users\rjvar\Desktop\needHelp2-master\sundboArD\Wavs\WallRemix.wav");

                        _soundPlayer.Play();
                        return _clientModel.MessageBoard;

                    default:

                        return _clientModel.MessageBoard;
                }
                return _clientModel.MessageBoard;

            }


            set { _clientModel.MessageBoard = value; NotifyPropertyChanged(); }

        }

        public DelegateCommand ConnectCommand { get; set; }
        public DelegateCommand SendCommand { get; set; }
        public DelegateCommand China2 { get; set; }
        public DelegateCommand BingBong { get; set; }
        public DelegateCommand Wrong { get; set; }
        public DelegateCommand GreatWall { get; set; }
        public DelegateCommand ReallyRich { get; set; }
        public DelegateCommand FakeNews { get; set; }
        public DelegateCommand BuildWall { get; set; }



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
            SendCommand = new DelegateCommand(
                a => _clientModel.Send("PressF"),
                b => _clientModel.Connected
            );
            China2 = new DelegateCommand(
                a => _clientModel.Send("China"),
                b => _clientModel.Connected
                );
            BingBong = new DelegateCommand(
                a => _clientModel.Send("BingBong"),
                b => _clientModel.Connected
                );
            GreatWall = new DelegateCommand(
               a => _clientModel.Send("GreatWall"),
               b => _clientModel.Connected
               );
            ReallyRich = new DelegateCommand(
               a => _clientModel.Send("ReallyRich"),
               b => _clientModel.Connected
               );
            FakeNews = new DelegateCommand(
               a => _clientModel.Send("FakeNews"),
               b => _clientModel.Connected
               );
           Wrong  = new DelegateCommand(
               a => _clientModel.Send("Wrong"),
               b => _clientModel.Connected
                );
            BuildWall = new DelegateCommand(
                a => _clientModel.Send("BuildWall"),
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
                China2.RaiseCanExecuteChanged();
                BingBong.RaiseCanExecuteChanged();
                Wrong.RaiseCanExecuteChanged();
                GreatWall.RaiseCanExecuteChanged();
                ReallyRich.RaiseCanExecuteChanged();
                FakeNews.RaiseCanExecuteChanged();
                BuildWall.RaiseCanExecuteChanged();
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

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using InTime.ServiceReference1;

namespace InTime
{
   public class AppState:INotifyPropertyChanged
    {
        public SoundState sound;
        public Client_User user;
        public ListBox current_ListBox;
        public ObservableCollection<Song_Playlist> playlists;
        public object Current_Tab;
        public Song_Playlist currentPlaylist;
        public bool IsPlaying;
        public DispatcherTimer playTimer;
        public MediaPlayer player;
        public Song currentSong;
        public bool dragStarted;
        public RepeatState repeat;

        public event PropertyChangedEventHandler PropertyChanged;

        public AppState()
        {
            player = new MediaPlayer();
            playTimer = new DispatcherTimer();
            IsPlaying = false;
            Current_Tab = null;
        }

        public ObservableCollection<Song_Playlist> Playlists
        {
            get { return playlists;}
            set
            {
                playlists = value;
                OnPropertyChanged("Playlists");
            }
        }
        public AppState(Client_User user)
        {
            player = new MediaPlayer();
            player.Volume = 0.7;
            playTimer = new DispatcherTimer();
            IsPlaying = false;
            repeat = RepeatState.NoRepeat;
            Current_Tab = null;
            this.user = user;
            playlists = new ObservableCollection<Song_Playlist>(user.Playlists);
        }

        public int DraggedIndex { get; set; }
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
   
}

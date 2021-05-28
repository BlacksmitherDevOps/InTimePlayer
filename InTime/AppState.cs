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
using InTime.ServiceReference1;

namespace InTime
{
   public class AppState:INotifyPropertyChanged
    {
        public SoundState sound;
        public Client_User user;
        public ListBox current_ListBox;
        public ObservableCollection<Song_Playlist> playlists;
        public Queue<Song> Queue;

        public event PropertyChangedEventHandler PropertyChanged;

        public AppState()
        {
           
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

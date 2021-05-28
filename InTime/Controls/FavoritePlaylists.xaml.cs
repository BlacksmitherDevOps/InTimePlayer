using InTime.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InTime.Controls
{
    /// <summary>
    /// Логика взаимодействия для FavoritePlaylists.xaml
    /// </summary>
    public partial class FavoritePlaylists : UserControl
    {
        public event OpenPlaylist OpenPlaylist;
        public FavoritePlaylists()
        {
            InitializeComponent();
            Init();
        }
        public void Init()
        {
            
        }
        public void AddList(Song_Playlist[] playlists)
        {
            foreach (var item in playlists)
            {
                Playlist_Item_Control item_Control = new Playlist_Item_Control(item);
                item_Control.OpenPlaylist += Item_Control_OpenPlaylist;
                panel.Children.Add(item_Control);
            }
        }

        private void Item_Control_OpenPlaylist(int id)
        {
            OpenPlaylist?.Invoke(id);
        }
    }
}

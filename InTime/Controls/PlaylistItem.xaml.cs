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
    /// Логика взаимодействия для PlaylistItem.xaml
    /// </summary>
    public partial class PlaylistItem : UserControl
    {
        public PlaylistItem()
        {
            InitializeComponent();
        }


        public string SongTitle
        {
            get { return (string)GetValue(SongTitleProperty); }
            set { SetValue(SongTitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SongTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SongTitleProperty =
            DependencyProperty.Register("SongTitle", typeof(string), typeof(PlaylistItem));



        public string SongAlbum
        {
            get { return (string)GetValue(SongAlbumProperty); }
            set { SetValue(SongAlbumProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SongAlbum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SongAlbumProperty =
            DependencyProperty.Register("SongAlbum", typeof(string), typeof(PlaylistItem));



        public string SongArtist
        {
            get { return (string)GetValue(SongArtistProperty); }
            set { SetValue(SongArtistProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SongArtist.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SongArtistProperty =
            DependencyProperty.Register("SongArtist", typeof(string), typeof(PlaylistItem));



        public DateTime SongDuration
        {
            get { return (DateTime)GetValue(SongDurationProperty); }
            set { SetValue(SongDurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SongDuration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SongDurationProperty =
            DependencyProperty.Register("SongDuration", typeof(DateTime), typeof(PlaylistItem));


    }
}

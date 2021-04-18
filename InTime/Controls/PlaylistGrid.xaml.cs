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
    /// Логика взаимодействия для PlaylistGrid.xaml
    /// </summary>
    public partial class PlaylistGrid : UserControl
    {
        public PlaylistGrid()
        {
            InitializeComponent();

        }




        public string ImageSource
        {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(string), typeof(PlaylistGrid));


        public string PlaylistName
        {
            get { return (string)GetValue(PlaylistNameProperty); }
            set { SetValue(PlaylistNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Playlistame.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaylistNameProperty =
            DependencyProperty.Register("PlaylistName", typeof(string), typeof(PlaylistGrid));



        public DateTime PlaylistDuration
        {
            get { return (DateTime)GetValue(PlaylistDurationProperty); }
            set { SetValue(PlaylistDurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlaylistDuration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaylistDurationProperty =
            DependencyProperty.Register("PlaylistDuration", typeof(DateTime), typeof(PlaylistGrid));



        public int SongsCount
        {
            get { return (int)GetValue(SongsCountProperty); }
            set { SetValue(SongsCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SongsCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SongsCountProperty =
            DependencyProperty.Register("SongsCount", typeof(int), typeof(PlaylistGrid));
    }
}

using InTime.ServiceReference1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для MainPlayerPage_Control.xaml
    /// </summary>
    public partial class MainPlayerPage_Control : UserControl
    {
        private AppState state;
        public MainPlayerPage_Control()
        {
            InitializeComponent();
            state= new AppState();
            state.sound = SoundState.HighSound;
            testSingerBord();


        }
        void testAddPlaylist()
        {
           
        }
        void testInfoBord()
        {
            PlaylistGrid grid = new PlaylistGrid();
            grid.ImageSource =@"C:\Player\InTimePlayer\InTime\Controls\3E-zsSjyGLU.jpg";
            grid.PlaylistDuration=DateTime.Now;
            grid.PlaylistName = "Custom playlist";
            grid.SongsCount = 322;
            List<PlaylistItem> lst = new List<PlaylistItem>();
            lst.Add(new PlaylistItem
            { SongAlbum = "Best Album", SongArtist = "Rammstein", ID = 2, SongDuration = DateTime.Now, SongTitle = "Sonne" });
            lst.Add(new PlaylistItem
            { SongAlbum = "Best Album", SongArtist = "Rammstein", ID = 2, SongDuration = DateTime.Now, SongTitle = "Sonne" });

            grid.SongList.ItemsSource = lst;

            grid.SimularSongList.Items.Add(new PlaylistItem
            { SongAlbum = "Wahrheit Oder Pflicht", SongArtist = "Oomph!", ID = 2, SongDuration = DateTime.Now, SongTitle = "Augen Auf!" });
            grid.SimularSongList.Items.Add(new PlaylistItem
            { SongAlbum = "Wahrheit Oder Pflicht", SongArtist = "Oomph!", ID = 2, SongDuration = DateTime.Now, SongTitle = "Burn Your Eyes" });
            grid.SimularSongList.Items.Add(new PlaylistItem
            { SongAlbum = "Wahrheit Oder Pflicht", SongArtist = "Oomph!", ID = 2, SongDuration = DateTime.Now, SongTitle = "Dein Weg" });
            tape_panel.Children.Add(grid);
        }
        void testSingerBord()
        {
            List<PlaylistItem> lst = new List<PlaylistItem>();
            lst.Add(new PlaylistItem { SongAlbum = "Best Album", SongArtist = "Rammstein", ID = 2, SongDuration = DateTime.Now, SongTitle = "Sonne" });
            lst.Add(new PlaylistItem { SongAlbum = "Best Album", SongArtist = "Rammstein", ID = 2, SongDuration = DateTime.Now, SongTitle = "Sonne" });
            lst.Add(new PlaylistItem { SongAlbum = "Best Album", SongArtist = "Rammstein", ID = 2, SongDuration = DateTime.Now, SongTitle = "Sonne" });
            lst.Add(new PlaylistItem { SongAlbum = "Best Album", SongArtist = "Rammstein", ID = 2, SongDuration = DateTime.Now, SongTitle = "Sonne" });
            lst.Add(new PlaylistItem { SongAlbum = "Best Album", SongArtist = "Rammstein", ID = 2, SongDuration = DateTime.Now, SongTitle = "Sonne" });


            SingerItem _Singer = new SingerItem();
            _Singer.Name = "Rammstein";

            AlbumItem album = new AlbumItem();
            album.Title = "First album";
            album.Songs = lst;

            AlbumItem album1 = new AlbumItem();
            album1.Title = "Second album";
            album1.Songs = lst;

            AlbumItem album2 = new AlbumItem();
            album2.Title = "Third album";
            album2.Songs = lst;

            _Singer.Albums = new List<AlbumItem>() { album, album1, album2 };
            SingerPage_Control list = new SingerPage_Control(_Singer);
            tape_panel.Children.Add(list);
        }
        #region Sound
        /// <summary>
        /// Изменение состояния картинки громкости
        /// SoundIcon_OnMouseEnter - при наведении на иконку
        /// SoundIcon_OnMouseDown - при нажатии на нее
        /// SoundIcon_OnMouseLeave - при выводе курсора за пределы картинки
        /// SoundSlider_OnValueChanged - при изменении громкости
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SoundIcon_OnMouseEnter(object sender, MouseEventArgs e)
        {
            switch (state.sound)
            {
                case SoundState.LowSound:
                    SoundIcon.Fill = new ImageBrush(new BitmapImage(new Uri("Assets/LowSoundRed.png",UriKind.RelativeOrAbsolute)));
                    break;
                case SoundState.MidSound:
                    SoundIcon.Fill = new ImageBrush(new BitmapImage(new Uri("Assets/MidSoundRed.png",UriKind.RelativeOrAbsolute)));
                    break;
                case SoundState.HighSound:
                    SoundIcon.Fill = new ImageBrush(new BitmapImage(new Uri("Assets/MaxVolumeRed.png",UriKind.RelativeOrAbsolute)));
                    break;
                case SoundState.Muted:
                    SoundIcon.Fill = new ImageBrush(new BitmapImage(new Uri("Assets/MutedRed.png",UriKind.RelativeOrAbsolute)));
                    break;
            }
        }
        private void SoundIcon_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            string tag=(string)SoundSlider.Tag;
            if (tag=="true")
            {
                state.sound = SoundState.Muted;
                SoundIcon.Fill=new ImageBrush(new BitmapImage(new Uri("Assets/MutedRed.png",UriKind.RelativeOrAbsolute)));
                SoundSlider.Tag = "false";
            }
            else
            {
                if (SoundSlider.Value < 30)
                {
                    state.sound = SoundState.LowSound;
                    SoundIcon.Fill =new ImageBrush(new BitmapImage(new Uri("Assets/LowSoundRed.png",UriKind.RelativeOrAbsolute)));
                }
                else if (SoundSlider.Value >= 30 && SoundSlider.Value <= 70)
                {
                    state.sound = SoundState.MidSound;
                    SoundIcon.Fill = new ImageBrush(new BitmapImage(new Uri("Assets/MidSoundRed.png",UriKind.RelativeOrAbsolute)));
                }
                else if (SoundSlider.Value > 70)
                {
                    state.sound = SoundState.HighSound;
                    SoundIcon.Fill =new ImageBrush(new BitmapImage(new Uri("Assets/MaxVolumeRed.png",UriKind.RelativeOrAbsolute)));
                }
                SoundSlider.Tag = "true";
            }
        }
        private void SoundIcon_OnMouseLeave(object sender, MouseEventArgs e)
        {
            switch (state.sound)
            {
                case SoundState.LowSound:
                    SoundIcon.Fill = new ImageBrush(new BitmapImage(new Uri("Assets/LowSoundPurple.png",UriKind.RelativeOrAbsolute)));
                    break;
                case SoundState.MidSound:
                    SoundIcon.Fill = new ImageBrush(new BitmapImage(new Uri("Assets/MidSoundPurple.png",UriKind.RelativeOrAbsolute)));
                    break;
                case SoundState.HighSound:
                    SoundIcon.Fill = new ImageBrush(new BitmapImage(new Uri("Assets/MaxVolumePurple.png",UriKind.RelativeOrAbsolute)));
                    break;
                case SoundState.Muted:
                    SoundIcon.Fill = new ImageBrush(new BitmapImage(new Uri("Assets/MutedPurple.png",UriKind.RelativeOrAbsolute)));
                    break;
            }
        }
        private void SoundSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
                if (SoundSlider.Value < 30)
                {
                    state.sound = SoundState.LowSound;
                    SoundIcon.Fill = new ImageBrush(new BitmapImage(new Uri("Assets/LowSoundPurple.png", UriKind.RelativeOrAbsolute)));
                }
                else if (SoundSlider.Value >= 30 && SoundSlider.Value <= 70)
                {
                    state.sound = SoundState.MidSound;
                    SoundIcon.Fill = new ImageBrush(new BitmapImage(new Uri("Assets/MidSoundPurple.png", UriKind.RelativeOrAbsolute)));
                }
                else if (SoundSlider.Value > 70)
                {
                    state.sound = SoundState.HighSound;
                    SoundIcon.Fill = new ImageBrush(new BitmapImage(new Uri("Assets/MaxVolumePurple.png", UriKind.RelativeOrAbsolute)));
                }
        }
        #endregion
        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            if (PlaylistBox.SelectedIndex >= 0)
            {
                PlaylistBox.SelectedIndex = -1;
            }
            ((RadioButton)sender).IsChecked = true;
        }
        private void PlaylistBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var child in RadioBtnsPanel.Children)
            {
                (child as RadioButton).IsChecked = false;
            }
        }
    }
}

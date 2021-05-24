using InTime.ServiceReference1;
using MaterialDesignThemes.Wpf;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InTime.Controls
{
    public partial class MainPlayerPage_Control : UserControl
    {
        private AppState state;
        Window mainWindow;
        public MainPlayerPage_Control(Window window,Client_User user)
        {
            InitializeComponent();
            state= new AppState(user);
            mainWindow = window;
            InitUser(user);
        }
        /* Service methods

        AddNewSinger - добавить нового исполнителя
        AddNewTrack - добавить новый трек
        AddNewAlbum - добавить новый альбом
        GetAlbum - сервис возвращает альбом С треками и С автором
        GetAllSingers - возвращает всех авторов с их альбомами(альбомы без песен)
        GetTrackStream - возвращает файловый поток MP3 файла
        (Системная)TempFunc - темп функция, используется для правок в бд и на сервере
        (Системная)DownloadFile - темп функция, используется для правок на сервере(загружает файл)
        Search - возвращает результат поиска
        GetSingerFull - врзвращает полную информацию об исполнителе со всеми альбомами и песнями

        */

        #region Startupinit
        void InitUser(Client_User user)
        {
            ProfileEditItem.CurrentUser = user;
            Profile_tb.Text = user.NickName;
            AvatarBrush.ImageSource = ConvertToImage(user.Image);
        }
        public BitmapSource ConvertToImage(byte[] arr)
        {
            using (MemoryStream ms = new MemoryStream(arr))
            {
                var decoder = BitmapDecoder.Create(ms,
                    BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                return decoder.Frames[0];
            }
        }

        #endregion
        void ShowRecomendsBord()
        {
            Recommendations_Control recommendations_Control = new Recommendations_Control();
            tape_panel.Child = recommendations_Control;
        }
        void testInfoBord()
        {
            PlaylistGrid grid = new PlaylistGrid();
            grid.ScrollCall += Grid_ScrollCall;
            grid.ImageSource =@"C:\Player\InTimePlayer\InTime\Controls\3E-zsSjyGLU.jpg";
            grid.PlaylistDuration=DateTime.Now;
            grid.PlaylistName = "Custom playlist";
            grid.SongsCount = 322;
            List<PlaylistItem> lst = new List<PlaylistItem>();
            lst.Add(new PlaylistItem { SongAlbum = "Best Album", SongArtist = "Rammstein", ID = 2, SongDuration = DateTime.Now, SongTitle = "Sonne" });
            lst.Add(new PlaylistItem { SongAlbum = "Best Album", SongArtist = "Rammstein", ID = 2, SongDuration = DateTime.Now, SongTitle = "Sonne" });
            lst.Add(new PlaylistItem { SongAlbum = "Best Album", SongArtist = "Rammstein", ID = 2, SongDuration = DateTime.Now, SongTitle = "Sonne" });
            lst.Add(new PlaylistItem { SongAlbum = "Best Album", SongArtist = "Rammstein", ID = 2, SongDuration = DateTime.Now, SongTitle = "Sonne" });
            lst.Add(new PlaylistItem { SongAlbum = "Best Album", SongArtist = "Rammstein", ID = 2, SongDuration = DateTime.Now, SongTitle = "Sonne" });
            lst.Add(new PlaylistItem { SongAlbum = "Best Album", SongArtist = "Rammstein", ID = 2, SongDuration = DateTime.Now, SongTitle = "Sonne" });
            lst.Add(new PlaylistItem { SongAlbum = "Best Album", SongArtist = "Rammstein", ID = 2, SongDuration = DateTime.Now, SongTitle = "Sonne" });
            lst.Add(new PlaylistItem { SongAlbum = "Best Album", SongArtist = "Rammstein", ID = 2, SongDuration = DateTime.Now, SongTitle = "Sonne" });
            lst.Add(new PlaylistItem { SongAlbum = "Best Album", SongArtist = "Rammstein", ID = 2, SongDuration = DateTime.Now, SongTitle = "Sonne" });
            lst.Add(new PlaylistItem { SongAlbum = "Best Album", SongArtist = "Rammstein", ID = 2, SongDuration = DateTime.Now, SongTitle = "Sonne" });
            lst.Add(new PlaylistItem { SongAlbum = "Best Album", SongArtist = "Rammstein", ID = 2, SongDuration = DateTime.Now, SongTitle = "Sonne" });
            lst.Add(new PlaylistItem
            { SongAlbum = "Best Album", SongArtist = "Rammstein", ID = 2, SongDuration = DateTime.Now, SongTitle = "Sonne" });

            grid.SongList.ItemsSource = lst;

            grid.SimularSongList.Items.Add(new PlaylistItem
            { SongAlbum = "Wahrheit Oder Pflicht", SongArtist = "Oomph!", ID = 2, SongDuration = DateTime.Now, SongTitle = "Augen Auf!" });
            grid.SimularSongList.Items.Add(new PlaylistItem
            { SongAlbum = "Wahrheit Oder Pflicht", SongArtist = "Oomph!", ID = 2, SongDuration = DateTime.Now, SongTitle = "Burn Your Eyes" });
            grid.SimularSongList.Items.Add(new PlaylistItem
            { SongAlbum = "Wahrheit Oder Pflicht", SongArtist = "Oomph!", ID = 2, SongDuration = DateTime.Now, SongTitle = "Dein Weg" });
            tape_panel.Child = grid;
        }

        private void Grid_ScrollCall(bool flag)
        {
            if (!flag)
                main_Viewer.LineUp();
            else
                main_Viewer.LineDown();
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
            list.ScrollCall += Grid_ScrollCall;
            tape_panel.Child = list;
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
        private void SoundIcon_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            string tag=(string)SoundSlider.Tag;
            if (tag=="true")
            {
                sound_icon.Kind = PackIconKind.VolumeOff;
                SoundSlider.Tag = "false";
            }
            else
            {
                SoundSlider_OnValueChanged(null,null);
                SoundSlider.Tag = "true";
            }
        }
        private void SoundSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SoundSlider.Value < 30)
            {
                state.sound = SoundState.LowSound;
                sound_icon.Kind = PackIconKind.VolumeLow;
            }
            else if (SoundSlider.Value >= 30 && SoundSlider.Value <= 70)
            {
                state.sound = SoundState.MidSound;
                sound_icon.Kind = PackIconKind.VolumeMedium;
            }
            else if (SoundSlider.Value > 70)
            {
                state.sound = SoundState.HighSound;
                sound_icon.Kind = PackIconKind.VolumeHigh;
            }
        }
        #endregion
        #region LeftPanelLists

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

        #endregion
        #region MediaButtonsAnimation

        private void BottomButtonsBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            ((PackIcon)((Border)sender).Child).Foreground.BeginAnimation(SolidColorBrush.ColorProperty, new ColorAnimation((Color)ColorConverter.ConvertFromString("#FF784242"), TimeSpan.FromSeconds(0.1)));

        }

        private void BottomButtonsBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            ((PackIcon)((Border)sender).Child).Foreground.BeginAnimation(SolidColorBrush.ColorProperty, new ColorAnimation((Color)ColorConverter.ConvertFromString("#FFFF0051"), TimeSpan.FromSeconds(0.1)));
        }

        #endregion
        #region WindowMove/Minimize/Maximize/Resize
        bool upperPanelClick;
        Point coord;
        private void upper_bord_MouseDown(object sender, MouseButtonEventArgs e)
        {
            upperPanelClick = true;
            coord = e.GetPosition(mainWindow);
        }

        private void upper_bord_MouseMove(object sender, MouseEventArgs e)
        {
            if (upperPanelClick && coord != e.GetPosition(mainWindow))
            {
                if (coord.Y < e.GetPosition(mainWindow).Y)
                    mainWindow.Top += e.GetPosition(mainWindow).Y - coord.Y;
                else if (coord.Y > e.GetPosition(mainWindow).Y)
                    mainWindow.Top -= coord.Y - e.GetPosition(mainWindow).Y;
                if (coord.X < e.GetPosition(mainWindow).X)
                    mainWindow.Left += e.GetPosition(mainWindow).X - coord.X;
                else if (coord.X > e.GetPosition(mainWindow).X)
                    mainWindow.Left -= coord.X - e.GetPosition(mainWindow).X;
            }
        }

        private void upper_bord_MouseLeave(object sender, MouseEventArgs e)
        {
            upperPanelClick = false;
        }

        private void upper_bord_MouseUp(object sender, MouseButtonEventArgs e)
        {
            upperPanelClick = false;
        }

        private void WindowControlButton_MouseEnter(object sender, MouseEventArgs e)
        {
            ((PackIcon)((Border)sender).Child).Foreground = Brushes.White;
        }

        private void WindowControlButton_MouseLeave(object sender, MouseEventArgs e)
        {
            ((PackIcon)((Border)sender).Child).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF0051"));
        }

        private void WindowControlButton_Hide_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            mainWindow.WindowState = WindowState.Minimized;
        }

        private void WindowControlButton_Close_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            mainWindow.Close();
        }
        bool maximized = false;
        Point RestoreCoord;
        Point RestoreSize;
        private void WindowControlButton_Maximize_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            if (!maximized)
            {
                RestoreCoord = new Point(mainWindow.Left, mainWindow.Top);
                RestoreSize = new Point(mainWindow.Width, mainWindow.Height);
                MaximizedAnimation();
            }
            else
            {
                mainWindow.WindowState = WindowState.Normal;
                RestoreAnimation();
            }
            maximized = !maximized;
        }


        #endregion
        #region Restore
        void RestoreAnimation()
        {
            DoubleAnimation moveX = new DoubleAnimation(RestoreCoord.X, TimeSpan.FromSeconds(0.3), FillBehavior.Stop);
            DoubleAnimation moveY = new DoubleAnimation(RestoreCoord.Y, TimeSpan.FromSeconds(0.3), FillBehavior.Stop);
            DoubleAnimation Width = new DoubleAnimation(RestoreSize.X, TimeSpan.FromSeconds(0.3), FillBehavior.Stop);
            Width.Completed += HeightOff_Completed;
            mainWindow.BeginAnimation(Window.TopProperty, moveY);
            mainWindow.BeginAnimation(Window.LeftProperty, moveX); 
            mainWindow.BeginAnimation(Window.WidthProperty, Width);
            
        }

        private void HeightOff_Completed(object sender, EventArgs e)
        {
            DoubleAnimation Height = new DoubleAnimation(RestoreSize.Y, TimeSpan.FromSeconds(0.3), FillBehavior.Stop);
            mainWindow.BeginAnimation(Window.HeightProperty, Height);
            MaximizeRestor_Icon.Kind = PackIconKind.WindowMaximize;
        }
        #endregion
        #region Maximized
        void MaximizedAnimation()
        { 
            DoubleAnimation move = new DoubleAnimation(0, TimeSpan.FromSeconds(0.2), FillBehavior.Stop);
            DoubleAnimation Width = new DoubleAnimation(System.Windows.SystemParameters.PrimaryScreenWidth, TimeSpan.FromSeconds(0.2), FillBehavior.Stop);
            mainWindow.Height = 60;
            Width.Completed += WidthOn_Completed;
            mainWindow.BeginAnimation(Window.TopProperty, move);
            mainWindow.BeginAnimation(Window.LeftProperty, move);
            mainWindow.BeginAnimation(Window.WidthProperty, Width);
        }

        private void WidthOn_Completed(object sender, EventArgs e)
        {
            //Console.WriteLine(mainWindow.Top);
            DoubleAnimation Height = new DoubleAnimation(System.Windows.SystemParameters.PrimaryScreenHeight, TimeSpan.FromSeconds(0.4), FillBehavior.Stop);
            Height.Completed += HeightOn_Completed;
            mainWindow.BeginAnimation(Window.HeightProperty, Height);
        }

        private void HeightOn_Completed(object sender, EventArgs e)
        {
            mainWindow.WindowState = WindowState.Maximized;
            MaximizeRestor_Icon.Kind = PackIconKind.WindowRestore;
        }
        #endregion
        #region Search

        private async void Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (Search.Text.Length < 1)
                return;
            if (e.Key == Key.Enter)
            {
                LoadingScreen();
                SearchPanel searchPanel = new SearchPanel();
                Service1Client client = new Service1Client();
                SearchResult searchResult = await client.SearchAsync(Search.Text);
                client.Close();
                searchPanel.AddSongs(searchResult.Songs);
                searchPanel.AddArtists(searchResult.Singers);
                searchPanel.AddGenres(searchResult.GenreSongs);
                searchPanel.AddAlbums(searchResult.Albums);
                tape_panel.Child = null;
                tape_panel.Child = searchPanel;
            }
        }
        void LoadingScreen()
        {
            tape_panel.Child = null;
            ProgressBar progressBar = new ProgressBar();
            progressBar.Style = FindResource("MaterialDesignCircularProgressBar") as Style;
            progressBar.Value = 0;
            progressBar.Foreground = Brushes.White;
            progressBar.Width = 50;
            progressBar.Height = 50;
            progressBar.VerticalAlignment = VerticalAlignment.Center;
            progressBar.HorizontalAlignment = HorizontalAlignment.Center;
            progressBar.IsIndeterminate = true;
            tape_panel.Child = progressBar;
        }

        #endregion
        #region PlaylisAdding

        private void AddPlaylistBtnClick(object sender, RoutedEventArgs e)
        {
            AddPlaylistItem.Visibility = Visibility.Visible;
            LeftPanel.Opacity = 0.7;
            CentralPanel.Opacity = 0.7;
            BottomPanel.Opacity = 0.7;
            Panel.SetZIndex(GridContainer, 2);
            Panel.SetZIndex(AddPlaylistItem, 2);
        }

        private void Window_OnPlaylistAdded(string name, string path)
        {
            Console.WriteLine(name, "  ", path);
        }


        private void AddPlaylistItemMouseDown(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(AddPlaylistItem);
            if (!(p.X >= 485 && p.X <= 935 && p.Y >= 185 && p.Y <= 535) && AddPlaylistItem._IsImageEdited == true)
            {
                AddPlaylistItem.Visibility = Visibility.Hidden;
                LeftPanel.Opacity = 1;
                CentralPanel.Opacity = 1;
                BottomPanel.Opacity = 1;
                Panel.SetZIndex(GridContainer, -1);
                Panel.SetZIndex(AddPlaylistItem, -1);
            }
            else
            {
                AddPlaylistItem._IsImageEdited = true;
            }
        }

        private void AddPlaylistItemOnChangesAccepted()
        {
            Console.WriteLine($"{AddPlaylistItem.PlaylistName} {AddPlaylistItem.ImagePath}");
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri(Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.LastIndexOf("InTime")) + @"InTime\Assets\playlisticon.png"));
            AddPlaylistItem.PlaylistImg.Fill = ib;
            AddPlaylistItem.PlaylistNameBox.Text = "";
            AddPlaylistItem.PlaylistName = "";
            AddPlaylistItem.Visibility = Visibility.Hidden;
            LeftPanel.Opacity = 1;
            CentralPanel.Opacity = 1;
            BottomPanel.Opacity = 1;
            Panel.SetZIndex(GridContainer, -1);
            Panel.SetZIndex(AddPlaylistItem, -1);
        }

        private void AddPlaylistItemOnWindowClosed()
        {
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri(Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.LastIndexOf("InTime")) + @"InTime\Assets\playlisticon.png"));
            AddPlaylistItem.PlaylistImg.Fill = ib;
            AddPlaylistItem.PlaylistNameBox.Text = "";
            AddPlaylistItem.PlaylistName = "";
            AddPlaylistItem.Visibility = Visibility.Hidden;
            LeftPanel.Opacity = 1;
            CentralPanel.Opacity = 1;
            BottomPanel.Opacity = 1;
            Panel.SetZIndex(GridContainer, -1);
            Panel.SetZIndex(AddPlaylistItem, -1);
        }

        #endregion
        #region PlaylistsContextMenu

        private void PlaylistDeleteMenu(object sender, RoutedEventArgs e)
        {

        }

        private void PlaylistEditmenu(object sender, RoutedEventArgs e)
        {
            AddPlaylistBtnClick(this,null);
        }

        #endregion
        #region ProfileEdit

        private void EditProfile(object sender, RoutedEventArgs e)
        {
            ProfileEditItem.Visibility = Visibility.Visible;
            LeftPanel.Opacity = 0.7;
            CentralPanel.Opacity = 0.7;
            BottomPanel.Opacity = 0.7;
            Panel.SetZIndex(GridContainer, 2);
            Panel.SetZIndex(ProfileEditItem, 2);
            ProfileEditItem.InitUser();
        }

        private void ProfileEditItemOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(ProfileEditItem);
            if (!(p.X >= 460 && p.X <= 960 && p.Y >= 60 && p.Y <= 660) && !ProfileEditItem.FilePicked)
            {
                ProfileEditItem.Visibility = Visibility.Hidden;
                LeftPanel.Opacity = 1;
                CentralPanel.Opacity = 1;
                BottomPanel.Opacity = 1;
                Panel.SetZIndex(GridContainer, -1);
                Panel.SetZIndex(ProfileEditItem, -1);
            }
            ProfileEditItem.FilePicked = false;
        }

        private void ProfileEditItemOnEditCancel()
        {
            ProfileEditItem.Visibility = Visibility.Hidden;
            LeftPanel.Opacity = 1;
            CentralPanel.Opacity = 1;
            BottomPanel.Opacity = 1;
            Panel.SetZIndex(GridContainer, -1);
            Panel.SetZIndex(ProfileEditItem, -1);
            ProfileEditItem.FilePicked = false;
        }

        private void ProfileEditItemOnProfileEdited(Client_User user)
        {
            state.user = user;
            InitUser(user);
        }

        #endregion

    }
}

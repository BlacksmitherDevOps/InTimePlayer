using InTime.Controls.Rectangular_Item_Panels;
using InTime.Controls.Rectangular_Items;
using InTime.ServiceReference1;
using MaterialDesignThemes.Wpf;
using NAudio.Wave;
using System;
using System.IO;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace InTime.Controls
{
    public partial class MainPlayerPage_Control : UserControl
    {
        private AppState state;
        Window mainWindow;
        public MainPlayerPage_Control(Window window, Client_User user)
        {
            InitializeComponent();
            state = new AppState(user);
            mainWindow = window;

            InitUser(user);
            ShowRecomendsBord();
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
            PlaylistBox.ItemsSource = state.user.Playlists;
            AddPlaylistItem.Playlist.Creator = user;
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
        async void ShowRecomendsBord()
        {
            Recommendations_Control recommendations_Control = new Recommendations_Control();
            recommendations_Control.ScrollCall += Grid_ScrollCall;
            recommendations_Control.UserName = ProfileEditItem.CurrentUser.NickName;
            recommendations_Control.OpenPlaylist += Recommendations_Control_OpenPlaylist;
            recommendations_Control.Init();
            tape_panel.Child = recommendations_Control;
        }
        async void ShowfavoritePlaylistsBord()
        {
            FavoritePlaylists favoritePlaylists = new FavoritePlaylists();
            favoritePlaylists.OpenPlaylist += OpenPlaylist;
            Service1Client client = new Service1Client();
            favoritePlaylists.AddList(await client.GetFavoritePlaylistsAsync(state.user.ID));
            tape_panel.Child = favoritePlaylists;
        }


        public async void ShowFavoritesBord()
        {
            LoadingScreen();
            NoImageList_Control noImageList_Control = Init_NoImageList_Control();
            Service1Client client = new Service1Client();
            noImageList_Control.CurrentPlaylist = await client.GetFavoriteTracksPlaylistAsync(state.user.ID);
            if (tape_panel.Child.GetType() != typeof(ProgressBar))
                return;
            noImageList_Control.Init();
            tape_panel.Child = noImageList_Control;
            client.Close();
        }

        public async void ShowRecentlyPlayedBord()
        {
            LoadingScreen();
            NoImageList_Control noImageList_Control = Init_NoImageList_Control();
            Service1Client client = new Service1Client();
            noImageList_Control.CurrentPlaylist = await client.GetRecentlyPlayedAsync(state.user.ID);
            if (tape_panel.Child.GetType() != typeof(ProgressBar))
                return;
            noImageList_Control.Init();
            tape_panel.Child = noImageList_Control;
            client.Close();
        }
        NoImageList_Control Init_NoImageList_Control()
        {
            NoImageList_Control noImageList_Control = new NoImageList_Control();
            noImageList_Control.OpenSingerPage += Playlist_OpenSingerPage;
            noImageList_Control.UserPlaylistChanged += UserPlaylistChanged;
            noImageList_Control.OnDragStarted += Playlist_OnDragStarted;
            noImageList_Control.ScrollCall += Grid_ScrollCall;

            noImageList_Control.PlaylistsInfo = state.Playlists;
            noImageList_Control.CurrentUser1 = state.user;
            return noImageList_Control;
        }
        async void OpenPlaylist(int id)
        {
            LoadingScreen();
            PlaylistGrid playlist = new PlaylistGrid(state.user);
            playlist.PlaylistsInfo = state.Playlists;
            playlist.OpenSingerPage += Playlist_OpenSingerPage;
            playlist.UserPlaylistChanged += UserPlaylistChanged;
            playlist.OnDragStarted += Playlist_OnDragStarted;
            playlist.PlaySong += Playlist_PlaySong;
            playlist.CurrentListboxUPD += Playlist_CurrentListboxUPD; ;
            playlist.PauseSong += Playlist_PauseSong;
            Service1Client client = new Service1Client();
            Song_Playlist _Playlist = await client.GetPlaylistByIDAsync(id);
            if (tape_panel.Child.GetType() != typeof(ProgressBar))
                return;
            client.Close();
            playlist.CurrentPlaylist = _Playlist;
            playlist.CurrentUser = state.user;
            playlist.Init();
            tape_panel.Child = playlist;
        }

        private void Playlist_CurrentListboxUPD(ListBox listBox)
        {
            state.current_ListBox = listBox;
        }

        private void Playlist_PauseSong()
        {
            player.Pause();
        }
        Song Current_Song;
        private void Playlist_PlaySong(Song song)
        {
            if(Current_Song == null)
            {
                Current_Song = song;
                PlaySongByID(Current_Song.ID);
                return;
            }
            if (song.ID != Current_Song.ID)
            {
                Current_Song = song;
                PlaySongByID(Current_Song.ID);
            }
            else player.Play();
        }

        private async void UserPlaylistChanged()
        {
            Service1Client client = new Service1Client();
            state.user.Playlists = await client.GetUserPlaylistsInfoAsync(state.user.ID);
            state.user.FavoritePlaylists = await client.GetUserFavoritePlaylistsInfoAsync(state.user.ID);
            client.Close();
            PlaylistBox.ItemsSource = null;
            PlaylistBox.ItemsSource = state.user.Playlists;
        }
        private async void Playlist_OpenSingerPage(int id)
        {
            LoadingScreen();
            OpenSingerPage(id);
        }
        private async void Recommendations_Control_OpenPlaylist(int id)
        {
            LoadingScreen();
            OpenPlaylist(id);
        }
        async void OpenSingerPage(int id)
        {
            Service1Client client = new Service1Client();
            Song_Singer singer;
            singer = await client.GetSingerFullAsync(id);
            SingerPage_Control singerPage_Control = new SingerPage_Control(singer);
            singerPage_Control.ScrollCall += Grid_ScrollCall;
            tape_panel.Child = singerPage_Control;
        }

        #region PlaySong
        MediaPlayer player = new MediaPlayer();
        async void PlaySongByID(int ID)
        {
            Service1Client client = new Service1Client();
            string path = Environment.CurrentDirectory + "\\temp.mp3";
            UpdateBottomPanel();
            byte[] file = await client.GetTrackAsync(ID);
            if (state.Queue.Peek().ID != ID)
                return;
            File.WriteAllBytes(path, file);
            if (state.Queue.Peek().ID != ID)
                return;
            PlaySong(path);
            client.Close();
        }
        async void UpdateBottomPanel()
        {
            singerBottomField.Text = Current_Song.Singers[0].Name;
            songBottomField.Text = Current_Song.Title;
            Service1Client client = new Service1Client();
            songImg.Source = ConvertToImage(await client.GetAlbumImageAsync(Current_Song.Album.ID));
            bottomDuration_slider.Maximum = Current_Song.Duration.TotalSeconds;
            bottomMaxDuration_tb.Text = Current_Song.Duration.ToString(@"mm\:ss");
        }
        void PlaySong(string path)
        {
            player.Open(new Uri(path));
            player.Volume = 0.3;
            player.MediaEnded += Player_MediaEnded;
            player.Play();
        }

        private void Player_MediaEnded(object sender, EventArgs e)
        {
            state.Queue.Dequeue();
            if (state.Queue.Count > 0)
            {
                state.current_ListBox.SelectedIndex = state.current_ListBox.Items.IndexOf(state.Queue.Peek());
                PlaySongByID(state.Queue.Peek().ID);
            }
        }
        private void Next_btn_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            player.Close();
            Player_MediaEnded(null,null);
        }
        #endregion
        #region failed player
        private Stream ms = new MemoryStream();
        //public void PlayMp3FromUrl(Stream song_stream)
        //{
        //    new Thread(delegate (object o)
        //    {
        //        using (var stream = song_stream)
        //        {
        //            byte[] buffer = new byte[65536]; // 64KB chunks
        //            int read;
        //            while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
        //            {
        //                var pos = ms.Position;
        //                ms.Position = ms.Length;
        //                ms.Write(buffer, 0, read);
        //                ms.Position = pos;
        //            }
        //        }
        //    }).Start();

        //    // Pre-buffering some data to allow NAudio to start playing
        //    new Thread(delegate (object o)
        //    {
        //        while (ms.Length < 65536 * 10)
        //        Thread.Sleep(1000);

        //    ms.Position = 0;
        //    using (WaveStream blockAlignedStream = new BlockAlignReductionStream(WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader(ms))))
        //    {
        //            waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback());
        //            waveOut.Init(blockAlignedStream);
        //            waveOut.Play();
        //            while (waveOut.PlaybackState == PlaybackState.Playing)
        //            {
        //                System.Threading.Thread.Sleep(100);
        //            }
        //    }
        //    }).Start();
        //}
        WaveOut waveOut;
        #endregion
        private void Grid_ScrollCall(bool flag)
        {
            if (!flag)
            {
                main_Viewer.LineUp();
                main_Viewer.LineUp();
            }
            else
            {
                main_Viewer.LineDown();
                main_Viewer.LineDown();
            }
        }

        private async void ShowAlbumsControl()
        {
            
        }

        private async void ShowRecentlyPlayedControl()
        {

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
            if (state == null)
                return;
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
            if (((RadioButton)sender).Content.ToString() == "Recomendations")
                ShowRecomendsBord();
            else if (((RadioButton)sender).Content.ToString() == "Albums")
                ShowfavoritePlaylistsBord();
            else if (((RadioButton)sender).Content.ToString() == "Recently Listened")
                ShowRecentlyPlayedBord();
            else if (((RadioButton)sender).Content.ToString() == "Favorites")
                ShowFavoritesBord();
        }
        private void PlaylistBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PlaylistBox.SelectedIndex != -1)
            {
                foreach (var child in RadioBtnsPanel.Children)
                {
                    (child as RadioButton).IsChecked = false;
                }

                OpenPlaylist((PlaylistBox.SelectedItem as Song_Playlist).ID);
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

                //prop
                searchPanel.Search_Str = searchResult.Search_Str;
                searchPanel.AddSongs(searchResult.Songs);
                searchPanel.AddArtists(searchResult.Singers);
                searchPanel.AddGenres(searchResult.Playlists);
                searchPanel.AddAlbums(searchResult.Albums);

                //events
                searchPanel.ViewAllSongs += SearchPanel_ViewAllSongs;
                searchPanel.ViewAllAlbums += SearchPanel_ViewAllAlbums;
                searchPanel.ViewAllSingers += SearchPanel_ViewAllSingers;
                searchPanel.ViewAllPlaylists += SearchPanel_ViewAllPlaylists;

                tape_panel.Child = null;
                tape_panel.Child = searchPanel;
            }
        }

        private async void SearchPanel_ViewAllPlaylists(string search_str)
        {
            LoadingScreen();
            Service1Client client = new Service1Client();
            var list = await client.Search_GetAllPlaylistsAsync(search_str);
            client.Close();

            if (tape_panel.Child.GetType() != typeof(ProgressBar))
                return;

            SearchResult_Playlists_Panel playlist_page = new SearchResult_Playlists_Panel();
            playlist_page.AddList(list);
            playlist_page.OpenPlaylist += Recommendations_Control_OpenPlaylist;

            tape_panel.Child = null;
            tape_panel.Child = playlist_page;
        }

        private async void SearchPanel_ViewAllSingers(string search_str)
        {
            LoadingScreen();
            Service1Client client = new Service1Client();
            var list = await client.Search_GetAllSongSingersAsync(search_str);
            client.Close();

            if (tape_panel.Child.GetType() != typeof(ProgressBar))
                return;

            SearchResult_Singers_Panel singer_Panel = new SearchResult_Singers_Panel();
            singer_Panel.AddList(list);
            singer_Panel.OpenSingerPage += Playlist_OpenSingerPage;

            tape_panel.Child = null;
            tape_panel.Child = singer_Panel;
        }

        private async void SearchPanel_ViewAllAlbums(string search_str)
        {
            LoadingScreen();
            Service1Client client = new Service1Client();
            var list = await client.Search_GetAllSingerAlbumsAsync(search_str);
            client.Close();

            if (tape_panel.Child.GetType() != typeof(ProgressBar))
                return;

            SearchResult_Albums_Panel albums_Panel = new SearchResult_Albums_Panel();
            albums_Panel.AddList(list);
            albums_Panel.OpenAlbum += Albums_Panel_OpenAlbum;

            tape_panel.Child = null;
            tape_panel.Child = albums_Panel;
        }

        private async void Albums_Panel_OpenAlbum(int id)
        {
            LoadingScreen();
            Service1Client client = new Service1Client();
            var album = await client.GetAlbumAsync(id);
            client.Close();

            if (tape_panel.Child.GetType() != typeof(ProgressBar))
                return;

            AlbumGrid albumGrid = new AlbumGrid(album);
            albumGrid.ScrollCall += Grid_ScrollCall;

            tape_panel.Child = null;
            tape_panel.Child = albumGrid;
        }


        private async void SearchPanel_ViewAllSongs(string search_str)
        {
            LoadingScreen();
            Service1Client client = new Service1Client();
            var list = await client.Search_GetAllSongsAsync(search_str);
            client.Close();

            if (tape_panel.Child.GetType() != typeof(ProgressBar))
                return;
            NoImageList_Control noImageList_Control = Init_NoImageList_Control();
            noImageList_Control.CurrentPlaylist = list;
            noImageList_Control.Init();
            tape_panel.Child = noImageList_Control;
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
            Console.WriteLine(AddPlaylistItem.Playlist.ID);
            state.playlists.Add(AddPlaylistItem.Playlist);
            PlaylistBox.ItemsSource = state.playlists;
            AddPlaylistItem.Playlist = new Song_Playlist();
            AddPlaylistItem.Playlist.Creator = state.user;
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri(Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.LastIndexOf("InTime")) + @"InTime\Assets\playlisticon.png"));
            AddPlaylistItem.PlaylistImg.Fill = ib;
            AddPlaylistItem.PlaylistNameBox.Text = "";
            AddPlaylistItem.Visibility = Visibility.Hidden;
            LeftPanel.Opacity = 1;
            CentralPanel.Opacity = 1;
            BottomPanel.Opacity = 1;
            Panel.SetZIndex(GridContainer, -1);
            Panel.SetZIndex(AddPlaylistItem, -1);
        }

        private void AddPlaylistItemOnWindowClosed()
        {
            AddPlaylistItem.Playlist = new Song_Playlist();
            AddPlaylistItem.Playlist.Creator = state.user;
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri(Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.LastIndexOf("InTime")) + @"InTime\Assets\playlisticon.png"));
            AddPlaylistItem.PlaylistImg.Fill = ib;
            AddPlaylistItem.PlaylistNameBox.Text = "";
            AddPlaylistItem.Visibility = Visibility.Hidden;
            LeftPanel.Opacity = 1;
            CentralPanel.Opacity = 1;
            BottomPanel.Opacity = 1;
            Panel.SetZIndex(GridContainer, -1);
            Panel.SetZIndex(AddPlaylistItem, -1);
        }

        #endregion
        #region PlaylistsContextMenu

        private async void PlaylistDeleteMenu(object sender, RoutedEventArgs e)
        {
            try
            {
                Service1Client client = new Service1Client();
                if (await client.DeletePlaylistAsync((PlaylistBox.SelectedItem as Song_Playlist).ID))
                {
                    state.Playlists.Remove((PlaylistBox.SelectedItem as Song_Playlist));
                    if (PlaylistBox.SelectedIndex > 0)
                    {
                        OpenPlaylist((PlaylistBox.Items[PlaylistBox.SelectedIndex-1] as Song_Playlist).ID);
                        PlaylistBox.SelectedIndex -= 1;
                    }
                    else
                    {
                        OpenPlaylist((PlaylistBox.Items[PlaylistBox.SelectedIndex+1] as Song_Playlist).ID);
                        PlaylistBox.SelectedIndex += 1;
                    }
                    PlaylistBox.ItemsSource = state.Playlists;
                    //PlaylistBox.Items.Remove(PlaylistBox.SelectedItem as Song_Playlist);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private async void Play_btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PlaySongByID(21);
        }


        private void RigtMouseBtnDown(object sender, MouseButtonEventArgs e)
        {
        }

        #region DragDrop
        private void Playlist_OnDragStarted(int id, int _idPlaylist)
        {
            foreach (RadioButton button in RadioBtnsPanel.Children)
            {
                if (button.Content.ToString() != "Favorites")
                {
                    button.IsEnabled = false;
                    button.Foreground = new SolidColorBrush(Colors.Gray);
                }
            }
            state.DraggedIndex = id;
        }

        private async void OnItemDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (state.DraggedIndex != -1)
                {
                    Service1Client client = new Service1Client();
                    Console.WriteLine(state.DraggedIndex);
                    Console.WriteLine(((sender as ListBoxItem).Content as Song_Playlist).Title);
                    Console.WriteLine(
                        await client.AddSongToPlaylistAsync(state.DraggedIndex,
                            ((sender as ListBoxItem).Content as Song_Playlist).ID));
                    state.DraggedIndex = -1;
                }
            }
            catch (FaultException<LoadPlaylistFailed> ex)
            {
                Console.WriteLine(ex.Detail.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void FavoritesPlaylistDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (state.DraggedIndex != -1)
                {
                    Service1Client client = new Service1Client();
                    await client.AddTrackToFavoriteAsync(state.user.ID,
                        state.DraggedIndex);
                    state.DraggedIndex = -1;
                }
            }
            catch (FaultException<LoadPlaylistFailed> ex)
            {
                Console.WriteLine(ex.Detail.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OwnerGridMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                foreach (RadioButton button in RadioBtnsPanel.Children)
                {
                    button.IsEnabled = true;
                    button.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF0051"));
                }
            }
        }


        #endregion

       
    }
}

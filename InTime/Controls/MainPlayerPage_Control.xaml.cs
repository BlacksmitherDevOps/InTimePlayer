using InTime.Controls.Rectangular_Item_Panels;
using InTime.Controls.Rectangular_Items;
using InTime.ServiceReference1;
using MaterialDesignThemes.Wpf;
using NAudio.Wave;
using System;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Resources;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

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
            if (noImageList_Control.CurrentPlaylist.ID == state.currentPlaylist.ID && noImageList_Control.SongList.SelectedIndex < 0&&state.currentSong!=null)
            {
                noImageList_Control.SongList.SelectedIndex = noImageList_Control.CurrentPlaylist.Songs.ToList()
                    .IndexOf(noImageList_Control.CurrentPlaylist.Songs.Where(n => n.ID == state.currentSong.ID)
                        .First());
            }
            tape_panel.Child = noImageList_Control;
            state.Current_Tab = noImageList_Control;
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
            noImageList_Control.OnSongPlaying += Playlist_OnSongPlaying;
            noImageList_Control.OnSongPaused+= Playlist_OnSongPaused;
            noImageList_Control.PlaylistsInfo = state.Playlists;
            noImageList_Control.CurrentUser1 = state.user;
            return noImageList_Control;
        }
        async void OpenPlaylist(int id)
        {
            
            LoadingScreen();
            PlaylistGrid playlist = new PlaylistGrid(state.user);
            playlist.OpenSingerPage += Playlist_OpenSingerPage;
            playlist.UserPlaylistChanged += UserPlaylistChanged;
            playlist.OnDragStarted += Playlist_OnDragStarted;
            playlist.ScrollCall += Grid_ScrollCall;
            playlist.OnSongPlaying += Playlist_OnSongPlaying;
            playlist.OnSongPaused += Playlist_OnSongPaused;
            Service1Client client = new Service1Client();
            Song_Playlist _Playlist = await client.GetPlaylistByIDAsync(id);
            if (tape_panel.Child.GetType() != typeof(ProgressBar))
                return;
            client.Close();
            playlist.PlaylistsInfo = state.Playlists;
            playlist.CurrentPlaylist = _Playlist;
            playlist.CurrentUser = state.user;
            playlist.Init();
            tape_panel.Child = playlist;
            state.Current_Tab = playlist;
            if (playlist.SongList.SelectedIndex < 0)
            {
                if (playlist.CurrentPlaylist.ID == state.currentPlaylist.ID)
                {
                    playlist.SongList.SelectedIndex = playlist.CurrentPlaylist.Songs.ToList().IndexOf(playlist.CurrentPlaylist.Songs.Where(n => n.ID == state.currentSong.ID).First());
                }
            }
        }

        private void Playlist_OnSongPaused()
        {
            if (state.player.CanPause)
                state.player.Pause();
        }

        private void Playlist_OnSongPlaying(Song_Playlist playlist, int songId,Song song)
        {
                Console.WriteLine(songId);
                Console.WriteLine(playlist.Songs[1].ID);
                state.currentSong = song;
                state.currentPlaylist = playlist;
                PlaySongByID(songId);
            
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
            state.Current_Tab = singerPage_Control;
            singerPage_Control.AlbumPlay += Playlist_OnSongPlaying;
            singerPage_Control.AlbumPause += Playlist_OnSongPaused;
        }

        #region PlaySong
        async void PlaySongByID(int ID)
        {
            if (state.player.CanPause)
            {
                state.player.Pause();
                state.player.Close();
                state.playTimer.Tick -= PlayTimer_Tick;
            }
            Service1Client client = new Service1Client();
            string path = Environment.CurrentDirectory + "\\temp.mp3";
            byte[] file = await client.GetTrackAsync(ID);
            if (state.currentSong.ID != ID)
                return;
            File.WriteAllBytes(path, file);
            if (state.currentSong.ID != ID)
                return;
            PlaySong(path);
            client.Close();
        }
        void PlaySong(string path)
        {
            state.player.Stop();
            state.player.Open(new Uri(path));
            state.player.MediaOpened += Player_MediaOpened;
            state.player.MediaEnded += Player_MediaEnded;
            state.player.Play();
        }

        private async void Player_MediaOpened(object sender, EventArgs e)
        {
            Service1Client client = new Service1Client();
            try
            {
                foreach (Song_Singer singer in state.currentSong.Singers)
                {
                    singerBottomField.Text=singer==state.currentSong.Singers[state.currentSong.Singers.Length-1]? singer.Name:singer.Name + ",";
                }
                songBottomField.Text = state.currentSong.Title;
                bottomMaxDuration_tb.Text = state.currentSong.Duration.ToString(@"mm\:ss");
                songImg.Source = ConvertToImage(await client.GetAlbumImageAsync(state.currentSong.Album.ID));
                bottomDuration_slider.Value = 0;
                bottomDuration_slider.Maximum = state.player.NaturalDuration.TimeSpan.TotalSeconds;
                state.playTimer.Interval = TimeSpan.FromSeconds(1);
                state.playTimer.Tick += PlayTimer_Tick;
                Console.WriteLine(bottomDuration_slider.Value);
                Console.WriteLine(bottomDuration_slider.Maximum);
                state.playTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PlayTimer_Tick(object sender, EventArgs e)
        {
            if (state.player.Position.Seconds < 10)
                bottomCurrentDuration_tb.Text = $"{state.player.Position.Minutes}:" + $"0{state.player.Position.Seconds}";
            else
            {
                bottomCurrentDuration_tb.Text = $"{state.player.Position.Minutes}:" + $"{state.player.Position.Seconds}";
            }
            bottomDuration_slider.Value += 1;
        }

        private void Player_MediaEnded(object sender, EventArgs e)
        {
            if (state.repeat == RepeatState.NoRepeat)
            {
                Next_btn_MouseLeftButtonUp(this, null);
            }
            else
            {
                Playlist_OnSongPlaying(state.currentPlaylist,
                    state.currentPlaylist.Songs[Array.IndexOf(state.currentPlaylist.Songs, state.currentSong)].ID,
                    state.currentPlaylist.Songs[Array.IndexOf(state.currentPlaylist.Songs, state.currentSong)]);
            }
        }
        private void Next_btn_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (state.player.HasAudio)
            {
                state.playTimer.Stop();
                if (Array.IndexOf(state.currentPlaylist.Songs, state.currentSong) ==
                    state.currentPlaylist.Songs.Length - 1)
                {
                    Playlist_OnSongPlaying(state.currentPlaylist, state.currentPlaylist.Songs[0].ID,
                        state.currentPlaylist.Songs[0]);
                    ChangeIndex("0");
                }
                else
                {
                    Playlist_OnSongPlaying(state.currentPlaylist,
                        state.currentPlaylist.Songs[Array.IndexOf(state.currentPlaylist.Songs, state.currentSong) + 1].ID,
                        state.currentPlaylist.Songs[Array.IndexOf(state.currentPlaylist.Songs, state.currentSong) + 1]);
                    ChangeIndex("+");
                }
            }
        }
        private void Prev_btn_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (state.player.HasAudio)
            {
                state.playTimer.Stop();
                if (state.player.Position >= TimeSpan.FromSeconds(5))
                {
                    bottomCurrentDuration_tb.Text = "0:00";
                    state.player.Position = TimeSpan.FromSeconds(0);
                    bottomDuration_slider.Value = 0;
                    state.playTimer.Start();
                }
                else
                {
                    if (state.currentSong == state.currentPlaylist.Songs[0])
                    {
                        bottomCurrentDuration_tb.Text = "0:00";
                        state.player.Position = TimeSpan.FromSeconds(0);
                        bottomDuration_slider.Value = 0;
                        state.playTimer.Start();
                    }
                    else
                    {
                        Playlist_OnSongPlaying(state.currentPlaylist,
                            state.currentPlaylist.Songs[Array.IndexOf(state.currentPlaylist.Songs, state.currentSong) - 1].ID,
                            state.currentPlaylist.Songs[Array.IndexOf(state.currentPlaylist.Songs, state.currentSong) - 1]);
                        ChangeIndex("-");
                    }

                }
            }
        }
        #endregion

        private void ChangeIndex(string value)
        {
            if (state.Current_Tab != null)
            {
                if (state.Current_Tab is PlaylistGrid)
                {
                    if ((state.Current_Tab as PlaylistGrid).SongList.SelectedIndex >= 0)
                    {
                        if (((state.Current_Tab as PlaylistGrid).CurrentPlaylist) == state.currentPlaylist)
                        {
                            (state.Current_Tab as PlaylistGrid).SongList.SelectedIndex = value == "0"?0: value == "+"?
                                (state.Current_Tab as PlaylistGrid).SongList.SelectedIndex + 1:
                                (state.Current_Tab as PlaylistGrid).SongList.SelectedIndex - 1;
                        }
                    }
                }
                else if (state.Current_Tab is NoImageList_Control)
                {
                    if ((state.Current_Tab as NoImageList_Control).SongList.SelectedIndex >= 0)
                    {
                        if (((state.Current_Tab as NoImageList_Control).CurrentPlaylist) == state.currentPlaylist)
                        {
                            (state.Current_Tab as NoImageList_Control).SongList.SelectedIndex = value == "0" ? 0 : value == "+" ?
                                (state.Current_Tab as NoImageList_Control).SongList.SelectedIndex + 1 :
                                (state.Current_Tab as NoImageList_Control).SongList.SelectedIndex - 1;
                        }
                    }
                }
                else if (state.Current_Tab is SingerPage_Control)
                {
                    Console.WriteLine("Singer page");
                    foreach (var child in (state.Current_Tab as SingerPage_Control).albums_panel.Children)
                    {
                        if (child is AlbumGrid)
                        {
                            if ((child as AlbumGrid).songs_lb.SelectedIndex >= 0)
                            {
                                Console.WriteLine(((child as AlbumGrid).songs_lb.SelectedItem as Song).Title);
                            }
                        }
                    }
                }

            }
        }

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
        private void SoundIcon_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            string tag=(string)SoundSlider.Tag;
            if (tag=="true")
            {
                sound_icon.Kind = PackIconKind.VolumeOff;
                SoundSlider.Value = 0;
                state.player.IsMuted = true;
                SoundSlider.Tag = "false";
            }
            else
            {
                //SoundSlider_OnValueChanged(null,null);
                state.player.IsMuted = false;
                SoundSlider.Value = state.player.Volume;
                Console.WriteLine(state.player.Volume);
                SoundSlider.Tag = "true";
            }
        }
        private void SoundSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (state == null)
            {
                return;
            }
            else
            {
                if (SoundSlider.Value != 0)
                {
                    if (SoundSlider.Value < 0.3 && SoundSlider.Value > 0)
                    {
                        state.sound = SoundState.LowSound;
                        sound_icon.Kind = PackIconKind.VolumeLow;
                    }
                    else if (SoundSlider.Value >= 0.3 && SoundSlider.Value <= 0.7)
                    {
                        state.sound = SoundState.MidSound;
                        sound_icon.Kind = PackIconKind.VolumeMedium;
                    }
                    else if (SoundSlider.Value > 0.7)
                    {
                        state.sound = SoundState.HighSound;
                        sound_icon.Kind = PackIconKind.VolumeHigh;
                    }

                    state.player.Volume = SoundSlider.Value;
                }
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
            if(((Border)sender)!= Repeat_btn)
            {
                ((PackIcon)((Border)sender).Child).Foreground.BeginAnimation(SolidColorBrush.ColorProperty, new ColorAnimation((Color)ColorConverter.ConvertFromString("#FF784242"), TimeSpan.FromSeconds(0.1)));
            }
            else if (state.repeat != RepeatState.RepeatSong)
            {
                ((PackIcon)((Border)sender).Child).Foreground.BeginAnimation(SolidColorBrush.ColorProperty, new ColorAnimation((Color)ColorConverter.ConvertFromString("#FF784242"), TimeSpan.FromSeconds(0.1)));
            }

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
            if (AddPlaylistItem.IsEdited)
            {
                AddPlaylistItem.Playlist=PlaylistBox.SelectedItem as Song_Playlist;
                AddPlaylistItem.PlaylistNameBox.Text =(PlaylistBox.SelectedItem as Song_Playlist).Title;
                if ((PlaylistBox.SelectedItem as Song_Playlist).Image != null)
                {
                    ImageBrush img = new ImageBrush();
                    img.ImageSource = ConvertToImage((PlaylistBox.SelectedItem as Song_Playlist).Image);
                    AddPlaylistItem.PlaylistImg.Fill = img;
                }
            }
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
            if (!AddPlaylistItem.IsEdited)
            {
                state.playlists.Add(AddPlaylistItem.Playlist);
                PlaylistBox.ItemsSource = state.playlists;
            }
            else
            {
                state.playlists[state.playlists.IndexOf(PlaylistBox.SelectedItem as Song_Playlist)]=AddPlaylistItem.Playlist;
                if (AddPlaylistItem._IsImageEdited)
                {
                    (state.Current_Tab as PlaylistGrid).ImageSource = AddPlaylistItem.Playlist.Image;
                    (state.Current_Tab as PlaylistGrid).playlistName_tb.Text = AddPlaylistItem.Playlist.Title;
                }
                AddPlaylistItem.IsEdited = false;
            }
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
            AddPlaylistItem.IsEdited = true;
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
                    if (state.Current_Tab != null)
                    {
                        if (state.Current_Tab is PlaylistGrid)
                        {
                             if((state.Current_Tab as PlaylistGrid).SongList.SelectedIndex>=0)
                             {
                                 if (((state.Current_Tab as PlaylistGrid).CurrentPlaylist) == state.currentPlaylist)
                                 {
                                     Song new_song = ((state.Current_Tab as PlaylistGrid).SongList.SelectedItem as Song);
                                     Playlist_OnSongPlaying(state.currentPlaylist, new_song.ID, new_song);
                                 }
                                else
                                 {

                                     Song new_song = ((state.Current_Tab as PlaylistGrid).SongList.SelectedItem as Song);
                                     Song_Playlist new_playlist = (state.Current_Tab as PlaylistGrid).CurrentPlaylist;
                                    Playlist_OnSongPlaying(new_playlist, new_song.ID, new_song);
                                }
                             }
                        }
                        else if (state.Current_Tab is NoImageList_Control)
                        {
                            Console.WriteLine("No image list");
                            if ((state.Current_Tab as NoImageList_Control).SongList.SelectedIndex >= 0)
                            {
                                Console.WriteLine(((state.Current_Tab as NoImageList_Control).SongList.SelectedItem as Song)
                                    .Title);
                            }
                        }
                        else if (state.Current_Tab is SingerPage_Control)
                        {
                            Console.WriteLine("Singer page");
                            foreach (var child in (state.Current_Tab as SingerPage_Control).albums_panel.Children)
                            {
                                if (child is AlbumGrid)
                                {
                                    
                                    if ((child as AlbumGrid).songs_lb.SelectedIndex >= 0)
                                    {
                                        Console.WriteLine(((child as AlbumGrid).songs_lb.SelectedItem as Song).Title);
                                    }
                                }
                            }
                        }
                
                    }
            
            //PlaySongByID(21);
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

        #region SongSlider

        private void BottomDuration_slider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!state.dragStarted)
            {
                state.player.Position = TimeSpan.FromSeconds(bottomDuration_slider.Value);
                state.dragStarted = true;
            }
        }

        private void BottomDuration_slider_OnDragStarted(object sender, DragStartedEventArgs e)
        {
            state.dragStarted = true;
        }

        private void BottomDuration_slider_OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            bottomDuration_slider.Value = ((Slider)sender).Value;
            state.dragStarted = false;
        }

        #endregion


        private void Repeat_btnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (state.repeat == RepeatState.NoRepeat)
            {
                state.repeat = RepeatState.RepeatSong;
                RepeatIcon.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF0051"));
            }
            else if (state.repeat == RepeatState.RepeatSong)
            {
                state.repeat = RepeatState.NoRepeat;
            }
        }
    }
}

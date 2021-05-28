using InTime.ServiceReference1;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
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
using InTime.ServiceReference1;

namespace InTime.Controls
{
    /// <summary>
    /// Логика взаимодействия для PlaylistGrid.xaml
    /// </summary>
    public partial class PlaylistGrid : UserControl
    {
        public event DragStarted OnDragStarted; 
        public event CurrentPlaylistUPD CurrentListboxUPD; 
        public event QueueUpdate QueueUpdate; 
        public event PlaySong PlaySong;
        public event PauseSong PauseSong;
        public event ScrollCall ScrollCall;
        public event OpenSingerPage OpenSingerPage;
        public event UserPlaylistChanged UserPlaylistChanged; 
        public PlaylistGrid(Client_User user)
        {
            InitializeComponent();
            this.User = user;
        }
        public void Init()
        {
            if (CurrentPlaylist.Image == null)
            {
                ImageSource = File.ReadAllBytes(Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.LastIndexOf("InTime")) + @"InTime\Assets\playlisticon.png");
            }
            else ImageSource = CurrentPlaylist.Image;
            PlaylistName = CurrentPlaylist.Title;
            SongsCount = CurrentPlaylist.Songs.Length;
            SongList.ItemsSource = CurrentPlaylist.Songs;
            playlistName_tb.Text = CurrentPlaylist.Title;
            playlistDur_tb.Text = GetDuration(CurrentPlaylist).ToString(@"hh\:mm\:ss");
            playlistCnt_tb.Text = CurrentPlaylist.Songs.Length.ToString();

            fav_btn.Visibility = CurrentUser.Playlists.Any(c => c.ID == CurrentPlaylist.ID) ? Visibility.Hidden : Visibility.Visible;
            fav_btn.Content = CurrentUser.FavoritePlaylists.Any(c => c.ID == CurrentPlaylist.ID) ? "Remove from favorites" : "Add to favorites";

            if (!User.Playlists.Contains(CurrentPlaylist)) SongMenu.Items.RemoveAt(0);

        }
        TimeSpan GetDuration(Song_Playlist playlist)
        {
            TimeSpan timeSpan = new TimeSpan();
            foreach (var item in playlist.Songs)
            {
                timeSpan += item.Duration;
            }
            return timeSpan;
        }
        public Client_User CurrentUser
        {
            get { return (Client_User)GetValue(CurrentUserProperty); }
            set { SetValue(CurrentUserProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentUserProperty =
            DependencyProperty.Register("CurrentUser", typeof(Client_User), typeof(PlaylistGrid));
        public Song_Playlist CurrentPlaylist
        {
            get { return (Song_Playlist)GetValue(PlaylistProperty); }
            set { SetValue(PlaylistProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaylistProperty =
            DependencyProperty.Register("Playlist", typeof(Song_Playlist), typeof(PlaylistGrid));
        public byte[] ImageSource
        {
            get { return (byte[])GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(byte[]), typeof(PlaylistGrid));


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

       
        private void ListBoxItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (((ListBoxItem)sender).IsSelected)
                return;
            Storyboard storyboard = FindResource("Selected_Animation") as Storyboard;
            ((ListBoxItem)sender).BeginStoryboard(storyboard);
            Border playBord = GetPlayBorder((ListBoxItem)sender);
            Border moreBord = GetMoreBorder((ListBoxItem)sender);
            if (playBord != null)
            {
                playBord.Child.Visibility = Visibility.Visible;
                moreBord.Child.Visibility = Visibility.Visible;
            }
        }
        private void ListBoxItem_MouseLeave(object sender, MouseEventArgs e)
        {
            if (((ListBoxItem)sender).IsSelected)
                return;
            if (((ListBoxItem)sender).Background.IsFrozen)
            {
                ((ListBoxItem)sender).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB8FF8F"));
                ((ListBoxItem)sender).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF746767"));
            }
            Storyboard storyboard = FindResource("Unselected_Animation") as Storyboard;
            ((ListBoxItem)sender).BeginStoryboard(storyboard);

            Border playBord = GetPlayBorder((ListBoxItem)sender);
            Border moreBord = GetMoreBorder((ListBoxItem)sender);
            if (playBord != null)
            {
                playBord.Child.Visibility = Visibility.Hidden;
                moreBord.Child.Visibility = Visibility.Hidden;
            }
        }
        private void ListBoxItem_Unselected(object sender, RoutedEventArgs e)
        {
            ListBoxItem_MouseLeave(sender, null);
        }
        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            if (OnSinger)
            {
                Song song = ((Song)((ListBoxItem)sender).Content);
                Console.WriteLine(song.Singers[0]);
                OpenSingerPage?.Invoke(song.Singers[0].ID);
                return;
            }
            Console.WriteLine(sender);
            ListBoxItem_MouseEnter(sender, null);
            ((ListBoxItem)sender).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB8FF8F"));
            ((ListBoxItem)sender).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF746767"));
            Border playBord = GetPlayBorder((ListBoxItem)sender);
            Border moreBord = GetMoreBorder((ListBoxItem)sender);
            if (playBord != null)
            {
                playBord.Child.Visibility = Visibility.Visible;
                moreBord.Child.Visibility = Visibility.Visible;
            }
            CurrentListboxUPD?.Invoke((ListBox)FindParent<ListBox>((ListBoxItem)sender));
        }
        Border GetPlayBorder(ListBoxItem item)
        {
            ListBox lb = FindParent<ListBox>(item);
            if (lb == null)
                return null;
            ListBoxItem myListBoxItem = (ListBoxItem)lb.ContainerFromElement(item);
            ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);
            DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
            return (Border)myDataTemplate.FindName("PlayBord", myContentPresenter);
        }
        Border GetMoreBorder(ListBoxItem item)
        {
            ListBox lb = FindParent<ListBox>(item);
            if (lb == null)
                return null;
            ListBoxItem myListBoxItem = (ListBoxItem)lb.ContainerFromElement(item);
            ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);
            DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
            return (Border)myDataTemplate.FindName("more_Bord", myContentPresenter);
        }
        private childItem FindVisualChild<childItem>(DependencyObject obj)
        where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
        private static T FindParent<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);
            if (parent == null) return null;
            var parentT = parent as T;
            return parentT ?? FindParent<T>(parent);
        }
        private void ListBoxItem_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta < 0)
            {
                ScrollCall?.Invoke(true);
            }
            else
            {
                ScrollCall?.Invoke(false);
            }
        }
        bool OnSinger = false;
        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            OnSinger = true;
            TextBlock textBlock = (TextBlock)sender;
            textBlock.TextDecorations = TextDecorations.Underline;
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            OnSinger = false;
            TextBlock textBlock = (TextBlock)sender;
            textBlock.TextDecorations = null;
        }

        private async void fav_btn_Click(object sender, RoutedEventArgs e)
        {
            Service1Client client = new Service1Client();
            if (fav_btn.Content.ToString() != "Remove from favorites")
            {
                await client.ClonePlaylistToFavoritePlaylistAsync(CurrentUser.ID, CurrentPlaylist.ID);
                fav_btn.Content = "Remove from favorites";
            }
            else
            {
                await client.RemoveFromPlaylistFavoriteAsync(CurrentUser.ID, CurrentPlaylist.ID);
                fav_btn.Content = "Add to favorites";
            }
            UserPlaylistChanged?.Invoke();
            client.Close();
            
        }
        public Client_User User
        {
            get { return (Client_User)GetValue(UserProperty); }
            set { SetValue(UserProperty, value); }
        }

        // Using a DependencyProperty as the backing store for User.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UserProperty =
            DependencyProperty.Register("User", typeof(Client_User), typeof(PlaylistGrid));


        private void OnContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            Playlists.Items.Clear();
            foreach (var song in PlaylistsInfo)
            {
                MenuItem tmp = new MenuItem();
                tmp.Click += AddToPlaylistClick;
                tmp.Header = song.Title;
                tmp.Tag = song;
                Playlists.Items.Add(tmp);
            }
        }

        private void AddToPlaylistClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Service1Client client = new Service1Client();
                client.AddSongToPlaylist((SongList.SelectedItem as Song).ID,
                    ((sender as MenuItem).Tag as Song_Playlist).ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }

        public ObservableCollection<Song_Playlist> PlaylistsInfo
        {
            get { return (ObservableCollection<Song_Playlist>)GetValue(PlaylistsInfoProperty); }
            set { SetValue(PlaylistsInfoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaylistsInfoProperty =
            DependencyProperty.Register("PlaylistsInfo", typeof(ObservableCollection<Song_Playlist>), typeof(PlaylistGrid));


        private void ContextMenuOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void RemoveSongClick(object sender, RoutedEventArgs e)
        {
            
        }

        private async void AddSongToFavorites(object sender, RoutedEventArgs e)
        {
            try
            {
                Service1Client client = new Service1Client();
                await client.AddTrackToFavoriteAsync(User.ID, (SongList.SelectedItem as Song).ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (SongList.SelectedItem != null)
                {
                    OnDragStarted?.Invoke((SongList.SelectedItem as Song).ID,CurrentPlaylist.ID);
                    DragDrop.DoDragDrop(SongList, (SongList.SelectedItem), DragDropEffects.Copy);
                }
            }
        }
        Border playBord;
        private void PlayBord_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (playBord != null)
            {
                if (playBord != sender as Border)
                    IconChange();
            }

            playBord = sender as Border;

            QueueUpdate?.Invoke(CreateQueue());
            IconChange();
        }
        Queue<Song> CreateQueue()
        {
            int index = SongList.SelectedIndex;
            Queue<Song> songs = new Queue<Song>();
            foreach (Song item in SongList.Items)
            {
                if (SongList.Items.IndexOf(item) >= index)
                    songs.Enqueue(item);
            }

            return songs;
        }
        void IconChange()
        {
            if (((PackIcon)playBord.Child).Kind != PackIconKind.PauseCircleOutline)
            {
                PlaySong?.Invoke(SongList.SelectedItem as Song);
                ((PackIcon)playBord.Child).Kind = PackIconKind.PauseCircleOutline;
            }
            else
            {
                PauseSong?.Invoke();
                ((PackIcon)playBord.Child).Kind = PackIconKind.PlayCircleOutline;
            }
        }
    }
}

using InTime.ServiceReference1;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace InTime.Controls
{
    /// <summary>
    /// Логика взаимодействия для NoImageList_Control.xaml
    /// </summary>
    public partial class NoImageList_Control : UserControl
    {
        public event ScrollCall ScrollCall;
        public event OpenSingerPage OpenSingerPage;
        public event UserPlaylistChanged UserPlaylistChanged;
        public event DragStarted OnDragStarted;
        public event PlaySongFromPlaylist OnSongPlaying;
        public event PauseSongFromPlaylist OnSongPaused;
        public NoImageList_Control()
        {
            InitializeComponent();
        }
        public void Init()
        {
            if(CurrentPlaylist!=null)
            SongList.ItemsSource = CurrentPlaylist.Songs;
            List_name.Text = CurrentPlaylist.Title;
            if (CurrentPlaylist.Title != "Favorite tracks")
            {
                SongMenu.Items.RemoveAt(0);
            }
        }
        public Client_User CurrentUser1
        {
            get { return (Client_User)GetValue(CurrentUser1Property); }
            set { SetValue(CurrentUser1Property, value); }
        }

        public static readonly DependencyProperty CurrentUser1Property =
            DependencyProperty.Register("CurrentUser1", typeof(Client_User), typeof(PlaylistGrid));
        public Song_Playlist CurrentPlaylist
        {
            get { return (Song_Playlist)GetValue(CurrentPlaylistProperty); }
            set { SetValue(CurrentPlaylistProperty, value); }
        }
        public static readonly DependencyProperty CurrentPlaylistProperty =
            DependencyProperty.Register("CurrentPlaylist", typeof(Song_Playlist), typeof(PlaylistGrid));



        public ObservableCollection<Song_Playlist> PlaylistsInfo
        {
            get { return (ObservableCollection<Song_Playlist>)GetValue(PlaylistsInfoProperty); }
            set { SetValue(PlaylistsInfoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaylistsInfoProperty =
            DependencyProperty.Register("PlaylistsInfo", typeof(ObservableCollection<Song_Playlist>), typeof(NoImageList_Control));



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

        private void SongListContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            Playlists.Items.Clear();
            foreach (var song in PlaylistsInfo)
            {
                MenuItem tmp = new MenuItem();
                tmp.Click += AddSongToPlaylist;
                tmp.Header = song.Title;
                tmp.Tag = song;
                Playlists.Items.Add(tmp);
            }
        }

        private void AddSongToPlaylist(object sender, RoutedEventArgs e)
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

        private void OnItemDrag(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (SongList.SelectedItem != null)
                {
                    OnDragStarted?.Invoke((SongList.SelectedItem as Song).ID, CurrentPlaylist.ID);
                    DragDrop.DoDragDrop(SongList, (SongList.SelectedItem), DragDropEffects.Copy);
                }
            }
        }

        private void RemoveSongFromFavorites(object sender, RoutedEventArgs e)
        {
            
        }
        Border playBord;
        private void PlayBord_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (playBord != null)
            {
                if (playBord != sender as Border)
                    IconChange();
            }

            playBord = sender as Border;

            IconChange();
        }

        void IconChange()
        {
            if (((PackIcon)playBord.Child).Kind != PackIconKind.PauseCircleOutline)
            {
                OnSongPlaying?.Invoke(CurrentPlaylist, (SongList.SelectedItem as Song).ID, (SongList.SelectedItem as Song));
                ((PackIcon)playBord.Child).Kind = PackIconKind.PauseCircleOutline;
            }
            else
            {
                OnSongPaused?.Invoke();
                ((PackIcon)playBord.Child).Kind = PackIconKind.PlayCircleOutline;
            }
        }
    }
}

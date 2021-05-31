using InTime.ServiceReference1;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
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

namespace InTime.Controls
{
    /// <summary>
    /// Логика взаимодействия для AlbumGrid.xaml
    /// </summary>
    public partial class AlbumGrid : UserControl
    {
        public event PlaySongFromPlaylist OnSongPlaying;
        public event PauseSong OnSongPaused;
        public event ScrollCall ScrollCall;
        public AlbumGrid(Singer_Album album)
        {
           InitializeComponent();
           InitControl(album);
        }
        void InitControl(Singer_Album album)
        {
            albumName_tb.Text = album.Title;
            songs_lb.ItemsSource = album.Songs;
            img_br.Source = ConvertToImage(album.Image);
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

        #region listBoxAnimations
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
            Border playBord = GetPlayBorder((ListBoxItem)sender);
            if (playBord != null)
                ((PackIcon)playBord.Child).Kind = PackIconKind.PlayCircleOutline;
            ListBoxItem_MouseLeave(sender, null);
        }
        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("click");
            SingerPage_Control tmp = FindParent<SingerPage_Control>(this);
            foreach (AlbumGrid child in tmp.albums_panel.Children)
            {
                if (child != this) child.songs_lb.SelectedIndex = -1;
            }
            ((ListBoxItem)sender).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB8FF8F"));
            Border playBord = GetPlayBorder((ListBoxItem)sender);
            if (playBord != null)
                ((PackIcon)playBord.Child).Kind = PackIconKind.PauseCircleOutline;
        }
        Border GetPlayBorder(ListBoxItem item)
        {
            ListBox lb = FindParent<ListBox>(item);
            ListBoxItem myListBoxItem = (ListBoxItem)lb.ContainerFromElement(item);
            ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);
            DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
            return (Border)myDataTemplate.FindName("PlayBord", myContentPresenter);
        }
        Border GetMoreBorder(ListBoxItem item)
        {
            ListBox lb = FindParent<ListBox>(item);
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
        #endregion

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

        private void Songs_lb_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void PlaylistSongClicked(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void PlayAlbumSong(object sender, MouseButtonEventArgs e)
        {
            Song[] arr = new Song[200];
            for (int i = 0; i < songs_lb.Items.Count; i++)
            {
                arr[i] = songs_lb.Items[i] as Song;
            }

            Console.WriteLine(songs_lb.SelectedIndex);
            OnSongPlaying?.Invoke(new Song_Playlist{Songs =arr },(songs_lb.SelectedItem as Song).ID,(songs_lb.SelectedItem as Song));
        }
    }
}

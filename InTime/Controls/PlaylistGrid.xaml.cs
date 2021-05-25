using InTime.ServiceReference1;
using MaterialDesignThemes.Wpf;
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
using System.Windows.Media.Animation;
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
        public event ScrollCall ScrollCall;
        public event OpenSingerPage OpenSingerPage;
        public PlaylistGrid()
        {
            InitializeComponent();
        }
        public void Init()
        {
            ImageSource = CurrentPlaylist.Image;
            PlaylistName = CurrentPlaylist.Title;
            SongsCount = CurrentPlaylist.Songs.Length;
            SongList.ItemsSource = CurrentPlaylist.Songs;
        }
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
                
            Border playBord = GetPlayBorder((ListBoxItem)sender);
            if (playBord != null)
                ((PackIcon)playBord.Child).Kind = PackIconKind.PlayCircleOutline;
            ListBoxItem_MouseLeave(sender, null);
        }
        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            
            ((ListBoxItem)sender).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB8FF8F"));
            Border playBord = GetPlayBorder((ListBoxItem)sender);
            if (playBord != null)
                ((PackIcon)playBord.Child).Kind = PackIconKind.PauseCircleOutline;
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

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            textBlock.TextDecorations = TextDecorations.Underline;
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            textBlock.TextDecorations = null;
        }

        bool fl = false;
        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            fl = true;
            OpenSingerPage?.Invoke(Int32.Parse(((TextBlock)sender).Tag.ToString()));
        }
    }
}

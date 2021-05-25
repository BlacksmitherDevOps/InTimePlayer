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
    /// Логика взаимодействия для Playlist_Control.xaml
    /// </summary>
    public partial class SingerPage_Control : UserControl
    {
        public event ScrollCall ScrollCall;
        public SingerPage_Control(Song_Singer singer)
        {
            InitializeComponent();
            InitControl(singer);
        }
        void InitControl(Song_Singer singer)
        {
            singerName_tb.Text = singer.Name;
            img_brush.ImageSource = ConvertToImage(singer.Image);
            foreach (var item in singer.Albums)
            {
                if (item.Songs.Length < 1)
                    continue;
                AddAlbum(item);
            }
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
        void AddAlbum(Singer_Album album)
        {
            AlbumGrid albumGrid = new AlbumGrid(album);
            albumGrid.ScrollCall += AlbumGrid_ScrollCall;
            albumGrid.Margin = new Thickness(0, 0, 0, 50);
            albums_panel.Children.Add(albumGrid);
        }

        private void AlbumGrid_ScrollCall(bool flag)
        {
            ScrollCall?.Invoke(flag);
        }
        #region listBoxAnimations
        private void ListBoxItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (((ListBoxItem)sender).IsSelected)
                return;
            Storyboard storyboard = FindResource("Selected_Animation") as Storyboard;
            ((ListBoxItem)sender).BeginStoryboard(storyboard);
            Border playBord = GetPlayBorder((ListBoxItem)sender);
            if (playBord != null)
                playBord.Child.Visibility = Visibility.Visible;
        }
        private void ListBoxItem_MouseLeave(object sender, MouseEventArgs e)
        {
            if (((ListBoxItem)sender).IsSelected)
                return;
            if (((ListBoxItem)sender).Background.IsFrozen)
                ((ListBoxItem)sender).Background = Brushes.Red;
            Storyboard storyboard = FindResource("Unselected_Animation") as Storyboard;
            ((ListBoxItem)sender).BeginStoryboard(storyboard);

            Border playBord = GetPlayBorder((ListBoxItem)sender);
            if (playBord != null)
                playBord.Child.Visibility = Visibility.Hidden;
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
            ((ListBoxItem)sender).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB8FF8F"));
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
    }
}

using InTime.ServiceReference1;
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
    /// Логика взаимодействия для Playlist_Scroller.xaml
    /// </summary>
    public partial class Playlist_Scroller : UserControl
    {
        public event ScrollCall ScrollCall;
        public Playlist_Scroller(List<Song_Playlist> playlists)
        {
            InitializeComponent();
            foreach (var item in playlists)
            {
                Playlist_Item_Control item_Control = new Playlist_Item_Control(item);
                item_Control.ScrollCall += Item_Control_ScrollCall;
                panel.Children.Add(item_Control);
            }
        }

        private void Item_Control_ScrollCall(bool flag)
        {
            ScrollCall?.Invoke(flag);
        }

        void Create_Playlist_Item()
        {
            
        }
        Point coord = new Point();
        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (click && e.GetPosition(bord).X != coord.X)
            {
                if (coord.X > e.GetPosition(bord).X)
                {
                    viewer.ScrollToHorizontalOffset(viewer.HorizontalOffset + (coord.X - e.GetPosition(bord).X));
                }
                else
                {
                    viewer.ScrollToHorizontalOffset(viewer.HorizontalOffset - (e.GetPosition(bord).X - coord.X));
                }
                coord = e.GetPosition(bord);
            }
        }
        bool click;
        private void panel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            click = true;
            coord = e.GetPosition(bord);
        }

        private void panel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            click = false;
        }

        private void bord_MouseLeave(object sender, MouseEventArgs e)
        {
            click = false;
        }
        private void viewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (viewer.HorizontalOffset == viewer.ScrollableWidth)
                right_Grid.Opacity = 0.5;
            else if (right_Grid.Opacity == 0.5)
                right_Grid.Opacity = 1;

            if (viewer.HorizontalOffset == 0)
                left_Grid.Opacity = 0.5;
            else if (left_Grid.Opacity == 0.5)
                left_Grid.Opacity = 1;
        }
        private void ArrowGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            if (((Grid)sender).Opacity == 0.5)
                return;
            Ellipse ellipse = (Ellipse)((Grid)sender).Children[1];
            ellipse.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6A6969"));
        }
        private void ArrowGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            if (((Grid)sender).Opacity == 0.5)
                return;
            Ellipse ellipse = (Ellipse)((Grid)sender).Children[1];
            ellipse.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF515151"));
        }

        private void left_Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation(viewer.HorizontalOffset - 240, TimeSpan.FromSeconds(0.3));
            Storyboard storyboard = new Storyboard();

            storyboard.Children.Add(animation);
            Storyboard.SetTarget(animation, viewer);
            Storyboard.SetTargetProperty(animation, new PropertyPath(ScrollViewerBehavior.HorizontalOffsetProperty));
            storyboard.Begin();
        }
        
        private void right_Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation(viewer.HorizontalOffset + 240, TimeSpan.FromSeconds(0.3));
            Storyboard storyboard = new Storyboard();

            storyboard.Children.Add(animation);
            Storyboard.SetTarget(animation, viewer);
            Storyboard.SetTargetProperty(animation, new PropertyPath(ScrollViewerBehavior.HorizontalOffsetProperty));
            storyboard.Begin();
        }
        public static class ScrollViewerBehavior
        {
            public static readonly DependencyProperty HorizontalOffsetProperty = DependencyProperty.RegisterAttached("HorizontalOffset", typeof(double), typeof(ScrollViewerBehavior), new UIPropertyMetadata(0.0, OnHorizontalOffsetChanged));
            public static void SetHorizontalOffset(FrameworkElement target, double value) => target.SetValue(HorizontalOffsetProperty, value);
            public static double GetHorizontalOffset(FrameworkElement target) => (double)target.GetValue(HorizontalOffsetProperty);
            private static void OnHorizontalOffsetChanged(DependencyObject target, DependencyPropertyChangedEventArgs e) => (target as ScrollViewer)?.ScrollToHorizontalOffset((double)e.NewValue);

            public static readonly DependencyProperty VerticalOffsetProperty = DependencyProperty.RegisterAttached("VerticalOffset", typeof(double), typeof(ScrollViewerBehavior), new UIPropertyMetadata(0.0, OnVerticalOffsetChanged));
            public static void SetVerticalOffset(FrameworkElement target, double value) => target.SetValue(VerticalOffsetProperty, value);
            public static double GetVerticalOffset(FrameworkElement target) => (double)target.GetValue(VerticalOffsetProperty);
            private static void OnVerticalOffsetChanged(DependencyObject target, DependencyPropertyChangedEventArgs e) => (target as ScrollViewer)?.ScrollToVerticalOffset((double)e.NewValue);
        }

        private void main_grid_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
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
    }
}

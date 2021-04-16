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
    /// Логика взаимодействия для Playlist_Control.xaml
    /// </summary>
    public partial class Playlist_Control : UserControl
    {
        public Playlist_Control()
        {
            InitializeComponent();
            AddSong();
            AddSong();
            AddSong();
            AddSong();
        }
        void AddSong()
        {
            songs_lb.Items.Add(new tempSong() { Singer = "Test Singer", Title = "Test Title", Album = "Test Album", Duration = "3:44" });
        }

        private void ListBoxItem_MouseLeave(object sender, MouseEventArgs e)
        {
            if (((ListBoxItem)sender).IsSelected)
                return;
            ColorAnimation color = new ColorAnimation((Color)ColorConverter.ConvertFromString("#FFFFCECE"), TimeSpan.FromSeconds(0.2));
            ((ListBoxItem)sender).Background.BeginAnimation(SolidColorBrush.ColorProperty, color);
        }

        private void ListBoxItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (((ListBoxItem)sender).IsSelected)
                return;
            ((ListBoxItem)sender).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFCECE"));
            ColorAnimation color = new ColorAnimation((Color)ColorConverter.ConvertFromString("#FFE83B3B"), TimeSpan.FromSeconds(0.2));
            ((ListBoxItem)sender).Background.BeginAnimation(SolidColorBrush.ColorProperty, color);
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            ((ListBoxItem)sender).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB8FF8F"));
        }

        private void ListBoxItem_Unselected(object sender, RoutedEventArgs e)
        {
            ListBoxItem_MouseLeave(sender, null);
        }
    }
}

using InTime.ServiceReference1;
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
    /// Логика взаимодействия для Playlist_Item_Control.xaml
    /// </summary>
    public partial class Playlist_Item_Control : UserControl
    {
        public Playlist_Item_Control(Song_Playlist playlist)
        {
            InitializeComponent();
            main_image.Source = ConvertToImage(playlist.Image);
            playlist_name_tb.Text = playlist.Title;
            author_name_tb.Text = playlist.Singers.First().Name;
        }
        BitmapSource ConvertToImage(byte[] arr)
        {
            using (MemoryStream ms = new MemoryStream(arr))
            {
                var decoder = BitmapDecoder.Create(ms,
                    BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                return decoder.Frames[0];
            }
        }

        private void main_grid_MouseEnter(object sender, MouseEventArgs e)
        {
            ColorAnimation animation = new ColorAnimation((Color)ColorConverter.ConvertFromString("#FF635D5D"), TimeSpan.FromSeconds(0.2));
            main_grid.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        }

        private void main_grid_MouseLeave(object sender, MouseEventArgs e)
        {
            ColorAnimation animation = new ColorAnimation((Color)ColorConverter.ConvertFromString("#FF403B3B"), TimeSpan.FromSeconds(0.2));
            main_grid.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        }
    }
}

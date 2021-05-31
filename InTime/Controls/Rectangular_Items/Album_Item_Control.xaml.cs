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

namespace InTime.Controls.Rectangular_Items
{
    /// <summary>
    /// Логика взаимодействия для Album_Item_Control.xaml
    /// </summary>
    public partial class Album_Item_Control : UserControl
    {
        public event ScrollCall ScrollCall;
        public event OpenAlbum OpenAlbum;
        int id;
        public Album_Item_Control(Singer_Album album)
        {
            InitializeComponent();
            id = album.ID;
            if (album.Image != null)
                main_image.Source = ConvertToImage(album.Image);
            playlist_name_tb.Text = album.Title;
            author_name_tb.Text = album.Singer.Name;
        }
        BitmapSource ConvertToImage(byte[] arr)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(arr))
                {
                    var decoder = BitmapDecoder.Create(ms,
                        BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                    return decoder.Frames[0];
                }
            }
            catch
            {
                return null;
            }
        }

        private void main_grid_MouseEnter(object sender, MouseEventArgs e)
        {
            ColorAnimation animation = new ColorAnimation((Color)ColorConverter.ConvertFromString("#FF635D5D"), TimeSpan.FromSeconds(0.2));
            main_grid.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
            main_image.Opacity = 0.7;
        }

        private void main_grid_MouseLeave(object sender, MouseEventArgs e)
        {
            ColorAnimation animation = new ColorAnimation((Color)ColorConverter.ConvertFromString("#FF403B3B"), TimeSpan.FromSeconds(0.2));
            main_grid.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
            main_image.Opacity = 1;
        }

        private void PlayIcoMouseEnter(object sender, MouseEventArgs e)
        {
            main_image.Opacity = 0.7;
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

        private void main_grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (click)
                OpenAlbum?.Invoke(id);
        }
        bool click;
        private void main_grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            click = true;
        }

        private void main_grid_MouseMove(object sender, MouseEventArgs e)
        {
            click = false;
        }
    }
}

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
        public Playlist_Scroller(List<Song_Playlist> playlists)
        {
            InitializeComponent();
            foreach (var item in playlists)
            {
                panel.Children.Add(new Playlist_Item_Control(item));
            }
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
    }
}

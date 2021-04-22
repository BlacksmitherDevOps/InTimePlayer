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
    /// Interaction logic for SearchPanel.xaml
    /// </summary>
    public partial class SearchPanel : UserControl
    {
        public SearchPanel()
        {
            InitializeComponent();
            AddSongs();
            AddAlbums();
            AddGenres();
            AddArtists();
        }

        public void AddSongs()
        {
            SearchItem Item1 = new SearchItem();
            Item1.Image = @"C:\Player\InTimePlayer\InTime\Controls\3E-zsSjyGLU.jpg";
            Item1.Title = "From code";
            Item1.Artist = "Im writen!";
            Item1.Height = 80;
            Item1.VerticalAlignment = VerticalAlignment.Center;
            Item1.Margin = new Thickness(0, 13, 7, 13);
            Grid.SetColumn(Item1,0);
            Grid.SetRow(Item1,0);
            SongsGrid.Children.Add(Item1);

            SearchItem Item2 = new SearchItem();
            Item2.Image = @"C:\Player\InTimePlayer\InTime\Controls\64eb0176b8ec007e2c0ffa65a92c8dc0.gif";
            Item2.Title = "From code";
            Item2.Artist = "Im writen!";
            Item2.Height = 80;
            Item2.VerticalAlignment = VerticalAlignment.Center;
            Item2.Margin = new Thickness(7, 13, 8, 13);
            Grid.SetColumn(Item2, 1);
            Grid.SetRow(Item2, 0);
            SongsGrid.Children.Add(Item2);

            SearchItem Item3 = new SearchItem();
            Item3.Image = @"C:\Player\InTimePlayer\InTime\Controls\64eb0176b8ec007e2c0ffa65a92c8dc0.gif";
            Item3.Title = "From code";
            Item3.Artist = "Im writen!";
            Item3.Height = 80;
            Item3.VerticalAlignment = VerticalAlignment.Center;
            Item3.Margin = new Thickness(0, 13, 7, 13);
            Grid.SetColumn(Item3, 0);
            Grid.SetRow(Item3, 1);
            SongsGrid.Children.Add(Item3);

            SearchItem Item4 = new SearchItem();
            Item4.Image = @"C:\Player\InTimePlayer\InTime\Controls\3E-zsSjyGLU.jpg";
            Item4.Title = "From code";
            Item4.Artist = "Im writen!";
            Item4.Height = 80;
            Item4.VerticalAlignment = VerticalAlignment.Center;
            Item4.Margin = new Thickness(7, 13, 8, 13);
            Grid.SetColumn(Item4, 1);
            Grid.SetRow(Item4, 1);
            SongsGrid.Children.Add(Item4);
        }
        public void AddAlbums()
        {
            SearchItem Item1 = new SearchItem();
            Item1.Image = @"C:\Player\InTimePlayer\InTime\Controls\3E-zsSjyGLU.jpg";
            Item1.Title = "From code";
            Item1.Artist = "Im writen!";
            Item1.Height = 80;
            Item1.VerticalAlignment = VerticalAlignment.Center;
            Item1.Margin = new Thickness(0, 13, 7, 13);
            Grid.SetColumn(Item1, 0);
            Grid.SetRow(Item1, 0);
            AlbumsGrid.Children.Add(Item1);

            SearchItem Item2 = new SearchItem();
            Item2.Image = @"C:\Player\InTimePlayer\InTime\Controls\64eb0176b8ec007e2c0ffa65a92c8dc0.gif";
            Item2.Title = "From code";
            Item2.Artist = "Im writen!";
            Item2.Height = 80;
            Item2.VerticalAlignment = VerticalAlignment.Center;
            Item2.Margin = new Thickness(7, 13, 8, 13);
            Grid.SetColumn(Item2, 1);
            Grid.SetRow(Item2, 0);
            AlbumsGrid.Children.Add(Item2);

            SearchItem Item3 = new SearchItem();
            Item3.Image = @"C:\Player\InTimePlayer\InTime\Controls\64eb0176b8ec007e2c0ffa65a92c8dc0.gif";
            Item3.Title = "From code";
            Item3.Artist = "Im writen!";
            Item3.Height = 80;
            Item3.VerticalAlignment = VerticalAlignment.Center;
            Item3.Margin = new Thickness(0, 13, 7, 13);
            Grid.SetColumn(Item3, 0);
            Grid.SetRow(Item3, 1);
            AlbumsGrid.Children.Add(Item3);

            SearchItem Item4 = new SearchItem();
            Item4.Image = @"C:\Player\InTimePlayer\InTime\Controls\3E-zsSjyGLU.jpg";
            Item4.Title = "From code";
            Item4.Artist = "Im writen!";
            Item4.Height = 80;
            Item4.VerticalAlignment = VerticalAlignment.Center;
            Item4.Margin = new Thickness(7, 13, 8, 13);
            Grid.SetColumn(Item4, 1);
            Grid.SetRow(Item4, 1);
            AlbumsGrid.Children.Add(Item4);
        }
        public void AddGenres()
        {
            SingleSearchItem Item1 = new SingleSearchItem();
            Item1.Image = @"C:\Player\InTimePlayer\InTime\Controls\3E-zsSjyGLU.jpg";
            Item1.Title = "Transcendence";
            Item1.Height = 80;
            Item1.VerticalAlignment = VerticalAlignment.Center;
            Item1.Margin = new Thickness(9, 13, 7, 13);
            Grid.SetColumn(Item1, 0);
            Grid.SetRow(Item1, 0);
            GenresGrid.Children.Add(Item1);

            SingleSearchItem Item2 = new SingleSearchItem();
            Item2.Image = @"C:\Player\InTimePlayer\InTime\Controls\64eb0176b8ec007e2c0ffa65a92c8dc0.gif";
            Item2.Title = "Rock";
            Item2.Height = 80;
            Item2.VerticalAlignment = VerticalAlignment.Center;
            Item2.Margin = new Thickness(7, 13, 8, 13);
            Grid.SetColumn(Item2, 1);
            Grid.SetRow(Item2, 0);
            GenresGrid.Children.Add(Item2);

            SingleSearchItem Item3 = new SingleSearchItem();
            Item3.Image = @"C:\Player\InTimePlayer\InTime\Controls\64eb0176b8ec007e2c0ffa65a92c8dc0.gif";
            Item3.Title = "Pop";
            Item3.Height = 80;
            Item3.VerticalAlignment = VerticalAlignment.Center;
            Item3.Margin = new Thickness(9, 13, 7, 13);
            Grid.SetColumn(Item3, 0);
            Grid.SetRow(Item3, 1);
            GenresGrid.Children.Add(Item3);

            SingleSearchItem Item4 = new SingleSearchItem();
            Item4.Image = @"C:\Player\InTimePlayer\InTime\Controls\3E-zsSjyGLU.jpg";
            Item4.Title = "Rap/Hip-Hop";
            Item4.Height = 80;
            Item4.VerticalAlignment = VerticalAlignment.Center;
            Item4.Margin = new Thickness(7, 13, 8, 13);
            Grid.SetColumn(Item4, 1);
            Grid.SetRow(Item4, 1);
            GenresGrid.Children.Add(Item4);
        }
        public void AddArtists()
        {
            SingleSearchItem Item1 = new SingleSearchItem();
            Item1.Image = @"C:\Player\InTimePlayer\InTime\Controls\3E-zsSjyGLU.jpg";
            Item1.Title = "Rammstein";
            Item1.Height = 80;
            Item1.VerticalAlignment = VerticalAlignment.Center;
            Item1.Margin = new Thickness(9, 13, 7, 13);
            Grid.SetColumn(Item1, 0);
            Grid.SetRow(Item1, 0);
            ArtistGrid.Children.Add(Item1);

            SingleSearchItem Item2 = new SingleSearchItem();
            Item2.Image = @"C:\Player\InTimePlayer\InTime\Controls\64eb0176b8ec007e2c0ffa65a92c8dc0.gif";
            Item2.Title = "30 Seconds To Mars";
            Item2.Height = 80;
            Item2.VerticalAlignment = VerticalAlignment.Center;
            Item2.Margin = new Thickness(7, 13, 8, 13);
            Grid.SetColumn(Item2, 1);
            Grid.SetRow(Item2, 0);
            ArtistGrid.Children.Add(Item2);

            SingleSearchItem Item3 = new SingleSearchItem();
            Item3.Image = @"C:\Player\InTimePlayer\InTime\Controls\64eb0176b8ec007e2c0ffa65a92c8dc0.gif";
            Item3.Title = "Within Temptation";
            Item3.Height = 80;
            Item3.VerticalAlignment = VerticalAlignment.Center;
            Item3.Margin = new Thickness(9, 13, 7, 13);
            Grid.SetColumn(Item3, 0);
            Grid.SetRow(Item3, 1);
            ArtistGrid.Children.Add(Item3);

            SingleSearchItem Item4 = new SingleSearchItem();
            Item4.Image = @"C:\Player\InTimePlayer\InTime\Controls\3E-zsSjyGLU.jpg";
            Item4.Title = "Taylor Swift";
            Item4.Height = 80;
            Item4.VerticalAlignment = VerticalAlignment.Center;
            Item4.Margin = new Thickness(7, 13, 8, 13);
            Grid.SetColumn(Item4, 1);
            Grid.SetRow(Item4, 1);
            ArtistGrid.Children.Add(Item4);
        }
    }
}

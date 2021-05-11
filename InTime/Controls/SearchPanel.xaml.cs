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
    /// Interaction logic for SearchPanel.xaml
    /// </summary>
    public partial class SearchPanel : UserControl
    {
        public SearchPanel()
        {
            InitializeComponent();
        }

        public void AddSongs(Song[] songs)
        {
            if (songs == null)
                return;
            if (songs.Length < 1)
                return;
            for (int i = 0; i < songs.Length && i < 4; i++)
            {
                AddSong(songs[i]);
            }
        }
       
        void GetFreeSlot(Grid grid, UIElement searchItem)
        {
            foreach (var row in grid.RowDefinitions)
            {
                foreach (var col in grid.ColumnDefinitions)
                {
                    foreach (SearchItem child in grid.Children)
                    {
                        if (Grid.GetColumn(child) != grid.ColumnDefinitions.IndexOf(col) && Grid.GetRow(child) != grid.RowDefinitions.IndexOf(row))
                        {
                            Grid.SetColumn(searchItem, grid.ColumnDefinitions.IndexOf(col));
                            Grid.SetRow(searchItem, grid.RowDefinitions.IndexOf(row));
                            return;
                        }
                    }
                }
            }

        }
        public void AddAlbums(Singer_Album[] albums)
        {
            if (albums == null)
                return;
            if (albums.Length < 1)
                return;
            for (int i = 0; i < albums.Length && i < 4; i++)
            {
                AddAlbums(albums[i]);
            }
        }
        public void AddGenres(Song[] songs)
        {
            if (songs == null)
                return;
            if (songs.Length < 1)
                return;
            for (int i = 0; i < songs.Length && i < 4; i++)
            {
                AddGenres(songs[i]);
            }
        }
        public void AddArtists(Song_Singer[] singers)
        {
            if (singers == null)
                return;
            if (singers.Length < 1)
                return;
            for (int i = 0; i < singers.Length && i < 4; i++)
            {
                AddArtist(singers[i]);
            }
        }
        void AddSong(Song song)
        {
            SearchItem Item = new SearchItem();
            //Item4.Image = @"C:\Player\InTimePlayer\InTime\Controls\3E-zsSjyGLU.jpg";
            Item.Title = song.Title;
            Item.Artist = song.Singers.First().Name;
            Item.Height = 80;
            Item.VerticalAlignment = VerticalAlignment.Center;
            GetFreeSlot(SongsGrid, Item);
            SongsGrid.Children.Add(Item);
        }
        void AddArtist(Song_Singer singer)
        {
            SingleSearchItem Item = new SingleSearchItem();
            Item.Image = singer.Image;
            Item.Title = singer.Name;
            Item.Height = 80;
            Item.VerticalAlignment = VerticalAlignment.Center;
            GetFreeSlot(ArtistGrid, Item);
            ArtistGrid.Children.Add(Item);
        }
        void AddGenres(Song song)
        {
            SingleSearchItem Item = new SingleSearchItem();
            //Item1.Image = @"C:\Player\InTimePlayer\InTime\Controls\3E-zsSjyGLU.jpg";
            Item.Title = song.Genre;
            Item.Height = 80;
            Item.VerticalAlignment = VerticalAlignment.Center;
            GetFreeSlot(GenresGrid, Item);
            GenresGrid.Children.Add(Item);
        }
        void AddAlbums(Singer_Album album)
        {
            SearchItem Item = new SearchItem();
            Item.Image = album.Image;
            Item.Title = album.Title;
            Item.Artist = album.Singer.Name;
            Item.Height = 80;
            Item.VerticalAlignment = VerticalAlignment.Center;
            GetFreeSlot(AlbumsGrid, Item);
            AlbumsGrid.Children.Add(Item);
        }
    }
}

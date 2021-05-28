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
        bool CheckRow(Grid grid, UIElement searchItem, int row)
        {
            if(grid.Children.Count < 1)
            {
                Grid.SetColumn(searchItem, 0);
                Grid.SetRow(searchItem, 0);
                return true;
            }
            for (int i = 0; i < 2; i++)
            {
                bool flag = false;
                foreach (UserControl child in grid.Children)
                {
                    if (Grid.GetColumn(child) == i && Grid.GetRow(child) == row)
                    {
                        flag = true;
                        break;
                    }
                }
                if(flag == false)
                {
                    Grid.SetColumn(searchItem, i);
                    Grid.SetRow(searchItem, row);
                    return true;
                }
            }
                
            return false;
        }
        void GetFreeSlot(Grid grid, UIElement searchItem)
        {
            int row = 0;
            while (true)
            {
                if (row > 1)
                    break;
                if (CheckRow(grid, searchItem, row))
                    break;
                row++;
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
                AddSong(songs[i]);
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
        async void AddSong(Song song)
        {
            Service1Client client = new Service1Client();
            song.Image = await client.GetAlbumImageAsync(song.Album.ID);
            SearchItem Item = new SearchItem();
            Item.Image = song.Image;
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

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}

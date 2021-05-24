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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InTime.Controls
{
    /// <summary>
    /// Логика взаимодействия для Recommendations_Control.xaml
    /// </summary>
    public partial class Recommendations_Control : UserControl
    {
        public event ScrollCall ScrollCall;
        public Recommendations_Control()
        {
            InitializeComponent();
            List<Song_Playlist> playlists = new List<Song_Playlist>();
            List<Song_Singer> song_Singers = new List<Song_Singer>();
            song_Singers.Add(new Song_Singer() { Name = "{AuthorName}" });
            playlists.Add(new Song_Playlist() { Image = File.ReadAllBytes(@"D:\WPF\InTimePlayer\InTime\Controls\3E-zsSjyGLU.jpg"), Title = "{PlaylistName}", Singers = song_Singers.ToArray() });
            playlists.Add(new Song_Playlist() { Image = File.ReadAllBytes(@"D:\WPF\InTimePlayer\InTime\Controls\3E-zsSjyGLU.jpg"), Title = "{PlaylistName}", Singers = song_Singers.ToArray() });
            playlists.Add(new Song_Playlist() { Image = File.ReadAllBytes(@"D:\WPF\InTimePlayer\InTime\Controls\3E-zsSjyGLU.jpg"), Title = "{PlaylistName}", Singers = song_Singers.ToArray() });
            playlists.Add(new Song_Playlist() { Image = File.ReadAllBytes(@"D:\WPF\InTimePlayer\InTime\Controls\3E-zsSjyGLU.jpg"), Title = "{PlaylistName}", Singers = song_Singers.ToArray() });
            playlists.Add(new Song_Playlist() { Image = File.ReadAllBytes(@"D:\WPF\InTimePlayer\InTime\Controls\3E-zsSjyGLU.jpg"), Title = "{PlaylistName}", Singers = song_Singers.ToArray() });
            playlists.Add(new Song_Playlist() { Image = File.ReadAllBytes(@"D:\WPF\InTimePlayer\InTime\Controls\3E-zsSjyGLU.jpg"), Title = "{PlaylistName}", Singers = song_Singers.ToArray() });
            playlists.Add(new Song_Playlist() { Image = File.ReadAllBytes(@"D:\WPF\InTimePlayer\InTime\Controls\3E-zsSjyGLU.jpg"), Title = "{PlaylistName}", Singers = song_Singers.ToArray() });
            AddList("Recently played", playlists);
            AddList("Special for you", playlists);
            AddList("New 2021", playlists);
        }
        public void AddList(string title, List<Song_Playlist> playlists)
        {
            TextBlock textBlock = new TextBlock() { Text= title, Style = FindResource("title_tb") as Style };
            Playlist_Scroller playlist_Scroller = new Playlist_Scroller(playlists);
            playlist_Scroller.ScrollCall += Playlist_Scroller_ScrollCall;
            tape_panel.Children.Add(textBlock);
            tape_panel.Children.Add(playlist_Scroller);
        }

        private void Playlist_Scroller_ScrollCall(bool flag)
        {
            ScrollCall?.Invoke(flag);
        }
    }
}

using InTime.ServiceReference1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
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
        public event OpenPlaylist OpenPlaylist;
        public Recommendations_Control()
        {
            InitializeComponent();
        }
        public void AddList(string title, List<Song_Playlist> playlists)
        {
            TextBlock textBlock = new TextBlock() { Text= title, Style = FindResource("title_tb") as Style };
            Playlist_Scroller playlist_Scroller = new Playlist_Scroller(playlists);
            playlist_Scroller.ScrollCall += Playlist_Scroller_ScrollCall;
            playlist_Scroller.OpenPlaylist += Playlist_Scroller_OpenPlaylist;
            tape_panel.Children.Add(textBlock);
            tape_panel.Children.Add(playlist_Scroller);
        }

        public async void Init()
        {
            try
            {
                Service1Client client = new Service1Client();
                AddList("Rock Today!", (await client.GetRockTodayAsync()).ToList());
                AddList("Chill with Pop Music~~", (await client.GetPopTodayAsync()).ToList());
                AddList("Dance!", (await client.GetHouseTodayAsync()).ToList());
                client.Close();
            }
            catch (FaultException<LoadPlaylistFailed> exception)
            {
                Console.WriteLine(exception.Detail.Message);
            }
        }
        private void Playlist_Scroller_OpenPlaylist(int id)
        {
            OpenPlaylist?.Invoke(id);
        }

        public string UserName
        {
            get { return (string)GetValue(UserNameProperty); }
            set { SetValue(UserNameProperty, value); }
        }
        public static readonly DependencyProperty UserNameProperty =
            DependencyProperty.Register("UserName", typeof(string), typeof(Recommendations_Control));
        async void RequestRock()
        {
            try
            {
                Service1Client client = new Service1Client();
                AddList("Rock Today!", (await client.GetRockTodayAsync()).ToList());
                AddList("Chill with Pop Music~~", (await client.GetPopTodayAsync()).ToList());
                AddList("Dance!", (await client.GetHouseTodayAsync()).ToList());
                client.Close();
            }
            catch (FaultException<LoadPlaylistFailed> exception)
            {
                Console.WriteLine(exception.Detail.Message);
            }
            
        }
        async void RequestPop()
        {
            try
            {
            Service1Client client = new Service1Client();
            AddList("Chill with Pop Music~~", (await client.GetPopTodayAsync()).ToList());
            client.Close();
            }
            catch (FaultException<LoadPlaylistFailed> exception)
            {
                Console.WriteLine(exception.Detail.Message);
            }
        }
        async void RequestHouse()
        {
            try
            {
                Service1Client client = new Service1Client();
                AddList("Dance!", (await client.GetHouseTodayAsync()).ToList());
                client.Close();
            }
            catch (FaultException<LoadPlaylistFailed> exception)
            {
                Console.WriteLine(exception.Detail.Message);
            }
}
        private void Playlist_Scroller_ScrollCall(bool flag)
        {
            ScrollCall?.Invoke(flag);
        }
    }
}

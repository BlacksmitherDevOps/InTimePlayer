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
using InTime.Controls;
using InTime.ServiceReference1;


namespace InTime
{
    public delegate void ScrollCall(bool flag);

    public delegate void DragStarted(int id,int _idPlaylist);
    public delegate void PlaySong(Song song);
    public delegate void CurrentPlaylistUPD(ListBox listBox);
    public delegate void QueueUpdate(Queue<Song> list);
    public delegate void PauseSong();

    public delegate void ViewAllSongs(string search_str);
    public delegate void ViewAllSingers(string search_str);
    public delegate void ViewAllAlbums(string search_str);
    public delegate void ViewAllPlaylists(string search_str);

    public delegate void OpenPlaylist(int id);
    public delegate void OpenAlbum(int id);
    public delegate void OpenSingerPage(int id);
    public delegate void UserPlaylistChanged();

    public delegate void PlaySongFromPlaylist(Song_Playlist playlist, int songId,Song song);
    public delegate void PauseSongFromPlaylist();

    public delegate void LogOut();

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetLoginScreen();


        }
        void SetLoginScreen()
        {
            this.Visibility = Visibility.Hidden;
            LoginScreen tmp = new LoginScreen();
            tmp.OnWindowClosed += Tmp_OnWindowClosed;
            tmp.ShowDialog();
        }
        private void Tmp_OnWindowClosed(Client_User user)
        {
            this.Visibility = Visibility.Visible;
            MainPlayerPage_Control mainPlayerPage_Control = new MainPlayerPage_Control(this, user);
            mainPlayerPage_Control.LogOutCall += MainPlayerPage_Control_LogOutCall;
            Control_Bord.Child = mainPlayerPage_Control;
            Console.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
        }

        private void MainPlayerPage_Control_LogOutCall()
        {
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\InTime\\auto.log"))
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\InTime\\auto.log");
            SetLoginScreen();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                Control_Bord.Padding = new Thickness(10);
            if (this.WindowState == WindowState.Normal)
                Control_Bord.Padding = new Thickness(0);
        }
    }
}

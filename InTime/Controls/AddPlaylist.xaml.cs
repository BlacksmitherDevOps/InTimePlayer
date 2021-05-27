using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using InTime.ServiceReference1;

namespace InTime.Controls
{
    public delegate void AddWindowClosed();

    public delegate void ChangesAccepted();
    public partial class AddPlaylist : UserControl
    {
        public event AddWindowClosed OnWindowClosed;
        public event ChangesAccepted OnChangesAccepted;
        public AddPlaylist()
        {
            Playlist = new Song_Playlist();
            _IsImageEdited = false;
            InitializeComponent();
        }
        private void SaveBtnMouseEnter(object sender, MouseEventArgs e)
        {
            SaveBtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF0051"));
            this.Cursor = Cursors.Hand;
        }

        private void SaveBtnMouseLeave(object sender, MouseEventArgs e)
        {
            SaveBtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8d0d26"));
            this.Cursor = Cursors.Arrow;
        }

        private async void SaveBtnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (PlaylistNameBox.Text == String.Empty)
            {
                AddPlaylistErrorBox.Visibility = Visibility.Visible;
                AddPlaylistErrorMessage.Text = "Playlist field should be signed. Please sign it and try again";
            }
            else if(!Regex.IsMatch(PlaylistNameBox.Text, @"^[a-zA-Z0-9 ]*$"))
            {
                AddPlaylistErrorBox.Visibility = Visibility.Visible;
                AddPlaylistErrorMessage.Text = "Playlist name contains unaccaptable characters,it may contains only letters,numbers and whitespace";
            }
            else
            {
                try
                {
                    Service1Client client = new Service1Client();
                    Playlist.Title = PlaylistNameBox.Text;
                    Song_Playlist tmp = await client.AddPlaylistAsync(Playlist);
                    OnChangesAccepted?.Invoke();
                    AddPlaylistErrorBox.Visibility = Visibility.Hidden;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    AddPlaylistErrorBox.Visibility = Visibility.Visible;
                    AddPlaylistErrorMessage.Text = "Network issues. Please check your internet connection and try again.";
                }
            }
        }

        private void PlaylistImgMouseEnter(object sender, MouseEventArgs e)
        {
            PlaylistImg.Opacity = 0.7;
            PencilIcon.Visibility = Visibility.Visible;
        }

        private void PlaylistImgMouseLeave(object sender, MouseEventArgs e)
        {
            PlaylistImg.Opacity = 1;
            PencilIcon.Visibility = Visibility.Hidden;
        }

        private void PencilIconMouseEnter(object sender, MouseEventArgs e)
        {
            PlaylistImg.Opacity = 0.7;
            PencilIcon.Visibility = Visibility.Visible;
        }

        private void PlaylistImgMouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                ImageBrush ib = new ImageBrush();
                ib.ImageSource = new BitmapImage(new Uri(dialog.FileName));
                PlaylistImg.Fill = ib;
                Playlist.Image = File.ReadAllBytes(dialog.FileName);
            }

            _IsImageEdited = false;
        }

        private void PencilIconMouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                ImageBrush ib = new ImageBrush();
                ib.ImageSource = new BitmapImage(new Uri(dialog.FileName));
                PlaylistImg.Fill = ib;
                Playlist.Image = File.ReadAllBytes(dialog.FileName);
            }
            _IsImageEdited = false;
        }

        private void CloseAddingMouseDown(object sender, MouseButtonEventArgs e)
        {
            AddPlaylistErrorBox.Visibility = Visibility.Hidden;
            OnWindowClosed?.Invoke();
        }
        private void CloseAddingMouseEnter(object sender, MouseEventArgs e)
        {
            CloseAdding.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF0051"));
        }
        private void CloseAddingMouseLeave(object sender, MouseEventArgs e)
        {
            CloseAdding.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ee3e61"));
        }




        public Song_Playlist Playlist
        {
            get { return (Song_Playlist)GetValue(PlaylistProperty); }
            set { SetValue(PlaylistProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Playlist.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaylistProperty =
            DependencyProperty.Register("Playlist", typeof(Song_Playlist), typeof(AddPlaylist));





        public bool _IsImageEdited
        {
            get { return (bool)GetValue(_IsImageEditedProperty); }
            set { SetValue(_IsImageEditedProperty, value); }
        }
        // Using a DependencyProperty as the backing store for _IsImageEdited.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty _IsImageEditedProperty =
            DependencyProperty.Register("_IsImageEdited", typeof(bool), typeof(AddPlaylist));
    }
}

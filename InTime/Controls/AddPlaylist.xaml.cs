using Microsoft.Win32;
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
    public delegate void AddWindowClosed();

    public delegate void ChangesAccepted();
    public partial class AddPlaylist : UserControl
    {
        public event AddWindowClosed OnWindowClosed;
        public event ChangesAccepted OnChangesAccepted;
        public AddPlaylist()
        {
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

        private void SaveBtnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (PlaylistNameBox.Text != String.Empty)
            {
                PlaylistName = PlaylistNameBox.Text;
                OnChangesAccepted?.Invoke();
            }
            else
            {
                OnWindowClosed?.Invoke();
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
                ImagePath = dialog.FileName;
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
                ImagePath = dialog.FileName;
            }
            _IsImageEdited = false;
        }

        private void CloseAddingMouseDown(object sender, MouseButtonEventArgs e)
        {
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


        public string PlaylistName
        {
            get { return (string)GetValue(PlaylistNameProperty); }
            set { SetValue(PlaylistNameProperty, value); }
        }
        public static readonly DependencyProperty PlaylistNameProperty =
            DependencyProperty.Register("PlaylistName", typeof(string), typeof(AddPlaylist));


        public string ImagePath
        {
            get { return (string)GetValue(ImagePathProperty); }
            set { SetValue(ImagePathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImagePath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImagePathProperty =
            DependencyProperty.Register("ImagePath", typeof(string), typeof(AddPlaylist));



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

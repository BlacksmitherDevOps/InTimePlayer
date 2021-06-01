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
using MaterialDesignThemes.Wpf;

namespace InTime.Controls
{
    /// <summary>
    /// Interaction logic for SearchItem.xaml
    /// </summary>
    public partial class SearchItem : UserControl
    {
        public event OpenAlbum OpenAlbum;
        public SearchItem()
        {
            InitializeComponent();
        }


        public string AlbumArtist
        {
            get { return (string)GetValue(ArtistProperty); }
            set { SetValue(ArtistProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Artist.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ArtistProperty =
            DependencyProperty.Register("AlbumArtist", typeof(string), typeof(SearchItem));


        public string AlbumTitle
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("AlbumTitle", typeof(string), typeof(SearchItem));

        public int AlbumID
        {
            get { return (int)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IDProperty =
            DependencyProperty.Register("AlbumID", typeof(int), typeof(SearchItem));

        public byte[] AlbumImage
        {
            get { return (byte[])GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Image.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("AlbumImage", typeof(byte[]), typeof(SearchItem));


        private void ImageRectangle_OnMouseEnter(object sender, MouseEventArgs e)
        {
            PlayIco.Visibility = Visibility.Visible;
            ImageRect.Opacity = 0.6;
        }

        private void ImageRectangle_OnMouseLeave(object sender, MouseEventArgs e)
        {
            PlayIco.Visibility = Visibility.Hidden;
            ImageRect.Opacity = 1;
        }

        private void Main_Grid_OnMouseEnter(object sender, MouseEventArgs e)
        {
            TextGrid.Background = Brushes.DimGray;
            Main_Grid.Background = Brushes.DimGray;
        }

        private void Main_Grid_OnMouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            TextGrid.Background = (Brush) bc.ConvertFrom("#FF2E2A2A");
            Main_Grid.Background = (Brush)bc.ConvertFrom("#FF2E2A2A");
        }

        private void PlayIco_OnMouseEnter(object sender, MouseEventArgs e)
        {
            PlayIco.Visibility = Visibility.Visible;
            ImageRect.Opacity = 0.6;
        }

        private void Main_Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenAlbum?.Invoke(AlbumID);
        }
    }
}

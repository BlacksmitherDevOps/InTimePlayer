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
    /// Interaction logic for SingleSearcItem.xaml
    /// </summary>
    public partial class SingleSearchItem : UserControl
    {
        public event OpenSingerPage OpenSingerPage;
        public SingleSearchItem()
        {
            InitializeComponent();
        }


        public byte[] SingerImage
        {
            get { return (byte[])GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Image.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("SingerImage", typeof(byte[]), typeof(SingleSearchItem));



        public string SingerTitle
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("SingerTitle", typeof(string), typeof(SingleSearchItem));

        public int SingerID
        {
            get { return (int)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IDProperty =
            DependencyProperty.Register("SingerID", typeof(int), typeof(SingleSearchItem));


        private void Main_Grid_OnMouseEnter(object sender, MouseEventArgs e)
        {
            TextGrid.Background = Brushes.DimGray;
            Main_Grid.Background = Brushes.DimGray;
        }

        private void Main_Grid_OnMouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            TextGrid.Background = (Brush)bc.ConvertFrom("#FF2E2A2A");
            Main_Grid.Background = (Brush)bc.ConvertFrom("#FF2E2A2A");
        }

        private void PlayIco_OnMouseEnter(object sender, MouseEventArgs e)
        {
            PlayIco.Visibility = Visibility.Visible;
            ImageRect.Opacity = 0.6;
        }

        private void ImageRect_OnMouseEnter(object sender, MouseEventArgs e)
        {
            PlayIco.Visibility = Visibility.Visible;
            ImageRect.Opacity = 0.6;
        }

        private void ImageRect_OnMouseLeave(object sender, MouseEventArgs e)
        {
            PlayIco.Visibility = Visibility.Hidden;
            ImageRect.Opacity = 1;
        }

        private void Main_Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenSingerPage?.Invoke(SingerID);
        }
    }
}

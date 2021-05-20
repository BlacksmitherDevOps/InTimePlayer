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
using Microsoft.Win32;

namespace InTime.Controls
{
    /// <summary>
    /// Interaction logic for ProfileEdit.xaml
    /// </summary>
    public partial class ProfileEdit : UserControl
    {
        public ProfileEdit()
        {
            InitializeComponent();
        }
        private void AvatarPickerMouseEnter(object sender, MouseEventArgs e)
        {
            EditIcon.Visibility = Visibility.Visible;
            AvatarPicker.Opacity = 0.6;
        }

        private void AvatarPickerMouseLeave(object sender, MouseEventArgs e)
        {
            EditIcon.Visibility = Visibility.Hidden;
            AvatarPicker.Opacity = 1;
        }

        private void EditIconMouseEnter(object sender, MouseEventArgs e)
        {
            EditIcon.Visibility = Visibility.Visible;
            AvatarPicker.Opacity = 0.6;
        }

        private void AvatarPickerMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                FilePicked = true;
                AvatarImgBrush.ImageSource= new BitmapImage(new Uri(dialog.FileName));
            }
        }
        private void RegisterPassbox1PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (RegisterPassbox1.Password.Length == 0)
                RegisterPassBlock1.Visibility = Visibility.Visible;
            else
                RegisterPassBlock1.Visibility = Visibility.Hidden;
        }

        private void RegisterPassboxPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (RegisterPassbox2.Password.Length == 0)
                RegisterPassBlock2.Visibility = Visibility.Visible;
            else
                RegisterPassBlock2.Visibility = Visibility.Hidden;
        }
        private void CancelRegBtnMouseEnter(object sender, MouseEventArgs e)
        {
            CancelRegrBtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF0051"));
            this.Cursor = Cursors.Hand;
        }

        private void ConfirmRegBtnMouseEnter(object sender, MouseEventArgs e)
        {
            ConfirmRegBtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF0051"));
            this.Cursor = Cursors.Hand;
        }

        private void CancelRegBtnMouseLeave(object sender, MouseEventArgs e)
        {
            CancelRegrBtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8d0d26"));
            this.Cursor = Cursors.Arrow;
        }

        private void ConfirmRegBtnMouseLeave(object sender, MouseEventArgs e)
        {
            ConfirmRegBtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8d0d26"));
            this.Cursor = Cursors.Arrow;
        }

        private void CancelRegBtnClick(object sender, MouseButtonEventArgs e)
        {
            AvatarImgBrush.ImageSource = new BitmapImage(new Uri(
                Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.LastIndexOf("InTime")) + @"InTime\Assets\profile.jpg"));
            
        }

        private void ConfirmRegBtnClick(object sender, MouseButtonEventArgs e)
        {
            
        }


        public bool FilePicked
        {
            get { return (bool)GetValue(FilePickedProperty); }
            set { SetValue(FilePickedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilePicked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilePickedProperty =
            DependencyProperty.Register("FilePicked", typeof(bool), typeof(ProfileEdit));


    }
}

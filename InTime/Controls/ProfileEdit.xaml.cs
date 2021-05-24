using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using InTime.ServiceReference1;
using Microsoft.Win32;

namespace InTime.Controls
{
    public delegate void ProfileEditCancel();
    /// <summary>
    /// Interaction logic for ProfileEdit.xaml
    /// </summary>
    public partial class ProfileEdit : UserControl
    {
        public event ProfileEditCancel OnEditCancel;
        public ProfileEdit()
        {
            InitializeComponent();
        }

        public void InitUser()
        {
            CurrentUser.ImagePath=String.Empty;
            RegisterLogin.Text = CurrentUser.NickName;
            MailBox.Text = CurrentUser.NickName;
            if (CurrentUser.Image != null) 
                AvatarImgBrush.ImageSource= ConvertToImage(CurrentUser.Image);
            else
                AvatarImgBrush.ImageSource= new BitmapImage(new Uri(
                    Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.LastIndexOf("InTime")) + @"InTime\Assets\profile.jpg"));
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
                CurrentUser.ImagePath = dialog.FileName;
            }
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            RegisterPassBlock1.Visibility =
                RegisterPassbox1.Password.Length == 0 ? Visibility.Visible : Visibility.Hidden;
            RegisterPassBlock2.Visibility =
                RegisterPassbox2.Password.Length == 0 ? Visibility.Visible : Visibility.Hidden;
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
            WrongPasswordInput.Visibility = Visibility.Hidden;
            EditProfileErrorBox.Visibility = Visibility.Hidden;
            OnEditCancel?.Invoke();
        }

        private async void ConfirmRegBtnClick(object sender, MouseButtonEventArgs e)
        {
            if (IsDataValid())
            {
                WrongPasswordInput.Visibility = Visibility.Hidden;
                EditProfileErrorBox.Visibility = Visibility.Hidden;
                if (RegisterLogin.Text != String.Empty) CurrentUser.NickName = RegisterLogin.Text;
                if (MailBox.Text != String.Empty) CurrentUser.Email = MailBox.Text;
                if (RegisterPassbox1.Password.Length != 0) CurrentUser.Password = RegisterPassbox1.Password;
                if (CurrentUser.ImagePath != String.Empty) CurrentUser.Image = File.ReadAllBytes(CurrentUser.ImagePath);
                try
                {
                    Service1Client client = new Service1Client();
                    await client.EditProfileAsync(CurrentUser);
                }
                catch
                {
                    EditProfileErrorBox.Visibility = Visibility.Visible;
                    EditProfileErrorMessage.Text =
                        "Network issues. Please check your\n;internet connection and try again.";
                    return;
                }
                WrongPasswordInput.Visibility = Visibility.Hidden;
                EditProfileErrorBox.Visibility = Visibility.Hidden;
            }
        }

        private bool IsDataValid()
        {
            if (MailBox.Text != null)
            {
                var tmp = new EmailAddressAttribute();
                if (!tmp.IsValid(MailBox.Text))
                {
                    WrongPasswordInput.Visibility = Visibility.Hidden;
                    EditProfileErrorBox.Visibility = Visibility.Visible;
                    EditProfileErrorMessage.Text = "Mail you entered isn't valid.\nPlease, try again.";
                    return false;
                }
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(RegisterLogin.Text, "^[a-zA-Z][a-zA-Z0-9._-]{0,21}([-.][^_]|[^-.]{2})$"))
            {
                WrongPasswordInput.Visibility = Visibility.Hidden;
                EditProfileErrorMessage.Text = "Login should start with letter and\ncontains only \"0-9\",\".\",\"_\".\".\"-\"";
                EditProfileErrorBox.Visibility = Visibility.Visible;
                return false;
            }
            else if (RegisterPassbox1.Password != RegisterPassbox2.Password)
            {
                WrongPasswordInput.Visibility = Visibility.Hidden;
                EditProfileErrorMessage.Text = "Passwords doesn't match.\nPlease, try again.";
                EditProfileErrorMessage.Visibility = Visibility.Visible;
                return false;
            }
            else if ((RegisterPassbox1.Password == RegisterPassbox2.Password) &&
                     (RegisterPassbox1.Password != String.Empty))
            {
                EditProfileErrorMessage.Visibility = Visibility.Hidden;
                WrongPasswordInput.Visibility = Visibility.Visible;
                return false;
            }

            return true;
        }

        public bool FilePicked
        {
            get { return (bool)GetValue(FilePickedProperty); }
            set { SetValue(FilePickedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilePicked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilePickedProperty =
            DependencyProperty.Register("FilePicked", typeof(bool), typeof(ProfileEdit));


        public Client_User CurrentUser
        {
            get { return (Client_User)GetValue(CurrentUserProperty); }
            set { SetValue(CurrentUserProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentUserProperty =
            DependencyProperty.Register("CurrentUser", typeof(int), typeof(ProfileEdit));

        public BitmapSource ConvertToImage(byte[] arr)
        {
            using (MemoryStream ms = new MemoryStream(arr))
            {
                var decoder = BitmapDecoder.Create(ms,
                    BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                return decoder.Frames[0];
            }
        }

    }
}

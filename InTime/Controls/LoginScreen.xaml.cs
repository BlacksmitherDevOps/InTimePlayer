using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Drawing;
using System.IO;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using MaterialDesignThemes.Wpf;
using InTime.ServiceReference1;

namespace InTime.Controls
{
    public delegate void WindowClosed(Client_User user);
    public partial class LoginScreen : Window
    {
        public event WindowClosed OnWindowClosed;
        public LoginScreen()
        {
            InitializeComponent();
           
        }

        #region LoginGrid

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (LoginPassBox.Password.Length == 0)
                PassBlock.Visibility = Visibility.Visible;
            else
                PassBlock.Visibility = Visibility.Hidden;
        }

        private void LoginBtnMouseEnter(object sender, MouseEventArgs e)
        {
            LoginBtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF0051"));
            this.Cursor = Cursors.Hand;
        }

        private void LoginBtnmouseLeave(object sender, MouseEventArgs e)
        {
            LoginBtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8d0d26"));
            this.Cursor = Cursors.Arrow;
        }

        private async void LoginBtnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if ((LoginBox.Text == String.Empty || LoginPassBox.Password == String.Empty) 
                    || (LoginBox.Text == String.Empty && LoginPassBox.Password == String.Empty))
                {
                    ErrorText.Text = "One or more fields should be \n; signed. Please, try again.";
                    NetworkError.Visibility = Visibility.Visible;
                }
                else 
                {
                    Console.WriteLine("asd");
                    Service1Client client = new Service1Client();
                    OnWindowClosed?.Invoke(await client.TryLoginAsync(new Login_User{Image = null,ExtensionData = null,Login=LoginBox.Text,Password = LoginPassBox.Password,Email = null}));
                    this.Close();
                }
            }
            catch (FaultException<LoginFailed> exception)
            {
                ErrorText.Text = exception.Detail.Message;
                NetworkError.Visibility = Visibility.Visible;
                return;
            }
            catch
            {
                ErrorText.Text = "Network issues. Please check your\ninternet connection and try again.";
                NetworkError.Visibility = Visibility.Visible;
                return;
            }

            NetworkError.Visibility = Visibility.Hidden;
        }

        private void RegBtnMouseEnter(object sender, MouseEventArgs e)
        {
            RegBtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF0051"));
            this.Cursor = Cursors.Hand;
        }

        private void RegBtnMouseLeave(object sender, MouseEventArgs e)
        {
            RegBtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8d0d26"));
            this.Cursor = Cursors.Arrow;
        }

        private void RegBtnMouseDown(object sender, MouseButtonEventArgs e)
        {
            LoginGrid.Visibility = Visibility.Hidden;
            SignUpGrid.Visibility = Visibility.Visible;
        }

        private void AccRecoveryMouseEnter(object sender, MouseEventArgs e)
        {
            AccRecovery.Foreground = new SolidColorBrush(Colors.White);
            AccRecovery.TextDecorations = TextDecorations.Underline;
            this.Cursor = Cursors.Hand;
        }

        private void AccRecoveryMouseLeave(object sender, MouseEventArgs e)
        {
            AccRecovery.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ee3e61"));
            AccRecovery.TextDecorations = null;
            this.Cursor = Cursors.Arrow;
        }

        private void AccRecoveryClick(object sender, MouseButtonEventArgs e)
        {
            LoginGrid.Visibility = Visibility.Hidden;
            RecoveryPage1.Visibility = Visibility.Visible;
        }

        #endregion

        #region RecoveryPage1



        public string RecoveredLogin
        {
            get { return (string)GetValue(RecoveredLoginProperty); }
            set { SetValue(RecoveredLoginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RecoveredLogin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RecoveredLoginProperty =
            DependencyProperty.Register("RecoveredLogin", typeof(string), typeof(LoginScreen));


        private void SendcodeMouseEnter(object sender, MouseEventArgs e)
        {
            SendCodeBtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF0051"));
            this.Cursor = Cursors.Hand;
        }
        private void ApplyBtnMouseEnter(object sender, MouseEventArgs e)
        {
            AcceptBtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF0051"));
            this.Cursor = Cursors.Hand;
        }
        private void CancelBtnMouseEnter(object sender, MouseEventArgs e)
        {
            CancelBtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF0051"));
            this.Cursor = Cursors.Hand;
        }
        private void SendCodeMouseLeave(object sender, MouseEventArgs e)
        {
            SendCodeBtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8d0d26"));
            this.Cursor = Cursors.Arrow;
        }
        private void CancelBtnMouseLeave(object sender, MouseEventArgs e)
        {
            CancelBtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8d0d26"));
            this.Cursor = Cursors.Arrow;
        }
        private void ApplyBtnMouseLeave(object sender, MouseEventArgs e)
        {
            AcceptBtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8d0d26"));
            this.Cursor = Cursors.Arrow;
        }
        private void RecoveryLoginTextChanged(object sender, TextChangedEventArgs e)
        {
            AcceptBtn.IsEnabled = SendCodeBtn.IsEnabled = RecoveryLogin.Text != String.Empty ? true : false;
            AcceptBtn.IsEnabled  = (RecoveryLogin.Text != String.Empty && CodeEnterBox.Text != String.Empty)? true : false;
        }
        private async void SendCodeClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Service1Client client = new Service1Client();
                Console.WriteLine(
                await client.RecoverPassCodeRequestAsync(new Login_User {Login = RecoveryLogin.Text}));
            }
            catch (FaultException<RecoverFailed> exception)
            {
                RecoveryPage1ErrorBox.Visibility = Visibility.Visible;
                RecoveryPage1MessageBox.Text = exception.Detail.Message;
                return;
            }
            catch
            {
                RecoveryPage1ErrorBox.Visibility = Visibility.Visible;
                RecoveryPage1MessageBox.Text = "Network issues. Please check your\ninternet connection and try again.";
                return;
            }
            RecoveryPage1ErrorBox.Visibility = Visibility.Hidden;
        }
        private void CancelBtnClick(object sender, MouseButtonEventArgs e)
        {
            RecoveryLogin.Text = "";
            CodeEnterBox.Text = "";
            RecoveryPage1.Visibility = Visibility.Hidden;
            LoginGrid.Visibility = Visibility.Visible;
        }
        private async void ApplyBtnClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Service1Client client = new Service1Client();
                var res=await client.RecoverPassCodeCheckAsync(new Login_User {Login = RecoveryLogin.Text}, CodeEnterBox.Text);
                Console.WriteLine(res);
                if (res)
                {
                    RecoveredLogin = RecoveryLogin.Text;
                    RecoveryLogin.Text = "";
                    CodeEnterBox.Text = "";
                    RecoveryPage1.Visibility = Visibility.Hidden;
                    RecoveryPage2.Visibility = Visibility.Visible;
                }
            }
            catch (FaultException<RecoverFailed> exception)
            {
                Console.WriteLine(exception.Detail.Message);
                RecoveryPage1ErrorBox.Visibility = Visibility.Visible;
                RecoveryPage1MessageBox.Text = exception.Detail.Message;
                return;
            }
            catch 
            {
                RecoveryPage1ErrorBox.Visibility = Visibility.Visible;
                RecoveryPage1MessageBox.Text = "Network issues. Please check your\ninternet connection and try again later.";
                return;
            }
            RecoveryPage1ErrorBox.Visibility = Visibility.Hidden;
            
        }
        #endregion

        #region RecoveryPage2

        private void PassboxOnPasswordChanged(object sender, RoutedEventArgs e)
        {
            PassBlock1.Visibility= Passbox1.Password.Length == 0? Visibility.Visible : Visibility.Hidden;
            PassBlock2.Visibility = Passbox2.Password.Length == 0 ? Visibility.Visible : Visibility.Hidden;
            if (Passbox1.Password != Passbox2.Password)
            {
                RecoveryPage2ErrorPassBox.Visibility = Visibility.Hidden;
                ConfirmBtnPage2.IsEnabled = false;
                RecoveryPage2ErrorBox.Visibility = Visibility.Visible;
                RecoveryPage2ErrorMessage.Text = "Passwords doesn't match.\nPlease, try again.";
            }
            else if (Passbox1.Password == Passbox2.Password && !Regex.IsMatch(Passbox1.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$"))
            {
                Console.WriteLine("asd");
                RecoveryPage2ErrorBox.Visibility = Visibility.Hidden;
                RecoveryPage2ErrorPassBox.Visibility = Visibility.Visible;
            }
            else
            {
                RecoveryPage2ErrorPassBox.Visibility = Visibility.Hidden;
                RecoveryPage2ErrorBox.Visibility = Visibility.Hidden;
                ConfirmBtnPage2.IsEnabled = true;
            }
        }
        private void CancelBtnPage2MouseEnter(object sender, MouseEventArgs e)
        {
            CancelBtnPage2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF0051"));
            this.Cursor = Cursors.Hand;
        }
        private void ConfirmBtnMouseEnter(object sender, MouseEventArgs e)
        {
            ConfirmBtnPage2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF0051"));
            this.Cursor = Cursors.Hand;
        }
        private void CancelBtnPage2MouseLeave(object sender, MouseEventArgs e)
        {
            CancelBtnPage2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8d0d26"));
            this.Cursor = Cursors.Arrow;
        }

        private void ConfirmBtnMouseLeave(object sender, MouseEventArgs e)
        {
            ConfirmBtnPage2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8d0d26"));
            this.Cursor = Cursors.Arrow;
        }

        private void CancelBtnPage2Click(object sender, MouseButtonEventArgs e)
        {
            Passbox1.Password = "";
            Passbox2.Password = "";
            RecoveryPage2ErrorPassBox.Visibility = Visibility.Hidden;
            RecoveryPage2.Visibility = Visibility.Hidden;
            LoginGrid.Visibility = Visibility.Visible;
        }

        private async void ConfirmBtnClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Service1Client client = new Service1Client();
                bool reg= await client.ChangePasswordAsync(new Login_User {Password = Passbox1.Password,Login = RecoveredLogin});
            }
            catch
            {
                ConfirmBtnPage2.IsEnabled = false;
                RecoveryPage2ErrorBox.Visibility = Visibility.Visible;
                RecoveryPage2ErrorMessage.Text = "Network issues. Please check your\n;internet connection and try again later.";
            }
            RecoveryPage2ErrorPassBox.Visibility = Visibility.Hidden;
            RecoveryPage2ErrorBox.Visibility = Visibility.Hidden;
            Passbox1.Password = "";
            Passbox2.Password = "";
            RecoveryPage2.Visibility = Visibility.Hidden;
            LoginGrid.Visibility = Visibility.Visible;
        }
        
        #endregion

        #region SignUp Page

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
                ImageBrush ib = new ImageBrush();
                ib.ImageSource = new BitmapImage(new Uri(dialog.FileName));
                AvatarPicker.Fill = ib;
                ImagePath = dialog.FileName;
                Console.WriteLine(ImagePath);
            }
        }

        private void EditIconMouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                ImageBrush ib = new ImageBrush();
                ib.ImageSource = new BitmapImage(new Uri(dialog.FileName));
                AvatarPicker.Fill = ib;
                ImagePath = dialog.FileName;
                Console.WriteLine(ImagePath);
            }
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
            RegisterErrorBox.Visibility = Visibility.Hidden;
            RegisterPassbox1.Password = "";
            RegisterPassbox2.Password = "";
            RegisterLogin.Text = "";
            RegisterMail.Text = "";
            SignUpGrid.Visibility = Visibility.Hidden;
            LoginGrid.Visibility = Visibility.Visible;
        }

        private async void ConfirmRegBtnClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Service1Client client = new Service1Client();
                await client.RegNewUserAsync(new Login_User
                {
                    Email = RegisterMail.Text,
                    ExtensionData = null,
                    Image = ImagePath == null ? null : File.ReadAllBytes(ImagePath),
                    Login = RegisterLogin.Text,
                    Password = RegisterPassbox1.Password
                });
                SignUpGrid.Visibility = Visibility.Hidden;
                LoginGrid.Visibility = Visibility.Visible;
            }
            catch (FaultException<RegFailed> exception)
            {
                RegisterErrorBox.Visibility = Visibility.Visible;
                RegisterErrorMessage.Text = exception.Detail.Message;
                return;
            }
            catch (Exception exception)
            {
                RegisterErrorBox.Visibility = Visibility.Visible;
                RegisterErrorMessage.Text =
                    "Network issues. Please check your\n;internet connection and try again.";
                return;
            }
        }
        private void RegisterFieldsTextChanged(object sender, TextChangedEventArgs e)
        {
            if (RegisterLogin.Text != String.Empty && RegisterMail.Text != String.Empty && RegisterPassbox1.Password != String.Empty && RegisterPassbox2.Password != String.Empty)
            {
                var tmp = new EmailAddressAttribute();
                if (RegisterPassbox1.Password != RegisterPassbox2.Password) 
                {
                    WrongPasswordInput.Visibility = Visibility.Hidden;
                    ConfirmRegBtn.IsEnabled = false;
                    RegisterErrorMessage.Text = "Passwords doesn't match.\nPlease, try again.";
                    RegisterErrorBox.Visibility = Visibility.Visible;
                }
                else if (!tmp.IsValid(RegisterMail.Text))
                {
                    WrongPasswordInput.Visibility = Visibility.Hidden;
                    ConfirmRegBtn.IsEnabled = false;
                    RegisterErrorMessage.Text = "Mail you entered isn't valid.\nPlease, try again.";
                    RegisterErrorBox.Visibility = Visibility.Visible;
                }
                else if(!Regex.IsMatch(RegisterLogin.Text, "^[a-zA-Z][a-zA-Z0-9._-]{0,21}([-.][^_]|[^-.]{2})$"))
                {
                    WrongPasswordInput.Visibility = Visibility.Hidden;
                    ConfirmRegBtn.IsEnabled = false;
                    RegisterErrorMessage.Text = "Login should start with letter and\ncontains only \"0-9\",\".\",\"_\".\".\"-\"";
                    RegisterErrorBox.Visibility = Visibility.Visible;
                }
                else if (RegisterPassbox1.Password == RegisterPassbox2.Password&&!Regex.IsMatch(RegisterPassbox1.Password,@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$"))
                {
                    WrongPasswordInput.Visibility = Visibility.Visible;
                    RegisterErrorBox.Visibility = Visibility.Hidden;
                }
                else
                {
                    WrongPasswordInput.Visibility = Visibility.Hidden;
                    RegisterErrorBox.Visibility = Visibility.Hidden;
                    ConfirmRegBtn.IsEnabled = true;
                }
            }
            else
            {
                WrongPasswordInput.Visibility = Visibility.Hidden;
                ConfirmRegBtn.IsEnabled = false;
                RegisterErrorMessage.Text = "All fields should be signed. Please sign\nall fields and try again.";
                RegisterErrorBox.Visibility = Visibility.Visible;
            }
           
        }
        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            RegisterPassBlock1.Text = RegisterPassbox1.Password.Length == 0 ? "Enter password ..." : "";
            RegisterPassBlock2.Text = RegisterPassbox2.Password.Length == 0 ? "Enter password ..." : "";
            RegisterFieldsTextChanged(this, null);
        }
        public string ImagePath
        {
            get { return (string)GetValue(ImagePathProperty); }
            set { SetValue(ImagePathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImagePath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImagePathProperty =
            DependencyProperty.Register("ImagePath", typeof(string), typeof(LoginScreen));


        #endregion

        #region UpperPanel
        bool upperPanelClick;
        Point coord;
        private void upper_bord_MouseDown(object sender, MouseButtonEventArgs e)
        {
            upperPanelClick = true;
            coord = e.GetPosition(this);
        }
        private void upper_bord_MouseMove(object sender, MouseEventArgs e)
        {
            if (upperPanelClick && coord != e.GetPosition(this))
            {
                if (coord.Y < e.GetPosition(this).Y)
                    this.Top += e.GetPosition(this).Y - coord.Y;
                else if (coord.Y > e.GetPosition(this).Y)
                    this.Top -= coord.Y - e.GetPosition(this).Y;
                if (coord.X < e.GetPosition(this).X)
                    this.Left += e.GetPosition(this).X - coord.X;
                else if (coord.X > e.GetPosition(this).X)
                    this.Left -= coord.X - e.GetPosition(this).X;
            }
        }
        private void upper_bord_MouseLeave(object sender, MouseEventArgs e)
        {
            upperPanelClick = false;
        }
        private void upper_bord_MouseUp(object sender, MouseButtonEventArgs e)
        {
            upperPanelClick = false;
        }

        private void WindowControlButton_MouseEnter(object sender, MouseEventArgs e)
        {
            ((PackIcon)((Border)sender).Child).Foreground = Brushes.White;
        }

        private void WindowControlButton_MouseLeave(object sender, MouseEventArgs e)
        {
            ((PackIcon)((Border)sender).Child).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF0051"));
        }

        private void WindowControlButton_Close_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }


        #endregion

        private async void admin_Click(object sender, RoutedEventArgs e)
        {
            Service1Client client = new Service1Client();
            try
            {
                loadBord.Visibility = Visibility.Visible;
                //await client.DownloadFileAsync(File.ReadAllBytes("d:\\rrk.jpg"));
                //await client.tmpAsync(File.ReadAllBytes("d:\\ger.jpg"));
                OnWindowClosed?.Invoke(await client.TryLoginAsync(new Login_User { Image = null, ExtensionData = null, Login = "BogdanLysenkoLox", Password = "dsa", Email = null }));
                this.Close();
            }
            catch (FaultException<LoginFailed> ex)
            {
                Console.WriteLine(ex.Detail.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            loadBord.Visibility = Visibility.Hidden;

        }
    }
}

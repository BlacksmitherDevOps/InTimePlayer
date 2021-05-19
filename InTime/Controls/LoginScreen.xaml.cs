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
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using MaterialDesignThemes.Wpf;

namespace InTime.Controls
{
    public delegate void WindowClosed();
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
            if (Pass.Password.Length == 0)
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

        private void LoginBtnMouseDown(object sender, MouseButtonEventArgs e)
        {
            OnWindowClosed?.Invoke();
            this.Close();
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
        private void SendCodeClick(object sender, MouseButtonEventArgs e)
        {

        }
        private void CancelBtnClick(object sender, MouseButtonEventArgs e)
        {
            RecoveryPage1.Visibility = Visibility.Hidden;
            LoginGrid.Visibility = Visibility.Visible;
        }
        private void ApplyBtnClick(object sender, MouseButtonEventArgs e)
        {
            RecoveryPage1.Visibility = Visibility.Hidden;
            RecoveryPage2.Visibility = Visibility.Visible;
        }
        #endregion

        #region RecoveryPage2

        private void Passbox1_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Passbox1.Password.Length == 0)
                PassBlock1.Visibility = Visibility.Visible;
            else
                PassBlock1.Visibility = Visibility.Hidden;
        }
        private void Passbox2_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Passbox2.Password.Length == 0)
                PassBlock2.Visibility = Visibility.Visible;
            else
                PassBlock2.Visibility = Visibility.Hidden;
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
            RecoveryPage2.Visibility = Visibility.Hidden;
            LoginGrid.Visibility = Visibility.Visible;
        }

        private void ConfirmBtnClick(object sender, MouseButtonEventArgs e)
        {
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
            SignUpGrid.Visibility = Visibility.Hidden;
            LoginGrid.Visibility = Visibility.Visible;
        }

        private void ConfirmRegBtnClick(object sender, MouseButtonEventArgs e)
        {
            if (RegisterLogin.Text == "" || RegisterPassbox1.Password == "" || RegisterPassbox2.Password == "")
            {
                RegPassError.Visibility = Visibility.Hidden;
                WrongDataInput.Visibility = Visibility.Visible;
            } 
            else if (RegisterPassbox1.Password != RegisterPassbox2.Password)
            {
                WrongDataInput.Visibility = Visibility.Hidden;
                RegPassError.Visibility = Visibility.Visible;
            }
        }
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
    }
}

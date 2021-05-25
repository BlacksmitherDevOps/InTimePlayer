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
    public delegate void OpenPlaylist(int id);
    public delegate void OpenSingerPage(int id);
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Visibility = Visibility.Hidden;
            LoginScreen tmp = new LoginScreen();
            tmp.OnWindowClosed += Tmp_OnWindowClosed;
            tmp.ShowDialog();
            
        }

        private void Tmp_OnWindowClosed(Client_User user)
        {
            this.Visibility = Visibility.Visible;
            Control_Bord.Child = new MainPlayerPage_Control(this,user);
            Console.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
        }
        void AutoLogin()
        {
           // Directory.Exists(Environment.UserName)
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

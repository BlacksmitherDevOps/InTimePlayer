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
    /// Логика взаимодействия для MainPlayerPage_Control.xaml
    /// </summary>
    public partial class MainPlayerPage_Control : UserControl
    {
        public MainPlayerPage_Control()
        {
            InitializeComponent();
            testAddPlaylist();
        }
        void testAddPlaylist()
        {
            List<tempPlaylist> lst = new List<tempPlaylist>();
            lst.Add(new tempPlaylist() { Title = "ListName" });
            lst.Add(new tempPlaylist() { Title = "ListName" });
            lst.Add(new tempPlaylist() { Title = "ListName" });
            myList_lb.ItemsSource = lst;
        }
    }
}

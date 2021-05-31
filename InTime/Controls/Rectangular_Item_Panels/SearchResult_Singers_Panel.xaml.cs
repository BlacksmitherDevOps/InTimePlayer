﻿using InTime.Controls.Rectangular_Items;
using InTime.ServiceReference1;
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

namespace InTime.Controls.Rectangular_Item_Panels
{
    /// <summary>
    /// Логика взаимодействия для SearchResult_Singers_Panel.xaml
    /// </summary>
    public partial class SearchResult_Singers_Panel : UserControl
    {
        public event OpenSingerPage OpenSingerPage;
        public SearchResult_Singers_Panel()
        {
            InitializeComponent();
        }
        public void AddList(Song_Singer[] albums)
        {
            foreach (var item in albums)
            {
                Singer_Item_Control item_Control = new Singer_Item_Control(item);
                item_Control.OpenSingerPage += Item_Control_OpenAlbum;
                panel.Children.Add(item_Control);
            }
        }

        private void Item_Control_OpenAlbum(int id)
        {
            OpenSingerPage?.Invoke(id);
        }
    }
}

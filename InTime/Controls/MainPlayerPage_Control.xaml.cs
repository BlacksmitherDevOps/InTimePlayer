using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
        private AppState state;
        public MainPlayerPage_Control()
        {
            InitializeComponent();
            state= new AppState();
            state.sound = SoundState.HighSound;
            testAddPlaylist();
            testInfoBord();
            testInfoBord();
            testInfoBord();
        }
        void testAddPlaylist()
        {
           
        }
        void testInfoBord()
        {
            Playlist_Control playlist_Control = new Playlist_Control();
            tape_panel.Children.Add(playlist_Control);
        }

        /// <summary>
        /// Изменение состояния картинки громкости
        /// SoundIcon_OnMouseEnter - при наведении на иконку
        /// SoundIcon_OnMouseDown - при нажатии на нее
        /// SoundIcon_OnMouseLeave - при выводе курсора за пределы картинки
        /// SoundSlider_OnValueChanged - при изменении громкости
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SoundIcon_OnMouseEnter(object sender, MouseEventArgs e)
        {
            switch (state.sound)
            {
                case SoundState.LowSound:
                    SoundIcon.Fill = new ImageBrush(new BitmapImage(new Uri(@"Assets/LowSoundRed.png",UriKind.Relative)));
                    break;
                case SoundState.MidSound:
                    SoundIcon.Fill = new ImageBrush(new BitmapImage(new Uri(@"Assets/MidSoundRed.png",UriKind.Relative)));
                    break;
                case SoundState.HighSound:
                    SoundIcon.Fill = new ImageBrush(new BitmapImage(new Uri(@"Assets/MaxVolumeRed.png",UriKind.Relative)));
                    break;
                case SoundState.Muted:
                    SoundIcon.Fill = new ImageBrush(new BitmapImage(new Uri(@"Assets/MutedRed.png",UriKind.Relative)));
                    break;
            }
        }
        private void SoundIcon_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            string tag=(string)SoundSlider.Tag;
            if (tag=="true")
            {
                state.sound = SoundState.Muted;
                SoundIcon.Fill=new ImageBrush(new BitmapImage(new Uri(@"Assets/MutedRed.png",UriKind.Relative)));
                SoundSlider.Tag = "false";
            }
            else
            {
                if (SoundSlider.Value < 30)
                {
                    state.sound = SoundState.LowSound;
                    SoundIcon.Fill =new ImageBrush(new BitmapImage(new Uri(@"Assets/LowSoundRed.png",UriKind.Relative)));
                }
                else if (SoundSlider.Value >= 30 && SoundSlider.Value <= 70)
                {
                    state.sound = SoundState.MidSound;
                    SoundIcon.Fill = new ImageBrush(new BitmapImage(new Uri(@"Assets/MidSoundRed.png",UriKind.Relative)));
                }
                else if (SoundSlider.Value > 70)
                {
                    state.sound = SoundState.HighSound;
                    SoundIcon.Fill =new ImageBrush(new BitmapImage(new Uri(@"Assets/MaxVolumeRed.png",UriKind.Relative)));
                }
                SoundSlider.Tag = "true";
            }
        }
        private void SoundIcon_OnMouseLeave(object sender, MouseEventArgs e)
        {
            switch (state.sound)
            {
                case SoundState.LowSound:
                    SoundIcon.Fill = new ImageBrush(new BitmapImage(new Uri(@"Assets/LowSoundPurple.png",UriKind.Relative)));
                    break;
                case SoundState.MidSound:
                    SoundIcon.Fill = new ImageBrush(new BitmapImage(new Uri(@"Assets/MidSoundPurple.png",UriKind.Relative)));
                    break;
                case SoundState.HighSound:
                    SoundIcon.Fill = new ImageBrush(new BitmapImage(new Uri(@"Assets/MaxVolumePurple.png",UriKind.Relative)));
                    break;
                case SoundState.Muted:
                    SoundIcon.Fill = new ImageBrush(new BitmapImage(new Uri(@"Assets/MutedPurple.png",UriKind.Relative)));
                    break;
            }
        }
        private void SoundSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
                if (SoundSlider.Value < 30)
                {
                    state.sound = SoundState.LowSound;
                    SoundIcon.Fill = new ImageBrush(new BitmapImage(new Uri(@"Assets/LowSoundPurple.png", UriKind.Relative)));
                }
                else if (SoundSlider.Value >= 30 && SoundSlider.Value <= 70)
                {
                    state.sound = SoundState.MidSound;
                    SoundIcon.Fill = new ImageBrush(new BitmapImage(new Uri(@"Assets/MidSoundPurple.png", UriKind.Relative)));
                }
                else if (SoundSlider.Value > 70)
                {
                    state.sound = SoundState.HighSound;
                    SoundIcon.Fill = new ImageBrush(new BitmapImage(new Uri(@"Assets/MaxVolumePurple.png", UriKind.Relative)));
                }
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            if (PlaylistBox.SelectedIndex >= 0)
            {
                PlaylistBox.SelectedIndex = -1;
            }
        }

        private void PlaylistBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var child in RadioBtnsPanel.Children)
            {
                (child as RadioButton).IsChecked = false;
            }
        }
    }
}

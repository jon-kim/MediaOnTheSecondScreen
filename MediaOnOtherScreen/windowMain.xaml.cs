using System.Windows;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MediaOnOtherScreen
{
    public partial class windowMain : Window
    {
        private ObservableCollection<MediaInfo> MediaInfos = new ObservableCollection<MediaInfo>();
        private ObservableCollection<Screen> Screens = new ObservableCollection<Screen>();
        private windowDisplay DISPLAY = null;
        private Screen SelectedScreen = null;

        public windowMain()
        {
            InitializeComponent();

            this.LoadMonitors();
            //this.LoadFilesFromDirectory(@"D:\[_PF_]\[I]\Pictures\Wall");

            this.menuItemDisplayOn.ItemsSource = this.Screens;
            this.listBoxMedia.ItemsSource = this.MediaInfos;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (System.Windows.Forms.Screen.AllScreens.Length > 1)
                this.SelectedScreen = this.Screens.First(s => s.Info.DeviceName != System.Windows.Forms.Screen.FromHandle(new System.Windows.Interop.WindowInteropHelper(this).Handle).DeviceName);
            else
                this.SelectedScreen = this.Screens[0];

            this.SelectedScreen.IsChecked = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.DISPLAY != null)
                this.DISPLAY.Close();
        }

        private void LoadMonitors()
        {
            List<System.Windows.Forms.Screen> screens = new List<System.Windows.Forms.Screen>(System.Windows.Forms.Screen.AllScreens);
            this.Screens = new ObservableCollection<Screen>(screens.Select(x => new Screen(screens.IndexOf(x) + 1, x)).ToArray());
        }

        private void LoadFiles(string[] files)
        {
            foreach (string f in files)
                this.MediaInfos.Add(new MediaInfo(f));
        }

        private void LoadFilesFromDirectory(string directoryPath)
        {
            this.LoadFiles(System.IO.Directory.GetFiles(directoryPath));
        }

        private void listBoxMedia_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (listBoxMedia.SelectedItem != null && this.SelectedScreen != null)
            {
                if (this.DISPLAY != null)
                {
                    this.DISPLAY.Clear();
                    this.DISPLAY.Screen = this.SelectedScreen;
                    this.DISPLAY.Mediainfo = listBoxMedia.SelectedItem as MediaInfo;
                    this.DISPLAY.Activate();
                }
                else
                {
                    this.DISPLAY = new windowDisplay(listBoxMedia.SelectedItem as MediaInfo, this.SelectedScreen);
                    this.DISPLAY.Show();
                }
            }

            this.Focus();
        }

        private void menuItemDisplayOn_Checked(object sender, RoutedEventArgs e)
        {
            this.SelectedScreen = (e.OriginalSource as System.Windows.Controls.MenuItem).Tag as Screen;
        }

        private void listBoxMedia_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effects = DragDropEffects.Copy;
        }

        private void listBoxMedia_Drop(object sender, DragEventArgs e)
        {
            this.LoadFiles((string[])e.Data.GetData(DataFormats.FileDrop));
        }

        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            this.DISPLAY.Clear();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MediaOnOtherScreen
{
    public partial class windowDisplay : Window
    {
        private Screen _screen = null;
        private MediaInfo _mediainfo = null;

        private windowDisplay()
        {
            InitializeComponent();
        }

        public windowDisplay(MediaInfo mediaInfo, Screen screen)
            : this()
        {
            this.Screen = screen;
            this.Mediainfo = mediaInfo;
        }

        public Screen Screen
        {
            get { return this._screen; }
            set
            {
                if (this.Screen != value)
                {
                    this._screen = value;

                    this.Height = this.Screen.Info.WorkingArea.Height;
                    this.Width = this.Screen.Info.WorkingArea.Width;

                    this.Top = (this.Screen.Info.WorkingArea.Top + this.Screen.Info.WorkingArea.Height) / 2;
                    this.Left = (this.Screen.Info.WorkingArea.Left + this.Screen.Info.WorkingArea.Width) / 2;
                }
            }
        }

        public MediaInfo Mediainfo
        {
            get { return this._mediainfo; }
            set
            {
                if (this.Mediainfo != value)
                {
                    this._mediainfo = value;

                    if (System.IO.File.Exists(this._mediainfo.Path))
                    {
                        BitmapImage pic = new BitmapImage();
                        pic.BeginInit();
                        pic.CacheOption = BitmapCacheOption.OnLoad;

                        if (System.IO.File.Exists(this._mediainfo.Path))
                            pic.UriSource = new Uri(this._mediainfo.Path);

                        if (this.Screen != null)
                            pic.DecodePixelWidth = this.Screen.Info.WorkingArea.Width;

                        pic.EndInit();

                        image.Source = pic;
                    }
                    else
                        image.Source = null;
                }
            }
        }

        public void Clear()
        {
            this.Mediainfo = new MediaInfo(string.Empty);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Maximized;
        }

        private void TakeScreenShot()
        {
            System.Drawing.Bitmap screen = new System.Drawing.Bitmap();
        }
    }
}

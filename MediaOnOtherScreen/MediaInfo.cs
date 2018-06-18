using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaOnOtherScreen
{
    public class MediaInfo
    {
        public string Path { get; private set; }
        public string Filename { get { return System.IO.Path.GetFileName(Path); } }

        public MediaInfo(string path)
        {
            this.Path = path;
        }
    }
}
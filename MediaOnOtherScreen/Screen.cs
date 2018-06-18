using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaOnOtherScreen
{
    public class Screen
    {
        public int Index { get; private set; }
        public System.Windows.Forms.Screen Info { get; private set; }
        public bool IsChecked { get; set; }

        public Screen(int index, System.Windows.Forms.Screen screen)
        {
            this.Index = index;
            this.Info = screen;
            this.IsChecked = false;
        }

        public override string ToString()
        {
            return string.Format("Screen {0}", this.Index);
        }
    }
}

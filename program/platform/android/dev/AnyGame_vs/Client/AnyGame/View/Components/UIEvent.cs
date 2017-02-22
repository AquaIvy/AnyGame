using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnyGame.UI
{
    delegate void UIEvent(UIElement sender, EventArgs e);

    class ClickArgs : EventArgs
    {
        public int a { get; set; }
    }
}

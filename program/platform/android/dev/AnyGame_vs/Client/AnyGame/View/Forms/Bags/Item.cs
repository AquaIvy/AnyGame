using AnyGame.View;
using AnyGame.View.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnyGame.View.Forms.Bags
{
    class Item : UIElement
    {
        public Item(float x,float y)
        {
            InitForm();
            SetXY(x, y);
        }

        private void InitForm()
        {
            imgBG = new UIImage("ui/common/90B667EB.png", 0, 0, UIUtils.MiddleCenter);
            var a = imgBG.anchorMin;
            AddChild(imgBG);
        }

        UIImage imgBG = null;
    }
}

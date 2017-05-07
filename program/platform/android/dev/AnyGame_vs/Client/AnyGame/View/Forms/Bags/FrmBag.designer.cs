using AnyGame.View;
using AnyGame.View.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AnyGame.View.Forms.Bags
{
    partial class FrmBag
    {
        private void InitForm()
        {
            SetBackground("ui/common/F0B3864B.png");
            this.Size = new Vector2(700, 500);
            this.Border = UIUtils.Border20;


            btnClose = new UIButton("aztec/button/button_close.png", Width - 50, 50);
            AddChild(btnClose);

            var imgItemBG = new UIImage("ui/common/11E2EE8F.png", 30, 100, UIUtils.UpperLeft, UIUtils.Border10);
            imgItemBG.Size = new Vector2(635, 370);
            AddChild(imgItemBG);

            var scroll = new UIScrollView(null, imgItemBG.x, imgItemBG.y, imgItemBG.Width, imgItemBG.Height, Layout.Vertical, null, null, true);
            AddChild(scroll);

            var t = new UIButton("aztec/button/button_02.png", 100, 100);
            scroll.AddChild(t);

            allItems = new List<Item>();
            for (int i = 0; i < 80; i++)
            {
                var x = i % 8 * 90 + 50;
                var y = i / 8 * 90 + 50;

                var item = new Item(x, y);
                scroll.AddChild(item);
                allItems.Add(item);
            }


            scroll.ContentHeight = 700;
        }

        UIButton btnClose = null;
        List<Item> allItems = null;
    }
}

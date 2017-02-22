using AnyGame.Content.Manager;
using AnyGame.Content.Texture;
using AnyGame.UI;
using AnyGame.View.Components;
using DogSE.Library.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace AnyGame.View.Forms.Shop
{
    class CShopChest : UIButton
    {
        public CShopChest(float x, float y)
            : base("ui/common/base_15.png", x, y)
        {
            InitForm();
        }

        void InitForm()
        {
            this.size = new Vector2(190, 340);
            this.border = UIUtils.Border15;

            txtName = new UIText("卡牌", 0, -150, 20, Color.black);
            txtName.alignment = TextAnchor.UpperCenter;
            AddChild(txtName);

            txtArena = new UIText("一阶竞技场", 0, -125, 20, Color.cyan);
            txtArena.alignment = TextAnchor.UpperCenter;
            AddChild(txtArena);

            imgChest_top = new UIImage("ui/kabao/chaoda_00.png", 0, 0, UIUtils.MiddleCenter);
            AddChild(imgChest_top);

            imgChest_bottom = new UIImage("ui/kabao/chaoda_0.png", 0, 0, UIUtils.MiddleCenter);
            AddChild(imgChest_bottom);

            imgChest_top.scale = imgChest_bottom.scale = new Vector3(0.6f, 0.6f, 1f);

            //价格
            int price = 1234;
            var imgprice = new UIImage("ui/icon/icon_diamond.png", -45, 130, UIUtils.MiddleCenter);
            AddChild(imgprice);

            txtPrice = new UIText(price.ToString(), imgprice.x + 40, imgprice.y, 20, Color.black);
            txtPrice.alignment = TextAnchor.MiddleCenter;
            AddChild(txtPrice);
        }

        UIText txtName = null;
        UIText txtArena = null;

        UIImage imgChest_top = null;
        UIImage imgChest_bottom = null;

        UIText txtPrice = null;
    }
}

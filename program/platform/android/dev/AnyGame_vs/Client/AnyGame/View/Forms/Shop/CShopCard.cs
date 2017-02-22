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
    class CShopCard : UIButton
    {
        public CShopCard(float x, float y)
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

            txtQuality = new UIText("普通", 0, -125, 20, Color.cyan);
            txtQuality.alignment = TextAnchor.UpperCenter;
            AddChild(txtQuality);

            imgCard = new UIImage("card/1203.png", 0, 0, UIUtils.MiddleCenter);
            AddChild(imgCard);

            //价格
            int price = 1234;
            var imgprice = new UIImage("ui/icon/icon_gold.png", -45, 130, UIUtils.MiddleCenter);
            AddChild(imgprice);

            txtPrice = new UIText(price.ToString(), imgprice.x + 40, imgprice.y, 20, Color.black);
            txtPrice.alignment = TextAnchor.MiddleCenter;
            AddChild(txtPrice);
        }

        UIText txtName = null;
        UIText txtQuality = null;

        UIImage imgCard = null;

        UIText txtPrice = null;
    }
}

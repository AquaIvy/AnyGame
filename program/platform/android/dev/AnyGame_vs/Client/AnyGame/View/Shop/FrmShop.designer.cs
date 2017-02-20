using AnyGame.Content.Manager;
using AnyGame.Content.Texture;
using AnyGame.UI;
using DogSE.Library.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace AnyGame.View.Shop
{
    partial class FrmShop
    {
        void InitForm()
        {
            SetBackground("ui/common/base_djs.png");
            this.size = new Vector2(GlobalInfo.width, GlobalInfo.height);
            this.border = UIUtils.Border20;

            scroll = new UIScrollView(null, 0, 0, GlobalInfo.width, GlobalInfo.height, Layout.Vertical, null, null, false);
            AddChild(scroll);

            float contentHeight = 110;

            #region 卡牌

            //card banner
            var imgBannerCard = new UIImage("ui/common/base_13.png", GlobalInfo.harfWidth, contentHeight, UIUtils.MiddleCenter);
            scroll.AddChild(imgBannerCard);

            var txtBannerCard = new UIText("卡牌商店", -201, -3, 30, Color.white, TextAnchor.MiddleCenter);
            imgBannerCard.AddChild(txtBannerCard);

            txtCountdown = new UIText("<color=black>新卡牌倒计时</color> 00:00:00", -50, -3, 24, Color.white, TextAnchor.MiddleLeft);
            txtCountdown.supportRichText = true;
            imgBannerCard.AddChild(txtCountdown);

            btnInfo = new UIButton("ui/button/button_in.png", 240, 0);
            imgBannerCard.AddChild(btnInfo);

            //six card
            card = new CShopCard[6];
            for (int i = 0; i < card.Length; i++)
            {
                float x = i % 3 * 210 + 140;
                float y = i / 3 * 360 + 250 + contentHeight;

                card[i] = new CShopCard(x, y);
                scroll.AddChild(card[i]);

            }

            contentHeight += (card.Length / 3) * 360;

            #endregion

            #region 宝箱

            contentHeight += 200;

            //宝箱 banner
            var imgBannerChest = new UIImage("ui/common/base_16.png", GlobalInfo.harfWidth, contentHeight, UIUtils.MiddleCenter);
            scroll.AddChild(imgBannerChest);

            var txtBannerChest = new UIText("宝箱商店", 0, -51.5f, 30, Color.white, TextAnchor.MiddleCenter);
            imgBannerChest.AddChild(txtBannerChest);

            chest = new CShopChest[6];
            for (int i = 0; i < chest.Length; i++)
            {
                float x = i % 3 * 210 + 140;
                float y = i / 3 * 360 + 250 + contentHeight;

                chest[i] = new CShopChest(x, y);
                scroll.AddChild(chest[i]);
            }

            #endregion


            scroll.contentHeight = 3000;
        }

        UIScrollView scroll = null;

        UIText txtCountdown = null;
        UIButton btnInfo = null;
        CShopCard[] card = null;

        CShopChest[] chest = null;
    }
}

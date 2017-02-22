using AnyGame.View.Components;
using AnyGame.View.Forms;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AnyGame.View.Banner
{
    class FrmBanner : FrmBase
    {
        public override FrmType Type { get { return FrmType.Banner; } }

        public FrmBanner()
            : base("FrmBanner")
        {
            name = "FrmBanner";
        }


        private void FrmBanner_OnClick(UIElement sender, EventArgs e)
        {
            int index = (int)sender.tag;

            switch (index)
            {
                case 0:
                    //Game.FrmShop.Show();
                    break;
                case 2:
                    //Game.FrmMain.Show();
                    break;
            }
        }

        protected void InitForm()
        {
            this.size = new Vector2(GlobalInfo.width, GlobalInfo.height);

            float w = 187 / 2f;
            float h = 49 / 2f;
            //float w = 0f;
            //float h = 0f;

            #region 顶部
            //等级
            btnLevel = new UIButton("ui/common/EXP_A_base.png", 120, 35);
            AddChild(btnLevel);

            var imgLvIcon = new UIImage("ui/icon/icon_exp.png", -11 - w, -11 - h);
            btnLevel.AddChild(imgLvIcon);

            txtLevel = new UIText("12", 32, 35, 22, Color.red, TextAnchor.MiddleCenter);
            txtLevel.Center();
            imgLvIcon.AddChild(txtLevel);

            imgExpProcess = new UIImage("ui/common/EXP_A.png", 66.8f - w, 5.5f - h);
            imgExpProcess.image.type = Image.Type.Filled;
            imgExpProcess.image.fillMethod = Image.FillMethod.Horizontal;
            imgExpProcess.image.fillAmount = 0.6f;
            btnLevel.AddChild(imgExpProcess);

            txtExp = new UIText("200/900", 120 - w, 20 - h, 20, Color.white, TextAnchor.MiddleCenter);
            btnLevel.AddChild(txtExp);

            //金币
            btnGold = new UIButton("ui/common/base_04.png", btnLevel.x + 235, btnLevel.y);
            AddChild(btnGold);

            var imgGoldIcon = new UIImage("ui/icon/icon_gold.png", 162.5f - w, 0 - h);
            btnGold.AddChild(imgGoldIcon);

            txtGold = new UIText("10002000", 164 - w, 23 - h, 24, Color.black, TextAnchor.MiddleRight);
            btnGold.AddChild(txtGold);

            btnAddGold = new UIButton("ui/button/button_add.png", 5.6f - w, 27.2f - h);
            btnGold.AddChild(btnAddGold);


            //宝石
            btnGem = new UIButton("ui/common/base_04.png", btnLevel.x + 480, btnLevel.y);
            AddChild(btnGem);

            var imgGemIcon = new UIImage("ui/icon/icon_diamond.png", 162.5f - w, 0 - h);
            btnGem.AddChild(imgGemIcon);

            txtGem = new UIText("10002000", 164 - w, 23 - h, 24, Color.black, TextAnchor.MiddleRight);
            btnGem.AddChild(txtGem);

            btnAddGem = new UIButton("ui/button/button_add.png", 5.6f - w, 27.2f - h);
            btnGem.AddChild(btnAddGem);

            #endregion

            #region 底部

            btnNavi = new UIButton[5];
            string[] iconPath = new string[5] { "mainicon_shop", "mainicon_card", "mainicon_battle", "mainicon_alliance", "mainicon_TV" };
            for (int i = 0; i < btnNavi.Length; i++)
            {
                float x = 110 + i * 125;
                float y = 1230;

                btnNavi[i] = new UIButton("ui/button/mainicon_02.png", x, y);
                btnNavi[i].tag = i;
                AddChild(btnNavi[i]);
                btnNavi[i].OnClick += FrmBanner_OnClick;


                var icon = new UIImage(string.Format("ui/icon/{0}.png", iconPath[i]), 0, 0);
                icon.Center();
                btnNavi[i].AddChild(icon);
            }

            #endregion

        }


        //Level
        UIButton btnLevel = null;
        UIText txtLevel = null;
        UIImage imgExpProcess = null;
        UIText txtExp = null;

        //Gold
        UIButton btnGold = null;
        UIText txtGold = null;
        UIButton btnAddGold = null;

        //Gem
        UIButton btnGem = null;
        UIText txtGem = null;
        UIButton btnAddGem = null;

        UIButton[] btnNavi = null;
    }
}

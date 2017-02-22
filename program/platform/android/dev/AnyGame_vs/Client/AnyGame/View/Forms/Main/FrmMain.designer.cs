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

namespace AnyGame.View.Main
{
    partial class FrmMain
    {
        protected void InitForm()
        {
            SetBackground("ui/common/base_djs.png");
            this.size = new Vector2(GlobalInfo.width, GlobalInfo.height);
            this.border = UIUtils.Border20;

            #region 玩家信息

            float w = 470 / 2f;
            float h = 50 / 2f;

            imgInfoBG = new UIImage("ui/common/base_17.png", GlobalInfo.harfWidth, 110, UIUtils.MiddleCenter);
            imgInfoBG.size = new Vector2(470, 50);
            imgInfoBG.border = UIUtils.Border15;
            AddChild(imgInfoBG);

            imgClan = new UIImage("ui/icon/icon_bage_none.png", 30 - w, imgInfoBG.harfHeight - h, UIUtils.MiddleCenter);
            imgInfoBG.AddChild(imgClan);

            //玩家姓名
            txtName = new UIText("玩家姓名", 60 - w, 5 - h, 20, Color.white);
            imgInfoBG.AddChild(txtName);

            txtClanName = new UIText("联盟", txtName.x, txtName.y + 23, 18, Color.white);
            imgInfoBG.AddChild(txtClanName);

            //奖杯
            var imgTrophyBG = new UIImage("ui/common/cjjdt_db.png", 400 - w, imgInfoBG.harfHeight - h, UIUtils.MiddleCenter);
            imgTrophyBG.border = UIUtils.Border10;
            imgTrophyBG.size = new Vector2(130, 38);
            imgInfoBG.AddChild(imgTrophyBG);

            var imgTrophyIcon = new UIImage("ui/icon/icon_04.png", -30, 0, UIUtils.MiddleCenter);
            imgTrophyBG.AddChild(imgTrophyIcon);

            txtTrophy = new UIText("500", 4, -10, 24, Color.yellow);
            imgTrophyBG.AddChild(txtTrophy);

            //头像
            btnPlayer = new UIButton("ui/button/button_playyer.png", -38 - w, imgInfoBG.harfHeight - h);
            imgInfoBG.AddChild(btnPlayer);

            //设置
            btnSetting = new UIButton("ui/button/button_setting.png", imgInfoBG.width + 40 - w, imgInfoBG.harfHeight - h);
            imgInfoBG.AddChild(btnSetting);


            #endregion


            #region 排行版、邮件、成就、训练场

            int dis = 100;
            btnRank = new UIButton("ui/icon/icon_01.png", 270 + dis * 0, 240);
            AddChild(btnRank);

            btnEmail = new UIButton("ui/icon/icon_02.png", 270 + dis * 1, btnRank.y);
            AddChild(btnEmail);

            btnAchieve = new UIButton("ui/icon/icon_03.png", 270 + dis * 2, btnRank.y);
            AddChild(btnAchieve);

            btnTrain = new UIButton("ui/icon/icon_04.png", 270 + dis * 3, btnRank.y);
            AddChild(btnTrain);

            btnFight = new UIButton("ui/button/button_09.png", GlobalInfo.harfWidth, GlobalInfo.height - 300);
            AddChild(btnFight);
            btnFight.OnClick += BtnFight_OnClick;

            var gem = new UIImage("ui/icon/icon_diamond.png", 0, 0);
            gem.Center();
            btnFight.AddChild(gem);

            #endregion


            #region 转盘

            #endregion

            #region 战斗宝箱

            #endregion

        }

        //玩家信息
        UIImage imgInfoBG = null;
        UIImage imgClan = null;
        UIText txtName = null;
        UIText txtClanName = null;
        UIText txtTrophy = null;

        UIButton btnPlayer = null;
        UIButton btnSetting = null;


        //排行版、邮件、成就、训练场
        UIButton btnRank = null;
        UIButton btnEmail = null;
        UIButton btnAchieve = null;
        UIButton btnTrain = null;

        UIButton btnFight = null;

    }
}

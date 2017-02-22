﻿using AnyGame.LoginPlugin;
using AnyGame.UI;
using DogSE.Client.Core;
using DogSE.Library.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeAge.Client.Controller.Login;
using TradeAge.Client.Entity.Character;
using TradeAge.Client.Entity.Login;
using UnityEngine;

namespace AnyGame.View.Login
{
    class FrmCreateCharacter : FrmBase
    {
        public override FrmType Type { get { return FrmType.Background; } }

        public FrmCreateCharacter() : base("FrmCreateCharacter")
        {
            InitForm();

            GameCenter.Controller.Login.CreatePlayerRet += OnCreatePlayerRet;

            btnCreate.OnClick += BtnCreate_OnClick;
        }

        private void OnCreatePlayerRet(CraetePlayerResult result)
        {
            if (result == CraetePlayerResult.Success)
            {
                Logs.Info("创建角色成功");
            }
            else
            {
                Logs.Error("创建角色失败 {0}", result.ToString());
            }

        }

        protected override void OnClosed()
        {
            GameCenter.Controller.Login.CreatePlayerRet -= OnCreatePlayerRet;

            base.OnClosed();
        }


        private void BtnCreate_OnClick(UIElement sender, EventArgs e)
        {
            var name = inputCharacterName.text;
            if (name.Trim().Length > 10)
            {
                return;
            }

            if (name.Trim().Length <= 0)
            {
                return;
            }

            Logs.Info("请求创建角色 {0}", name);
            GameCenter.Controller.Login.CreatePlayer(name, Sex.Male);
        }

        private void InitForm()
        {
            var characters = new UIButton[3];
            for (int i = 0; i < characters.Length; i++)
            {
                characters[i] = new UIButton("card/" + (1204 + i) + ".png", 200 + 150 * i, 400);
                AddChild(characters[i]);
            }

            inputCharacterName = new UIInputField("ui/common/base_01.png", 360, 850);
            inputCharacterName.pivot = UIUtils.MiddleCenter;
            inputCharacterName.border = UIUtils.Border10;
            inputCharacterName.size = new Vector2(270, 65);

            inputCharacterName.textContent.Center();
            inputCharacterName.textContent.alignment = TextAnchor.MiddleCenter;
            inputCharacterName.textContent.SetXY(0, 0);
            inputCharacterName.textContent.fontsize = 35;

            AddChild(inputCharacterName);

            btnCreate = new UIButton("ui/button/queding.png", 360, 1020);
            AddChild(btnCreate);
        }

        UIInputField inputCharacterName = null;
        UIButton btnCreate = null;
    }
}
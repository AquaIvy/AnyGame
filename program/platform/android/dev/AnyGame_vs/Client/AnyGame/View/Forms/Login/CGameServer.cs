﻿using AnyGame.LoginPlugin;
using AnyGame.View;
using AnyGame.View.Components;
using UnityEngine;

namespace AnyGame.View.Forms.Login
{
    class CGameServer : UIButton
    {
        GameServer data = null;

        public CGameServer(GameServer data, float x, float y)
            : base("aztec/common/base_01.png", x, y, UIUtils.MiddleCenter, UIUtils.Border10, null, 0)
        {
            this.data = data;

            InitForm();
        }

        public void SetData(GameServer data)
        {
            this.data = data;

            txtServerName.text = data.Name;
            txtCharacterName.text = data.CharacterName;
            txtServerState.text = data.Status.ToString();
        }


        private void InitForm()
        {
            this.Size = new Vector2(250, 50);

            txtServerName = new UIText(data.Name, 10, 15, 20, Color.white);
            AddChild(txtServerName);
            txtServerName.anchorMin = txtServerName.anchorMax = UIUtils.UpperLeft;

            txtCharacterName = new UIText(data.CharacterName, 105, txtServerName.y, 20, Color.white);
            AddChild(txtCharacterName);
            txtCharacterName.anchorMin = txtCharacterName.anchorMax = UIUtils.UpperLeft;

            txtServerState = new UIText(data.Status.ToString(), 190, txtServerName.y, 20, Color.white);
            AddChild(txtServerState);
            txtServerState.anchorMin = txtServerState.anchorMax = UIUtils.UpperLeft;
        }

        UIText txtServerName = null;
        UIText txtCharacterName = null;
        UIText txtServerState = null;
    }
}

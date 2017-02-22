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

namespace AnyGame.View.Forms.Main
{
    partial class FrmMain : FrmBase
    {
        public override FrmType Type { get { return FrmType.Background; } }

        public FrmMain()
            : base("FrmMain")
        {
        }



        private void BtnFight_OnClick(UIElement sender, EventArgs e)
        {
            Game.FrmLogin.Show();
        }

    }
}

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
    /// <summary>
    /// 主界面UI
    /// </summary>
    partial class FrmMain : FrmBase
    {
        public override FrmType Type { get { return FrmType.Background; } }

        public FrmMain()
            : base("FrmMain")
        {
            InitForm();

            btnBag.OnClick += BtnBag_OnClick;
        }

        private void BtnBag_OnClick(UIElement sender, EventArgs e)
        {
            Logs.Error("i am click");
        }
    }
}

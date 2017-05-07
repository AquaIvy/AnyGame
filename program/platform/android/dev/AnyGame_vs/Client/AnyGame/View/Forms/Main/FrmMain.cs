using AnyGame.Content.Manager;
using AnyGame.Content.Texture;
using AnyGame.View;
using AnyGame.View.Components;
using AnyGame.View.Forms.Bags;
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
        public override FrmType Type { get { return FrmType.Main; } }
        public override FrmLayer Layer { get { return FrmLayer.Background; } }

        public FrmMain()
            : base("FrmMain")
        {
            InitForm();

            btnBag.OnClick += BtnBag_OnClick;
        }

        private void BtnBag_OnClick(UIElement sender, EventArgs e)
        {
            var bag = new FrmBag();
            Game.FrmBag = bag;
            UIRoot.Show(bag);
        }

        protected override void Update(int milliseconds)
        {
            base.Update(milliseconds);

            //if (Game.FrmBag == null)
            //{
            //    BtnBag_OnClick(this, null);
            //}
            //else
            //{
            //    Game.FrmBag.Close();
            //    //Game.FrmBag = null;
            //}
        }

    }
}

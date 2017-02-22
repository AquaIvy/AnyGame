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

namespace AnyGame.View.Forms.Shop
{
    partial class FrmShop : FrmBase
    {
        public override FrmType Type { get { return FrmType.Background; } }

        public FrmShop() : base("FrmShop")
        {
            InitForm();



        }

        protected override void OnClosed()
        {
            Game.FrmShop = null;

            base.OnClosed();
        }

    }
}

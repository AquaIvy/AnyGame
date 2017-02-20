using AnyGame.Content.Manager;
using AnyGame.Global;
using AnyGame.View;
using DogSE.Library.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace RoyalWar.View.Notice
{
    public class FrmBigScene : FrmBase
    {
        private GameObject btnClose = null;
        public override FormType winType { get { return FormType.BigScene; } }
        public override FormLayer winLayer { get { return FormLayer.Modal; } }

        public override string[] UsingAtlas
        {
            get { return new string[] { }; }
        }

        public FrmBigScene(Game game) : base(game)
        {

        }

        protected override void BindingEvents(Action onShowed = null)
        {
            btnClose = CanvasMgr.FindControl(CanvasMgr.GetPathMap(winType.ToString(), "btnClose"));
            btnClose.GetComponent<Button>().onClick.RemoveAllListeners();
            btnClose.GetComponent<Button>().onClick.AddListener(() =>
            {
                //this.Hide();
                this.Close();
            });

            base.BindingEvents(onShowed);
        }
    }
}

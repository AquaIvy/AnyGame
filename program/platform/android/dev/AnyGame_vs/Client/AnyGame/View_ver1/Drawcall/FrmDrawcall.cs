using AnyGame.Content.Manager;
using AnyGame.Global;
using AnyGame.View;
using DogSE.Library.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace RoyalWar.View.Drawcall
{
    public class FrmDrawcall : FrmBase
    {
        private GameObject btnClose = null;

        public override FormType winType { get { return FormType.Drawcall; } }
        public FrmDrawcall(Game game) : base(game)
        {

        }

        public override void Show(Action onShowed = null)
        {
            if (IsVisible)
            {
                Logs.Info("{0} is showing ", winType.ToString());
                return;
            }

            if (IsCreated)
            {
                base.Show(onShowed);
                return;
            }

            IsCreated = true;

            Game.StartCoroutine(SceneMgr.CreateUI(winType.ToString(),
                CanvasMgr.Control,
                CanvasMgr.basicControl + "/",
                (cur, max) =>
                {
                    Game.FrmFadeInOut.SetSliderValue();
                },
                BindingEvents,
                onShowed));
        }

        private void BindingEvents(Action onShowed = null)
        {
            //屏幕适应
            var canvasscale = CanvasMgr.FindControl("Drawcall");
            if (canvasscale != null)
            {
                var rt = canvasscale.GetComponent<RectTransform>();
                rt.sizeDelta = GlobalInfo.CanvasParentRect;
            }

            btnClose = CanvasMgr.FindControl(CanvasMgr.GetPathMap(winType.ToString(), "btnClose"));
            btnClose.GetComponent<Button>().onClick.RemoveAllListeners();
            btnClose.GetComponent<Button>().onClick.AddListener(() =>
            {
                var uiconfig = JsonMgr.LoadUI(FormType.Main.ToString());
                Game.FrmFadeInOut.SetTotalValue(uiconfig.Count);

                Game.FrmFadeInOut.Show(() =>
                {
                    Game.DisposeAllWin();
                    Game.FrmMain.Show();
                });
            });

            self = GameObject.Find(CanvasMgr.basicControl + "/" + winType);
            base.Show(onShowed);

        }

        public override void Close(Action onClosed = null)
        {
            base.Close(onClosed);
        }


        public override void Dispose(Action onDisposed = null)
        {
            base.Dispose();
        }
    }
}

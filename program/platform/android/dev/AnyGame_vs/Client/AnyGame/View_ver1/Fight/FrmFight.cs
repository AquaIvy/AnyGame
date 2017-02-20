using AnyGame.Content.Manager;
using AnyGame.View;
using DogSE.Library.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace RoyalWar.View.Fight
{
    public class FrmFight : FrmBase
    {
        private GameObject btnReturn = null;

        public override FormType winType { get { return FormType.Fight; } }
        public FrmFight(Game game) : base(game)
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
            btnReturn = CanvasMgr.FindControl(CanvasMgr.GetPathMap(winType.ToString(), "btnReturn"));
            btnReturn.GetComponent<Button>().onClick.RemoveAllListeners();
            btnReturn.GetComponent<Button>().onClick.AddListener(() =>
            {
                SceneMgr.SkipSceneSync("MainScene");
            });

            CreateHero();

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

        public void CreateHero()
        {
            var parent = GameObject.Find("/GameWorld").transform;

            Game.StartCoroutine(SceneMgr.CreateGameWorld("Fight_UI",
                parent,
                "/GameWorld/",
                (cur, max) =>
                {
                    Game.FrmFadeInOut.SetSliderValue();
                },
                null,
                null));
        }
    }
}

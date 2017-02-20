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

namespace RoyalWar.View.Main
{
    public class FrmMain : FrmBase
    {
        private GameObject btnStartFight = null;

        private GameObject btnShop = null;
        private GameObject btnCard = null;
        private GameObject btnFight = null;
        private GameObject btnTribe = null;
        private GameObject btnTV = null;

        //主滑动
        private GameObject mainScrollView = null;       //物体
        public ScrollRect mainScrollRect = null;        //显示区
        public Scrollbar mainScrollbar = null;          //横向滑块

        //Vertical滑动
        private GameObject verticalScrollView = null;       //物体
        public ScrollRect verticalScrollRect = null;        //显示区
        public Scrollbar verticalScrollbar = null;          //横向滑块

        public ScrollbarMgr scrollbarMgr;

        public override FormType winType { get { return FormType.Main; } }
        public FrmMain(Game game) : base(game)
        {
            Game.inputMgr.OnTouchFirstMoved += InputMgr_OnTouchFirstMoved;
            Game.inputMgr.OnTouchMoved += InputMgr_OnTouchMoved;
            Game.inputMgr.OnTouchEnd += InputMgr_OnTouchEnd;
        }


        private void InputMgr_OnTouchFirstMoved(object sender, TouchFirstMovedEventArgs e)
        {
            //Logs.Warn("first move " + e.moveDirection);

            if (e.moveDirection == MoveDirection.Horizontal)
            {
                mainScrollRect.horizontal = true;
                verticalScrollRect.vertical = false;
            }
            else if (e.moveDirection == MoveDirection.Vertical)
            {
                mainScrollRect.horizontal = false;
                verticalScrollRect.vertical = true;

                scrollbarMgr.MoveCancel();
            }
        }
        private void InputMgr_OnTouchMoved(object sender, TouchMovedEventArgs e)
        {
            //Logs.Warn("move " + e.moveDistance);

            if (e.moveDirection == MoveDirection.Horizontal)
            {
                scrollbarMgr.scrollbar.value -= e.deltaPosition.x * 0.001f;
            }
        }
        private void InputMgr_OnTouchEnd(object sender, TouchEndEventArgs e)
        {
            //Logs.Warn("end move " + e.moveDistance);

            mainScrollRect.horizontal = true;
            verticalScrollRect.vertical = true;

            float x = e.moveDistance.x;
            float y = e.moveDistance.y;

            if (e.moveDirection == MoveDirection.Horizontal)
            {
                //横向移动大于5，开始滑动
                if (/*(x / y) > (3f / 2) && */Mathf.Abs(x) > 5)
                {
                    if (x > 0)
                    {
                        scrollbarMgr.MoveLeft();
                    }
                    else
                    {
                        scrollbarMgr.MoveRight();
                    }
                }
                //横向移动小于5，滑回去
                else if (/*(x / y) > (3f / 2) &&*/ Mathf.Abs(x) <= 5)
                {
                    scrollbarMgr.MoveCancel();
                }
            }
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
            var canvasscale = CanvasMgr.FindControl("Main");
            if (canvasscale != null)
            {
                var rt = canvasscale.GetComponent<RectTransform>();
                rt.sizeDelta = GlobalInfo.CanvasParentRect;
            }

            //在主滑块的物体上   添加 mgr
            var mainScrollbarGO = CanvasMgr.FindControl(CanvasMgr.GetPathMap(winType.ToString(), "mainScrollbar"));
            if (mainScrollbarGO != null)
            {
                scrollbarMgr = mainScrollbarGO.AddComponent<ScrollbarMgr>();
                scrollbarMgr.ContentCount = 5;
                //scrollbarMgr.CurContentIndex = 0;
                //scrollbarMgr.MoveCancel();
            }

            var mainScrollViewGO = CanvasMgr.FindControl(CanvasMgr.GetPathMap(winType.ToString(), "mainScrollView"));
            if (mainScrollViewGO != null)
            {
                mainScrollRect = mainScrollViewGO.GetComponent<ScrollRect>();
            }

            var verticalScrollViewGO = CanvasMgr.FindControl(CanvasMgr.GetPathMap(winType.ToString(), "verticalScrollView"));
            if (verticalScrollViewGO != null)
            {
                verticalScrollRect = verticalScrollViewGO.GetComponent<ScrollRect>();
            }

            //mainScrollRect.horizontal = false;
            //verticalScrollRect.vertical = false;

            btnStartFight = CanvasMgr.FindControl(CanvasMgr.GetPathMap(winType.ToString(), "btnStartFight"));
            btnStartFight.GetComponent<Button>().onClick.RemoveAllListeners();
            btnStartFight.GetComponent<Button>().onClick.AddListener(() =>
            {
                //var uiconfig = JsonMgr.LoadUI(FormType.CreateCharacter.ToString());
                //Game.FrmFadeInOut.SetTotalValue(uiconfig.Count);

                //Game.FrmFadeInOut.Show(() =>
                //{
                //    Dispose();
                //    Game.FrmCreateCharacter.Show();
                //    //Game.FrmBag.Show();
                //});

                SceneMgr.SkipSceneSync("FightScene");
            });

            btnShop = CanvasMgr.FindControl(CanvasMgr.GetPathMap(winType.ToString(), "btnShop"));
            btnShop.GetComponent<Button>().onClick.RemoveAllListeners();
            btnShop.GetComponent<Button>().onClick.AddListener(() =>
            {
                //SceneMgr.SkipSceneSync("3D_Fight_Scene");

                Logs.Warn("dataPath  " + Application.dataPath);
                Logs.Warn("streamingAssetsPath  " + Application.streamingAssetsPath);
                Logs.Warn(" jar:file  " + "jar:file://" + Application.dataPath + "!/assets/");

                //string p = Application.streamingAssetsPath + "/matchlist.txt";
                //Logs.Warn("p " + File.Exists(p));

                string p2 = "jar:file://" + Application.dataPath + "!/assets/";
                var files = Directory.GetFiles(p2);
                Logs.Warn("count " + files.Length);
                foreach (var item in files)
                {
                    Logs.Warn(item);
                }


            });

            btnCard = CanvasMgr.FindControl(CanvasMgr.GetPathMap(winType.ToString(), "btnCard"));
            btnCard.GetComponent<Button>().onClick.RemoveAllListeners();
            btnCard.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (scrollbarMgr != null)
                {
                    scrollbarMgr.MoveLeft();
                    //scrollbarMgr.scrollbar.value -= 0.1f;
                }
            });

            btnFight = CanvasMgr.FindControl(CanvasMgr.GetPathMap(winType.ToString(), "btnFight"));
            btnFight.GetComponent<Button>().onClick.RemoveAllListeners();
            btnFight.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (scrollbarMgr != null)
                {
                    scrollbarMgr.MoveRight();
                    //scrollbarMgr.scrollbar.value += 0.1f;
                }
            });

            btnTribe = CanvasMgr.FindControl(CanvasMgr.GetPathMap(winType.ToString(), "btnTribe"));
            btnTribe.GetComponent<Button>().onClick.RemoveAllListeners();
            btnTribe.GetComponent<Button>().onClick.AddListener(() =>
            {
                Game.DisposeAllWin();

                SpriteMgr.allAtlas.Clear();
                Resources.UnloadUnusedAssets();
            });

            btnTV = CanvasMgr.FindControl(CanvasMgr.GetPathMap(winType.ToString(), "btnTV"));
            btnTV.GetComponent<Button>().onClick.RemoveAllListeners();
            btnTV.GetComponent<Button>().onClick.AddListener(() =>
            {
                var uiconfig = JsonMgr.LoadUI(FormType.Drawcall.ToString());
                Game.FrmFadeInOut.SetTotalValue(uiconfig.Count);

                Game.FrmFadeInOut.Show(() =>
                {
                    Game.DisposeAllWin();
                    Game.FrmDrawcall.Show();
                    //Game.StartCoroutine(CanvasMgr.Clear(CanvasMgr.Control,
                    //    () =>
                    //    {
                    //        Game.FrmDrawcall.Show();
                    //    })
                    //);
                });
            });

            mainScrollView = CanvasMgr.FindControl("Main/Scroll View");
            if (mainScrollView != null)
            {
                var mainScrollRect = mainScrollView.GetComponent<ScrollRect>();
                mainScrollRect.elasticity = 0.1f;
                mainScrollRect.decelerationRate = 0.3f;
                //scrollrect.scrollSensitivity = 0f;

                Logs.Warn("setting is ok");
            }
            else
            {
                Logs.Warn("why no");
            }

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

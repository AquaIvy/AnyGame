using AnyGame.Content.Manager;
using AnyGame.Global;
using AnyGame.View;
using DG.Tweening;
using DogSE.Library.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AnyGame.View.Main
{
    public class FrmMain : FrmBase
    {
        private GameObject btnStartFight = null;

        //主滑动
        public ScrollRect mainScrollRect = null;        //显示区

        //Vertical 显示区        
        public ScrollRect shopScrollRect = null;        //显示区
        public ScrollRect cardScrollRect = null;        //显示区

        public ScrollbarMgr MainScrollbarMgr;




        ///////////////////////////////////////////////////////////
        private GameObject btnAddCoin = null;
        private GameObject btnAddGem = null;

        private GameObject btnShop = null;
        private GameObject btnCard = null;
        private GameObject btnMain = null;
        private GameObject btnTribe = null;
        private GameObject btnTV = null;

        ///////////////////////////////////////////////////////////




        public override FormType winType { get { return FormType.Main; } }
        public override FormLayer winLayer { get { return FormLayer.FillScreen; } }
        public override string[] UsingAtlas
        {
            get { return new string[] { "Material", "UI" }; }
        }

        public FrmMain(Game game) : base(game)
        {
            Game.inputMgr.OnTouchBegin += InputMgr_OnTouchBegin;
            Game.inputMgr.OnTouchFirstMoved += InputMgr_OnTouchFirstMoved;
            Game.inputMgr.OnTouchMoved += InputMgr_OnTouchMoved;
            Game.inputMgr.OnTouchEnd += InputMgr_OnTouchEnd;


            top_touch_area = Screen.height - 70 * (Screen.height / GlobalInfo.CanvasDevHeight);
            bottom_touch_area = 100 * (Screen.height / GlobalInfo.CanvasDevHeight);
        }

        bool errorTouchArea = false;
        float top_touch_area = 1280 - 70;
        float bottom_touch_area = 100;
        private void InputMgr_OnTouchBegin(object sender, TouchBeginEventArgs e)
        {
            if (e.position.y > top_touch_area
                || e.position.y < bottom_touch_area)
            {
                errorTouchArea = true;
            }

            Logs.Info("touch " + top_touch_area + "  " + bottom_touch_area + "  " + e.position.y + "  " + errorTouchArea);
        }

        private void InputMgr_OnTouchFirstMoved(object sender, TouchFirstMovedEventArgs e)
        {
            //Logs.Warn("first move " + e.moveDirection);
            if (errorTouchArea) { return; }

            MainScrollbarMgr.isTouching = true;

            if (e.moveDirection == MoveDirection.Horizontal)
            {
                mainScrollRect.horizontal = true;

                shopScrollRect.vertical = false;
                cardScrollRect.vertical = false;
            }
            else if (e.moveDirection == MoveDirection.Vertical)
            {
                mainScrollRect.horizontal = false;

                cardScrollRect.vertical = true;
                shopScrollRect.vertical = true;

                MainScrollbarMgr.MoveCancel();
            }
        }
        private void InputMgr_OnTouchMoved(object sender, TouchMovedEventArgs e)
        {
            //Logs.Warn("move " + e.moveDistance);
            if (errorTouchArea) { return; }

            if (e.moveDirection == MoveDirection.Horizontal)
            {
                MainScrollbarMgr.scrollbar.value -= e.deltaPosition.x * 0.001f;
            }
        }
        private void InputMgr_OnTouchEnd(object sender, TouchEndEventArgs e)
        {
            //Logs.Warn("end move " + e.moveDistance);
            if (errorTouchArea)
            {
                errorTouchArea = false;
                return;
            }

            MainScrollbarMgr.isTouching = false;

            mainScrollRect.horizontal = true;
            shopScrollRect.vertical = true;
            cardScrollRect.vertical = true;

            float x = e.moveDistance.x;
            float y = e.moveDistance.y;

            if (e.moveDirection == MoveDirection.Horizontal)
            {
                //横向移动大于5，开始滑动
                if (Mathf.Abs(x) > 5)
                {
                    if (x > 0)
                    {
                        MainScrollbarMgr.MoveLeft();
                    }
                    else
                    {
                        MainScrollbarMgr.MoveRight();
                    }
                }
                //横向移动小于5，滑回去
                else if (Mathf.Abs(x) <= 5)
                {
                    MainScrollbarMgr.MoveCancel();
                }
            }
        }


        protected override void BindingEvents(Action onShowed = null)
        {
            //屏幕适应
            var bg_scale = CanvasMgr.FindControl("Main");
            if (bg_scale != null)
            {
                var rt = bg_scale.GetComponent<RectTransform>();
                rt.sizeDelta = GlobalInfo.CanvasParentRect;
            }

            btnAddCoin = BindingButton(btnAddCoin, "btnAddCoin", () =>
            {
            });

            btnAddGem = BindingButton(btnAddGem, "btnAddGem", () =>
            {
            });



            //主   滑块   添加 mgr
            var mainScrollbarGO = CanvasMgr.FindControl(CanvasMgr.GetPathMap(winType.ToString(), "MainScrollbar"));
            if (mainScrollbarGO != null)
            {
                MainScrollbarMgr = mainScrollbarGO.AddComponent<ScrollbarMgr>();
                MainScrollbarMgr.scrollbar = mainScrollbarGO.GetComponent<Scrollbar>();
                MainScrollbarMgr.ContentCount = 5;
                MainScrollbarMgr.MoveSet(2);
            }

            #region 中间5个纵向区域

            //主  横向滑动
            var mainScrollView_GO = CanvasMgr.FindControl(CanvasMgr.GetPathMap(winType.ToString(), "MainScrollView"));
            if (mainScrollView_GO != null)
            {
                mainScrollRect = mainScrollView_GO.GetComponent<ScrollRect>();
                mainScrollRect.elasticity = 0.1f;
                mainScrollRect.decelerationRate = 0.3f;
            }

            var shopScrollView_GO = CanvasMgr.FindControl(CanvasMgr.GetPathMap(winType.ToString(), "ShopVerticalView"));
            if (shopScrollView_GO != null)
            {
                shopScrollRect = shopScrollView_GO.GetComponent<ScrollRect>();

            }

            var cardScrollView_GO = CanvasMgr.FindControl(CanvasMgr.GetPathMap(winType.ToString(), "CardVerticalView"));
            if (cardScrollView_GO != null)
            {
                cardScrollRect = cardScrollView_GO.GetComponent<ScrollRect>();
            }
            #endregion

            #region 底部5个导航按钮

            btnShop = BindingButton(btnShop, "btnShop", () =>
                 {
                     MainScrollbarMgr.Move(0);
                 });

            btnCard = BindingButton(btnCard, "btnCard", () =>
                 {
                     MainScrollbarMgr.Move(1);
                 });


            btnMain = BindingButton(btnMain, "btnMain", () =>
                 {
                     MainScrollbarMgr.Move(2);
                 });


            btnTribe = BindingButton(btnTribe, "btnTribe", () =>
                 {
                     MainScrollbarMgr.Move(3);
                 });


            btnTV = BindingButton(btnTV, "btnTV", () =>
                 {
                     //MainScrollbarMgr.Move(4);
                     FiveNaviBtnAnim();
                 });

            #endregion

            base.BindingEvents(onShowed);

        }

        private void AddEventTrigger(GameObject go)
        {
            var listener = go.GetComponent<EventTrigger>();
            if (listener == null)
                listener = go.AddComponent<EventTrigger>();

            listener.triggers = new List<EventTrigger.Entry>();

            //设置滑动事件监听
            EventTrigger.Entry onMove = new EventTrigger.Entry();
            onMove.eventID = EventTriggerType.Drag;
            onMove.callback = new EventTrigger.TriggerEvent();
            UnityAction<BaseEventData> downcallback = new UnityAction<BaseEventData>((data) =>
            {
                Logs.Info("data " + data);
            });

            onMove.callback.AddListener(downcallback);


            listener.triggers.Add(onMove);
        }

        protected override void OnShowed()
        {
            base.OnShowed();

            FiveNaviBtnAnim();
        }

        private void FiveNaviBtnAnim()
        {
            var rtShop = btnShop.GetComponent<RectTransform>();
            var rtCard = btnCard.GetComponent<RectTransform>();
            var rtMain = btnMain.GetComponent<RectTransform>();
            var rtTribe = btnTribe.GetComponent<RectTransform>();
            var rtTV = btnTV.GetComponent<RectTransform>();

            var startPos = new Vector3(-300f, 0f, 0f);
            float duration = 1.0f;

            rtShop.localPosition = startPos;
            rtCard.localPosition = startPos;
            rtMain.localPosition = startPos;
            rtTribe.localPosition = startPos;
            rtTV.localPosition = startPos;

            Tweener xShop = rtShop.DOLocalMoveX(-248, duration);
            Tweener xCard = rtCard.DOLocalMoveX(-146, duration);
            Tweener xMain = rtMain.DOLocalMoveX(0, duration);
            Tweener xTribe = rtTribe.DOLocalMoveX(146, duration);
            Tweener xTV = rtTV.DOLocalMoveX(248, duration);

            Ease ease = Ease.OutCirc;
            xShop.SetEase(ease);
            xCard.SetEase(ease);
            xMain.SetEase(ease);
            xTribe.SetEase(ease);
            xTV.SetEase(ease);
        }

        protected override void OnClosed()
        {
            base.OnClosed();
        }
    }
}

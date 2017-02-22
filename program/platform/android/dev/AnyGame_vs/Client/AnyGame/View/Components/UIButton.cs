using AnyGame.Content.Manager;
using AnyGame.Content.Texture;
using AnyGame.UI;
using DG.Tweening;
using DogSE.Library.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AnyGame.View.Components
{
    class UIButton : UIImageBase
    {
        private static string gameObjectName = "Button";

        public Button button { get; set; }
        public UIText textComponent { get; set; }

        public event UIEvent OnClick;            //点击触发

        public event UIEvent OnBeginDown;        //刚刚接触上触发
        public event UIEvent OnEndDown;          //按到底触发

        public event UIEvent OnBeginUp;          //刚刚抬起触发
        public event UIEvent OnEndUp;            //抬起到默认状态触发

        public event UIEvent OnPress;            //按住触发一次（未实现）
        public event UIEvent OnPressUpdate;      //按住拼命触发（未实现）

        private bool isClickScale = true;


        private static readonly Vector3 minScale = new Vector3(0.95f, 0.95f, 1f);
        private Tweener tweenDown = null;
        private Tweener tweenUp = null;

        /// <summary>
        /// 图片中心为原点，不使用文字（textComponent组件必定为null）
        /// </summary>
        /// <param name="imgPath"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public UIButton(string imgPath, float x, float y)
           : this(imgPath, x, y, UIUtils.MiddleCenter, Vector4.zero, string.Empty, 20, true)
        {

        }

        /// <summary>
        /// 图片中心为原点
        /// </summary>
        /// <param name="imgPath"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public UIButton(string imgPath, float x, float y, string text, int fontsize)
           : this(imgPath, x, y, UIUtils.MiddleCenter, Vector4.zero, text, fontsize, true)
        {

        }

        public UIButton(string imgPath, float x, float y, Vector2 pivot, Vector4 border, string text, int fontsize)
           : this(imgPath, x, y, pivot, border, text, fontsize, true)
        {

        }

        public UIButton(string imgPath, float x, float y, Vector2 pivot, Vector4 border, string text, int fontsize, bool isClickScale)
            : base(imgPath, x, y, pivot, border)
        {
            this.isClickScale = isClickScale;

            name = gameObjectName;

            button = go.AddComponent<Button>();

            if (!string.IsNullOrEmpty(text))
            {
                this.textComponent = new UIText(text, 0, 0, fontsize, Color.white);
                this.textComponent.alignment = TextAnchor.MiddleCenter;
                this.textComponent.Center();
                AddChild(this.textComponent);
            }

            this.anchorMax = this.anchorMin = UIUtils.UpperLeft;
            this.pivot = pivot;
            SetXY(x, y);

            button.onClick.AddListener(() =>
            {
                OnClick?.Invoke(this, null);
            });

            //if (isClickScale)
            //{
            //    var listener = go.GetComponent<EventTrigger>();
            //    if (listener == null)
            //        listener = go.AddComponent<EventTrigger>();

            //    listener.triggers = new List<EventTrigger.Entry>();

            //    //设置按下动画
            //    EventTrigger.Entry onDown = new EventTrigger.Entry();
            //    onDown.eventID = EventTriggerType.PointerDown;
            //    onDown.callback = new EventTrigger.TriggerEvent();
            //    UnityAction<BaseEventData> downcallback = new UnityAction<BaseEventData>(OnButtonDown);
            //    onDown.callback.AddListener(downcallback);

            //    //设置抬起动画
            //    EventTrigger.Entry onUp = new EventTrigger.Entry();
            //    onUp.eventID = EventTriggerType.PointerUp;
            //    onUp.callback = new EventTrigger.TriggerEvent();
            //    UnityAction<BaseEventData> upcallback = new UnityAction<BaseEventData>(OnButtonUp);
            //    onUp.callback.AddListener(upcallback);

            //    listener.triggers.Add(onDown);
            //    listener.triggers.Add(onUp);
            //}
        }


        public override bool enable
        {
            get { return button.enabled; }
            set { button.enabled = value; }
        }

        private void OnButtonDown(BaseEventData eventData)
        {
            ResetTweenScale();

            OnBeginDown?.Invoke(this, null);

            tweenDown = eventData.selectedObject.GetComponent<RectTransform>().DOScale(minScale, 0.15f);
            tweenDown.SetEase(Ease.InOutBack);
            tweenDown.OnStepComplete(() =>
            {
                OnEndDown?.Invoke(this, null);
            });
        }

        private void OnButtonUp(BaseEventData eventData)
        {
            ResetTweenScale();

            OnBeginUp?.Invoke(this, null);

            tweenUp = eventData.selectedObject.GetComponent<RectTransform>().DOScale(Vector3.one, 0.15f);
            tweenUp.SetEase(Ease.InOutBack);
            tweenUp.OnStepComplete(() =>
            {
                OnEndUp?.Invoke(this, null);
            });
        }

        private void ResetTweenScale()
        {
            tweenDown?.Kill(false);
            tweenUp?.Kill(false);
        }

    }
}

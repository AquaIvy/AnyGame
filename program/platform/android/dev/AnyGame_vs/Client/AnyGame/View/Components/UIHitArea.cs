using AnyGame.Content.Manager;
using AnyGame.Content.Texture;
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

namespace AnyGame.UI
{
    /// <summary>
    /// 透明点击区
    /// </summary>
    class UIHitArea : UIImageBase
    {
        public event UIEvent OnClick;


        public UIHitArea(float x, float y, float width, float height, bool isShowArea = false)
            : base(null, x, y, UIUtils.UpperLeft, Vector4.zero)
        {
            name = "hitarea";

            if (!isShowArea)
            {
                image.color = new Color(0, 0, 0, 0);
            }


            size = new Vector2(width, height);

            SetXY(x, y);

            var listener = go.GetComponent<EventTrigger>();
            if (listener == null)
                listener = go.AddComponent<EventTrigger>();

            listener.triggers = new List<EventTrigger.Entry>();

            //设置按下动画
            EventTrigger.Entry onDown = new EventTrigger.Entry();
            onDown.eventID = EventTriggerType.PointerDown;
            onDown.callback = new EventTrigger.TriggerEvent();
            UnityAction<BaseEventData> downcallback = new UnityAction<BaseEventData>(OnButtonDown);
            onDown.callback.AddListener(downcallback);

            listener.triggers.Add(onDown);
        }

        private void OnButtonDown(BaseEventData eventData)
        {
            OnClick?.Invoke(this, null);
        }

    }
}

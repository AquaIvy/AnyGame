using AnyGame.Content.Manager;
using AnyGame.Content.Texture;
using AnyGame.View;
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
    class UIScrollView : UIImageBase
    {
        public ScrollRect scrollRect;

        public UIMask uiViewport;
        public UINode uiContent;

        public Layout layout = Layout.Horizontal;


        private float scrollrectWidth = 100f;         //可视区域宽度
        private float scrollrectHeight = 100f;        //可视区域高度
        private float _contentWidth = 100f;     //内容宽度
        private float _contentHeight = 100f;    //内容高度

        private bool isNeedMask = false;

        public float ContentWidth
        {
            get { return _contentWidth; }
            set
            {
                switch (layout)
                {
                    case Layout.Horizontal:
                        uiContent.Width = value - scrollrectWidth;
                        break;
                    case Layout.Vertical:
                        uiContent.Width = value;
                        break;
                    case Layout.Grid:
                        uiContent.Width = value - scrollrectWidth;
                        break;
                }
                _contentWidth = value;
            }
        }

        public float ContentHeight
        {
            get { return _contentHeight; }
            set
            {
                switch (layout)
                {
                    case Layout.Horizontal:
                        uiContent.Height = value;
                        break;
                    case Layout.Vertical:
                        uiContent.Height = value;
                        break;
                    case Layout.Grid:
                        uiContent.Height = value;
                        break;
                }
                _contentHeight = value;
            }
        }

        public UIScrollView(string imgPath, float x, float y, float width, float height,
            Layout layout, UIScrollbar horizontalScrollbar, UIScrollbar verticalScrollbar,
            bool isNeedMask)
            : base(imgPath, x, y)
        {
            this.scrollrectWidth = width;
            this.scrollrectHeight = height;
            this.isNeedMask = isNeedMask;
            this.layout = layout;

            Name = "ScrollRect";

            //image.enabled = !string.IsNullOrEmpty(imgPath);
            image.color = Color.clear;

            // 1. ScrollRect 组件
            scrollRect = go.AddComponent<ScrollRect>();
            scrollRect.horizontal = layout == Layout.Horizontal || layout == Layout.Grid;
            scrollRect.vertical = layout == Layout.Vertical || layout == Layout.Grid;
            Size = new Vector2(width, height);

            if (horizontalScrollbar != null)
                scrollRect.horizontalScrollbar = horizontalScrollbar.scrollbar;

            if (verticalScrollbar != null)
                scrollRect.verticalScrollbar = verticalScrollbar.scrollbar;

            // 2. 裁切区域
            if (isNeedMask)
            {
                uiViewport = new UIMask(0, 0, width, height);
                uiViewport.Name = "Viewport";
                uiViewport.anchorMin = Vector2.zero;
                uiViewport.anchorMax = Vector2.one;
                uiViewport.Pivot = UIUtils.UpperLeft;
                base.AddChild(uiViewport);
            }



            // 3. 内容区
            uiContent = new UINode(0, 0);
            uiContent.Name = "Content";
            ContentWidth = width;
            ContentHeight = height;

            if (layout == Layout.Horizontal)
            {
                uiContent.anchorMin = UIUtils.UpperLeft;
                uiContent.anchorMax = UIUtils.UpperRight;
                uiContent.Pivot = UIUtils.UpperLeft;
            }
            else if (layout == Layout.Vertical)
            {
                uiContent.anchorMin = UIUtils.LowerLeft;
                uiContent.anchorMax = UIUtils.UpperLeft;
                uiContent.Pivot = UIUtils.UpperLeft;
            }
            else if (layout == Layout.Grid)
            {
                uiContent.anchorMin = UIUtils.UpperLeft;
                uiContent.anchorMax = UIUtils.UpperRight;
                uiContent.Pivot = UIUtils.UpperLeft;
            }

            // 4. 给ScrollRect赋值
            if (isNeedMask)
            {
                uiViewport.AddChild(uiContent);
                uiViewport.Size = new Vector2(width, height);
            }
            else
            {
                base.AddChild(uiContent);
            }

            scrollRect.content = uiContent.rt;
            if (isNeedMask)
            {
                scrollRect.viewport = uiViewport.rt;
            }
        }

        public override UIElement AddChild(UIElement element)
        {
            return uiContent.AddChild(element);
        }
    }
}

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

        public float contentWidth
        {
            get { return _contentWidth; }
            set
            {
                switch (layout)
                {
                    case Layout.Horizontal:
                        uiContent.width = value - scrollrectWidth;
                        break;
                    case Layout.Vertical:
                        uiContent.width = value;
                        break;
                    case Layout.Grid:
                        uiContent.width = value - scrollrectWidth;
                        break;
                }
                _contentWidth = value;
            }
        }

        public float contentHeight
        {
            get { return _contentHeight; }
            set
            {
                switch (layout)
                {
                    case Layout.Horizontal:
                        uiContent.height = value;
                        break;
                    case Layout.Vertical:
                        uiContent.height = value;
                        break;
                    case Layout.Grid:
                        uiContent.height = value;
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

            name = "ScrollRect";

            //image.enabled = !string.IsNullOrEmpty(imgPath);
            image.color = Color.clear;

            // 1. ScrollRect 组件
            scrollRect = go.AddComponent<ScrollRect>();
            scrollRect.horizontal = layout == Layout.Horizontal || layout == Layout.Grid;
            scrollRect.vertical = layout == Layout.Vertical || layout == Layout.Grid;
            size = new Vector2(width, height);

            if (horizontalScrollbar != null)
                scrollRect.horizontalScrollbar = horizontalScrollbar.scrollbar;

            if (verticalScrollbar != null)
                scrollRect.verticalScrollbar = verticalScrollbar.scrollbar;

            // 2. 裁切区域
            if (isNeedMask)
            {
                uiViewport = new UIMask(0, 0, width, height);
                uiViewport.name = "Viewport";
                uiViewport.anchorMin = Vector2.zero;
                uiViewport.anchorMax = Vector2.one;
                uiViewport.pivot = UIUtils.UpperLeft;
                base.AddChild(uiViewport);
            }



            // 3. 内容区
            uiContent = new UINode(0, 0);
            uiContent.name = "Content";
            contentWidth = width;
            contentHeight = height;

            if (layout == Layout.Horizontal)
            {
                uiContent.anchorMin = UIUtils.UpperLeft;
                uiContent.anchorMax = UIUtils.UpperRight;
                uiContent.pivot = UIUtils.UpperLeft;
            }
            else if (layout == Layout.Vertical)
            {
                uiContent.anchorMin = UIUtils.LowerLeft;
                uiContent.anchorMax = UIUtils.UpperLeft;
                uiContent.pivot = UIUtils.UpperLeft;
            }
            else if (layout == Layout.Grid)
            {
                uiContent.anchorMin = UIUtils.UpperLeft;
                uiContent.anchorMax = UIUtils.UpperRight;
                uiContent.pivot = UIUtils.UpperLeft;
            }

            // 4. 给ScrollRect赋值
            if (isNeedMask)
            {
                uiViewport.AddChild(uiContent);
                uiViewport.size = new Vector2(width, height);
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

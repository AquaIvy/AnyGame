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

namespace AnyGame.View.Components
{
    class UIText : UIElement
    {
        private static string gameObjectName = "Text";
        private static Vector2 gameObjectSize = new Vector2(0.1f, 0.1f);

        public Text textComponent { get; set; }

        public UIText(string text, float x, float y, int fontsize, Color color)
            : this(text, x, y, fontsize, color,
                  TextAnchor.UpperLeft,
                  HorizontalWrapMode.Overflow, VerticalWrapMode.Overflow)
        {

        }

        public UIText(string text, float x, float y, int fontsize, Color color, TextAnchor alignment)
            : this(text, x, y, fontsize, color,
                  alignment,
                  HorizontalWrapMode.Overflow, VerticalWrapMode.Overflow)
        {

        }

        public UIText(string text, float x, float y, int fontsize, Color color,
            TextAnchor alignment,
            HorizontalWrapMode horizontalOverflow, VerticalWrapMode verticalOverflow)
        {
            textComponent = go.AddComponent<Text>();

            name = gameObjectName;

            this.font = FontMgr.font;
            this.text = text;
            this.fontsize = fontsize;
            this.color = color;
            this.alignment = alignment;
            this.horizontalOverflow = horizontalOverflow;
            this.verticalOverflow = verticalOverflow;
            this.supportRichText = false;

            SetAnchor(alignment);

            //this.size = gameObjectSize;
            SetXY(x, y);
        }

        private void SetAnchor(TextAnchor alignment)
        {
            switch (alignment)
            {
                case TextAnchor.UpperLeft:
                    pivot = UIUtils.UpperLeft;
                    break;
                case TextAnchor.UpperCenter:
                    pivot = UIUtils.UpperCenter;
                    break;
                case TextAnchor.UpperRight:
                    pivot = UIUtils.UpperRight;
                    break;
                case TextAnchor.MiddleLeft:
                    pivot = UIUtils.MiddleLeft;
                    break;
                case TextAnchor.MiddleCenter:
                    pivot = UIUtils.MiddleCenter;
                    break;
                case TextAnchor.MiddleRight:
                    pivot = UIUtils.MiddleRight;
                    break;
                case TextAnchor.LowerLeft:
                    pivot = UIUtils.LowerLeft;
                    break;
                case TextAnchor.LowerCenter:
                    pivot = UIUtils.LowerCenter;
                    break;
                case TextAnchor.LowerRight:
                    pivot = UIUtils.LowerRight;
                    break;
            }
        }


        private string _text;
        public string text
        {
            get { return _text; }
            set
            {
                _text = value;
                textComponent.text = _text;
                size = new Vector2(textComponent.preferredWidth * 1.5f, textComponent.preferredHeight * 1.5f);
            }
        }

        /// <summary>
        /// 字体
        /// </summary>
        public Font font
        {
            get { return textComponent.font; }
            set { textComponent.font = value; }
        }

        /// <summary>
        /// 文字的锚点位置   TextAnchor.UpLeft
        /// </summary>
        public TextAnchor alignment
        {
            get { return textComponent.alignment; }
            set
            {
                textComponent.alignment = value;
                SetAnchor(value);
            }
        }

        /// <summary>
        /// 字体大小
        /// </summary>
        public int fontsize
        {
            get { return textComponent.fontSize; }
            set { textComponent.fontSize = value; }
        }

        /// <summary>
        /// 字体颜色
        /// </summary>
        public Color color
        {
            get { return textComponent.color; }
            set { textComponent.color = value; }
        }

        /// <summary>
        /// 【水平】方向文字溢出方式
        /// </summary>
        public HorizontalWrapMode horizontalOverflow
        {
            get { return textComponent.horizontalOverflow; }
            set { textComponent.horizontalOverflow = value; }
        }

        /// <summary>
        /// 【垂直】方向文字溢出方式
        /// </summary>
        public VerticalWrapMode verticalOverflow
        {
            get { return textComponent.verticalOverflow; }
            set { textComponent.verticalOverflow = value; }
        }

        /// <summary>
        /// 是否支持富文本
        /// </summary>
        public bool supportRichText
        {
            get { return textComponent.supportRichText; }
            set { textComponent.supportRichText = value; }
        }
    }
}

using AnyGame.Content.Manager;
using AnyGame.Content.Texture;
using DogSE.Library.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace AnyGame.UI
{
    /// <summary>
    /// 常用数据
    /// </summary>
    class UIUtils
    {
        //设计屏幕分辨率
        public static readonly Vector2 Screen = new Vector2(GlobalInfo.width, GlobalInfo.height);

        //常用pivot
        public static readonly Vector2 UpperLeft = new Vector2(0f, 1f);
        public static readonly Vector2 UpperCenter = new Vector2(0.5f, 1f);
        public static readonly Vector2 UpperRight = new Vector2(1f, 1f);

        public static readonly Vector2 MiddleLeft = new Vector2(0f, 0.5f);
        public static readonly Vector2 MiddleCenter = new Vector2(0.5f, 0.5f);
        public static readonly Vector2 MiddleRight = new Vector2(1f, 0.5f);

        public static readonly Vector2 LowerLeft = new Vector2(0f, 0f);
        public static readonly Vector2 LowerCenter = new Vector2(0.5f, 0f);
        public static readonly Vector2 LowerRight = new Vector2(1f, 0f);

        //常用border
        public static readonly Vector4 Border10 = new Vector4(10, 10, 10, 10);
        public static readonly Vector4 Border15 = new Vector4(15, 15, 15, 15);
        public static readonly Vector4 Border20 = new Vector4(20, 20, 20, 20);
        public static readonly Vector4 Border25 = new Vector4(25, 25, 25, 25);
        public static readonly Vector4 Border30 = new Vector4(30, 30, 30, 30);
    }
}

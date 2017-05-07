using AnyGame.Content.Manager;
using AnyGame.Content.Texture;
using AnyGame.View;
using DogSE.Library.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace AnyGame.View.Components
{
    class UIImage : UIImageBase
    {
        public UIImage(string imgPath, float x, float y)
            : base(imgPath, x, y, UIUtils.UpperLeft, Vector4.zero)
        {

        }

        public UIImage(string imgPath, float x, float y, Vector2 pivot)
            : base(imgPath, x, y, pivot, Vector4.zero)
        {

        }

        //public UIImage(string imgPath, float x, float y, Vector4 border)
        //    : base(imgPath, x, y, UIUtils.UpperLeft, border)
        //{

        //}

        public UIImage(string imgPath, float x, float y, Vector2 pivot, Vector4 border)
            : base(imgPath, x, y, pivot, border)
        {

        }
    }
}

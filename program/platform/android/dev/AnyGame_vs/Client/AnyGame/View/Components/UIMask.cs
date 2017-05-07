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
    class UIMask : UIImageBase
    {
        private static string gameObjectName = "Mask";

        public Mask mask;
        private bool isShowMaskGraphic = false;

        public UIMask(float x, float y, float width, float height, bool isShowMaskGraphic = false)
            : base(null, x, y, UIUtils.UpperLeft, Vector4.zero)
        {
            this.isShowMaskGraphic = isShowMaskGraphic;

            Name = gameObjectName;

            mask = go.AddComponent<Mask>();

            mask.showMaskGraphic = isShowMaskGraphic;

            this.Width = width;
            this.Height = height;
        }
    }
}

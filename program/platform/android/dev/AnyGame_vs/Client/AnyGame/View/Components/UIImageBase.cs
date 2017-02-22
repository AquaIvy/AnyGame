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
    abstract class UIImageBase : UIElement
    {
        private static string gameObjectName = "Image";

        protected SpriteWrap spriteWrap;
        public Image image { get; set; }

        private Vector4 _border;
        public Vector4 border
        {
            get { return _border; }
            set
            {
                _border = value;
                if (image != null)
                {
                    image.type = Image.Type.Sliced;
                    SetImage(spriteWrap.name);
                }
            }
        }

        public UIImageBase(string imgPath, float x, float y)
            : this(imgPath, x, y, UIUtils.UpperLeft, Vector4.zero)
        {

        }

        public UIImageBase(string imgPath, float x, float y, Vector2 pivot, Vector4 border)
        {
            name = gameObjectName;
            image = go.AddComponent<Image>();
            _border = border;
            if (border != Vector4.zero)
            {
                image.type = Image.Type.Sliced;
            }

            SetImage(imgPath);

            this.pivot = pivot;

            SetXY(x, y);
            SetNativeSize();
        }

        public UIImageBase SetImage(string imgPath)
        {
            if (!string.IsNullOrEmpty(imgPath))
            {
                spriteWrap = TextureMgr.CreateSprite(imgPath, _border);
                image.sprite = spriteWrap.sprite;
                image.material = spriteWrap.textureWrap.material;
            }

            return this;
        }

        private Vector2 _pivot = UIUtils.UpperLeft;
        public override Vector2 pivot
        {
            get { return _pivot; }
            set
            {
                _pivot = value;

                if (spriteWrap == null || spriteWrap.sample == null)
                {
                    base.pivot = value;
                    return;
                }

                var sample = spriteWrap.sample;

                float pivot_zero_x = (-sample.offset.x / sample.size.x);            //原始sprite以左上角为(0,0)时的pivot.x
                float pivot_zero_y = 1 - (-sample.offset.y / sample.size.y);        //原始sprite以左上角为(0,0)时的pivot.y

                float pivot_x = pivot_zero_x + (sample.sourceSize.x / sample.size.x * value.x);
                float pivot_y = pivot_zero_y - (sample.sourceSize.y / sample.size.y * (1 - value.y));

                //_pivot = new Vector2(pivot_x, pivot_y);
                //rt.pivot = _pivot;
                base.pivot = new Vector2(pivot_x, pivot_y);
            }
        }

        public UIElement SetNativeSize()
        {
            if (image != null)
            {
                image.SetNativeSize();
            }

            return this;
        }


        public override void Dispose()
        {
            if (image != null)
            {
                Sprite.Destroy(image.sprite);
                image.sprite = null;
                spriteWrap = null;
            }

            base.Dispose();
        }
    }
}

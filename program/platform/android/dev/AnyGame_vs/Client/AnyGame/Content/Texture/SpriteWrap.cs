using DogSE.Library.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AnyGame.Content.Texture
{
    /// <summary>
    /// Sprite包装类
    /// </summary>
    public class SpriteWrap
    {
        public static Vector2 centerPivot = new Vector2(0.5f, 0.5f);

        public string name { get; private set; }
        public SpriteSample sample { get; private set; }
        public TextureWrap textureWrap { get; private set; }
        public Sprite sprite { get; private set; }

        private int texHeight;

        public Vector4 border { get; set; }

        public bool isNineSquare { get { return border != Vector4.zero; } }


        public SpriteWrap(TextureWrap textureWrap, SpriteSample sample, Vector4 border)
        {
            this.textureWrap = textureWrap;
            this.sample = sample;
            this.name = sample.name;
            this.border = border;

            texHeight = textureWrap.height;

            textureWrap.AddRefCounter();

            CreateSprite();
        }

        ~SpriteWrap()
        {
            //Logs.Info("dispose SpriteWrap   name");

            textureWrap.DecRefCounter();
        }

        private void CreateSprite()
        {
            var info = sample;

            //九宫格Sprite
            if (isNineSquare)
            {
                Sprite s = Sprite.Create(textureWrap.mainTex,
                    new Rect(info.rect.x, info.rect.y, info.rect.width, info.rect.height),
                    centerPivot,
                    100.0f,
                    0,
                    SpriteMeshType.FullRect,
                    border
                    );
                s.name = info.name;

                this.sprite = s;
            }
            //一般Sprite
            else
            {
                Sprite s = Sprite.Create(textureWrap.mainTex,
                //Unity位置信息
                new Rect(info.rect.x, info.rect.y, info.rect.width, info.rect.height),
                //Cocos位置信息
                //new Rect(info.rect.x, texHeight - info.rect.y - info.rect.height, info.rect.width, info.rect.height),
                centerPivot,
                100.0f,
                0,
                SpriteMeshType.FullRect
                );
                s.name = info.name;

                this.sprite = s;
            }
        }

    }
}

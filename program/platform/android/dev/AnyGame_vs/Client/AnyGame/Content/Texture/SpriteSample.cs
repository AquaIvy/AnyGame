using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AnyGame.Content.Texture
{
    /// <summary>
    /// Sprite 简单信息（裁切信息）
    /// </summary>
    public class SpriteSample
    {
        /// <summary>
        /// Sprite名字  例：profile/1006.png
        /// </summary>
        public string name;

        /// <summary>
        /// 所属texture的名字    例：profile/texture_0
        /// </summary>
        public string textureName;

        /// <summary>
        /// Sprite大小
        /// </summary>
        public Vector2 size { get; set; }

        /// <summary>
        /// Sprite原始大小
        /// </summary>
        public Vector2 sourceSize { get; set; }

        /// <summary>
        /// 偏移，即xy上被裁减掉的像素点
        /// </summary>
        public Vector2 offset { get; set; }

        /// <summary>
        /// 所在texture的位置信息
        /// </summary>
        public Rect rect { get; set; }

        /// <summary>
        /// Sprite的中心点
        /// </summary>
        //public Vector2 pivot = new Vector2();

        /// <summary>
        /// Sprite的九宫格裁切信息
        /// </summary>
        //public Vector4 border = new Vector4();

        /// <summary>
        /// 是否为九宫格
        /// </summary>
        //public bool isSquared = false;
    }
}

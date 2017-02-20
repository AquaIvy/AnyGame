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
    class UIScrollbar : UIImageBase
    {
        public Scrollbar scrollbar;
        public UIScrollbar(string imgPath, float x, float y)
            : base(imgPath, x, y)
        {
        }
    }
}

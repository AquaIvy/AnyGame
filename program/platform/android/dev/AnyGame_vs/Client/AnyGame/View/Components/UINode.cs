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

namespace AnyGame.View.Components
{
    class UINode : UIElement
    {
        private static string gameObjectName = "Node";

        public UINode() : this(0, 0)
        {

        }

        public UINode(string name) : this(0, 0)
        {
            this.name = name;
        }


        public UINode(float x, float y)
        {
            name = gameObjectName;
            var img = go.AddComponent<Image>();
            GameObject.Destroy(img);

            SetXY(x, y);
        }
    }
}

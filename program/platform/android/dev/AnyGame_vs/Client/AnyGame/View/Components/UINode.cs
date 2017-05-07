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

        public UINode()
            : this(gameObjectName, 0, 0)
        {

        }

        public UINode(string name)
            : this(name, 0, 0)
        {

        }

        public UINode(float x, float y)
            : this(gameObjectName, x, y)
        {

        }

        public UINode(string name, float x, float y)
        {
            this.Name = name;

            SetXY(x, y);
        }
    }
}

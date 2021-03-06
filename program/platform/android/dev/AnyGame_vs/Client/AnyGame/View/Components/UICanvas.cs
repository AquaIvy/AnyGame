﻿using AnyGame.Content.Manager;
using AnyGame.Content.Texture;
using DogSE.Library.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace AnyGame.View.Components
{
    class UICanvas : UIElement
    {
        public Canvas canvas;
        public CanvasScaler canvasScaler;
        public GraphicRaycaster graphicRaycaster;

        public UICanvas()
        {
            canvas = go.AddComponent<Canvas>();
            canvasScaler = go.AddComponent<CanvasScaler>();
            graphicRaycaster = go.AddComponent<GraphicRaycaster>();


        }

        public static UICanvas Find(string gameObjectName)
        {
            var goc = GameObject.Find(gameObjectName);
            if (goc == null)
            {
                Logs.Error("未找到Canvas【{0}】", gameObjectName);
                return null;
            }

            var uican = new UICanvas();
            GameObject.Destroy(uican.go);
            uican.go = goc;
            uican.rt = goc.GetComponent<RectTransform>();
            uican.Name = gameObjectName;
            uican.canvas = goc.GetComponent<Canvas>();
            uican.canvasScaler = goc.GetComponent<CanvasScaler>();
            uican.graphicRaycaster = goc.GetComponent<GraphicRaycaster>();

            return uican;
        }

    }
}

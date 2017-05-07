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
    public enum Layout
    {
        Horizontal = 0,
        Vertical = 1,
        Grid = 2
    }

    class UIContent : UIElement
    {
        public Layout layout = Layout.Horizontal;
        
        public ContentSizeFitter sizeFitter;

        public HorizontalLayoutGroup horizontal;
        public VerticalLayoutGroup vertical;
        public GridLayoutGroup grid;

        public UIContent(Layout layout)
        {
            this.layout = layout;

            Name = "content";

            sizeFitter = go.AddComponent<ContentSizeFitter>();
            sizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;

            if (layout == Layout.Horizontal)
            {
                horizontal = go.AddComponent<HorizontalLayoutGroup>();
            }
            else if (layout == Layout.Vertical)
            {
                vertical = go.AddComponent<VerticalLayoutGroup>();
            }
            else if (layout == Layout.Grid)
            {
                grid = go.AddComponent<GridLayoutGroup>();
            }

        }
    }
}

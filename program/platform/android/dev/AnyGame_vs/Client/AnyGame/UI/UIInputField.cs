using AnyGame.Content.Manager;
using AnyGame.Content.Texture;
using DogSE.Library.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace AnyGame.UI
{
    class UIInputField : UIImageBase
    {
        public InputField inputField;

        public UIText textContent;

        private static string gameObjectName = "InputField";
        private static string gameObjectName_Text = "Text";

        public UIInputField(string imgPath, float x, float y)
            : this(imgPath, x, y, UIUtils.UpperLeft, Vector4.zero)
        {

        }

        public UIInputField(string imgPath, float x, float y, Vector2 pivot, Vector4 border)
            : base(imgPath, x, y, pivot, border)
        {
            name = gameObjectName;

            image.enabled = !string.IsNullOrEmpty(imgPath);

            inputField = go.AddComponent<InputField>();

            textContent = new UIText(string.Empty, 10, 0, 22, Color.red);
            textContent.name = gameObjectName_Text;
            this.AddChild(textContent);

            inputField.textComponent = textContent.textComponent;

            inputField.onValueChanged.RemoveAllListeners();
            inputField.onValueChanged.AddListener(content =>
            {
                textContent.text = inputField.text;
            });
        }

        public string text
        {
            get { return inputField.text; }
            set { inputField.text = value; }
        }
    }
}

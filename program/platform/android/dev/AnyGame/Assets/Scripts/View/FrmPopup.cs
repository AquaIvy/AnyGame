using Assets.Scripts.Update;
using Assets.Scripts.Utils;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.View
{
    class FrmPopup : FrmBase
    {
        public Text txtContent = null;
        public Button btnOK = null;
        public Button btnCancel = null;

        public FrmPopup()
        {
            InitForm();
        }

        public void Show(string content, Action onOK, Action onCancel)
        {
            if (LoaderCenter.Loader.FrmPopup == null)
            {
                LoaderCenter.Loader.FrmPopup = new FrmPopup();
            }

            txtContent.text = content;
            BindingEvent(btnOK, () => { onOK(); });
            BindingEvent(btnCancel, () => { onCancel(); });

            this.Show();
        }

        private void BindingEvent(Button btn, UnityAction action)
        {
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(action);
        }

        public override void Dispose()
        {
            LoaderCenter.Loader.FrmPopup = null;

            base.Dispose();
        }

        private void InitForm()
        {
            var go = Resources.Load<GameObject>("FrmPopup");
            gameObject = GameObject.Instantiate<GameObject>(go);
            transform = gameObject.transform;

            txtContent = transform.Find("txtContent").GetComponent<Text>();
            btnOK = transform.Find("btnOK").GetComponent<Button>();
            btnCancel = transform.Find("btnCancel").GetComponent<Button>();
        }
    }
}

using AnyGame.Utils.TweenLite;
using AnyGame.View.Components;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AnyGame.View.Forms
{
    abstract class FrmBase : UIImageBase
    {

        public abstract FrmType Type { get; }


        private TweenLite tl { set { alltls.Add(value); } }
        private List<TweenLite> alltls = new List<TweenLite>();

        private Task it { set { alltasks.Add(value); } }
        private List<Task> alltasks = new List<Task>();


        public FrmBase(string name)
            : base(null, 0, 0)
        {
            Game = Game.instance;

            this.name = name;

            image.enabled = false;
        }


        public void SetBackground(string imgPath)
        {
            SetImage(imgPath);
            image.type = Image.Type.Sliced;
            image.enabled = true;
        }

        public bool isShowing { get; private set; }
        public void Show()
        {
            if (OnShowing())
            {
                go.SetActive(true);
                isShowing = true;

                OnShowed();
            }
        }

        protected virtual bool OnShowing()
        {
            return true;
        }

        protected virtual void OnShowed()
        {

        }



        public void Hide()
        {
            go.SetActive(false);
            isShowing = false;
        }

        public void Close()
        {
            if (OnClosing())
            {
                DisposeAllChilds();
                if (this.go != null)
                {
                    GameObject.Destroy(this.go);
                }

                spriteWrap = null;
                image.sprite = null;
                image.enabled = false;

                alltls.ForEach(o => o?.Release());
                alltasks.ForEach(o => o?.Release());

                OnClosed();
            }
        }

        protected virtual bool OnClosing()
        {
            return true;
        }

        protected virtual void OnClosed()
        {
            isShowing = false;
        }

        public override void Dispose()
        {
            Close();
        }
    }

    public enum FrmType
    {
        /// <summary>
        /// 底板窗体
        /// </summary>
        Background = 0,

        /// <summary>
        /// 用户自定义窗体
        /// </summary>
        Custom,

        /// <summary>
        /// 弹出窗（弹出效果）
        /// </summary>
        Popup,

        /// <summary>
        /// 顶部、底部窗体
        /// </summary>
        Banner,

        /// <summary>
        /// 特效eng 
        /// </summary>
        Effect
    }
}

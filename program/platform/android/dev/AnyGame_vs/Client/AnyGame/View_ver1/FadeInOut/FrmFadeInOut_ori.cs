using AnyGame.Content.Manager;
using AnyGame.Global;
using DogSE.Library.Log;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AnyGame.View.FadeInOut
{
    public class FrmFadeInOut : FrmBase
    {
        private GameObject Slider = null;
        private Slider SliderCompoment = null;
        private Text txtNotice = null;

        private int curValue = 0;
        private int totalValue = 0;

        public override FormType winType { get { return FormType.FadeInOut; } }

        public FrmFadeInOut(Game game) : base(game)
        {

        }


        /// <summary>
        /// 显示过度界面
        /// </summary>
        /// <param name="onShowed">显示出来后（此时进度条还为0，没有移动）</param>
        public override void Show(Action onShowed = null)
        {
            if (IsVisible)
            {
                Logs.Info("{0} is showing ", winType.ToString());
                return;
            }

            if (IsCreated)
            {
                base.Show(onShowed);
                return;
            }

            IsCreated = true;

            //删除加载器的一个无用物体
            var entergame = GameObject.Find("/EnterGame");
            if (entergame != null)
            {
                GameObject.Destroy(entergame);
            }

            var uigo = SceneMgr.CreateUIGameObject(GlobalInfo.RES_GAME_UI + "Canvas_FadeInOut_ab.assetbundle");
            if (uigo == null)
            {
                Logs.Error("首次创建过度界面失败");
                return;
            }
            uigo.name = uigo.name.Replace("_ab", "");
            uigo.AddComponent<DontDestroyThisGameObject>();

            CanvasMgr.Init();

            //因为过度界面是第一次启动，也只会执行一次，所以不需要onUpdate啦~~
            Game.StartCoroutine(SceneMgr.CreateUI(winType.ToString(),
                CanvasMgr.FadeInOut,
                CanvasMgr.basicFadeInOut + "/",
                null,
                BindingEvents,
                onShowed));
        }

        private void BindingEvents(Action onShowed = null)
        {
            var canvasscale = CanvasMgr.FindFadeInOut("FadeInOut");
            if (canvasscale != null)
            {
                var rt = canvasscale.GetComponent<RectTransform>();
                rt.sizeDelta = GlobalInfo.CanvasParentRect;
            }

            Slider = CanvasMgr.FindFadeInOut(CanvasMgr.GetPathMap(winType.ToString(), "Slider"));
            SliderCompoment = Slider.GetComponent<Slider>();
            txtNotice = CanvasMgr.FindFadeInOut(CanvasMgr.GetPathMap(winType.ToString(), "txtNotice")).GetComponent<Text>();

            self = GameObject.Find(CanvasMgr.basicFadeInOut + "/" + winType);
            base.Show(onShowed);

        }

        /// <summary>
        /// 隐藏过度界面
        /// </summary>
        public override void Close(Action onClosed = null)
        {
            curValue = totalValue = 0;
            Game.StartCoroutine(SetSliderValue(curValue, totalValue));

            base.Close(onClosed);
        }


        public override void Dispose(Action onDisposed = null)
        {
            Logs.Warn("你为什么要释放过度界面呢~");
            //base.Dispose();
        }

        private IEnumerator<float> SetSliderValue(int cur, int max)
        {
            if (/*IsVisible &&*/ SliderCompoment != null && txtNotice != null)
            {
                txtNotice.text = string.Format("{0}/{1}", cur, max);

                float value = max < 1 ? 0.0f : ((cur * 1.0f) / max);
                SliderCompoment.value = value;
                yield return SliderCompoment.value;
            }
        }

        /// <summary>
        /// 设定滑动条进度（由方法自动计算，无需传入参数）
        /// </summary>
        /// <returns></returns>
        public float SetSliderValue()
        {
            curValue += 10;
            float percent = (curValue * 1.0f) / totalValue;
            Game.StartCoroutine(SetSliderValue(curValue, totalValue));

            if (curValue >= totalValue)
            {
                Game.Invoke(t =>
                {
                    if (t > 700)
                    {
                        Close();
                        return true;
                    }
                    return false;
                });
            }
            return percent;
        }

        /// <summary>
        /// 重置过度界面需要加载的资源总数，
        /// 每次显示过度界面时必须调用该方法
        /// </summary>
        /// <param name="max"></param>
        public void SetTotalValue(int max)
        {
            if (max < 1)
            {
                max = 1;
            }

            totalValue = max;
            curValue = 0;
        }
    }
}

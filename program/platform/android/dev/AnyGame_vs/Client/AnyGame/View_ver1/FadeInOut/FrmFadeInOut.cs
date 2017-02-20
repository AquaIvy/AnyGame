using AnyGame.Content.Manager;
using AnyGame.Global;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AnyGame.View.FadeInOut
{
    public class FrmFadeInOut : FrmBase
    {
        private Slider m_slider = null;
        private Text txtNotice = null;

        //private int curValue = 0;
        private int totalValue = 0;

        public override FormType winType { get { return FormType.FadeInOut; } }
        public override FormLayer winLayer { get { return FormLayer.FillScreen; } }
        public override string[] UsingAtlas
        {
            get { return new string[] { }; }
        }

        public FrmFadeInOut(Game game) : base(game)
        {

        }

        protected override void BindingEvents(Action onShowed = null)
        {
            //屏幕适应
            var canvasscale = CanvasMgr.FindFadeInOut("FadeInOut");
            if (canvasscale != null)
            {
                var rt = canvasscale.GetComponent<RectTransform>();
                rt.sizeDelta = GlobalInfo.CanvasParentRect;
            }

            var slider_GO = CanvasMgr.FindFadeInOut(CanvasMgr.GetPathMap(winType.ToString(), "Slider"));
            m_slider = slider_GO.GetComponent<Slider>();
            txtNotice = CanvasMgr.FindFadeInOut(CanvasMgr.GetPathMap(winType.ToString(), "txtNotice")).GetComponent<Text>();

            base.BindingEvents(onShowed);
        }


        private IEnumerator<float> SetSliderValue(int cur, int max)
        {
            if (/*IsVisible &&*/ m_slider != null && txtNotice != null)
            {
                txtNotice.text = string.Format("{0}/{1}", cur, max);

                float value = max < 1 ? 0.0f : ((cur * 1.0f) / max);
                m_slider.value = value;
                yield return m_slider.value;
            }
        }

        private void SetSliderValueSync(int cur, int max)
        {
            if (m_slider != null && txtNotice != null)
            {
                txtNotice.text = string.Format("{0}/{1}", cur, max);

                float value = max < 1 ? 0.0f : ((cur * 1.0f) / max);
                m_slider.value = value;
            }
        }

        /// <summary>
        /// 设定滑动条进度（由方法自动计算，无需传入参数）
        /// </summary>
        /// <returns></returns>
        public float SetSliderValue(int curValue)
        {
            float percent = (curValue * 1.0f) / totalValue;
            //Game.StartCoroutine(SetSliderValue(curValue, totalValue));
            SetSliderValueSync(curValue, totalValue);

            //if (curValue >= totalValue)
            //{
            //    Game.Invoke(t =>
            //    {
            //        if (t > 100)
            //        {
            //            //Close();
            //            this.Hide();
            //            return true;
            //        }
            //        return false;
            //    });
            //}
            return percent;
        }

        /// <summary>
        /// 重置过度界面需要加载的资源总数，
        /// 每次显示过度界面时必须调用该方法
        /// </summary>
        /// <param name="max"></param>
        public void SetTotalValue(int max)
        {
            if (max < 0)
            {
                max = 0;
            }

            totalValue = max;
            //curValue = 0;
        }
    }
}

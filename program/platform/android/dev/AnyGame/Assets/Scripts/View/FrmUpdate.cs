using Assets.Scripts.Update;
using Assets.Scripts.Utils;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.View
{
    public class FrmUpdate : FrmBase
    {
        public string Name { get { return "FrmUpdate"; } }

        private Transform PanelBase = null;
        private Transform PanelTemplate = null;
        private Transform content = null;
        private GameObject noticeTemplate;
        private Text txtNotice;
        private Image process;


        public FrmUpdate()
        {
            LoaderCenter.Event.OnLog += Event_OnLog;
            LoaderCenter.Event.OnDownloadOneItem += Event_OnDownloadOneItem;
            LoaderCenter.Event.OnDownloadError += Event_OnDownloadError;
            LoaderCenter.Event.OnDownloadFinish += Event_OnDownloadFinish;

            InitForm();
        }

        public override void Show()
        {
            base.Show();

            LoaderCenter.Update.Start();
        }

        private void InitForm()
        {
            var go = Resources.Load<GameObject>("FrmUpdate");
            gameObject = GameObject.Instantiate<GameObject>(go);
            transform = gameObject.transform;


            //panel
            PanelBase = transform.Find("PanelBase");
            PanelTemplate = transform.Find("PanelTemplate");

            content = PanelBase.Find("BG/Scroll View/Viewport/Content");
            noticeTemplate = PanelTemplate.Find("updateNotice").gameObject;

            process = PanelBase.Find("Slider/Process").GetComponent<Image>();
            txtNotice = PanelBase.Find("txtNotice").GetComponent<Text>();
        }

        public override void Dispose()
        {
            LoaderCenter.Loader.FrmUpdate = null;

            base.Dispose();
        }



        private void Event_OnDownloadOneItem(object sender, ComplateOneItemArgs e)
        {
            txtNotice.text = string.Format("正在{0}  {1}/{2}", e.State, e.CurNum, e.TotalNum);
            process.fillAmount = e.CurNum * 1.0f / e.TotalNum;
        }


        private void Event_OnDownloadFinish(object sender, EventArgs e)
        {
            LoaderCenter.Event.Log(LogLevel.Notice, "所有下载结束，准备ReflectionAssembly");

            LoaderCenter.Loader.StartGame();
        }

        private void Event_OnDownloadError(object sender, DownloadErrorArgs e)
        {
            string content = string.Format("{0} 出错 {1}，\n是否重试", e.State, e.Message);
            FrmPopup popup = new FrmPopup();
            popup.Show(content, () =>
            {
                e.RetryAction();
                popup.Dispose();

            }, () =>
            {
                Application.Quit();
            });
        }

        private void Event_OnLog(object sender, LogArgs e)
        {
            DisplayLog(e.Level, e.Text);
        }

        public void DisplayLog(LogLevel level, string text)
        {
            string color = string.Empty;
            switch (level)
            {
                case LogLevel.None:
                case LogLevel.Debug:
                case LogLevel.Info:
                    color = "#00ff00ff";
                    Debug.Log("【" + level + "】 " + text);
                    break;
                case LogLevel.Notice:
                    color = "#00ffffff";
                    Debug.Log("【" + level + "】 " + text);
                    break;
                case LogLevel.Warning:
                    color = "#ffa500ff";
                    Debug.LogWarning("【" + level + "】 " + text);
                    break;
                case LogLevel.Error:
                    color = "#ff0000ff";
                    Debug.LogError("【" + level + "】 " + text);
                    break;
            }
            string richtext = string.Format("<color={0}>{1}</color> {2}", color, level.ToString(), text);

            AddNotice(richtext);
        }

        private void AddNotice(string text)
        {
            var ins = (GameObject)GameObject.Instantiate(noticeTemplate);
            ins.transform.SetParent(content);
            ins.transform.localScale = Vector3.one;

            ins.GetComponent<Text>().text = text;
            ins.GetComponent<LayoutElement>().preferredHeight = ins.GetComponent<Text>().preferredHeight + 40;

            ins.SetActive(true);
        }


    }
}
using AnyGame.Content.Manager;
using DogSE.Library.Log;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using AztecGame.Utils;
using System.Collections.Generic;

namespace AnyGame.View
{
    public abstract class FrmBase
    {
        #region filed & property

        private FormStatus m_status = FormStatus.UnCreated;
        /// <summary>
        /// 窗体显示状态
        /// </summary>
        public FormStatus Status { get; private set; }

        /// <summary>
        /// 获取或设置窗体的"是否已创建"状态
        /// </summary>
        public bool IsCreated { get; private set; }

        /// <summary>
        /// 获取或设置界面的显示状态
        /// </summary>
        public bool IsVisible { get; private set; }

        /// <summary>
        /// 主控组件
        /// </summary>
        public Game Game { get; private set; }

        /// <summary>
        /// 该窗体类在游戏世界所对应的GameObject，
        /// 便于SetActive() 或 Destroy()
        /// </summary>
        public GameObject self { get; protected set; }
        private string selfStructPath = string.Empty;

        /// <summary>
        /// 窗体类型
        /// </summary>
        public abstract FormType winType { get; }

        /// <summary>
        /// 窗体的显示层次
        /// </summary>
        public abstract FormLayer winLayer { get; }

        /// <summary>
        /// 窗体引用图集，在窗体Close时会释放所引用的图集资源
        /// </summary>
        public abstract string[] UsingAtlas { get; }

        public FrmBase(Game game)
        {
            Game = game;
        }

        #endregion


        #region Show

        /// <summary>
        /// 显示窗体,创建到 Control画布，有过度界面
        /// </summary>
        public void Show()
        {
            Show(winType.ToString(),
                CanvasMgr.Control,
                CanvasMgr.basicControl + "/",
                (cur, max) =>
                {
                    Game.FrmFadeInOut.SetSliderValue(cur);
                },
                BindingEvents,
                OnShowed
                );
        }

        /// <summary>
        /// 显示窗体，需自己分配父节点，有过度界面
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="parentPath"></param>
        public void Show(Transform parent,
            string parentPath)
        {
            Show(winType.ToString(),
                parent,
                parentPath,
                (cur, max) =>
                {
                    Game.FrmFadeInOut.SetSliderValue(cur);
                },
                BindingEvents,
                OnShowed
                );
        }

        /// <summary>
        /// 显示窗体,创建到 Control画布，无过度界面
        /// </summary>
        public void ShowDialog()
        {
            Show(winType.ToString(),
                CanvasMgr.Control,
                CanvasMgr.basicControl + "/",
                null,
                BindingEvents,
                OnShowed
                );
        }

        /// <summary>
        /// 显示窗体，需自己分配父节点，无过度界面
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="parentPath"></param>
        public void ShowDialog(Transform parent,
            string parentPath)
        {
            Show(winType.ToString(),
                parent,
                parentPath,
                null,
                BindingEvents,
                OnShowed
                );
        }

        /// <summary>
        /// 显示窗体，自己传入所有参数
        /// </summary>
        public void Show(string uiName,
            Transform parent,
            string parentPath,
            Action<int, int> onUpdate = null,
            Action<Action> onFinish = null,
            Action onAfterFinish = null)
        {
            //防止玩家快速多次点击
            if (m_status == FormStatus.ReadyToShow) { return; }

            var ori_status = m_status;
            m_status = FormStatus.ReadyToShow;

            if (OnShowing())
            {
                if (IsVisible)
                {
                    m_status = FormStatus.Showing;
                    Logs.Info("{0} is already showed.", winType.ToString());
                    return;
                }

                //如果创建了窗体UI，并被隐藏
                if (IsCreated)
                {
                    OnShowed();

                    return;
                }

                //没创建过窗体，或释放掉了窗体，则先创建
                IsCreated = true;
                selfStructPath = parentPath + uiName;

                //【携程】 加载UI窗体
                Game.StartCoroutine(SceneMgr.CreateUI(uiName,
                    parent,
                    parentPath,
                    onUpdate,
                    onFinish,
                    onAfterFinish
                    ));
            }
            else
            {
                m_status = ori_status;
            }
        }

        /// <summary>
        /// 第一个执行，
        /// 在加载UI窗体【前】执行
        /// </summary>
        /// <returns></returns>
        protected virtual bool OnShowing()
        {
            return true;
        }

        /// <summary>
        /// 第三个执行，
        /// 在加载UI窗体【后】执行
        /// </summary>
        protected virtual void OnShowed()
        {
            if (winLayer == FormLayer.FillScreen
                && winType != FormType.FadeInOut)
            {
                Game.FrmFadeInOut.Hide();
            }

            if (self != null)
            {
                IsVisible = true;
                m_status = FormStatus.Showing;
                self.SetActive(true);
            }
            else
            {
                Logs.Error("{0} is null when OnShowed.", winType.ToString());
            }
        }

        /// <summary>
        /// 第二个执行，
        /// 在加载UI窗体【时】执行，
        /// 如果窗体已经加载则不执行
        /// </summary>
        /// <param name="onShowed"></param>
        protected virtual void BindingEvents(Action onShowed = null)
        {
            self = GameObject.Find(selfStructPath);
            if (self == null)
            {
                Logs.Error("{0} is null when BindingEvents.", winType.ToString());
            }

            if (onShowed != null)
            {
                onShowed();
            }
        }

        #endregion


        #region Close

        /// <summary>
        /// 关闭窗体，会释放GameObject资源
        /// </summary>
        public void Close()
        {
            //防止玩家快速多次点击
            if (m_status == FormStatus.ReadyToClose) { return; }

            var ori_status = m_status;
            m_status = FormStatus.ReadyToClose;

            if (OnClosing())
            {
                if (self == null)
                {
                    Logs.Error("{0} is null when Close.", winType.ToString());
                }
                else
                {
                    Game.Destroy(self);
                }

                m_status = FormStatus.Closed;
                IsCreated = false;
                IsVisible = false;
                self = null;

                //TextureMgr.Dispose(UsingAtlas);
                Resources.UnloadUnusedAssets();
                GC.Collect();

                OnClosed();
            }
            else
            {
                m_status = ori_status;
            }
        }

        protected virtual bool OnClosing()
        {
            return true;
        }

        protected virtual void OnClosed()
        {

        }

        #endregion


        #region Hide

        public void Hide()
        {
            if (self == null)
            {
                Logs.Error("{0} is null when Hide.", winType.ToString());
                return;
            }

            self.SetActive(false);
            IsVisible = false;
            m_status = FormStatus.Hide;
        }

        #endregion


        #region func

        protected GameObject BindingButton(GameObject go, string mapName, UnityAction onClick)
        {
            return BindingButton(go, CanvasType.Control, mapName, onClick, true);
        }

        protected GameObject BindingButton(GameObject go, string mapName, UnityAction onClick, bool isClickScale)
        {
            return BindingButton(go, CanvasType.Control, mapName, onClick, isClickScale);
        }

        protected GameObject BindingButton(GameObject go, CanvasType type, string mapName, UnityAction onClick, bool isClickScale)
        {
            string mapPath = CanvasMgr.GetPathMap(winType.ToString(), mapName);
            switch (type)
            {
                case CanvasType.Control:
                    go = CanvasMgr.FindControl(mapPath);
                    break;
                case CanvasType.Tips:
                    go = CanvasMgr.FindTips(mapPath);
                    break;
                case CanvasType.Anim:
                    go = CanvasMgr.FindAnim(mapPath);
                    break;
                case CanvasType.Announcement:
                    go = CanvasMgr.FindAnnouncement(mapPath);
                    break;
                case CanvasType.FadeInOut:
                    go = CanvasMgr.FindFadeInOut(mapPath);
                    break;
            }

            if (go == null)
            {
                Logs.Error("BindingButton Error. {0}  {1}", mapName, mapPath);
                return null;
            }

            var btn = go.GetComponent<Button>();
            if (btn == null)
            {
                Logs.Error("BindingButton Error , no Button Compoent. {0}  {1}", mapName, mapPath);
                return null;
            }

            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(onClick);

            if (isClickScale)
            {
                var listener = go.GetComponent<EventTrigger>();
                if (listener == null)
                    listener = go.AddComponent<EventTrigger>();

                listener.triggers = new List<EventTrigger.Entry>();

                //设置按下动画
                EventTrigger.Entry onDown = new EventTrigger.Entry();
                onDown.eventID = EventTriggerType.PointerDown;
                onDown.callback = new EventTrigger.TriggerEvent();
                UnityAction<BaseEventData> downcallback = new UnityAction<BaseEventData>(OnButtonDown);
                onDown.callback.AddListener(downcallback);

                //设置抬起动画
                EventTrigger.Entry onUp = new EventTrigger.Entry();
                onUp.eventID = EventTriggerType.PointerUp;
                onUp.callback = new EventTrigger.TriggerEvent();
                UnityAction<BaseEventData> upcallback = new UnityAction<BaseEventData>(OnButtonUp);
                onUp.callback.AddListener(upcallback);

                listener.triggers.Add(onDown);
                listener.triggers.Add(onUp);
            }

            return go;
        }

        private Vector3 btnEndScale = new Vector3(0.9f, 0.9f, 1f);
        private void OnButtonDown(BaseEventData eventData)
        {
            Tweener scale = eventData.selectedObject.GetComponent<RectTransform>().DOScale(btnEndScale, 0.15f);
            scale.SetEase(Ease.InOutBack);
        }

        private void OnButtonUp(BaseEventData eventData)
        {
            Tweener scale = eventData.selectedObject.GetComponent<RectTransform>().DOScale(Vector3.one, 0.3f);
            scale.SetEase(Ease.InOutBack);
        }


        #endregion
    }
}

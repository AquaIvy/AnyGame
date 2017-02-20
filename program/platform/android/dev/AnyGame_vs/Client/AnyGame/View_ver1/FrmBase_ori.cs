using DogSE.Library.Log;
using System;
using UnityEngine;

namespace AnyGame.View
{
    /// <summary>
    /// 窗体的基类
    /// </summary>
    public abstract class FrmBase
    {
        /// <summary>
        /// 获取或设置窗体的"是否已创建"状态
        /// </summary>
        public bool IsCreated { get; protected set; }

        /// <summary>
        /// 获取或设置界面的显示状态
        /// </summary>
        public bool IsVisible { get; protected set; }

        public Game Game { get; private set; }

        /// <summary>
        /// 该窗体类在游戏世界所对应的GameObject
        /// </summary>
        public GameObject self { get; protected set; }

        /// <summary>
        /// 窗体类型
        /// </summary>
        public abstract FormType winType { get; }

        public FrmBase(Game game)
        {
            Game = game;
        }

        /// <summary>
        /// 首次显示时要先创建窗体
        /// </summary>
        //public virtual void Create()
        //{

        //}

        /// <summary>
        /// 显示窗体
        /// </summary>
        /// <param name="onShowed"></param>
        public virtual void Show(Action onShowed = null)
        {
            if (self == null)
            {
                Logs.Error("self == null  {0}  Show()", winType.ToString());
                return;
            }

            self.SetActive(true);
            IsVisible = true;

            if (onShowed != null)
            {
                onShowed();
            }

        }


        /// <summary>
        /// 关闭窗体
        /// </summary>
        public virtual void Close(Action onClosed = null)
        {
            if (self == null)
            {
                Logs.Error("self == null  {0}  Close()", winType.ToString());
                return;
            }

            self.SetActive(false);
            IsVisible = false;

            if (onClosed != null)
            {
                onClosed();
            }
        }

        /// <summary>
        /// 删除GameObject并释放资源
        /// </summary>
        public virtual void Dispose(Action onDisposed = null)
        {
            if (self == null)
            {
                Logs.Info("self == null  {0}  Dispose()", winType.ToString());
            }
            else
            {
                Game.Destroy(self);
            }

            IsCreated = false;
            IsVisible = false;
            self = null;

            if (onDisposed != null)
            {
                onDisposed();
            }
        }
    }
}

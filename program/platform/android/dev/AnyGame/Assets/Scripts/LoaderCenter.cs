using Assets.Scripts.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    /// <summary>
    /// 加载器的控制中心，
    /// 加载游戏时的各种Controller、Entity都从这里访问
    /// </summary>
    class LoaderCenter
    {
        /// <summary>
        /// 设置加载器，必须执行
        /// </summary>
        /// <param name="loader"></param>
        public static void SetLoader(Loader loader)
        {
            Loader = loader;
        }

        /// <summary>
        /// 加载器
        /// </summary>
        public static Loader Loader { get; private set; }

        /// <summary>
        /// 下载/拷贝 相关方法
        /// </summary>
        public static readonly UpdateController Update = new UpdateController();

        /// <summary>
        /// 下载/拷贝 相关事件
        /// </summary>
        public static readonly UpdateEvent Event = new UpdateEvent();
    }
}

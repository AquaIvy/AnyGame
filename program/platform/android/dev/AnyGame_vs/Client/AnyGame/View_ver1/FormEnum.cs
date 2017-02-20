using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnyGame.View
{
    /// <summary>
    /// 窗体的显示层次
    /// </summary>
    public enum FormLayer
    {
        /// <summary>
        /// 未知窗体，预留
        /// </summary>
        Unknwon = 0,

        /// <summary>
        /// 全屏，会关闭其他所有窗体
        /// </summary>
        FillScreen,

        /// <summary>
        /// 模态窗体（弹出框）
        /// </summary>
        Modal,

        /// <summary>
        /// 锚点在顶部的窗体
        /// </summary>
        AnchorTop,

        /// <summary>
        /// 锚点在底部的窗体
        /// </summary>
        AnchorBottom,
    }

    public enum FormStatus
    {
        /// <summary>
        /// 未知状态，预留
        /// </summary>
        Unknow = 0,

        /// <summary>
        /// 未创建，如果创建过一次再Close掉，则为FormStatus.Closed状态.
        /// （好吧，确实是个没什么卵用的设定）
        /// </summary>
        UnCreated,

        /// <summary>
        /// 准备打开窗体
        /// </summary>
        ReadyToShow,

        /// <summary>
        /// 正在显示中
        /// </summary>
        Showing,

        /// <summary>
        /// 准备关闭窗体
        /// </summary>
        ReadyToClose,

        /// <summary>
        /// 已经关闭.（已经释放了Texture资源，Destroy掉了gameObject）
        /// </summary>
        Closed,

        /// <summary>
        /// 隐藏
        /// </summary>
        Hide,
    }

    /// <summary>
    /// 窗体类型（名字），
    /// 每创建一个新的窗体必须在这里添加一条记录
    /// </summary>
    public enum FormType
    {
        FadeInOut = 0,
        Main,
        Fight,
        Notice,
        BigScene,
    }

}

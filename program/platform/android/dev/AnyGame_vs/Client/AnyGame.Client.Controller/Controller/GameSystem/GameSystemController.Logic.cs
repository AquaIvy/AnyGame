using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DogSE.Client.Core;
using DogSE.Client.Core.Net;
using DogSE.Client.Core.Task;
using DogSE.Library.Time;

namespace AnyGame.Client.Controller.GameSystem
{

    /// <summary>
    /// GameSystem
    /// </summary>
    public partial class GameSystemController : BaseGameSystemController
    {
        private readonly GameController controller;

        public GameSystemController(GameController gc, NetController nc)
            : this(nc)
        {
            controller = gc;
        }

        private DateTime sendTime;

        internal override void OnGetSystemTimeResult(long time)
        {
            GetSystemTimeResultEvent?.Invoke(this, new GetSystemTimeResultEventArgs
            {
                Time = time,
            });
        }

        internal override void OnNotice(string noticeContext)
        {
            NoticeEvent?.Invoke(this, new NoticeEventArgs
            {
                NoticeContext = noticeContext,
            });
        }

        internal override void OnServerStatus(string title, string context, bool isNoticeOnce, bool isMaintain)
        {
            ServerStatusEvent?.Invoke(this, new ServerStatusEventArgs
            {
                Title = title,
                Context = context,
                IsNoticeOnce = isNoticeOnce,
                IsMaintain = isMaintain,
            });
        }

        /// <summary>
        /// 获取服务端时间结果
        /// </summary>
        public event EventHandler<GetSystemTimeResultEventArgs> GetSystemTimeResultEvent;

        /// <summary>
        /// 公告
        /// </summary>
        public event EventHandler<NoticeEventArgs> NoticeEvent;

        /// <summary>
        /// 服务器状态
        /// </summary>
        public event EventHandler<ServerStatusEventArgs> ServerStatusEvent;


    }

    /// <summary>
    /// 获取服务端时间结果 【参数】
    /// </summary>
    public class GetSystemTimeResultEventArgs : EventArgs
    {
        /// <summary>
        /// 服务端时间
        /// </summary>
        public long Time { get; internal set; }
    }

    /// <summary>
    /// 公告 【参数】
    /// </summary>
    public class NoticeEventArgs : EventArgs
    {
        /// <summary>
        /// 公告内容
        /// </summary>
        public string NoticeContext { get; internal set; }
    }

    /// <summary>
    /// 服务器状态 【参数】
    /// </summary>
    public class ServerStatusEventArgs : EventArgs
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; internal set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Context { get; internal set; }

        /// <summary>
        /// 公告是否只播放一次
        /// </summary>
        public bool IsNoticeOnce { get; internal set; }

        /// <summary>
        /// 是否维护中
        /// </summary>
        public bool IsMaintain { get; internal set; }
    }



}

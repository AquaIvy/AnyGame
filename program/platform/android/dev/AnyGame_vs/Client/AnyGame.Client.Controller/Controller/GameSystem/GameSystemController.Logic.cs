using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DogSE.Client.Core;
using DogSE.Client.Core.Net;
using DogSE.Client.Core.Task;

namespace AnyGame.Client.Controller.GameSystem
{



    /// <summary>
    /// 
    /// </summary>
    public partial class GameSystemController : BaseGameSystemController
    {
        private readonly GameController controller;

        public GameSystemController(GameController gc, NetController nc)
            : this(nc)
        {
            controller = gc;
        }


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

    public class GetSystemTimeResultEventArgs : EventArgs
    {
        public Int64 Time { get; internal set; }
    }
    public class NoticeEventArgs : EventArgs
    {
        public String NoticeContext { get; internal set; }
    }
    public class ServerStatusEventArgs : EventArgs
    {
        public String Title { get; internal set; }
        public String Context { get; internal set; }
        public Boolean IsNoticeOnce { get; internal set; }
        public Boolean IsMaintain { get; internal set; }
    }



}

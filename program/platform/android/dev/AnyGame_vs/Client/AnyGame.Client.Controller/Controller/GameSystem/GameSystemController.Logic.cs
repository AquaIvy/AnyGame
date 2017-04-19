

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
        }


        internal override void OnNotice(string noticeContext)
        {
        }


        internal override void OnServerStatus(string title, string context, bool isNoticeOnce, bool isMaintain)
        {
        }




    }






}



using AnyGame.LoginPlugin;
using AnyGame.UI;
using AnyGame.View.Components;
using UnityEngine;

namespace AnyGame.View.Forms.Login
{
    partial class FrmLogin
    {
        private void InitForm()
        {
            SetBackground("ui/common/base_26.png");
            this.size = new Vector2(GlobalInfo.width, GlobalInfo.height);
            this.border = new Vector4(10, 10, 10, 10);

            node1 = new UINode(280, -400);
            AddChild(node1);

            var txt1 = new UIText("账号", 120, 675, 24, Color.black);
            node1.AddChild(txt1);

            inputAccount = new UIInputField("ui/common/di_11.png", 200, 655);
            inputAccount.border = new Vector4(10, 10, 10, 10);
            inputAccount.size = new Vector2(350, 70);
            inputAccount.textContent.y = 24;
            node1.AddChild(inputAccount);

            var txt2 = new UIText("密码", 120, 780, 24, Color.black);
            node1.AddChild(txt2);

            inputPwd = new UIInputField("ui/common/di_11.png", 200, 755);
            inputPwd.border = new Vector4(10, 10, 10, 10);
            inputPwd.size = new Vector2(350, 70);
            inputPwd.textContent.y = 24;
            node1.AddChild(inputPwd);

            btnLogin = new UIButton("ui/button/button_09.png", GlobalInfo.harfWidth, GlobalInfo.height - 200, "登录", 30);
            AddChild(btnLogin);

            btnLogoff = new UIButton("ui/button/button_09.png", 1148, 70, "注销", 30);
            AddChild(btnLogoff);

            //AddChild(new UIImage("ui/kabao/mofa_00.png", 0, 0));
            //AddChild(new UIImage("ui/kabao/mofa_0.png", GlobalInfo.width, 0, UIUtils.MiddleCenter));
        }

        /// <summary>
        /// 创建一个默认服务器
        /// </summary>
        private void InitNode2()
        {
            if (node2 != null) { return; }

            node2 = new UINode();
            AddChild(node2);

            curSelectServer = new CGameServer(curServer, 640, 360);
            node2.AddChild(curSelectServer);

            curSelectServer.OnClick += LastLoginServer_OnClick;
        }

        /// <summary>
        /// 创建所有服务器列表
        /// </summary>
        private void InitNode3()
        {
            if (node3 != null) { return; }

            node3 = new UINode();
            AddChild(node3);

            var imgScrollBG = new UIImage("ui/common/base_01.png", 480, 60, UIUtils.UpperLeft, UIUtils.Border10);
            imgScrollBG.size = new Vector2(300, 400);
            node3.AddChild(imgScrollBG);

            var scroll = new UIScrollView(null, 0, 0, 300, 400, Layout.Vertical, null, null, true);
            imgScrollBG.AddChild(scroll);

            for (int i = 0; i < allServers.Count; i++)
            {
                var x = 150;
                var y = 60 * i + 40;
                var serv = new CGameServer(allServers[i], x, y);
                scroll.AddChild(serv);
                serv.tag = allServers[i];

                serv.OnClick += Serv_OnClick;
            }
        }

        private void Serv_OnClick(UIElement sender, System.EventArgs e)
        {
            GameServer data = (GameServer)sender.tag;

            SelectServer(data);

            node3.visible = false;
            node2.visible = true;
        }

        UINode node1 = null;
        UINode node2 = null;
        UINode node3 = null;

        UIInputField inputAccount = null;
        UIInputField inputPwd = null;

        UIButton btnLogin = null;
        UIButton btnLogoff = null;

        CGameServer curSelectServer = null;

    }
}

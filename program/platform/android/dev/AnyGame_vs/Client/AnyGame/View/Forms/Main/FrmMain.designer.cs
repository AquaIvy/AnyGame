using AnyGame.View;
using AnyGame.View.Components;
using UnityEngine;

namespace AnyGame.View.Forms.Main
{
    partial class FrmMain
    {
        protected void InitForm()
        {
            //SetBackground("aztec/common/base_djs.png");
            //this.size = new Vector2(GlobalInfo.width, GlobalInfo.height);
            //this.border = UIUtils.Border20;

            btnBag = new UIButton("aztec/icon/mainicon_card.png", GlobalInfo.Width - 100, GlobalInfo.Height - 150);
            AddChild(btnBag);

            int startx = 600;
            int dis = 100;
            btnSpell = new UIButton("ui/icon/function_1.png", startx + dis * 0, GlobalInfo.Height - 50);
            AddChild(btnSpell);

            btnTalisman = new UIButton("ui/icon/function_2.png", startx + dis * 1, btnSpell.y);
            AddChild(btnTalisman);

            btnTask = new UIButton("ui/icon/function_3.png", startx + dis * 2, btnSpell.y);
            AddChild(btnTask);

            btnZhenFa = new UIButton("ui/icon/function_4.png", startx + dis * 3, btnSpell.y);
            AddChild(btnZhenFa);
        }

        UIButton btnBag = null;             //背包
        UIButton btnSpell = null;           //技能
        UIButton btnTalisman = null;        //法宝
        UIButton btnTask = null;            //任务
        UIButton btnZhenFa = null;          //阵法

        UIButton btnPlayer = null;
    }
}

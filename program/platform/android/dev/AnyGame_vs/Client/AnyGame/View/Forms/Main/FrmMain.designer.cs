using AnyGame.UI;
using AnyGame.View.Components;
using UnityEngine;

namespace AnyGame.View.Forms.Main
{
    partial class FrmMain
    {
        protected void InitForm()
        {
            //SetBackground("ui/common/base_djs.png");
            //this.size = new Vector2(GlobalInfo.width, GlobalInfo.height);
            //this.border = UIUtils.Border20;

            btnBag = new UIButton("ui/icon/mainicon_card.png", GlobalInfo.width - 100, GlobalInfo.height - 100);
            AddChild(btnBag);
        }

        UIButton btnBag = null;
        UIButton btnPlayer = null;
    }
}

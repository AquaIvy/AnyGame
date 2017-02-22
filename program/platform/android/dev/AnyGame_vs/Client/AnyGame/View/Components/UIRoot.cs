using AnyGame.View.Forms;
using System.Collections.Generic;
using System.Linq;

namespace AnyGame.View.Components
{
    class UIRoot
    {
        private static UICanvas uiroot = null;
        private static List<FrmBase> allFrmBase = new List<FrmBase>();

        private static FrmBase curBackgroundForm = null;
        private static List<FrmBase> allPopup = new List<FrmBase>();

        private static UINode NodeBackground = null;
        private static UINode NodeBanner = null;
        private static UINode NodePopup = null;
        private static UINode NodeEffect = null;

        public static void Show(FrmBase frm)
        {
            if (frm.Type == FrmType.Background)
            {
                Show(frm, "background");
            }
            else if (frm.Type == FrmType.Banner)
            {
                Show(frm, "banner");
            }
            else if (frm.Type == FrmType.Popup)
            {
                Show(frm, "popup");
            }
            else if (frm.Type == FrmType.Effect)
            {
                Show(frm, "effect");
            }
        }

        public static void Show(FrmBase frm, string layer)
        {

            if (uiroot == null)
            {
                uiroot = UICanvas.Find("Canvas");

                NodeBackground = new UINode("NodeBackground");
                uiroot.AddChild(NodeBackground);

                NodeBanner = new UINode("NodeBanner");
                uiroot.AddChild(NodeBanner);

                NodePopup = new UINode("NodePopup");
                uiroot.AddChild(NodePopup);

                NodeEffect = new UINode("NodeEffect");
                uiroot.AddChild(NodeEffect);
            }

            UINode node = null;
            if (layer == "background")
            {
                node = NodeBackground;
            }
            else if (layer == "banner")
            {
                node = NodeBanner;
            }
            else if (layer == "popup")
            {
                node = NodePopup;
            }
            else if (layer == "effect")
            {
                node = NodeEffect;
            }

            if (frm.Type == FrmType.Background)
            {
                if (curBackgroundForm != null)
                {
                    var find = allFrmBase.FirstOrDefault(o => o.Type == FrmType.Background);
                    allFrmBase.Remove(find);
                    curBackgroundForm.Dispose();
                }
                curBackgroundForm = frm;

                frm.SetXY(-GlobalInfo.width / 2f, -GlobalInfo.height / 2f);
            }

            if (frm.Type == FrmType.Popup)
            {
                allPopup.Add(frm);
            }

            allFrmBase.Add(frm);
            node.AddChild(frm);
        }

        public static int GetFormCount()
        {
            return allFrmBase.Count;
        }
    }
}

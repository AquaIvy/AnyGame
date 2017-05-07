using AnyGame.View.Forms;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

        public static void Init()
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
        }

        public static void Show(FrmBase frm)
        {
            if (frm.Layer == FrmLayer.Background)
            {
                Show(frm, "background");
            }
            else if (frm.Layer == FrmLayer.Banner)
            {
                Show(frm, "banner");
            }
            else if (frm.Layer == FrmLayer.Popup)
            {
                Show(frm, "popup");
            }
            else if (frm.Layer == FrmLayer.Effect)
            {
                Show(frm, "effect");
            }
        }

        public static void Show(FrmBase frm, string layer)
        {
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

            if (frm.Layer == FrmLayer.Background)
            {
                if (curBackgroundForm != null)
                {
                    var find = allFrmBase.FirstOrDefault(o => o.Layer == FrmLayer.Background);
                    allFrmBase.Remove(find);
                    curBackgroundForm.Dispose();
                }
                curBackgroundForm = frm;

                frm.SetXY(-GlobalInfo.HarfWidth, -GlobalInfo.HarfHeight);
            }

            if (frm.Layer == FrmLayer.Popup)
            {
                frm.SetXY(-frm.Width / 2f, -frm.Height / 2f);

                var bgpanel = new UIImage("aztec/common/frame_s_0.png", frm.HarfWidth, frm.HarfHeight, UIUtils.MiddleCenter, UIUtils.Border10);
                bgpanel.Size = UIUtils.Screen;
                bgpanel.Alpha = 0.5f;

                frm.AddChild(bgpanel);
                bgpanel.SetSiblingIndex(0);

                allPopup.Add(frm);
            }

            Game.instance.AddForms(frm);

            allFrmBase.Add(frm);
            node.AddChild(frm);
        }

        public static int GetFormCount()
        {
            return allFrmBase.Count;
        }
    }
}

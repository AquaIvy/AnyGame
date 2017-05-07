using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnyGame.View.Forms.Bags
{
    partial class FrmBag : FrmBase
    {
        public override FrmType Type { get { return FrmType.Bag; } }
        public override FrmLayer Layer { get { return FrmLayer.Popup; } }

        public FrmBag()
            : base("FrmBag")
        {
            InitForm();

            btnClose.OnClick += BtnClose_OnClick;

            SetData();
        }

        private void SetData()
        {
            
        }

        private void BtnClose_OnClick(Components.UIElement sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}

using Syncfusion.Windows.Forms.Tools;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Konto.Core.Shared.Libs
{
    public partial class KontoTextBoxExt : TextBoxExt
    {
        private bool _EnterMoveNextControl = true;
        // private Color _FocusPreviousBackColor;
        private static Font _defaultFont =
             new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));


        public KontoTextBoxExt()
        {
            Font = _defaultFont;
            Style = theme.Metro;
            UseBorderColorOnFocus = true;
        }
        public bool EnterMoveNextControl
        {
            get { return _EnterMoveNextControl; }
            set { _EnterMoveNextControl = value; Invalidate(); }
        }

        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                if (value == this.Font)
                    base.Font = _defaultFont;
                else
                    base.Font = value;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            // base.OnKeyDown(e);
            if (this._EnterMoveNextControl && ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return)))
            {
                this.ProcessDialogKey(Keys.Tab);
                e.Handled = false;
            }
        }
        private bool ShouldSerializeFont()
        {
            return (!Font.Equals(_defaultFont));
        }

        protected override void OnEnter(EventArgs e)
        {
            this.SelectAll();
        }
    }
}

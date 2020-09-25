using Syncfusion.Windows.Forms.Tools;
using Syncfusion.WinForms.ListView;
using System;
using System.Windows.Forms;

namespace Konto.Core.Shared.Libs
{
    public partial class KontoComboBoxEx : SfComboBox
    {
        private bool _EnterMoveNextControl = true;
        
        private static System.Drawing.Font _defaultFont =
        new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        

        public bool EnterMoveNextControl
        {
            get { return _EnterMoveNextControl; }
            set { _EnterMoveNextControl = value; Invalidate(); }
        }

        protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
        {
            // base.OnKeyDown(e);
            if (this._EnterMoveNextControl && (e.KeyCode == Keys.Enter))
            {
               // this.HidePopup();
                //this.ClosePopup();
                if (this.SelectedIndex == -1 && !string.IsNullOrEmpty(this.Text)) return;

                this.ProcessDialogKey(Keys.Tab);

                e.Handled = true;
            }
        }


        protected override void OnEnter(EventArgs e)
        {
            //  base.OnEnter(e);
            //   if (this.FocusedBackColor != null)
            // {
            //   this._FocusPreviousBackColor = this.BackColor;
            // this.BackColor = this.FocusedBackColor;
            //}
          //  this.SelectionStart = 0;
           // this.SelectionLength = this.Text.Length;
           // this.ShowPopup();

        }

        //public void ClosePopup()
        //{
        //    this.HidePopup();
        //}

    }
}

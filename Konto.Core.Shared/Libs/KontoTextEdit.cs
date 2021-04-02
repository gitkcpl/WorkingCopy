using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Core.Shared.Libs
{
    public partial class KontoTextEdit : TextEdit
    {
        public KontoTextEdit()
        {
            InitializeComponent();
        }

        public KontoTextEdit(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);

            BeginInvoke(new MethodInvoker(() =>
            {
                this.SelectionStart = 0;
                this.SelectionLength = this.Text.Length;
            }));
        }
    }
}

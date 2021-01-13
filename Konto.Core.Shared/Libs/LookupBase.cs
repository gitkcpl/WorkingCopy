using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Core.Shared.Libs
{
    public partial class LookupBase : UserControl
    {
       

        public object PrimaryKey { get; set; }
        public string LookupTitle { get; set; }

        public event EventHandler SelectedValueChanged;
        public LookupBase()
        {
            InitializeComponent();
        }
        protected void OnSelectedValueChanged(EventArgs e)
        {
            if (  this.SelectedValue == null) this.SelectedText = string.Empty;
            EventHandler handler = SelectedValueChanged;
            handler?.Invoke(this, e);
        }

        public object SelectedValue
        {
            get { return this.PrimaryKey; }
            set
            {
                if (this.PrimaryKey !=  value)
                {
                    this.PrimaryKey = value;
                    OnSelectedValueChanged(new EventArgs());
                }
            }
        }

        public string SelectedText { get; set; }
        public bool RequiredField { get; set; }
    }
}

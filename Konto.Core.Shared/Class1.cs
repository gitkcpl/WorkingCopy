using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Core.Shared
{
    public class RowInfo
    {
        public RowInfo(GridView view, int rowHandle)
        {
            this.RowHandle = rowHandle;
            this.View = view;
        }
        public GridView View;
        public int RowHandle;
    }
}

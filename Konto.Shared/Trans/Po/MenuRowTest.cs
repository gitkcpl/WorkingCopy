using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Shared.Trans.Po
{
    class RowInfo
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

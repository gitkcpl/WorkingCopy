using AutoMapper;
using AutoMapper.QueryableExtensions;
using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Masters.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Konto.Shared.Masters.Voucher
{
    public partial class VoucherLkpWindow : LookupForm
    {
        List<VoucherLookupDto> _modelList = new List<VoucherLookupDto>();
        public int VTypeId { get; set; }
        public VoucherLkpWindow()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.Voucher_Lookup;
            this.FormClassName = "Konto.Shared.Masters.Voucher.VoucherIndex";
            this.AsemblyName = "Konto.Shared";
        }
        public override void LoadData()
        {
            base.LoadData();

            var vtype = "Y";
            if (this.VTypeId == 0)
            {
                vtype = "N";
            }

            using (var _db = new KontoContext())
            {
                _modelList = (from vc in _db.Vouchers
                              join st in _db.VchSetups on vc.Id equals st.VoucherId
                              where (this.VTypeId > 0 && vc.VTypeId == VTypeId || "N" == vtype) && st.CompId == KontoGlobals.CompanyId 
                              && vc.IsDeleted == false && vc.IsActive
                              orderby vc.VoucherName
                              select new VoucherLookupDto()
                              {
                                  DisplayText = vc.VoucherName,
                                  VTypeId = vc.VTypeId,
                                  SortName = vc.SortName,
                                  AccId = vc.BookAcId,
                                  Email = st.EmailAfterSave,
                                  FixBook = st.BookFix,
                                  Increment = st.Increment,
                                  InvoiceHeading = st.InvoiceHeading,
                                  LastSerial = st.Last_Serial,
                                  Mask = st.Serial_Mask,
                                  Max = st.Max_Value,
                                  PreFillZero = st.PreFillZero,
                                  PrintAfterSave = st.PrintAfterSave,
                                  RefVoucherId = vc.RefVoucherId,
                                  Reset = st.FyReset,
                                  Sms = st.SmsAfterSave,
                                  StartFrom = st.StartFrom,
                                  Width = st.VchWidth,
                                  Id = vc.Id,ManualSeries = st.ManualSeries
                              }
                    ).ToList();

            }

            customGridControl1.DataSource = _modelList;

            if (string.IsNullOrEmpty(this.GridLayoutFileName) || this.KontoView == null) return;

            KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName, this.KontoView);

            this.ActiveControl = customGridControl1;


        }

        private void AreaLkpWindow_Shown(object sender, EventArgs e)
        {
            if (this.SelectedValue <= 0) return;
            var item = _modelList.FirstOrDefault(x => x.Id == this.SelectedValue);
            var index = _modelList.IndexOf(item);
            if (index >= 0)
            {
                customGridView1.FocusedRowHandle = index;
            }
        }
    }
}

using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Konto.Shared.Masters.Acc
{
    public partial class AddressLkpWindow : LookupForm
    {
        List<AddressLookupDto> _modelList = new List<AddressLookupDto>();
        public int DelvAccId { get; set; }
       
        public AddressLkpWindow()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.Address_Lookup_List_Layout;
            this.FormClassName = "Konto.Shared.Masters.Acc.AddressView";
            this.AsemblyName = "Konto.Shared";
        }
        public override void LoadData()
        {
            base.LoadData();
            this.RefId = this.DelvAccId;

            using (KontoContext db = new KontoContext())
            {
                
                var spcol = db.SpCollections.FirstOrDefault(k => k.Id ==
                                       (int)SpCollectionEnum.DeliveryAddressList);
               

                if (spcol == null)
                {
                    _modelList = 
                        db.Database.SqlQuery<AddressLookupDto>(
                     "dbo.DeliveryAddressList @AccountId={0}",
                      this.DelvAccId).ToList();

                }
                else
                {
                    _modelList = 
                        db.Database.SqlQuery<AddressLookupDto>(
                     spcol.Name + " @AccountId={0}",
                    this.DelvAccId).ToList();
                }

               
            }

            customGridControl1.DataSource = _modelList;

            if (string.IsNullOrEmpty(this.GridLayoutFileName) || this.KontoView == null) return;

            KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName, this.KontoView);

            this.ActiveControl = customGridControl1;
            this.customGridView1.FocusedColumn = this.customGridView1.Columns[1];

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
            customGridView1.FocusedColumn = customGridView1.VisibleColumns[0];
            
        }

        public override void Ok()
        {
            var adr = this.SelectedItem as AddressLookupDto;
            if (adr != null)
                this.SelectedTex = adr.Address;

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Popup;
using DevExpress.Utils;
using System.Windows.Forms;
using Konto.App.Shared;
using Konto.Data.Models.Masters.Dtos;
using Konto.Data;

namespace Konto.Shared.Masters.Item
{
    [UserRepositoryItem("RegisterItemLookup")]
    public class RepositoryItemItemLookup : RepositoryItemButtonEdit
    {
        static RepositoryItemItemLookup()
        {
            RegisterItemLookup();
            
        }

        public const string CustomEditName = "ItemLookup";

        public RepositoryItemItemLookup()
        {
           
            _SelectedValue = null;
            _SelectedText = string.Empty;
        }

        public override string EditorTypeName => CustomEditName;

        public static void RegisterItemLookup()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(ItemLookup), typeof(RepositoryItemItemLookup), typeof(ItemLookupViewInfo), new ItemLookupPainter(), true, img));
        }



        private object _SelectedValue;
        public object SelectedValue
        {
            get { return _SelectedValue; }
            set
            {
                if (_SelectedValue != value)
                {
                    _SelectedValue = value;
                    
                    OnPropertiesChanged();
                }
            }
        }
        private ProductLookupDto _LkpDto;
        public ProductLookupDto LkpDto
        {
            get { return _LkpDto; }
            set
            {
                if (_LkpDto != value)
                {
                    _LkpDto = value;
                    OnPropertiesChanged();
                }
            }
        }

        private string _SelectedText;
        public string DisplayText
        {
            get { return _SelectedText; }
            set
            {
                if (_SelectedText != value)
                {
                    _SelectedText = value;
                    OnPropertiesChanged();
                }
            }
        }
        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                RepositoryItemItemLookup source = item as RepositoryItemItemLookup;
                if (source == null) return;
                //
            }
            finally
            {
                EndUpdate();
            }
        }
    }

    [ToolboxItem(true)]
    public class ItemLookup : ButtonEdit
    {
        static ItemLookup()
        {
            RepositoryItemItemLookup.RegisterItemLookup();
        }
       
        public ItemLookup()
        {
           // this.Properties.Buttons[0].Shortcut = new KeyShortcut(System.Windows.Forms.Keys.F1);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemItemLookup Properties => base.Properties as RepositoryItemItemLookup;

        public override string EditorTypeName => RepositoryItemItemLookup.CustomEditName;

        
        protected override void OnClickButton(DevExpress.XtraEditors.Drawing.EditorButtonObjectInfoArgs buttonInfo)
        {
            ShowList();
            base.OnClickButton(buttonInfo);
        }

        private void ShowList()
        {
            var frm = new ProductLkpWindow
            {
                SelectedTex = this.Properties.DisplayText,
                SelectedValue = Convert.ToInt32(this.Properties.SelectedValue),
                Tag = MenuId.Product_Master
            };
            frm.ShowDialog(this.Parent.Parent.Parent);
            if (frm.DialogResult == DialogResult.OK)
            {
                
               
                this.Properties.SelectedValue = frm.SelectedValue;
               
                this.Properties.LkpDto = frm.customGridView1.GetRow(frm.customGridView1.FocusedRowHandle) as ProductLookupDto;
                this.Properties.DisplayText = this.Properties.LkpDto.ProductName;
                this.Text = this.Properties.LkpDto.ProductName;
            }
            this.Parent.SelectNextControl(this, true, true, true, false);
        }

    }

    public class ItemLookupViewInfo : ButtonEditViewInfo
    {
        public ItemLookupViewInfo(RepositoryItem item) : base(item)
        {
        }
    }

    public class ItemLookupPainter : ButtonEditPainter
    {
        public ItemLookupPainter()
        {
        }
    }
}

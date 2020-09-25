using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using DevExpress.LookAndFeel.Helpers;
using DevExpress.Utils;
using DevExpress.Utils.Controls;
using DevExpress.Utils.Drawing;
using DevExpress.Utils.Menu;
using DevExpress.Utils.Serializing;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Dragging;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Base.Handler;
using DevExpress.XtraGrid.Views.Base.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.Drawing;
using DevExpress.XtraGrid.Views.Grid.Handler;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using Konto.App.Shared;

namespace Konto.Core.Shared.Libs
{
    public class CustomGridView : GridView
    {

       
        public CustomGridView() : base() { }
        protected internal virtual void SetGridControlAccessMetod(GridControl newControl) { SetGridControl(newControl); }
        protected override string ViewName { get { return "CustomGridView"; } }
        protected override GridColumnCollection CreateColumnCollection() { return new CustomGridColumnCollection(this); }
        protected override GridOptionsCustomization CreateOptionsCustomization() { return new CustomGridOptionsCustomization(); }
        [Description("Provides access to the View's customization options."), Category("Options"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        XtraSerializableProperty(XtraSerializationVisibility.Content, XtraSerializationFlags.DefaultValue)]
        public new CustomGridOptionsCustomization OptionsCustomization { get { return base.OptionsCustomization as CustomGridOptionsCustomization; } }
        protected virtual internal EmbeddedLookAndFeel GetLookAndFeel() { return ElementsLookAndFeel; }
        
        GridColumn ActiveCol;
        ///<summary>
        ///</summary>
        public ControlNavigator nav;
        private PrintingSystem _ps;

        protected override void RaisePopupMenuShowing(PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == GridMenuType.Column)
            {
                var menu1 = (GridViewColumnMenu)e.Menu;
                if (menu1.Column != null)
                {
                    menu1.Items.Add(HeadingChanger(menu1.Column, "Change Column Heading"));

                    menu1.Items.Add(FixedCol(menu1.Column, "Fixed Column-->Left"));
                    menu1.Items.Add(FixedCol(menu1.Column, "Fixed Column-->Right"));

                    //  if (this.AllowPrint)
                      menu1.Items.Add(PrintPreview(menu1.Column, "Print Preview"));



                    // if (this.AllowExport)
                      menu1.Items.Add(ExportToExcel(menu1.Column, "Export To Excel"));


                }
            }
        }
        public DXMenuItem PrintPreview(GridColumn column, string caption)
        {
            var item = new DXMenuItem("Print Preview", PrintPrev_Click);

            ActiveCol = column;
            return item;
        }
        public DXMenuItem FixedCol(GridColumn column, string caption)
        {
            var item = new DXMenuItem(caption, FixedCol_Click);

            ActiveCol = column;
            return item;
        }
        public DXMenuItem HeadingChanger(GridColumn column, string caption)
        {
            var item = new DXMenuItem("Change Column Heading", OnChangeClick);

            ActiveCol = column;
            return item;
        }
        public void OnChangeClick(object sender, EventArgs e)
        {

            var def = ActiveCol.Caption;
            InputBoxResult name = InputBox.Show("Enter New Heading", "Modify Column Heading", def);
            if (String.IsNullOrWhiteSpace(name.Text))
            {
                name.Text = def;
            }

            var info = new MenuInfo(ActiveCol, name.Text);


            info.Column.Caption = name.Text;
        }
        public void FixedCol_Click(object sender, EventArgs e)
        {

            var item = (DXMenuItem)sender;
            var info = new MenuInfo(ActiveCol, Name);
            if (info == null)
            {
                return;
            }
            if (item.Caption == "Fixed Column-->Left")
                info.Column.Fixed = info.Column.Fixed == FixedStyle.None ? FixedStyle.Left : FixedStyle.None;
            else if (item.Caption == "Fixed Column-->Right")
                info.Column.Fixed = info.Column.Fixed == FixedStyle.None ? FixedStyle.Right : FixedStyle.None;

        }
        public void PrintPrev_Click(object sender, EventArgs e)
        {
            _ps = new PrintingSystem();
            var pl = new PrintableComponentLink();
            pl.CreateMarginalHeaderArea += this.Link1CreateMarginalHeaderArea;
            _ps.Links.AddRange(new object[] { pl });
            pl.Component = this.GridControl;
            pl.ShowPreview();

        }
        private void Link1CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            CreatePageHeader(e.Graph);
        }
        private Brick CreateBrick(string format, PageInfo info)
        {
            PageInfoBrick brick = new PageInfoBrick();
            brick.Format = format;
            brick.PageInfo = info;
            brick.Sides = BorderSide.None;
            if (format.Contains("\n"))
                brick.Rect = new RectangleF(0, 0, 0, 54);
            else
                brick.Rect = new RectangleF(0, 0, 0, 18);
            brick.Font = new Font("Arial", 10, FontStyle.Bold);
            // brick.Alignment = BrickAlignment.Center;
            brick.AutoWidth = true;
            return brick;
        }
        private PageTableBrick CreateTable(Brick[] bricks)
        {
            PageTableBrick table = new PageTableBrick();
            foreach (Brick brick in bricks)
            {
                TableRow row = table.Rows.AddRow();
                row.Bricks.Add(brick);
            }
            table.UpdateSize();
            return table;
        }

        void CreatePageHeader(BrickGraphics gr)
        {
            Brick[] bricks = new Brick[] {CreateBrick(KontoGlobals.ComputerName, PageInfo.None),
                                                 CreateBrick(KontoGlobals.ComputerName, PageInfo.None)};
            gr.Font = _ps.Graph.DefaultFont;
            PageTableBrick table = CreateTable(bricks);
            table.LineAlignment = BrickAlignment.Far;
            gr.DrawBrick(table);

        }
        public class MenuInfo
        {

            ///<summary>
            ///</summary>
            ///<param name="column"></param>
            ///<param name="name"></param>
            public MenuInfo(GridColumn column, string name)
            {
                this.Column = column;
                this.caption = name;
            }
            public string caption;
            public GridColumn Column;
        }
        public void Export_To_Excel(object sender, EventArgs e)
        {

            var gv = (GridView)this;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.RestoreDirectory = true;
            sfd.FileName = string.Format(@"{0}.xlsx", DateTime.Now.Ticks);
            sfd.ShowDialog();
            gv.ExportToXlsx(sfd.FileName);
            MessageBox.Show(@"File Exported Sucessfully");
            Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
        }

        public DXMenuItem ExportToExcel(GridColumn column, string caption)
        {
            var item = new DXMenuItem("Export To Excel", Export_To_Excel);

            ActiveCol = column;
            return item;
        }

     
        //protected virtual void PopulateHideEdit()
        protected virtual bool GetColumnHideState(CustomGridColumn column)
        {
            return column.OptionsColumn.AllowQuickHide && OptionsCustomization.AllowQuickHideColumns;
        }
        protected virtual bool GetColumnMoveState(CustomGridColumn column)
        {
            return column.OptionsColumn.AllowMove && OptionsCustomization.AllowColumnMoving;
        }
    }
    public  class CustomGridControl : GridControl
    {
        public CustomGridControl() : base() { }
        protected override void RegisterAvailableViewsCore(InfoCollection collection)
        {
            base.RegisterAvailableViewsCore(collection);
            collection.Add(new CustomGridInfoRegistrator());
        }
        protected override BaseView CreateDefaultView() { return CreateView("CustomGridView"); }
    }
    public class CustomGridInfoRegistrator : GridInfoRegistrator
    {
        public CustomGridInfoRegistrator() : base() { }
        public override string ViewName { get { return "CustomGridView"; } }
        public override BaseViewHandler CreateHandler(BaseView view) { return new CustomGridHandler(view as GridView); }
        public override BaseViewPainter CreatePainter(BaseView view) { return new CustomGridPainter(view as GridView); }
        public override BaseViewInfo CreateViewInfo(BaseView view) { return new CustomGridViewInfo(view as GridView); }
        public override BaseView CreateView(GridControl grid)
        {
            CustomGridView view = new CustomGridView();
            view.SetGridControlAccessMetod(grid);
            return view;
        }
    }
    public class CustomGridViewInfo : GridViewInfo
    {
        public QuickCustomisationIconStatus QuickCustomisationIconStatus;
        static int QuickCustomisationWidth = 10, QuickCustomisationHeight = 11, QuickCustomisationSpacing = 2;
        Rectangle quickCustumisationBounds;
        public CustomGridViewInfo(GridView gridView)
            : base(gridView)
        {
            quickCustumisationBounds = Rectangle.Empty;
            QuickCustomisationIconStatus = QuickCustomisationIconStatus.Hidden;
        }
        public virtual Rectangle QuickCustomisationBounds
        {
            get
            {
                Rectangle rec = new Rectangle();
                rec.Location = new Point(ColumnsInfo[0].Bounds.Right - QuickCustomisationWidth - QuickCustomisationSpacing,
                    ColumnsInfo[0].Bounds.Top + QuickCustomisationSpacing);
                rec.Size = new Size(QuickCustomisationWidth, QuickCustomisationHeight);
                return rec;
            }
        }
        public virtual bool IsQuickCustomisationButton(Point p) { return QuickCustomisationBounds.Contains(p); }
        public bool AllowQuickCustomisation { get { return ((CustomGridView)View).OptionsCustomization.AllowQuickCustomisation; } }

        public virtual QuickCustomizationIcon QuickCustomisationIcon { get { return ((CustomGridView)View).OptionsCustomization.QuickCustomizationIcons; } }
    }
    public class CustomGridPainter : GridPainter
    {
        public CustomGridPainter(GridView view) : base(view) { }
        protected override void DrawIndicatorCore(GridViewDrawArgs e, IndicatorObjectInfoArgs info, int rowHandle, IndicatorKind kind)
        {
            base.DrawIndicatorCore(e, info, rowHandle, kind);
            DrawQuickCustomisationIcon(e, info, kind);
        }
        protected virtual void DrawQuickCustomisationIcon(GridViewDrawArgs e, IndicatorObjectInfoArgs info, IndicatorKind kind)
        {
            if (kind == DevExpress.Utils.Drawing.IndicatorKind.Header && ((CustomGridViewInfo)e.ViewInfo).QuickCustomisationIconStatus != QuickCustomisationIconStatus.Hidden)
                DrawQuickCustomisationIconCore(e, info, ((CustomGridViewInfo)e.ViewInfo).QuickCustomisationIcon, ((CustomGridViewInfo)e.ViewInfo).QuickCustomisationBounds, ((CustomGridViewInfo)e.ViewInfo).QuickCustomisationIconStatus);
        }
        protected virtual void DrawQuickCustomisationIconCore(GridViewDrawArgs e, IndicatorObjectInfoArgs info, QuickCustomizationIcon icon, Rectangle bounds, QuickCustomisationIconStatus status)
        {
            Rectangle patchedRec = new Rectangle(bounds.X + 1, bounds.Y, bounds.Width - 1, bounds.Height);
            GridColumnInfoArgs args = new GridColumnInfoArgs(e.Cache, e.ViewInfo.ColumnsInfo[0].Column);
            args.Cache = e.Cache;
            args.Bounds = patchedRec;
            ((HeaderObjectInfoArgs)args).HeaderPosition = HeaderPositionKind.Center;
            if (status == QuickCustomisationIconStatus.Hot)
                ((HeaderObjectInfoArgs)args).State = ObjectState.Hot;
            ElementsPainter.Column.DrawObject(args);

            if (icon.Image != null)
            {
                Rectangle rec = new Rectangle();
                rec.Location = new Point(bounds.Left + 1, bounds.Top + 1);
                rec.Size = new Size(bounds.Width - 2, bounds.Height - 2);
                ImageAttributes attr = new ImageAttributes();
                attr.SetColorKey(icon.TransperentColor, icon.TransperentColor);
                e.Graphics.DrawImage(icon.Image, rec, 0, 0, icon.Image.Width, icon.Image.Height, GraphicsUnit.Pixel, attr);
            }
        }
    }
    public class CustomGridHandler : GridHandler
    {
        public CustomGridHandler(GridView gridView) : base(gridView) { }
        protected override GridDragManager CreateDragManager() { return new CustomGridDragManager(View); }
        
        protected override bool OnMouseMove(MouseEventArgs ev)
        {
            DXMouseEventArgs e = DXMouseEventArgs.GetMouseArgs(ev);
            Point p = new Point(e.X, e.Y);
            UpdateQuickCustumisationIconState(p);
            return base.OnMouseMove(ev);
        }
        protected virtual void UpdateQuickCustumisationIconState(Point p)
        {
            CustomGridViewInfo vi = ViewInfo as CustomGridViewInfo;
            if (!vi.AllowQuickCustomisation) return;
            GridHitInfo hi = ViewInfo.CalcHitInfo(p);
            if (hi.HitTest == GridHitTest.ColumnButton)
            {
                if (vi.IsQuickCustomisationButton(p))
                {
                    if (vi.QuickCustomisationIconStatus != QuickCustomisationIconStatus.Hot)
                    {
                        vi.QuickCustomisationIconStatus = QuickCustomisationIconStatus.Hot;
                        ViewInfo.View.Invalidate();
                    }
                    return;
                }
                if (vi.QuickCustomisationIconStatus != QuickCustomisationIconStatus.Enabled)
                {
                    vi.QuickCustomisationIconStatus = QuickCustomisationIconStatus.Enabled;
                    ViewInfo.View.Invalidate();
                }
            }
            else
                if (vi.QuickCustomisationIconStatus != QuickCustomisationIconStatus.Hidden)
            {
                vi.QuickCustomisationIconStatus = QuickCustomisationIconStatus.Hidden;
                ViewInfo.View.Invalidate();
            }
        }
    }
    public class CustomGridDragManager : GridDragManager
    {
        public CustomGridDragManager(GridView view) : base(view) { }
        protected override PositionInfo CalcColumnDrag(GridHitInfo hit, GridColumn column)
        {
            PositionInfo patchedPI = new PositionInfo();
            patchedPI = base.CalcColumnDrag(hit, column);
            if (patchedPI.Index == HideElementPosition && patchedPI.Valid)
            {
                CustomGridColumn col = column as CustomGridColumn;
                if (col != null)
                    if (!col.OptionsColumn.AllowQuickHide)
                    {
                        patchedPI = new PositionInfo();
                        patchedPI.Valid = false;
                    }
            }
            return patchedPI;
        }
    }
    public class CustomGridColumn : GridColumn
    {
        public CustomGridColumn() : base() { }
        protected override OptionsColumn CreateOptionsColumn() { return new CustomOptionsColum(); }
        [Description("Provides access to the column's options."), Category("Options"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        XtraSerializableProperty(XtraSerializationVisibility.Content, XtraSerializationFlags.DefaultValue)]
        public new CustomOptionsColum OptionsColumn
        {
            get { return (CustomOptionsColum)base.OptionsColumn; }
        }
    }
    public class CustomGridColumnCollection : GridColumnCollection
    {
        public CustomGridColumnCollection(ColumnView view) : base(view) { }
        protected override GridColumn CreateColumn() { return new CustomGridColumn(); }
    }
    public class CustomOptionsColum : OptionsColumn
    {
        bool allowQuickHide;
        public CustomOptionsColum()
            : base()
        {
            allowQuickHide = true;
        }
        [Description("Gets or sets whether the column allow quick hide."), DefaultValue(true), XtraSerializableProperty()]
        public bool AllowQuickHide
        {
            set
            {
                if (allowQuickHide == value) return;
                allowQuickHide = value;
            }
            get { return allowQuickHide; }
        }
    }
    public class CustomGridOptionsCustomization : GridOptionsCustomization
    {
        bool allowQuickCustomisation;
        QuickCustomizationIcon quickCustomizationIcon;
        public CustomGridOptionsCustomization()
            : base()
        {
            this.allowQuickCustomisation = true;
            quickCustomizationIcon = new QuickCustomizationIcon();
        }
        [Description("Gets or sets a value which specifies whether end-users can use quick customisation drop dawn."), DefaultValue(true), XtraSerializableProperty()]
        public virtual bool AllowQuickCustomisation
        {
            get { return allowQuickCustomisation; }
            set
            {
                if (allowQuickCustomisation == value) return;
                bool prevValue = allowQuickCustomisation;
                allowQuickCustomisation = value;
                OnChanged(new BaseOptionChangedEventArgs("AllowQuickCustomisation", prevValue, allowQuickCustomisation));
            }
        }
        [Description("Allow chose different icon for QuickCustomizationButton."), Category("Options"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        XtraSerializableProperty()]
        public virtual QuickCustomizationIcon QuickCustomizationIcons { get { return quickCustomizationIcon; } }
    }
    public class QuickCustomizationIcon : ViewBaseOptions
    {
        Image image;
        Color transperentColor;
        public QuickCustomizationIcon()
        {
            transperentColor = Color.Empty;
        }
        [Description("Allow to chose image to show on QuickCustomisationButton"), XtraSerializableProperty()]
        public Image Image { set { if (image != value) image = value; } get { return image; } }
        [Description("Allow to chose transperent color for QuickCustumisationImage"), XtraSerializableProperty()]
        public Color TransperentColor { get { return transperentColor; } set { if (transperentColor != value) transperentColor = value; } }
    }
    public enum QuickCustomisationIconStatus { Hidden, Enabled, Hot };
}

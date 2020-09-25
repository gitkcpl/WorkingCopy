namespace KontoWin
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::KontoWin.SplashScreen1), true, true, true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControlAdv1 = new Syncfusion.Windows.Forms.Tools.TabControlAdv();
            this.tabPageAdv1 = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.compBarStaticItem = new DevExpress.XtraBars.BarStaticItem();
            this.branchBarStaticItem = new DevExpress.XtraBars.BarStaticItem();
            this.yearBarStaticItem = new DevExpress.XtraBars.BarStaticItem();
            this.userBarStaticItem = new DevExpress.XtraBars.BarStaticItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.checkForBarStaticItem = new DevExpress.XtraBars.BarStaticItem();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.accBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.productBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.salesBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.purchaseBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.receiptBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.paymentBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.ledgerBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.outsBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.bsBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.barAndDockingController1 = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.repositoryItemHypertextLabel1 = new DevExpress.XtraEditors.Repository.RepositoryItemHypertextLabel();
            this.repositoryItemHypertextLabel2 = new DevExpress.XtraEditors.Repository.RepositoryItemHypertextLabel();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlAdv1)).BeginInit();
            this.tabControlAdv1.SuspendLayout();
            this.tabPageAdv1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHypertextLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHypertextLabel2)).BeginInit();
            this.SuspendLayout();
            // 
            // splashScreenManager1
            // 
            splashScreenManager1.ClosingDelay = 500;
            // 
            // tabControlAdv1
            // 
            this.tabControlAdv1.ActiveTabFont = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlAdv1.BeforeTouchSize = new System.Drawing.Size(835, 315);
            this.tabControlAdv1.Controls.Add(this.tabPageAdv1);
            this.tabControlAdv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlAdv1.FocusOnTabClick = false;
            this.tabControlAdv1.Location = new System.Drawing.Point(0, 53);
            this.tabControlAdv1.Name = "tabControlAdv1";
            this.tabControlAdv1.Size = new System.Drawing.Size(835, 315);
            this.tabControlAdv1.TabIndex = 0;
            this.tabControlAdv1.TabStyle = typeof(Syncfusion.Windows.Forms.Tools.TabRendererMetro);
            this.tabControlAdv1.ThemeName = "TabRendererMetro";
            this.tabControlAdv1.ThemesEnabled = true;
            // 
            // tabPageAdv1
            // 
            this.tabPageAdv1.BackgroundImage = global::KontoWin.Properties.Resources.erp_hero_facts;
            this.tabPageAdv1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tabPageAdv1.Controls.Add(this.treeList1);
            this.tabPageAdv1.Image = null;
            this.tabPageAdv1.ImageSize = new System.Drawing.Size(16, 16);
            this.tabPageAdv1.Location = new System.Drawing.Point(3, 28);
            this.tabPageAdv1.Name = "tabPageAdv1";
            this.tabPageAdv1.ShowCloseButton = true;
            this.tabPageAdv1.Size = new System.Drawing.Size(828, 283);
            this.tabPageAdv1.TabIndex = 1;
            this.tabPageAdv1.Text = "Dashboard";
            this.tabPageAdv1.ThemesEnabled = false;
            // 
            // treeList1
            // 
            this.treeList1.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(158)))), ((int)(((byte)(218)))));
            this.treeList1.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
            this.treeList1.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeList1.Appearance.FocusedCell.Options.UseForeColor = true;
            this.treeList1.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeList1.Appearance.Row.Options.UseFont = true;
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeList1.KeyFieldName = "Id";
            this.treeList1.Location = new System.Drawing.Point(0, 0);
            this.treeList1.LookAndFeel.SkinName = "Office 2019 Colorful";
            this.treeList1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.treeList1.MenuManager = this.barManager1;
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.Editable = false;
            this.treeList1.OptionsFind.AllowIncrementalSearch = true;
            this.treeList1.OptionsFind.ExpandNodesOnIncrementalSearch = true;
            this.treeList1.OptionsView.ShowVertLines = true;
            this.treeList1.ParentFieldName = "ParentId";
            this.treeList1.SelectImageList = this.imageCollection1;
            this.treeList1.Size = new System.Drawing.Size(248, 283);
            this.treeList1.TabIndex = 0;
            this.treeList1.ViewStyle = DevExpress.XtraTreeList.TreeListViewStyle.TreeView;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.FieldName = "ModuleDesc";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2,
            this.bar3,
            this.bar1});
            this.barManager1.Controller = this.barAndDockingController1;
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageCollection1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.compBarStaticItem,
            this.branchBarStaticItem,
            this.yearBarStaticItem,
            this.userBarStaticItem,
            this.checkForBarStaticItem,
            this.salesBarButtonItem,
            this.accBarButtonItem,
            this.productBarButtonItem,
            this.purchaseBarButtonItem,
            this.ledgerBarButtonItem,
            this.outsBarButtonItem,
            this.bsBarButtonItem,
            this.receiptBarButtonItem,
            this.paymentBarButtonItem});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 25;
            this.barManager1.MdiMenuMergeStyle = DevExpress.XtraBars.BarMdiMenuMergeStyle.Never;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemHypertextLabel1,
            this.repositoryItemHypertextLabel2});
            this.barManager1.StatusBar = this.bar3;
            this.barManager1.UseF10KeyForMenu = false;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.FloatLocation = new System.Drawing.Point(268, 119);
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.compBarStaticItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.branchBarStaticItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.yearBarStaticItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.userBarStaticItem)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // compBarStaticItem
            // 
            this.compBarStaticItem.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.compBarStaticItem.Caption = "Comp";
            this.compBarStaticItem.Id = 5;
            this.compBarStaticItem.ItemAppearance.Normal.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.compBarStaticItem.ItemAppearance.Normal.Options.UseFont = true;
            this.compBarStaticItem.Name = "compBarStaticItem";
            // 
            // branchBarStaticItem
            // 
            this.branchBarStaticItem.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.branchBarStaticItem.Caption = "Branch";
            this.branchBarStaticItem.Id = 6;
            this.branchBarStaticItem.ItemAppearance.Normal.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.branchBarStaticItem.ItemAppearance.Normal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.branchBarStaticItem.ItemAppearance.Normal.Options.UseFont = true;
            this.branchBarStaticItem.ItemAppearance.Normal.Options.UseForeColor = true;
            this.branchBarStaticItem.LeftIndent = 15;
            this.branchBarStaticItem.Name = "branchBarStaticItem";
            // 
            // yearBarStaticItem
            // 
            this.yearBarStaticItem.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.yearBarStaticItem.Caption = "Year";
            this.yearBarStaticItem.Id = 7;
            this.yearBarStaticItem.ItemAppearance.Normal.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yearBarStaticItem.ItemAppearance.Normal.Options.UseFont = true;
            this.yearBarStaticItem.LeftIndent = 15;
            this.yearBarStaticItem.Name = "yearBarStaticItem";
            // 
            // userBarStaticItem
            // 
            this.userBarStaticItem.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.userBarStaticItem.Caption = "User";
            this.userBarStaticItem.Id = 8;
            this.userBarStaticItem.ItemAppearance.Normal.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userBarStaticItem.ItemAppearance.Normal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.userBarStaticItem.ItemAppearance.Normal.Options.UseFont = true;
            this.userBarStaticItem.ItemAppearance.Normal.Options.UseForeColor = true;
            this.userBarStaticItem.LeftIndent = 15;
            this.userBarStaticItem.Name = "userBarStaticItem";
            // 
            // bar3
            // 
            this.bar3.BarAppearance.Normal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(158)))), ((int)(((byte)(218)))));
            this.bar3.BarAppearance.Normal.Options.UseBackColor = true;
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.checkForBarStaticItem)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // checkForBarStaticItem
            // 
            this.checkForBarStaticItem.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.checkForBarStaticItem.Caption = "Check For Update";
            this.checkForBarStaticItem.Id = 10;
            this.checkForBarStaticItem.ItemAppearance.Normal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(158)))), ((int)(((byte)(218)))));
            this.checkForBarStaticItem.ItemAppearance.Normal.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkForBarStaticItem.ItemAppearance.Normal.ForeColor = System.Drawing.Color.White;
            this.checkForBarStaticItem.ItemAppearance.Normal.Options.UseBackColor = true;
            this.checkForBarStaticItem.ItemAppearance.Normal.Options.UseFont = true;
            this.checkForBarStaticItem.ItemAppearance.Normal.Options.UseForeColor = true;
            this.checkForBarStaticItem.Name = "checkForBarStaticItem";
            this.checkForBarStaticItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.checkForBarStaticItem_ItemClick);
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 4";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 1;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.accBarButtonItem, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.productBarButtonItem, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.salesBarButtonItem, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.purchaseBarButtonItem, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.receiptBarButtonItem, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.paymentBarButtonItem, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.ledgerBarButtonItem, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.outsBarButtonItem, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bsBarButtonItem, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.Text = "Custom 4";
            // 
            // accBarButtonItem
            // 
            this.accBarButtonItem.Caption = "Account";
            this.accBarButtonItem.Id = 17;
            this.accBarButtonItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("accBarButtonItem.ImageOptions.SvgImage")));
            this.accBarButtonItem.ItemAppearance.Normal.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accBarButtonItem.ItemAppearance.Normal.Options.UseFont = true;
            this.accBarButtonItem.Name = "accBarButtonItem";
            this.accBarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.B1_ItemClick);
            // 
            // productBarButtonItem
            // 
            this.productBarButtonItem.Caption = "Product";
            this.productBarButtonItem.Id = 18;
            this.productBarButtonItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("productBarButtonItem.ImageOptions.SvgImage")));
            this.productBarButtonItem.ItemAppearance.Normal.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productBarButtonItem.ItemAppearance.Normal.Options.UseFont = true;
            this.productBarButtonItem.Name = "productBarButtonItem";
            this.productBarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.B1_ItemClick);
            // 
            // salesBarButtonItem
            // 
            this.salesBarButtonItem.Caption = "Sales";
            this.salesBarButtonItem.Id = 16;
            this.salesBarButtonItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("salesBarButtonItem.ImageOptions.SvgImage")));
            this.salesBarButtonItem.ItemAppearance.Normal.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.salesBarButtonItem.ItemAppearance.Normal.Options.UseFont = true;
            this.salesBarButtonItem.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F11);
            this.salesBarButtonItem.Name = "salesBarButtonItem";
            this.salesBarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.B1_ItemClick);
            // 
            // purchaseBarButtonItem
            // 
            this.purchaseBarButtonItem.Caption = "Purchase";
            this.purchaseBarButtonItem.Id = 19;
            this.purchaseBarButtonItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("purchaseBarButtonItem.ImageOptions.SvgImage")));
            this.purchaseBarButtonItem.ItemAppearance.Normal.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.purchaseBarButtonItem.ItemAppearance.Normal.Options.UseFont = true;
            this.purchaseBarButtonItem.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12);
            this.purchaseBarButtonItem.Name = "purchaseBarButtonItem";
            this.purchaseBarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.B1_ItemClick);
            // 
            // receiptBarButtonItem
            // 
            this.receiptBarButtonItem.Caption = "Receipt\r";
            this.receiptBarButtonItem.Id = 23;
            this.receiptBarButtonItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("receiptBarButtonItem.ImageOptions.SvgImage")));
            this.receiptBarButtonItem.ItemAppearance.Normal.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.receiptBarButtonItem.ItemAppearance.Normal.Options.UseFont = true;
            this.receiptBarButtonItem.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6);
            this.receiptBarButtonItem.Name = "receiptBarButtonItem";
            this.receiptBarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.B1_ItemClick);
            // 
            // paymentBarButtonItem
            // 
            this.paymentBarButtonItem.Caption = "Payment";
            this.paymentBarButtonItem.Id = 24;
            this.paymentBarButtonItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("paymentBarButtonItem.ImageOptions.SvgImage")));
            this.paymentBarButtonItem.ItemAppearance.Normal.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paymentBarButtonItem.ItemAppearance.Normal.Options.UseFont = true;
            this.paymentBarButtonItem.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7);
            this.paymentBarButtonItem.Name = "paymentBarButtonItem";
            this.paymentBarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.B1_ItemClick);
            // 
            // ledgerBarButtonItem
            // 
            this.ledgerBarButtonItem.Caption = "Ledger";
            this.ledgerBarButtonItem.Id = 20;
            this.ledgerBarButtonItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("ledgerBarButtonItem.ImageOptions.SvgImage")));
            this.ledgerBarButtonItem.ItemAppearance.Normal.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ledgerBarButtonItem.ItemAppearance.Normal.ForeColor = System.Drawing.Color.Blue;
            this.ledgerBarButtonItem.ItemAppearance.Normal.Options.UseFont = true;
            this.ledgerBarButtonItem.ItemAppearance.Normal.Options.UseForeColor = true;
            this.ledgerBarButtonItem.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8);
            this.ledgerBarButtonItem.Name = "ledgerBarButtonItem";
            this.ledgerBarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.B1_ItemClick);
            // 
            // outsBarButtonItem
            // 
            this.outsBarButtonItem.Caption = "Outstanding";
            this.outsBarButtonItem.Id = 21;
            this.outsBarButtonItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("outsBarButtonItem.ImageOptions.SvgImage")));
            this.outsBarButtonItem.ItemAppearance.Normal.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outsBarButtonItem.ItemAppearance.Normal.Options.UseFont = true;
            this.outsBarButtonItem.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9);
            this.outsBarButtonItem.Name = "outsBarButtonItem";
            this.outsBarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.B1_ItemClick);
            // 
            // bsBarButtonItem
            // 
            this.bsBarButtonItem.Caption = "Balancesheet";
            this.bsBarButtonItem.Id = 22;
            this.bsBarButtonItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bsBarButtonItem.ImageOptions.SvgImage")));
            this.bsBarButtonItem.ItemAppearance.Normal.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bsBarButtonItem.ItemAppearance.Normal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(22)))), ((int)(((byte)(91)))));
            this.bsBarButtonItem.ItemAppearance.Normal.Options.UseFont = true;
            this.bsBarButtonItem.ItemAppearance.Normal.Options.UseForeColor = true;
            this.bsBarButtonItem.Name = "bsBarButtonItem";
            this.bsBarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.B1_ItemClick);
            // 
            // barAndDockingController1
            // 
            this.barAndDockingController1.AppearancesBar.ItemsFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barAndDockingController1.AppearancesBar.MainMenuAppearance.Hovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(158)))), ((int)(((byte)(218)))));
            this.barAndDockingController1.AppearancesBar.MainMenuAppearance.Hovered.ForeColor = System.Drawing.Color.White;
            this.barAndDockingController1.AppearancesBar.MainMenuAppearance.Hovered.Options.UseBackColor = true;
            this.barAndDockingController1.AppearancesBar.MainMenuAppearance.Hovered.Options.UseForeColor = true;
            this.barAndDockingController1.AppearancesBar.MainMenuAppearance.Normal.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barAndDockingController1.AppearancesBar.MainMenuAppearance.Normal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(22)))), ((int)(((byte)(91)))));
            this.barAndDockingController1.AppearancesBar.MainMenuAppearance.Normal.Options.UseFont = true;
            this.barAndDockingController1.AppearancesBar.MainMenuAppearance.Normal.Options.UseForeColor = true;
            this.barAndDockingController1.AppearancesBar.SubMenu.AppearanceMenu.Hovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(158)))), ((int)(((byte)(218)))));
            this.barAndDockingController1.AppearancesBar.SubMenu.AppearanceMenu.Hovered.ForeColor = System.Drawing.Color.White;
            this.barAndDockingController1.AppearancesBar.SubMenu.AppearanceMenu.Hovered.Options.UseBackColor = true;
            this.barAndDockingController1.AppearancesBar.SubMenu.AppearanceMenu.Hovered.Options.UseForeColor = true;
            this.barAndDockingController1.LookAndFeel.SkinName = "Office 2019 Colorful";
            this.barAndDockingController1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.barAndDockingController1.PropertiesBar.AllowLinkLighting = false;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(835, 53);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 368);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(835, 25);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 53);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 315);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(835, 53);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 315);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "account.png");
            this.imageCollection1.Images.SetKeyName(1, "backup.png");
            this.imageCollection1.Images.SetKeyName(2, "cash.png");
            this.imageCollection1.Images.SetKeyName(3, "company.png");
            this.imageCollection1.Images.SetKeyName(4, "design.png");
            this.imageCollection1.Images.SetKeyName(5, "gray.png");
            this.imageCollection1.Images.SetKeyName(6, "group.png");
            this.imageCollection1.Images.SetKeyName(7, "location.png");
            this.imageCollection1.Images.SetKeyName(8, "master.png");
            this.imageCollection1.Images.SetKeyName(9, "opening.png");
            this.imageCollection1.Images.SetKeyName(10, "product.png");
            this.imageCollection1.Images.SetKeyName(11, "production.png");
            this.imageCollection1.Images.SetKeyName(12, "purchase.png");
            this.imageCollection1.Images.SetKeyName(13, "report.png");
            this.imageCollection1.Images.SetKeyName(14, "reportview.png");
            this.imageCollection1.Images.SetKeyName(15, "sale.png");
            this.imageCollection1.Images.SetKeyName(16, "setup.png");
            this.imageCollection1.Images.SetKeyName(17, "tax.png");
            this.imageCollection1.Images.SetKeyName(18, "tools.png");
            this.imageCollection1.Images.SetKeyName(19, "transaction.png");
            this.imageCollection1.Images.SetKeyName(20, "user.png");
            this.imageCollection1.Images.SetKeyName(21, "voucher.png");
            // 
            // repositoryItemHypertextLabel1
            // 
            this.repositoryItemHypertextLabel1.Name = "repositoryItemHypertextLabel1";
            // 
            // repositoryItemHypertextLabel2
            // 
            this.repositoryItemHypertextLabel2.Name = "repositoryItemHypertextLabel2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionFont = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClientSize = new System.Drawing.Size(835, 393);
            this.Controls.Add(this.tabControlAdv1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.Name = "Form1";
            this.Text = "Konto 2020.1- Inventory & Accounting System";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabControlAdv1)).EndInit();
            this.tabControlAdv1.ResumeLayout(false);
            this.tabPageAdv1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHypertextLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHypertextLabel2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.TabControlAdv tabControlAdv1;
        private Syncfusion.Windows.Forms.Tools.TabPageAdv tabPageAdv1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController1;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraBars.BarStaticItem compBarStaticItem;
        private DevExpress.XtraBars.BarStaticItem branchBarStaticItem;
        private DevExpress.XtraBars.BarStaticItem yearBarStaticItem;
        private DevExpress.XtraBars.BarStaticItem userBarStaticItem;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.BarStaticItem checkForBarStaticItem;
        private DevExpress.XtraEditors.Repository.RepositoryItemHypertextLabel repositoryItemHypertextLabel2;
        private DevExpress.XtraEditors.Repository.RepositoryItemHypertextLabel repositoryItemHypertextLabel1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem salesBarButtonItem;
        private DevExpress.XtraBars.BarButtonItem accBarButtonItem;
        private DevExpress.XtraBars.BarButtonItem productBarButtonItem;
        private DevExpress.XtraBars.BarButtonItem purchaseBarButtonItem;
        private DevExpress.XtraBars.BarButtonItem ledgerBarButtonItem;
        private DevExpress.XtraBars.BarButtonItem outsBarButtonItem;
        private DevExpress.XtraBars.BarButtonItem bsBarButtonItem;
        private DevExpress.XtraBars.BarButtonItem receiptBarButtonItem;
        private DevExpress.XtraBars.BarButtonItem paymentBarButtonItem;
    }
}


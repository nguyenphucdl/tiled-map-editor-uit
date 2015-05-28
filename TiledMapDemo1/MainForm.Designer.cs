namespace TiledMapDemo1
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeViewMapLayer = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.copyToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.pasteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolNewLayer = new System.Windows.Forms.ToolStripButton();
            this.toolRemoveLayer = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolRectangleDrawing = new System.Windows.Forms.ToolStripButton();
            this.toolLineDrawing = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.quadtreeLbl = new System.Windows.Forms.ToolStripLabel();
            this.toolStripAddQuadtree = new System.Windows.Forms.ToolStripButton();
            this.lblPosMove = new System.Windows.Forms.Label();
            this.lblZoom = new System.Windows.Forms.Label();
            this.lblGid = new System.Windows.Forms.Label();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.tileSheetPanel = new TiledMapDemo1.BufferedPanel();
            this.tileSheetGraphic = new TiledMapDemo1.BufferedGraphic();
            this.bufferedGraphic1 = new TiledMapDemo1.BufferedGraphic();
            this.mainPanel = new TiledMapDemo1.BufferedPanel();
            this.workplaceGraphic = new TiledMapDemo1.BufferedGraphic();
            this.shapeContainer2 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape1 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSelectObject = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.tileSheetPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1403, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.importToolStripMenuItem,
            this.sToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fromImageToolStripMenuItem});
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.importToolStripMenuItem.Text = "Import";
            // 
            // fromImageToolStripMenuItem
            // 
            this.fromImageToolStripMenuItem.Name = "fromImageToolStripMenuItem";
            this.fromImageToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.fromImageToolStripMenuItem.Text = "From Image...";
            this.fromImageToolStripMenuItem.Click += new System.EventHandler(this.fromImageToolStripMenuItem_Click);
            // 
            // sToolStripMenuItem
            // 
            this.sToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tileMapToolStripMenuItem,
            this.imageToolStripMenuItem});
            this.sToolStripMenuItem.Name = "sToolStripMenuItem";
            this.sToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.sToolStripMenuItem.Text = "Export";
            // 
            // tileMapToolStripMenuItem
            // 
            this.tileMapToolStripMenuItem.Name = "tileMapToolStripMenuItem";
            this.tileMapToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.tileMapToolStripMenuItem.Text = "Map...";
            // 
            // imageToolStripMenuItem
            // 
            this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            this.imageToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.imageToolStripMenuItem.Text = "Image...";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // treeViewMapLayer
            // 
            this.treeViewMapLayer.Location = new System.Drawing.Point(11, 99);
            this.treeViewMapLayer.Name = "treeViewMapLayer";
            this.treeViewMapLayer.Size = new System.Drawing.Size(312, 146);
            this.treeViewMapLayer.TabIndex = 14;
            this.treeViewMapLayer.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewMapExplorer_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label1.Location = new System.Drawing.Point(11, 73);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(10, 5, 0, 5);
            this.label1.Size = new System.Drawing.Size(312, 23);
            this.label1.TabIndex = 15;
            this.label1.Text = "Layers";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator,
            this.cutToolStripButton,
            this.copyToolStripButton,
            this.pasteToolStripButton,
            this.toolStripSeparator5,
            this.helpToolStripButton,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.toolNewLayer,
            this.toolRemoveLayer,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.toolRectangleDrawing,
            this.toolLineDrawing,
            this.toolStripSeparator3,
            this.quadtreeLbl,
            this.toolStripAddQuadtree,
            this.toolStripSeparator4,
            this.toolStripSelectObject});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1403, 25);
            this.toolStrip1.TabIndex = 16;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.newToolStripButton.Text = "&New";
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "&Open";
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "&Save";
            this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // cutToolStripButton
            // 
            this.cutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cutToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripButton.Image")));
            this.cutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutToolStripButton.Name = "cutToolStripButton";
            this.cutToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.cutToolStripButton.Text = "C&ut";
            // 
            // copyToolStripButton
            // 
            this.copyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copyToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripButton.Image")));
            this.copyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolStripButton.Name = "copyToolStripButton";
            this.copyToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.copyToolStripButton.Text = "&Copy";
            // 
            // pasteToolStripButton
            // 
            this.pasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pasteToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripButton.Image")));
            this.pasteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteToolStripButton.Name = "pasteToolStripButton";
            this.pasteToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.pasteToolStripButton.Text = "&Paste";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.helpToolStripButton.Text = "He&lp";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Margin = new System.Windows.Forms.Padding(10, 1, 0, 2);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(34, 22);
            this.toolStripLabel1.Text = "Map:";
            // 
            // toolNewLayer
            // 
            this.toolNewLayer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolNewLayer.Image = ((System.Drawing.Image)(resources.GetObject("toolNewLayer.Image")));
            this.toolNewLayer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolNewLayer.Name = "toolNewLayer";
            this.toolNewLayer.Size = new System.Drawing.Size(23, 22);
            this.toolNewLayer.Text = "New Layer";
            this.toolNewLayer.Click += new System.EventHandler(this.toolNewLayer_Click);
            // 
            // toolRemoveLayer
            // 
            this.toolRemoveLayer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolRemoveLayer.Image = ((System.Drawing.Image)(resources.GetObject("toolRemoveLayer.Image")));
            this.toolRemoveLayer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolRemoveLayer.Name = "toolRemoveLayer";
            this.toolRemoveLayer.Size = new System.Drawing.Size(23, 22);
            this.toolRemoveLayer.Text = "RemoveLayer";
            this.toolRemoveLayer.Click += new System.EventHandler(this.toolRemoveLayer_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Margin = new System.Windows.Forms.Padding(10, 1, 0, 2);
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(45, 22);
            this.toolStripLabel2.Text = "Object:";
            // 
            // toolRectangleDrawing
            // 
            this.toolRectangleDrawing.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolRectangleDrawing.Image = ((System.Drawing.Image)(resources.GetObject("toolRectangleDrawing.Image")));
            this.toolRectangleDrawing.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolRectangleDrawing.Name = "toolRectangleDrawing";
            this.toolRectangleDrawing.Size = new System.Drawing.Size(23, 22);
            this.toolRectangleDrawing.Text = "Rectangle";
            this.toolRectangleDrawing.Click += new System.EventHandler(this.toolRectangleDrawing_Click);
            // 
            // toolLineDrawing
            // 
            this.toolLineDrawing.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolLineDrawing.Image = ((System.Drawing.Image)(resources.GetObject("toolLineDrawing.Image")));
            this.toolLineDrawing.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolLineDrawing.Name = "toolLineDrawing";
            this.toolLineDrawing.Size = new System.Drawing.Size(23, 22);
            this.toolLineDrawing.Text = "Line";
            this.toolLineDrawing.Click += new System.EventHandler(this.toolLineDrawing_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // quadtreeLbl
            // 
            this.quadtreeLbl.Margin = new System.Windows.Forms.Padding(10, 1, 5, 2);
            this.quadtreeLbl.Name = "quadtreeLbl";
            this.quadtreeLbl.Size = new System.Drawing.Size(59, 22);
            this.quadtreeLbl.Text = "Quadtree:";
            // 
            // toolStripAddQuadtree
            // 
            this.toolStripAddQuadtree.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripAddQuadtree.Image = ((System.Drawing.Image)(resources.GetObject("toolStripAddQuadtree.Image")));
            this.toolStripAddQuadtree.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripAddQuadtree.Name = "toolStripAddQuadtree";
            this.toolStripAddQuadtree.Size = new System.Drawing.Size(23, 22);
            this.toolStripAddQuadtree.Text = "toolStripAddQuadtree";
            this.toolStripAddQuadtree.ToolTipText = "AddQuadTree";
            this.toolStripAddQuadtree.Click += new System.EventHandler(this.toolStripAddQuadtree_Click);
            // 
            // lblPosMove
            // 
            this.lblPosMove.AutoSize = true;
            this.lblPosMove.Location = new System.Drawing.Point(335, 716);
            this.lblPosMove.Name = "lblPosMove";
            this.lblPosMove.Size = new System.Drawing.Size(25, 13);
            this.lblPosMove.TabIndex = 19;
            this.lblPosMove.Text = "Pos";
            // 
            // lblZoom
            // 
            this.lblZoom.AutoSize = true;
            this.lblZoom.Location = new System.Drawing.Point(587, 716);
            this.lblZoom.Name = "lblZoom";
            this.lblZoom.Size = new System.Drawing.Size(34, 13);
            this.lblZoom.TabIndex = 20;
            this.lblZoom.Text = "Zoom";
            // 
            // lblGid
            // 
            this.lblGid.AutoSize = true;
            this.lblGid.Location = new System.Drawing.Point(12, 716);
            this.lblGid.Name = "lblGid";
            this.lblGid.Size = new System.Drawing.Size(23, 13);
            this.lblGid.TabIndex = 22;
            this.lblGid.Text = "Gid";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip2.Location = new System.Drawing.Point(1379, 49);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(24, 689);
            this.toolStrip2.TabIndex = 24;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(21, 20);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // propertyGrid
            // 
            this.propertyGrid.Location = new System.Drawing.Point(12, 268);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(308, 184);
            this.propertyGrid.TabIndex = 25;
            // 
            // tileSheetPanel
            // 
            this.tileSheetPanel.AutoScroll = true;
            this.tileSheetPanel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tileSheetPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tileSheetPanel.CausesValidation = false;
            this.tileSheetPanel.Controls.Add(this.tileSheetGraphic);
            this.tileSheetPanel.Controls.Add(this.bufferedGraphic1);
            this.tileSheetPanel.Dirty = false;
            this.tileSheetPanel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.tileSheetPanel.Location = new System.Drawing.Point(14, 475);
            this.tileSheetPanel.Name = "tileSheetPanel";
            this.tileSheetPanel.Size = new System.Drawing.Size(309, 229);
            this.tileSheetPanel.TabIndex = 21;
            // 
            // tileSheetGraphic
            // 
            this.tileSheetGraphic.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.tileSheetGraphic.Dirty = false;
            this.tileSheetGraphic.Location = new System.Drawing.Point(13, 15);
            this.tileSheetGraphic.Name = "tileSheetGraphic";
            this.tileSheetGraphic.Size = new System.Drawing.Size(264, 209);
            this.tileSheetGraphic.TabIndex = 21;
            this.tileSheetGraphic.WorkplaceSize = new System.Drawing.Size(264, 209);
            this.tileSheetGraphic.Zoom = 1.25F;
            this.tileSheetGraphic.MouseLeave += new System.EventHandler(this.tileSheetGraphic_MouseLeave);
            // 
            // bufferedGraphic1
            // 
            this.bufferedGraphic1.Dirty = true;
            this.bufferedGraphic1.Location = new System.Drawing.Point(0, 0);
            this.bufferedGraphic1.Name = "bufferedGraphic1";
            this.bufferedGraphic1.Size = new System.Drawing.Size(0, 0);
            this.bufferedGraphic1.TabIndex = 20;
            this.bufferedGraphic1.WorkplaceSize = new System.Drawing.Size(0, 0);
            this.bufferedGraphic1.Zoom = 1F;
            // 
            // mainPanel
            // 
            this.mainPanel.AutoScroll = true;
            this.mainPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.mainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainPanel.CausesValidation = false;
            this.mainPanel.Controls.Add(this.workplaceGraphic);
            this.mainPanel.Controls.Add(this.shapeContainer2);
            this.mainPanel.Dirty = false;
            this.mainPanel.Location = new System.Drawing.Point(351, 73);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(979, 631);
            this.mainPanel.TabIndex = 17;
            // 
            // workplaceGraphic
            // 
            this.workplaceGraphic.Dirty = true;
            this.workplaceGraphic.Location = new System.Drawing.Point(0, 0);
            this.workplaceGraphic.Name = "workplaceGraphic";
            this.workplaceGraphic.Size = new System.Drawing.Size(0, 0);
            this.workplaceGraphic.TabIndex = 20;
            this.workplaceGraphic.WorkplaceSize = new System.Drawing.Size(0, 0);
            this.workplaceGraphic.Zoom = 1F;
            this.workplaceGraphic.MouseLeave += new System.EventHandler(this.workplaceGraphic_MouseLeave);
            // 
            // shapeContainer2
            // 
            this.shapeContainer2.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer2.Name = "shapeContainer2";
            this.shapeContainer2.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rectangleShape1});
            this.shapeContainer2.Size = new System.Drawing.Size(977, 629);
            this.shapeContainer2.TabIndex = 21;
            this.shapeContainer2.TabStop = false;
            // 
            // rectangleShape1
            // 
            this.rectangleShape1.Location = new System.Drawing.Point(1009, 85);
            this.rectangleShape1.Name = "rectangleShape1";
            this.rectangleShape1.Size = new System.Drawing.Size(21, 13);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSelectObject
            // 
            this.toolStripSelectObject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSelectObject.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSelectObject.Image")));
            this.toolStripSelectObject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSelectObject.Name = "toolStripSelectObject";
            this.toolStripSelectObject.Size = new System.Drawing.Size(23, 22);
            this.toolStripSelectObject.Text = "Select Object";
            this.toolStripSelectObject.Click += toolStripSelectObject_Click;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1403, 738);
            this.Controls.Add(this.propertyGrid);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.lblGid);
            this.Controls.Add(this.tileSheetPanel);
            this.Controls.Add(this.lblZoom);
            this.Controls.Add(this.lblPosMove);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeViewMapLayer);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tiled Map";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tileSheetPanel.ResumeLayout(false);
            this.mainPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }




        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromImageToolStripMenuItem;
        private System.Windows.Forms.TreeView treeViewMapLayer;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private BufferedPanel mainPanel;
        public System.Windows.Forms.Label lblPosMove;
        private BufferedGraphic workplaceGraphic;
        private System.Windows.Forms.Label lblZoom;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imageToolStripMenuItem;
        private BufferedPanel tileSheetPanel;
        private BufferedGraphic bufferedGraphic1;
        private BufferedGraphic tileSheetGraphic;
        public System.Windows.Forms.Label lblGid;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer2;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton cutToolStripButton;
        private System.Windows.Forms.ToolStripButton copyToolStripButton;
        private System.Windows.Forms.ToolStripButton pasteToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripButton toolRectangleDrawing;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.ToolStripButton toolNewLayer;
        private System.Windows.Forms.ToolStripButton toolRemoveLayer;
        private System.Windows.Forms.ToolStripButton toolLineDrawing;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripAddQuadtree;
        private System.Windows.Forms.ToolStripLabel quadtreeLbl;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripSelectObject;
    }
}


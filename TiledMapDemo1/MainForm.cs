﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TiledMapDemo1.Model;
using TiledMapDemo1.Quadtree;

namespace TiledMapDemo1
{
    public partial class MainForm : Form
    {
        
        string tiledMapFile = @"map1-2\map1-2.tmx";

        WorkplaceModel workplaceModel = new WorkplaceModel();

        #region Fields
        Point m_LastMovePoint = new Point(-1, -1);
        bool m_MouseWheelState = false;
        #endregion

        #region Constructor
        public MainForm()
        {
            InitializeComponent();
            RegisterWorkplaceEvents();
            //workplaceModel.LoadTileMap(tiledMapFile);

            ContextMenu cm = new ContextMenu();
            MenuItem itemPickTile = new MenuItem("Pick Tile");
            itemPickTile.Tag = "PickTile";
            itemPickTile.Click += itemPickTile_Click;
            cm.MenuItems.Add(itemPickTile);
            this.ContextMenu = cm;

            this.KeyPreview = true;
        }

        void itemPickTile_Click(object sender, EventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            if (item.Tag == "PickTile")
            {
                workplaceModel.itemPickTile_Click(sender, e);
            }
        }
        private void RegisterWorkplaceEvents()
        {
            mainPanel.OnMouseWheelAction += workplacePanel_MouseWheel;
            mainPanel.OnMouseDownAction = worlplaceGraphic_MouseDown;
            workplaceGraphic.OnDraw = DrawWorkplaceGraphic;
            workplaceGraphic.OnMove = WorkplaceGraphic_MouseMove;
            workplaceGraphic.OnSizeChange = WorkplaceGraphic_SizeChange;
            workplaceGraphic.MouseClick += workplaceGraphic_MouseClick;
            workplaceGraphic.OnMouseDownAction = worlplaceGraphic_MouseDown;
            workplaceGraphic.OnMouseUpAction = worlplaceGraphic_MouseUp;
            
            
            tileSheetGraphic.OnDraw = DrawTileSheetGraphic;
            tileSheetGraphic.OnMove = tileSheetGraphic_MouseMove;
            tileSheetGraphic.MouseClick += tileSheetGraphic_MouseClick;

            workplaceModel.SetWorkPlaceGraphic(workplaceGraphic);
            workplaceModel.SetTileSheetGraphic(tileSheetGraphic);
            workplaceModel.SetLayerTreeView(treeViewMapLayer);
            workplaceModel.SetPropertyMapGrid(propertyGrid);
        }

        #endregion

        #region Form Events
        private void fromImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportForm importFrm = new ImportForm();

            if (importFrm.ShowDialog() == DialogResult.OK)
            {
                tiledMapFile = importFrm.MapPath;
                workplaceModel.LoadTileMap(tiledMapFile);
            }
        }
        private void tileSheetGraphic_MouseClick(object sender, MouseEventArgs e)
        {
            workplaceModel.tileSheetGraphic_MouseClick(this, e);
        }
        private void workplaceGraphic_MouseClick(object sender, MouseEventArgs e)
        {
            workplaceModel.workplaceGraphic_MouseClick(this, e);
        }
        private void WorkplaceGraphic_SizeChange(EventArgs e)
        {           
            workplaceModel.WorkplaceGraphic_SizeChange(e);
        }
        private void WorkplaceGraphic_MouseMove(MouseEventArgs e)
        {
            int cellSize = (int)(16 * workplaceGraphic.Zoom);
            Point movePoint = new Point(e.X / cellSize, e.Y / cellSize);
            if (m_LastMovePoint.X == movePoint.X && m_LastMovePoint.Y == movePoint.Y)
                return;
            if (!m_MouseWheelState)
                workplaceGraphic.Redraw();
            if (workplaceModel != null)
                workplaceModel.WorkplaceGraphic_MouseMove(this, e);

            m_MouseWheelState = false;
            m_LastMovePoint = movePoint;
        }
        private void workplacePanel_MouseWheel(MouseEventArgs e)
        {
            m_MouseWheelState = true;
            if (e.Delta > 0)
                workplaceGraphic.Zoom += 0.25f;
            else
                workplaceGraphic.Zoom -= 0.25f;
            lblZoom.Text = "Zoom :" + workplaceGraphic.Zoom * 100 + "%";
        }
        private void tileSheetGraphic_MouseMove(MouseEventArgs e)
        {
            int cellSize = (int)(16 * tileSheetGraphic.Zoom);
            Point movePoint = new Point(e.X / cellSize, e.Y / cellSize);
            if (m_LastMovePoint.X == movePoint.X && m_LastMovePoint.Y == movePoint.Y)
                return;
            if (!m_MouseWheelState)
                tileSheetGraphic.Redraw();
            if (workplaceModel != null)
                workplaceModel.tileSheetGraphic_MouseMove(this, e);

            m_MouseWheelState = false;
            m_LastMovePoint = movePoint;
        }


        private void DrawWorkplaceGraphic(Graphics graphics)
        {
            workplaceModel.DrawWorkPlace(graphics);            
        }

        private void DrawTileSheetGraphic(Graphics graphics)
        {
            workplaceModel.DrawTileSheet(graphics);
        }

        private void workplaceGraphic_MouseLeave(object sender, EventArgs e)
        {
            if (workplaceModel.Initialized)
            {
                workplaceGraphic.Redraw();
            }
        }

        private void tileSheetGraphic_MouseLeave(object sender, EventArgs e)
        {
            if (workplaceModel.Initialized)
            {
                tileSheetGraphic.Redraw();
            }
        }

        private void worlplaceGraphic_MouseUp(MouseEventArgs e)
        {
            if (workplaceModel.Initialized)
            {
                workplaceModel.worlplaceGraphic_MouseUp(this, e);
            }
        }

        private void worlplaceGraphic_MouseDown(MouseEventArgs e)
        {
            if (workplaceModel.Initialized)
            { 
                workplaceModel.worlplaceGraphic_MouseDown(this, e);
            }
        }

        #endregion

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.Title = "Select file to be upload";
            fDialog.Filter = "TMX map(*.tmx)|*.tmx*";
            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                tiledMapFile = fDialog.FileName.ToString();
                workplaceModel.LoadTileMap(tiledMapFile);
            }
        }

        private void btnRectDrawing_Click(object sender, EventArgs e)
        {
            //workplaceModel.Drawing = true;   
        }

        private void treeViewMapExplorer_AfterSelect(object sender, TreeViewEventArgs e)
        {
            workplaceModel.treeViewMapExplorer_AfterSelect(sender, e);
        }

        private void toolRectangleDrawing_Click(object sender, EventArgs e)
        {
            workplaceModel.toolRectangleDrawing_Click(sender,e);
        }

        private void toolLineDrawing_Click(object sender, EventArgs e)
        {
            workplaceModel.Drawing = true;
            workplaceModel.DrawingType = DrawingType.LINE;
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
                worlplaceGraphic_MouseDown(e);
        }

        private void toolRemoveLayer_Click(object sender, EventArgs e)
        {
            workplaceModel.toolRemoveLayer_Click(sender, e);
        }

        private void toolNewLayer_Click(object sender, EventArgs e)
        {
            workplaceModel.toolNewLayer_Click(sender, e);
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            workplaceModel.saveToolStripButton_Click(sender, e);
        }

        private void toolStripAddQuadtree_Click(object sender, EventArgs e)
        {
            QuadtreeWriter qwriter = new QuadtreeWriter(workplaceModel);
            qwriter.Prepare();
            qwriter.Save();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            
            workplaceModel.MainForm_KeyDown(sender, e);            
        }

        private void toolStripSelectObject_Click(object sender, System.EventArgs e)
        {
            workplaceModel.toolStripSelectObject_Click(sender, e);
        }

        private void btnSetViewport_Click(object sender, EventArgs e)
        {
            workplaceModel.btnSetViewport_Click(sender, e);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void lblZoom_Click(object sender, EventArgs e)
        {

        }

        private void lblPosMove_Click(object sender, EventArgs e)
        {

        }











































        //private void btnSave_Click(object sender, EventArgs e)
        //{
        //    Bitmap tempBitmap = new Bitmap(workplaceImage.Width, workplaceImage.Height);
        //    Graphics graphics = Graphics.FromImage(tempBitmap);
        //    graphics.DrawImage(workplaceImage, new Rectangle(0, 0, workplaceImage.Width, workplaceImage.Height));

        //    int cellSize = 16;

        //    int m = workplaceGraphic.Width / cellSize;
        //    int n = workplaceGraphic.Height / cellSize;

        //    for (int i = 0; i < m; i++)
        //    {
        //        graphics.DrawLine(Pens.Aqua, new Point(i * cellSize, 0), new Point(i * cellSize, n * cellSize));
        //    }

        //    for (int j = 0; j < n; j++)
        //    {
        //        graphics.DrawLine(Pens.Aqua, new Point(0, j * cellSize), new Point(m * cellSize, j * cellSize));
        //    }
        //    tempBitmap.Save("temp.png", ImageFormat.Png);
        //}

        //private void panel1_MouseMove(object sender, MouseEventArgs e)
        //{
        //    lblPosMove.Text = "Pos  [" + e.X + "," + e.Y + "]";

        //    mainGraphics.DrawRectangle(Pens.Cyan, new Rectangle(20, 20 ,20, 20));
        //}

        //private void mainPanel_MouseClick(object sender, MouseEventArgs e)
        //{

        //    for (int i = 0; i < 100; i++)
        //        for (int j = 0; j < 100; j++)
        //        {
        //            mainGraphics.DrawRectangle(Pens.Red, new Rectangle(i * 16, j * 16, 16, 16));
        //        }
        //    Image image = Image.FromFile(imagePath);

        //    mainGraphics.DrawImage(image, new Point(0, 0));
        //}

        //private bool draw = false;

        //private void workPanel_Paint(object sender, PaintEventArgs e)
        //{
        //    if (!draw)
        //    {
        //        draw = true;
        //        for (int i = 0; i < 10; i++)
        //            for (int j = 0; j < 10; j++)
        //            {
        //                mainGraphics.DrawRectangle(Pens.Red, new Rectangle(i * 20, j * 20, 20, 20));
        //            }
        //        Image image = Image.FromFile(imagePath);

        //        mainGraphics.DrawImage(image, new Point(0, 0));
        //    }
        //}

        //private void mainPanel_Scroll(object sender, ScrollEventArgs e)
        //{
        //    draw = false;
        //}
    }
}

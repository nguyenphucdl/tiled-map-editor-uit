using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TiledMapDemo1.Model;

namespace TiledMapDemo1
{
    public class WorkplaceModel
    {
        #region Fields Renew
        private bool m_Initialized = false;
        private TileMap m_TileMap = null;
        private string m_FilePath = null;
        private BufferedGraphic m_WorkPlaceGraphic = null;
        private BufferedGraphic m_TileSheetGraphic = null;
        private LayerContext m_WorkPlaceContext = null;
        private LayerContext m_TileSetContext = null;
        //private List<MapLayer> m_MapLayers = null;
        //private List<TileSet> m_Tilesets = null;
        private Dictionary<int, Tile> m_GidTileDict = null;
        private Dictionary<string, Image> m_ImageDict = null;
        private Dictionary<string, TileSet> m_TileSetDict = null;
        private Dictionary<string, MapLayer> m_MapLayerDict = null;
        private GridLayer m_WorkPlaceGrid = null;
        private GridLayer m_TileSheetGrid = null;
        #endregion

        #region Setters & Getters
        public bool Initialized
        {
            get { return m_Initialized; }
            private set { m_Initialized = value; }
        }
        public void SetTiledMap(TileMap tileMap)
        {
            m_TileMap = tileMap;
        }
        public void SetWorkPlaceGraphic(BufferedGraphic workplaceGraphic)
        {
            m_WorkPlaceGraphic = workplaceGraphic;
        }
        public void SetTileSheetGraphic(BufferedGraphic tileSheetGraphic)
        {
            m_TileSheetGraphic = tileSheetGraphic;
        }
        public TileSet CurrentTileSet
        {
            get
            {
                if (m_TileSetDict.ContainsKey(m_TileSetContext.CurrentLayerId))
                    return m_TileSetDict[m_TileSetContext.CurrentLayerId];
                return null;
            }
        }

        #endregion


        #region Constructor
        public WorkplaceModel()
        {

        }
        #endregion

        #region Functions
        public bool LoadTileMap(string mapPath)
        {
            if (!Init(mapPath))
                return false;

            LoadWorkspace();

            m_Initialized = true;
            return true;
        }
        private bool Init(string mapPath)
        {
            if (!CheckRequirements())
                return false;
            m_WorkPlaceContext = new LayerContext();
            m_TileSetContext = new LayerContext();
            m_GidTileDict = new Dictionary<int, Tile>();
            m_ImageDict = new Dictionary<string, Image>();
            m_TileSetDict = new Dictionary<string, TileSet>();
            m_MapLayerDict = new Dictionary<string, MapLayer>();

            m_FilePath = mapPath;

            
            TmxLoader mapLoader = new TmxLoader(mapPath);
            if (!mapLoader.Read())
                return false;

            m_TileMap = mapLoader.GetTileMap();

            foreach (TileSet tileSet in m_TileMap.TileSets)
            {
                m_TileSetDict.Add(tileSet.Name, tileSet);
            }

            foreach (MapLayer layer in m_TileMap.Layers)
            {
                m_MapLayerDict.Add(layer.Name, layer);
            }
            return true;
        }
        private void LoadWorkspace()
        {
            LoadGidDict();
            LoadMapLayer();
            ConfigGraphics();
            LoadGridLayer();
        }

        private void LoadGidDict()
        {
            foreach (TileSet tileSet in m_TileSetDict.Values)
            {
                int firstId = tileSet.FirstId;
                int m = tileSet.Image.Width / tileSet.TileWidth;
                int n = tileSet.Image.Height / tileSet.TileHeight;
                int uid;
                Image tileSetImage = Image.FromFile(m_TileMap.Path + @"\..\" + tileSet.Image.Name);
                m_ImageDict.Add(tileSet.Image.Name, tileSetImage);
                m_TileSetContext.AddLayer(tileSet.Name, 10);


                for (int j = 0; j < n; j++)
                {
                    for (int i = 0; i < m; i++)
                    {
                        Rectangle region = new Rectangle(i * tileSet.TileWidth, j * tileSet.TileHeight, tileSet.TileWidth - 1, tileSet.TileHeight - 1);
                        uid = firstId++;
                        Tile tile = new Tile(uid, tileSet.Image, region);
                        // Add uid & tile to Gid Dict
                        m_GidTileDict.Add(uid, tile);

                        // Add region to tilesheet context
                        m_TileSetContext.DrawImage(tileSetImage, region, region);
                    }
                }
            }

            m_TileSetContext.CurrentIndex = 0;
            m_TileSetContext.HideAll();
            m_TileSetContext.Show(0);
        }

        private void LoadMapLayer()
        {
            int index = 10, drawId;

            foreach (MapLayer layer in m_MapLayerDict.Values)
            {
                m_WorkPlaceContext.AddLayer(layer.Name, 10);

                // Render strict (1 layer render in 2 times)
                // First draw i + j % 2 == 0
                for (int j = 0; j < layer.Height; j++)
                {
                    for (int i = 0; i < layer.Width; i++)
                    {
                        if ((i + j) % 2 == 0)
                            continue;
                        drawId = layer.TileIds[i, j];
                        Tile temp = m_GidTileDict[drawId];
                        Rectangle region = temp.Region;
                        Rectangle dest = new Rectangle(i * m_TileMap.TileWidth - 1, j * m_TileMap.TileHeight - 1, m_TileMap.TileWidth + 2, m_TileMap.TileHeight + 2);

                        m_WorkPlaceContext.DrawImage(m_ImageDict[temp.Image.Name], region, dest);

                    }
                }
                // Second draw i + j % 2 != 0
                for (int j = 0; j < layer.Height; j++)
                {
                    for (int i = 0; i < layer.Width; i++)
                    {
                        if ((i + j) % 2 != 0)
                            continue;
                        drawId = layer.TileIds[i, j];
                        Tile temp = m_GidTileDict[drawId];
                        Rectangle region = temp.Region;
                        Rectangle dest = new Rectangle(i * m_TileMap.TileWidth, j * m_TileMap.TileHeight, m_TileMap.TileWidth, m_TileMap.TileHeight);

                        m_WorkPlaceContext.DrawImage(m_ImageDict[temp.Image.Name], region, dest);
                    }
                }

                index += 10;
            }
        }

        private void LoadGridLayer()
        {
            m_WorkPlaceGrid = new GridLayer(m_WorkPlaceContext, "WorkPlaceGrid", 100, new Size(m_TileMap.TileWidth, m_TileMap.TileHeight), new Size(m_TileMap.Width * m_TileMap.TileWidth, m_TileMap.Height * m_TileMap.TileHeight));
            m_WorkPlaceGrid.SetGraphicsContext(m_WorkPlaceGraphic.CreateGraphics());
            m_WorkPlaceContext.RemoveLayer("WorkPlaceGrid");
            m_WorkPlaceContext.AddLayer(m_WorkPlaceGrid);

            m_TileSheetGrid = new GridLayer(m_TileSetContext, "TileSheetGrid", 100, new Size(m_TileMap.TileWidth, m_TileMap.TileHeight), new Size(m_TileSheetGraphic.Width, m_TileSheetGraphic.Height));
            m_TileSheetGrid.SetGraphicsContext(m_TileSheetGraphic.CreateGraphics());
            m_TileSetContext.RemoveLayer("TileSheetGrid");
            m_TileSetContext.AddLayer(m_TileSheetGrid);
        }

        private void ConfigGraphics()
        {
            TileSet curTileSet = m_TileSetDict[m_TileSetContext.CurrentLayerId];
            m_TileSheetGraphic.Zoom = 1.5f;
            m_TileSheetGraphic.WorkplaceSize = new Size((int)(curTileSet.Width * m_TileSheetGraphic.Zoom), (int)(curTileSet.Height * m_TileSheetGraphic.Zoom));
            

            m_WorkPlaceGraphic.WorkplaceSize = new Size(m_TileMap.Width * m_TileMap.TileWidth, m_TileMap.Height * m_TileMap.TileHeight);

            
        }

        private bool CheckRequirements()
        {
            if (m_WorkPlaceGraphic == null || m_TileSheetGraphic == null)
                return false;
            return true;
        }
        #endregion









        #region Drawing
        private bool m_drawing = false;

        public bool Drawing
        {
            get { return m_drawing; }
            set { m_drawing = value; }
        }
        
        #endregion








        #region Functions

        public void WorkplaceGraphic_SizeChange(EventArgs e)
        {
            if (Initialized)
                m_WorkPlaceGrid.SetGraphicsContext(m_WorkPlaceGraphic.CreateGraphics());
        }

        

        public void WorkplaceGraphic_MouseMove(MainForm owner, MouseEventArgs e)
        {
            if (!Initialized)
                return;

            int cellSize = (int)(16 * m_WorkPlaceGraphic.Zoom);
            Point movePoint = new Point(e.X / cellSize, e.Y / cellSize);

            int gid = m_TileMap.Layers[0].TileIds[movePoint.X, movePoint.Y];


            if(!Drawing)
                m_WorkPlaceGrid.HightLight(movePoint.X, movePoint.Y, m_WorkPlaceGraphic.Zoom);

            if (_sheetChoose.Count != 0)
            {
                Image srcImg = m_ImageDict[m_GidTileDict[_sheetChoose[0]].Image.Name];
                Tile tile = m_GidTileDict[_sheetChoose[0]];
                Rectangle src = m_GidTileDict[_sheetChoose[0]].Region;
                Rectangle dest = new Rectangle(movePoint.X * m_TileMap.Size.TileWidth, movePoint.Y * m_TileMap.Size.TileHeight, src.Width + 1, src.Height + 1);
                m_WorkPlaceGrid.DrawDirect(srcImg, dest, src, m_WorkPlaceGraphic.Zoom);
            }

            owner.lblPosMove.Text = "Index [" + movePoint.X + "," + movePoint.Y + "], e[" + e.X + "," + e.Y + "]";
            owner.lblGid.Text = "Gid [" + gid + "]";

            if (Drawing)
            {
                if(m_BeginDrawingPoint.X == -1 || m_BeginDrawingPoint.Y == -1)
                    return;
                Point beginPoint = new Point((int)(m_BeginDrawingPoint.X * m_WorkPlaceGraphic.Zoom),(int)(m_BeginDrawingPoint.Y * m_WorkPlaceGraphic.Zoom));
                Point toPoint = new Point((int)(e.X * m_WorkPlaceGraphic.Zoom), (int)(e.Y * m_WorkPlaceGraphic.Zoom));
                int deltaX = toPoint.X - beginPoint.X;
                int deltaY = toPoint.Y - beginPoint.Y;
                Point location = new Point();
                if (deltaX > 0 && deltaY > 0)
                {
                    location = new Point(beginPoint.X, beginPoint.Y);
                }
                else if (deltaX > 0 && deltaY < 0)
                {
                    location = new Point(beginPoint.X, beginPoint.Y + deltaY);
                }
                else if (deltaY > 0)
                {
                    location = new Point(toPoint.X, toPoint.Y - deltaY);
                }
                else
                {
                    location = new Point(toPoint.X, toPoint.Y);
                }

                m_WorkPlaceGrid.DrawRectangleDirect(location.X, location.Y, (int)Math.Abs(deltaX), (int)Math.Abs(deltaY), 1 / m_WorkPlaceGraphic.Zoom);

            }
        }

        Point m_BeginDrawingPoint = new Point(-1, -1);

        public void workplaceGraphic_MouseClick(MainForm mainForm, MouseEventArgs e)
        {
            //if (Drawing)
            //    m_BeginDrawingPoint = new Point((int)(m_WorkPlaceGraphic.Zoom), (int)(m_WorkPlaceGraphic.Zoom));
        }

        public void worlplaceGraphic_MouseDown(MainForm mainForm, MouseEventArgs e)
        {
            if (Drawing)
                m_BeginDrawingPoint = new Point(e.X, e.Y);
        }

        public void tileSheetGraphic_MouseMove(MainForm owner, MouseEventArgs e)
        {
            if (!Initialized)
                return;
            
            int tw = (int)(CurrentTileSet.TileWidth * m_TileSheetGraphic.Zoom);
            int th = (int)(CurrentTileSet.TileHeight * m_TileSheetGraphic.Zoom);

            Point moveIndex = new Point(e.X / tw, e.Y / th);
            
            int gid = moveIndex.Y * CurrentTileSet.Dimension.X + moveIndex.X + 1;

            if (!m_GidTileDict.ContainsKey(gid))
                return;

            m_TileSheetGrid.HightLight(moveIndex.X, moveIndex.Y, m_TileSheetGraphic.Zoom);
            Tile tile = null;
            if (m_GidTileDict.TryGetValue(gid, out tile))
            {
                owner.lblGid.Text = "Gid [" + gid + "], Index[" + moveIndex.X + "," + moveIndex.Y + "]";
            }
        }

        public List<int> _sheetChoose = new List<int>();

        public void tileSheetGraphic_MouseClick(MainForm owner, MouseEventArgs e)
        {
            if (!Initialized)
                return;
            int cellSize = (int)(16 * m_TileSheetGraphic.Zoom);
            Point movePoint = new Point(e.X / cellSize, e.Y / cellSize);
            int gid = movePoint.Y * 11 + movePoint.X + 1;
            Tile tile = null;
            
            if (m_GidTileDict.TryGetValue(gid, out tile))
            {
                if (_sheetChoose.Contains(gid))
                {
                    m_TileSheetGrid.RemoveRegion(tile.Region);
                    _sheetChoose.Remove(gid);
                }
                else
                {
                    DrawingRectangle shape = new DrawingRectangle(tile.Region.X, tile.Region.Y, tile.Region.Width, tile.Region.Height);
                    shape.DrawingPen = new Pen(Brushes.DarkBlue, 2);
                    m_TileSheetGrid.AddDrawingShape(shape);
                    _sheetChoose.Add(gid);
                }
                m_TileSheetGraphic.Dirty = true;
            }
        }

        public void DrawWorkPlace(Graphics graphics)
        {
            if (!Initialized)
                return;
            m_WorkPlaceContext.Draw(graphics);
        }

        public void DrawTileSheet(Graphics graphics)
        {
            if (!Initialized)
                return;
            m_TileSetContext.Draw(graphics);
        }
        #endregion


        
    }
}

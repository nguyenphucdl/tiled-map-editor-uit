using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TiledMapDemo1.Model;

namespace TiledMapDemo1
{
    public enum DrawingType
    {
        RECTANGLE,
        LINE
    }

    public class WorkplaceModel
    {
        #region Fields Renew
        private bool m_Initialized = false;
        private TileMap m_TileMap = null;
        private int m_currentGID = -1;
        private bool m_selectObjectMode = false;
        private ObjectProperty m_newObjectProperty = null;
        public TileMap TileMap
        {
            get { return m_TileMap; }
            set { m_TileMap = value; }
        }
        private string m_FilePath = null;
        private BufferedGraphic m_WorkPlaceGraphic = null;
        private BufferedGraphic m_TileSheetGraphic = null;
        private LayerContext m_WorkPlaceContext = null;
        private LayerContext m_TileSetContext = null;
        private Dictionary<int, Tile> m_GidTileDict = null;
        private Dictionary<string, Image> m_ImageDict = null;
        private Dictionary<string, TileSet> m_TileSetDict = null;
        private Dictionary<string, MapLayer> m_MapLayerDict = null;
        private GridLayer m_WorkPlaceGrid = null;
        private GridLayer m_TileSheetGrid = null;
        private TreeView m_LayersTreeView = null;
        private PropertyGrid m_PropertyGrid = null;
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
        public void SetLayerTreeView(TreeView layerTreeView)
        {
            m_LayersTreeView = layerTreeView;
        }
        public void SetPropertyMapGrid(PropertyGrid propertyGrid)
        {
            m_PropertyGrid = propertyGrid;
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

        #region Load Map Functions
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

        private void LoadTreeView()
        {
            Font boldFont = new Font(m_LayersTreeView.Font, FontStyle.Bold);
            TreeNode mapRootNode = new TreeNode("Map");
            TreeNode mapLayerNode = new TreeNode("Layers");
            TreeNode mapTileSheetsNode = new TreeNode("Tile Sheets");
            mapRootNode.NodeFont = boldFont;
            mapLayerNode.NodeFont = boldFont;
            mapTileSheetsNode.NodeFont = boldFont;
            mapRootNode.Nodes.Add(mapLayerNode);
            mapRootNode.Nodes.Add(mapTileSheetsNode);

            #region Load Layers View
            foreach (MapLayer l in m_MapLayerDict.Values)
            {
                TreeNode lNode = new TreeNode(l.Name);
                lNode.Name = l.Name;
                lNode.Tag = l.Type.ToString();
                mapLayerNode.Nodes.Add(lNode);
            }
            foreach (TileSet tileSet in m_TileSetDict.Values)
            {
                TreeNode tNode = new TreeNode(tileSet.Name);
                tNode.Name = tileSet.Name;
                tNode.Tag = "TileSheet";
                mapTileSheetsNode.Nodes.Add(tNode);
            }
            #endregion

            m_LayersTreeView.Nodes.Clear();
            m_LayersTreeView.Nodes.Add(mapRootNode);
            m_LayersTreeView.ExpandAll();
        }

        private void LoadWorkspace()
        {
            LoadGidDict();
            LoadMapLayer();
            ConfigGraphics();
            LoadGridLayer();
            LoadTreeView();
        }

        private void LoadGidDict()
        {
            m_TileSetContext.Clear();
            m_ImageDict.Clear();

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
            if (m_TileSetDict.Values.Count != 0)
            {
                m_TileSetContext.CurrentIndex = 0;
                m_TileSetContext.HideAll();
                m_TileSetContext.Show(0);
            }

            m_TileSheetGraphic.Dirty = true;
        }

        private void LoadMapLayer()
        {
            int index = 10, drawId;
            Color drawingColor, defaultColor = Color.Blue;
            m_WorkPlaceContext.Clear();

            foreach (MapLayer layer in m_MapLayerDict.Values)
            {
                m_WorkPlaceContext.AddLayer(layer.Name, 10, layer.Type);

                if (layer.Type == LayerType.TILEMAP)
                {
                    TileMapLayer tileMapLayer = (TileMapLayer)layer;

                    #region Render Tile Map Strict
                    // Render strict (1 layer render in 2 times)
                    // First draw i + j % 2 == 0
                    for (int j = 0; j < tileMapLayer.Height; j++)
                    {
                        for (int i = 0; i < tileMapLayer.Width; i++)
                        {
                            if ((i + j) % 2 == 0)
                                continue;
                            drawId = tileMapLayer.TileIds[i, j];
                            Tile temp = m_GidTileDict[drawId];
                            
                            Rectangle region = temp.Region;
                            Rectangle dest = new Rectangle(i * m_TileMap.TileWidth - 1, j * m_TileMap.TileHeight - 1, m_TileMap.TileWidth + 2, m_TileMap.TileHeight + 2);

                            
                            if(m_ImageDict.ContainsKey(temp.Image.Name))
                                m_WorkPlaceContext.DrawImage(m_ImageDict[temp.Image.Name], region, dest);

                        }
                    }
                    // Second draw i + j % 2 != 0
                    for (int j = 0; j < tileMapLayer.Height; j++)
                    {
                        for (int i = 0; i < tileMapLayer.Width; i++)
                        {
                            if ((i + j) % 2 != 0)
                                continue;
                            drawId = tileMapLayer.TileIds[i, j];
                            Tile temp = m_GidTileDict[drawId];
                            
                            Rectangle region = temp.Region;
                            Rectangle dest = new Rectangle(i * m_TileMap.TileWidth, j * m_TileMap.TileHeight, m_TileMap.TileWidth, m_TileMap.TileHeight);
                            if (m_ImageDict.ContainsKey(temp.Image.Name))
                                m_WorkPlaceContext.DrawImage(m_ImageDict[temp.Image.Name], region, dest);
                        }
                    }
                    #endregion
                }
                else if (layer.Type == LayerType.OBJECT)
                {
                    TileObjectGroup tileObjectGroup = (TileObjectGroup)layer;

                    #region render Object
                    foreach (TileObject tiobj in tileObjectGroup.Objects)
                    {
                        if (tiobj.ObjectType == TileObjectType.NORMAL)
                        {
                            Point pos = tiobj.Position;
                            Size size = (tiobj.Size.Width != -1)? tiobj.Size: new Size(1, 1);
                            drawingColor = defaultColor;
                            if (tiobj.Color != Color.Pink) 
                                drawingColor = tiobj.Color;

                            m_WorkPlaceContext.DrawRectange(pos.X, pos.Y, size.Width, size.Height, LayerType.OBJECT, drawingColor);
                        }
                        else if (tiobj.ObjectType == TileObjectType.POLYLINE)
                        {
                            string dataLines = tiobj.Data.ToString();

                            string[] dataPoints = dataLines.Split(' ', ',');

                            Point porigin = tiobj.Position;
                            Point pfrom = porigin, pto = new Point(-1, -1);
                            int xOffset, yOffset = 0;

                            drawingColor = defaultColor;
                            if (tiobj.Color != Color.Pink)
                                drawingColor = tiobj.Color;

                            for (int i = 2; i < dataPoints.Length; i += 2)
                            {
                                xOffset = int.Parse(dataPoints[i]);
                                yOffset = int.Parse(dataPoints[i + 1]);
                                
                                pto = new Point(porigin.X + xOffset, porigin.Y + yOffset);
                                m_WorkPlaceContext.DrawLine(pfrom.X, pfrom.Y, pto.X, pto.Y, LayerType.OBJECT, drawingColor);
                                pfrom = pto;
                            }
                        }
                    }
                    #endregion
                }

                index += 10;
            }

            m_WorkPlaceGraphic.Dirty = true;
            #region Load Layers Tree View
            
            #endregion
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
        private DrawingType m_drawingType = DrawingType.RECTANGLE;

        public bool Drawing
        {
            get { return m_drawing; }
            set { m_drawing = value; }
        }

        public DrawingType DrawingType
        {
            get { return m_drawingType; }
            set { m_drawingType = value; }
        }
        #endregion

        #region Functions

        public void WorkplaceGraphic_SizeChange(EventArgs e)
        {
            if (Initialized)
                m_WorkPlaceGrid.SetGraphicsContext(m_WorkPlaceGraphic.CreateGraphics());
        }

        public void treeViewMapExplorer_AfterSelect(object sender, TreeViewEventArgs e)
        {
            String nodeText = e.Node.Name;
            if (e.Node.Tag != null)
            {
                String nodeTag = e.Node.Tag.ToString();
                if (nodeTag == LayerType.OBJECT.ToString())
                {
                    m_PropertyGrid.SelectedObject = m_MapLayerDict[nodeText];
                }
                else if (nodeTag == LayerType.TILEMAP.ToString())
                {
                    m_PropertyGrid.SelectedObject = m_MapLayerDict[nodeText];
                }
                else if (nodeTag == "TileSheet")
                {
                    m_PropertyGrid.SelectedObject = m_TileSetDict[nodeText];
                }
            }
        }

        public void ResetPropertyGrid()
        {
            m_PropertyGrid.SelectedObject = null;
        }
        #region Add & Remove Layer
        public void toolRemoveLayer_Click(object sender, EventArgs e)
        {
            TreeNode removeNode = m_LayersTreeView.SelectedNode;
            if (removeNode.Tag != null)
            {
                String tagName = removeNode.Tag.ToString();
                if (tagName == "Layer")
                {
                    m_MapLayerDict.Remove(removeNode.Name);
                    if (removeNode.Name == "Object Layer")
                    {
                        m_WorkPlaceContext.RemoveLayer(removeNode.Name);
                    }
                }
                else if (tagName == "TileSheet")
                {
                    m_TileSetDict.Remove(removeNode.Name);
                    LoadGidDict();
                    LoadMapLayer();
                }
                
                LoadTreeView();
                ResetPropertyGrid();
                m_WorkPlaceGraphic.Dirty = true;
            }
            

        }

        public void toolNewLayer_Click(object sender, EventArgs e)
        {
            if(!m_MapLayerDict.ContainsKey("Object Layer"))
            {
                MapLayer newLayer = new MapLayer("Object Layer", m_TileMap.Width, m_TileMap.Height);
                newLayer.Type = LayerType.OBJECT;
                m_MapLayerDict.Add(newLayer.Name, newLayer);
                m_WorkPlaceContext.AddLayer(newLayer.Name, 10, LayerType.OBJECT);
                LoadTreeView();


               TileObjectGroup newTileObjectGr = new TileObjectGroup("Object Layer", m_TileMap.Width, m_TileMap.Height);
               newTileObjectGr.Type = LayerType.OBJECT;
               m_TileMap.Layers.Add(newTileObjectGr);
            }
        }
        #endregion

        #region Save
        public void saveToolStripButton_Click(object sender, EventArgs e)
        {
            //Test
            Bitmap bitmap = new Bitmap(m_WorkPlaceGraphic.Width, m_WorkPlaceGraphic.Height);
            m_WorkPlaceGraphic.DrawToBitmap(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
            bitmap.Save(m_TileMap.Path + @"\..\" + "preview.png");
            

            TmxWriter mapWriter = new TmxWriter(m_TileMap);
            mapWriter.Read();
            mapWriter.Save(m_TileMap.Path);
        }
        #endregion

        public void WorkplaceGraphic_MouseMove(MainForm owner, MouseEventArgs e)
        {
            if (!Initialized)
                return;

            int cellSize = (int)(CurrentTileSet.TileWidth * m_WorkPlaceGraphic.Zoom);
            Point movePoint = new Point(e.X / cellSize, e.Y / cellSize);
            
            TileMapLayer tileMapLayer = (TileMapLayer)m_TileMap.Layers[0];
            int gid = 0;
            try
            {
                gid = tileMapLayer.TileIds[movePoint.X, movePoint.Y];
            }
            catch(IndexOutOfRangeException pe)
            {
                return;
            }
            


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

            if (Drawing)
            {
                if (m_BeginDrawingPoint.X == -1 || m_BeginDrawingPoint.Y == -1)
                    return;
                Point beginPoint = new Point((int)(m_BeginDrawingPoint.X * m_WorkPlaceGraphic.Zoom), (int)(m_BeginDrawingPoint.Y * m_WorkPlaceGraphic.Zoom));
                Point toPoint = new Point((int)(e.X * m_WorkPlaceGraphic.Zoom), (int)(e.Y * m_WorkPlaceGraphic.Zoom));


                if (DrawingType == DrawingType.RECTANGLE)
                {
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
                else if (DrawingType == DrawingType.LINE)
                {
                    m_WorkPlaceGrid.DrawLineDirect(beginPoint.X, beginPoint.Y, toPoint.X, toPoint.Y, 1/ m_WorkPlaceGraphic.Zoom);
                }
                
                
            }


            owner.lblPosMove.Text = "Index [" + movePoint.X + "," + movePoint.Y + "], e[" + e.X + "," + e.Y + "]";
            owner.lblGid.Text = "Gid [" + gid + "]";
            m_currentGID = gid;
        }

        Point m_BeginDrawingPoint = new Point(-1, -1);

        public void workplaceGraphic_MouseClick(MainForm mainForm, MouseEventArgs e)
        {
            if (_sheetChoose.Count != 0)
            {
                Drawing = false;
                Point idx = MapIndexOfPoint(new Point(e.X, e.Y));

                Image srcImg = m_ImageDict[m_GidTileDict[_sheetChoose[0]].Image.Name];
                Tile tile = m_GidTileDict[_sheetChoose[0]];
                Rectangle src = m_GidTileDict[_sheetChoose[0]].Region;
                Rectangle dest = new Rectangle(idx.X * m_TileMap.Size.TileWidth, idx.Y * m_TileMap.Size.TileHeight, src.Width + 2, src.Height + 2);

                TileMapLayer tileMapLayer = (TileMapLayer)m_TileMap.Layers[0];
                tileMapLayer.TileIds[idx.X, idx.Y] = tile.Id; // Update tile map
                
                m_WorkPlaceContext.DrawImage(srcImg, src, dest, LayerType.TILEMAP);

                
                m_WorkPlaceGraphic.Dirty = true;
            }

            if(m_selectObjectMode)
            {
                Rectangle selectedBound = new Rectangle();
                List<DrawingShape> listRemove = new List<DrawingShape>();
                foreach(Layer layer in m_WorkPlaceContext.Layers)
                {
                    if(layer.Type == LayerType.OBJECT)
                    {
                        DrawingRectangle rectDrawing = null;
                        foreach(DrawingShape shape in layer.Shapes)
                        {
                            if(shape is DrawingRectangle)
                            {
                                rectDrawing = (DrawingRectangle)shape;

                                Rectangle rect = rectDrawing.GetBound();

                                Point test = e.Location;
                                float zoom = m_WorkPlaceGraphic.Zoom;
                                test.X = (int)(test.X / zoom);
                                test.Y = (int)(test.Y / zoom);

                                if(rect.Contains(test))
                                {
                                    selectedBound = rect;
                                    SearchTileObjectWithBound(selectedBound);
                                    if (m_newObjectProperty != null && m_newObjectProperty.RemoveState)
                                    {
                                        listRemove.Add(shape);
                                    }
                                }
                            }
                        }
                        if (listRemove.Count != 0)
                        {
                            foreach(DrawingShape deleteShape in listRemove)
                            {
                                layer.Shapes.Remove(deleteShape);
                            }
                            listRemove.Clear();
                            m_WorkPlaceGraphic.Dirty = true;
                        }
                    }
                }

                
            }
            
        }

        private void SearchTileObjectWithBound(Rectangle boundFind)
        {
            TileObject deleteObj = null;
            foreach (MapLayer l in m_TileMap.Layers)
            {
                if (l.Type == LayerType.OBJECT)
                {
                    TileObjectGroup tileObjGr = (TileObjectGroup)l;
                    foreach (TileObject obj in tileObjGr.Objects)
                    {
                        Rectangle objBound = obj.GetBound();
                        if (objBound == boundFind)
                        {
                            m_newObjectProperty = new ObjectProperty();
                            m_newObjectProperty.TileObject = obj;
                            if (m_newObjectProperty.ShowDialog() == DialogResult.Abort)
                            {
                                deleteObj = obj;
                            }
                        }
                    }
                    if(m_newObjectProperty != null && m_newObjectProperty.RemoveState)
                    {
                        tileObjGr.Objects.Remove(deleteObj);
                    }
                }
            }
        }

        private Point MapIndexOfPoint(Point e)
        {
            int cellSize = (int)(CurrentTileSet.TileWidth * m_WorkPlaceGraphic.Zoom);
            Point idx = new Point(e.X / cellSize, e.Y / cellSize);
            return idx;
        }

        private int MapGidOfPoint(Point e)
        {
            Point idx = MapIndexOfPoint(e);
            return (m_TileMap.Layers[0] as TileMapLayer).TileIds[idx.X, idx.Y];
        }

        private void ResetDrawingState()
        {
            m_BeginDrawingPoint = new Point(-1, -1);
            Drawing = false;
            Tile oldTile = null;
            foreach (int oldGid in _sheetChoose)
            {
                if (!_sheetChoose.Contains(oldGid))
                    return;
                if (!m_GidTileDict.TryGetValue(oldGid, out oldTile))
                    return;

                m_TileSheetGrid.RemoveRegion(oldTile.Region);
                //_sheetChoose.Remove(oldGid);
            }
            _sheetChoose.Clear();
            m_TileSheetGraphic.Dirty = true;
        }

        public void worlplaceGraphic_MouseDown(MainForm mainForm, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ResetDrawingState();
            }

            if (Drawing)
                m_BeginDrawingPoint = new Point(e.X, e.Y);
        }

        public void worlplaceGraphic_MouseUp(MainForm mainForm, MouseEventArgs e)
        {
            if (Drawing)
            {
                if (m_BeginDrawingPoint.X == -1 || m_BeginDrawingPoint.Y == -1)
                    return;
                Point beginPoint = new Point((int)(m_BeginDrawingPoint.X * 1/ m_WorkPlaceGraphic.Zoom), (int)(m_BeginDrawingPoint.Y * 1/ m_WorkPlaceGraphic.Zoom));
                Point toPoint = new Point((int)(e.X * 1/ m_WorkPlaceGraphic.Zoom), (int)(e.Y * 1/ m_WorkPlaceGraphic.Zoom));
                TileObject tiObj = new TileObject(); 
                if (DrawingType == DrawingType.RECTANGLE)
                {
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

                    //m_WorkPlaceGrid.DrawRectangleDirect(location.X, location.Y, (int)Math.Abs(deltaX), (int)Math.Abs(deltaY), 1 / m_WorkPlaceGraphic.Zoom);

                    m_WorkPlaceContext.DrawRectange(location.X, location.Y, (int)(deltaX), (int)(deltaY), LayerType.OBJECT);

                    //tiObj = new TileObject("", location, new Size((int)(deltaX), (int)(deltaY)));
                    if(m_newObjectProperty != null)
                    {
                        tiObj.Type = m_newObjectProperty.TileObject.Type;
                        tiObj.Name = m_newObjectProperty.TileObject.Name;
                        tiObj.Position = location;
                        tiObj.Size = new Size((int)(deltaX), (int)(deltaY));
                    }
                    
                }
                else if (DrawingType == DrawingType.LINE)
                {
                    m_WorkPlaceContext.DrawLine(beginPoint.X, beginPoint.Y, toPoint.X, toPoint.Y, LayerType.OBJECT);

                    StringBuilder strBuilder = new StringBuilder();
                    strBuilder.AppendFormat("{0},{1}",beginPoint.X - beginPoint.X, beginPoint.Y - beginPoint.Y);
                    strBuilder.AppendFormat(" {0},{1}", toPoint.X - beginPoint.X, toPoint.Y - beginPoint.Y);

                    string dataLines = strBuilder.ToString();

                    tiObj = new TileObject("", new Point(beginPoint.X, beginPoint.Y), new Size(-1, -1));
                    tiObj.ObjectType = TileObjectType.POLYLINE;
                    tiObj.Position = beginPoint;
                    tiObj.Data = dataLines;
                }
                // Update tilemap Object layer
                foreach (MapLayer l in m_TileMap.Layers)
                {
                    if (l.Type == LayerType.OBJECT)
                    {
                        TileObjectGroup tileObjGr = (TileObjectGroup)l;
                        tiObj.Id = tileObjGr.Objects.Count + 1;
                        tileObjGr.AddObject(tiObj);
                    }
                }

                m_WorkPlaceGraphic.Dirty = true;
                m_BeginDrawingPoint.X = -1;
                m_BeginDrawingPoint.Y = -1;
            }
        }

        public void toolStripSelectObject_Click(object sender, EventArgs e)
        {
            m_selectObjectMode = true;
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

            int cellSize = (int)(CurrentTileSet.TileWidth * m_TileSheetGraphic.Zoom);
            Point movePoint = new Point(e.X / cellSize, e.Y / cellSize);
            int gid = movePoint.Y * CurrentTileSet.Dimension.X + movePoint.X + 1;
            Tile tile, oldTile = null;
            
            if (m_GidTileDict.TryGetValue(gid, out tile))
            {
                if (_sheetChoose.Count != 0)
                {
                    int oldGid = _sheetChoose[0];

                    if (!_sheetChoose.Contains(oldGid))
                        return;
                    if(!m_GidTileDict.TryGetValue(oldGid, out oldTile))
                        return;

                    m_TileSheetGrid.RemoveRegion(oldTile.Region);
                    _sheetChoose.Remove(oldGid);
                    
                }
                  
                DrawingRectangle shape = new DrawingRectangle(tile.Region.X, tile.Region.Y, tile.Region.Width, tile.Region.Height);
                shape.DrawingPen = new Pen(Brushes.Red, 2);
                m_TileSheetGrid.AddDrawingShape(shape);
                _sheetChoose.Add(gid);
                
                m_TileSheetGraphic.Dirty = true;
            }
        }

        public void itemPickTile_Click(object sender, EventArgs e)
        {
            int gid = m_currentGID;
            _sheetChoose.Add(gid);
        }

        public void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                _sheetChoose.Clear();
                m_WorkPlaceGraphic.Dirty = true;
                m_newObjectProperty = null;
                m_selectObjectMode = false;
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


        public void toolRectangleDrawing_Click(object sender, EventArgs e)
        {
            Drawing = true;
            DrawingType = DrawingType.RECTANGLE;

            m_newObjectProperty = new ObjectProperty();
            TileObject newTileObj = new TileObject();
            m_newObjectProperty.TileObject = newTileObj;
            if (m_newObjectProperty.ShowDialog() == DialogResult.Cancel)
            {
                m_newObjectProperty = null;
            }
        }
    }
}

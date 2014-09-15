using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapDemo1.Model;
using TiledMapDemo1.Utils;

namespace TiledMapDemo1.MapTools
{
    public class ImageToMap
    {
        #region TileMap results
        public TileMap TileMap { get; private set; }

        public List<TileCell> TileSetList { get; private set; }

        public int[,]MapInfo { get; private set; }

        public string MapName { get; private set; }

        public string MapSavePath { get; private set; }

        #endregion

        #region Image info
        public Image Image { get; private set; }

        /* ImageManipulation is converted from Image to Bitmap due to process with pixel data */
        public Bitmap ImageManipulation { get; private set; }

        public Size TileSize { get; private set; }

        public Size MapSize { get; private set; }

        public bool IsValid
        {
            get
            {
                return ImageToMap.isTiledMapValid(TileSize, Image, MapName);
            }
        }

        public bool IsProcessSuccess { get; private set; }
        #endregion

        #region Constructor
        public ImageToMap(int tileWidth, int tileHeight, Image image, string name)
        {
            Image = image;

            ImageManipulation = new Bitmap(Image);

            TileSize = new Size(tileWidth, tileHeight);

            MapSize = new Size(ImageManipulation.Width, ImageManipulation.Height);

            MapName = name;
            // Default results
            TileMap = null;
            TileSetList = null;
            MapInfo = null;
            IsProcessSuccess = false;
        }
        #endregion

        #region Functions
        public bool process(Action<int> reportProgressAction)
        {
            if (!IsValid) return false;

            Point mapDimension = new Point(MapSize.Width / TileSize.Width, MapSize.Height / TileSize.Height);

            double totalProgress = mapDimension.X * mapDimension.Y;
            
            double currentProgress = 0;



            TileSetList = new List<TileCell>();
            initMapInfo(mapDimension.Y, mapDimension.X);

            int startMapCode = 1;
            
            for (int i = 0; i < mapDimension.Y; i++)
            {
                for (int j = 0; j < mapDimension.X; j++)
                {
                    currentProgress++;

                    Rectangle tileRect = new Rectangle(j * TileSize.Width, i * TileSize.Height, TileSize.Width, TileSize.Height);
                    Image tileImage = ImageUtility.Crop(tileRect, ImageManipulation);

                    reportProgressAction((int)Math.Floor(currentProgress/ totalProgress * 100));

                    TileCell tile = new TileCell(i, j, tileImage);

                    if (TileSetList.Count == 0)
                    {
                        tile.MapCode = startMapCode;
                        startMapCode++;
                        TileSetList.Add(tile);
                    }
                    else
                    {
                        if (!IsTileExistInList(TileSetList, ref tile))
                        {
                            tile.MapCode = startMapCode;
                            startMapCode++;
                            TileSetList.Add(tile);
                        }
                    }
                    MapInfo[i, j] = tile.MapCode;
                }
            }

            FileUtility.CreateFolderIfNotExist(MapName);
            string tileSetName = MapName + "-TileSet" + TileSetList.Count + ".png";
            string mapFolder = MapName;
            string tileSetPath = MapName + "//" + tileSetName;
            FileUtility.DeleteFileIfExist(tileSetPath);

            Bitmap tileSetCombinedBitmap = ImageToMap.CombineTileSet(TileSetList, TileSize);

            // Save combined tileset bitmap to tileset path
            tileSetCombinedBitmap.Save(tileSetPath);
            


            // Prepare TileMap result
            TileSetImage tileSetImage = TileMapFactory.createTileSetImage(tileSetName, tileSetCombinedBitmap.Width, tileSetCombinedBitmap.Height);
            TileSet tileSetResult = TileMapFactory.createTileSet(1, MapName, TileSize.Width, TileSize.Height, tileSetImage);
            TileMapSize tileMapSize = TileMapFactory.createTileMapSize(MapSize.Width, MapSize.Height, TileSize.Width, TileSize.Height);
            
            int firstId = tileSetResult.FirstId;
            int offset = firstId - MapInfo[0, 0];
            for(int i = 0 ; i < mapDimension.Y; i++)
                for (int j = 0; j < mapDimension.X; j++)
                {
                    MapInfo[i, j] += offset;
                }
            MapLayer mapLayerResult = TileMapFactory.createMapLayer(MapName + "Layer1", MapSize.Width, MapSize.Height, MapInfo);

            TileMap = TileMapFactory.createTileMap(tileMapSize, TileMapFactory.packList<TileSet>(tileSetResult),
                                                    TileMapFactory.packList<MapLayer>(mapLayerResult), mapFolder);
            TmxFormat tileMapTmx = new TmxFormat(TileMap);
            string tileMapTMXPath = MapName + "\\" + MapName + ".tmx";
            MapSavePath =  Directory.GetCurrentDirectory() + "\\"  + tileMapTMXPath;
            FileUtility.DeleteFileIfExist(tileMapTMXPath);
            tileMapTmx.saveTileMap(MapSavePath);

            IsProcessSuccess = true;
            return true;
        }
        #endregion

        #region Valid checking utility
        private void initMapInfo(int m, int n)
        {
            MapInfo = new int[m, n];
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    MapInfo[i, j] = 0;
        }

        public static Bitmap CombineTileSet(List<TileCell> tileSetList, Size tileSize)
        {
            // Check valid param
            if (tileSetList.Count == 0)
                return null;

            int total = tileSetList.Count;

            int n = (int)Math.Ceiling(Math.Sqrt((double)total));


            Bitmap combinedBitmap = new Bitmap(n * tileSize.Width, n * tileSize.Height);

            Graphics g = Graphics.FromImage(combinedBitmap);

            Image imgDraw;
            int index = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (index < tileSetList.Count)
                    {
                        imgDraw = tileSetList.ElementAt<TileCell>(index).Image;
                        Point pos = new Point(j * tileSize.Width, i * tileSize.Height);
                        Rectangle rect = new Rectangle(pos, tileSize);
                        g.DrawImage(imgDraw, rect);
                        index++;
                    }
                }
            }
            g.Dispose();
            return combinedBitmap;
        }

        public static bool isTiledMapValid(Size tileSize, Image imageMap, string name)
        {
            if (imageMap == null || tileSize.Width < 1 || tileSize.Height < 1)
                return false;
            if ((imageMap.Width % tileSize.Width != 0) || (imageMap.Height % tileSize.Height != 0))
                return false;
            if (String.IsNullOrEmpty(name))
                return false;
            return true;
        }

        public static bool IsTileExistInList(List<TileCell> listTile, ref TileCell tile)
        {
            int count = 0;
            foreach (TileCell item in listTile)
            {
                if (DemoUtils.CompareBitmaps(item.Image, tile.Image) == true)
                {
                    tile.MapCode = item.MapCode;
                    return true;
                }
            }
            return false;
        }

        #endregion

    }
}

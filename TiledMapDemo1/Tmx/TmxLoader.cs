using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TiledMapDemo1.Model;
using TiledMapDemo1.Utils;

namespace TiledMapDemo1
{
    public class TmxLoader
    {
        private String m_tmxFilePath;
        private XDocument m_tmxDocument;
        private bool m_isValid = false;


        public TmxLoader(String filePath)
        {
            m_tmxFilePath = filePath;
        }

        public bool Read()
        {
            if (!File.Exists(m_tmxFilePath))
                return false;
            m_tmxDocument = XDocument.Load(m_tmxFilePath);
            m_isValid = true;
            return true;
        }

        public TileMap GetTileMap()
        {
            if(!m_isValid)
                return null;

            XElement mapElement = m_tmxDocument.Element("map");
            List<TileSet> tileSets = _parseListTileSets(mapElement.Elements("tileset"));
            List<MapLayer> layers = _parseMapLayers(mapElement.Elements("layer"));

            TileMap parseTileMap = _parseTileMapInfo(mapElement);
            parseTileMap.Path = m_tmxFilePath;
            parseTileMap.Layers = layers;
            parseTileMap.TileSets = tileSets;
            TileMapSize size = new TileMapSize(parseTileMap.Width, parseTileMap.Height, parseTileMap.TileWidth, parseTileMap.TileHeight);
            parseTileMap.Size = size;

            return parseTileMap;
        }

        private TileMap _parseTileMapInfo(XElement map)
        {
            string version = map.Attribute("version").Value;
            string orientation = map.Attribute("orientation").Value;
            int width = int.Parse(map.Attribute("width").Value);
            int height = int.Parse(map.Attribute("height").Value);
            int tilewidth = int.Parse(map.Attribute("tilewidth").Value);
            int tileheight = int.Parse(map.Attribute("tileheight").Value);

            TileMap tileMap = new TileMap();
            tileMap.Version = version;
            tileMap.Orientation = orientation;
            tileMap.Width = width;
            tileMap.Height = height;
            tileMap.TileWidth = tilewidth;
            tileMap.TileHeight = tileheight;
            return tileMap;
        }

        private List<MapLayer> _parseMapLayers(IEnumerable<XElement> mapLayerElements)
        {
            string layerName;
            int layerWidth, layerHeight, gid;
            List<MapLayer> layerList = new List<MapLayer>();

            foreach (XElement element in mapLayerElements)
            {
                layerName = element.Attribute("name").Value;
                layerWidth = int.Parse(element.Attribute("width").Value);
                layerHeight = int.Parse(element.Attribute("height").Value);
                MapLayer layer = new MapLayer(layerName, layerWidth, layerHeight);

                XElement dataElement = element.Element("data");

                List<XElement> tileElements = dataElement.Elements("tile").ToList();

                int[,] tileIds = new int[layerWidth, layerHeight ];

                for (int j = 0; j < layerHeight; j++)
                {
                    for (int i = 0; i < layerWidth; i++)
                    {
                        gid = int.Parse((tileElements.First()).Attribute("gid").Value);
                        tileIds[i, j] = gid;
                        tileElements.RemoveAt(0);
                    }
                }

                layer.TileIds = tileIds;

                layerList.Add(layer);
            }
            return layerList;
        }

        public List<TileSet> GetTileSets()
        {
            if (!m_isValid)
                return null;
            return null;
        }

        private List<TileSet> _parseListTileSets(IEnumerable<XElement> tilesetElements)
        {
            List<TileSet> list = new List<TileSet>();
            int firstId, lastId, tileWidth, tileHeight, imageWidth, imageHeight;
            string tilesetName, imageSource;

            foreach (XElement element in tilesetElements)
            {
                TileSet tileSet = new TileSet();
                tilesetName = element.Attribute("name").Value;
                tileWidth = int.Parse(element.Attribute("tilewidth").Value);
                tileHeight = int.Parse(element.Attribute("tileheight").Value);
                firstId = int.Parse(element.Attribute("firstgid").Value);

                XElement sourceElement = element.Element("image");
                imageSource = sourceElement.Attribute("source").Value;
                imageWidth = int.Parse(sourceElement.Attribute("width").Value);
                imageHeight = int.Parse(sourceElement.Attribute("height").Value);

                lastId = firstId + (imageWidth / tileWidth) * (imageHeight / tileHeight) -1;

                TileSetImage tilesetImage = new TileSetImage(imageSource, imageWidth, imageHeight);
                tileSet.FirstId = firstId;
                tileSet.LastId = lastId;
                tileSet.Name = tilesetName;
                tileSet.TileWidth = tileWidth;
                tileSet.TileHeight = tileHeight;
                tileSet.Image = tilesetImage;

                list.Add(tileSet);
            }

            return list;
        }

        public List<MapLayer> GetMapLayers()
        {
            if (!m_isValid)
                return null;
            return null;
        }
    }
}

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
    public class TmxWriter
    {
        private String      m_tmxSaveFilePath;
        private XDocument   m_tmxDocument;
        private bool        m_isValid = false;
        private TileMap     m_tileMap;

        public TmxWriter(TileMap tileMap)
        {
            m_tileMap = tileMap;

        }

        public bool Read() // Check valid tilemap
        {
            _parse();
            m_isValid = true;
            return true;
        }

        public bool Save(String m_tmxSaveFilePath)
        {
            if (!m_isValid)
                return false;
            m_tmxDocument.Save(m_tmxSaveFilePath);
            return true;
        }

        private void _parse()
        {
            m_tmxDocument = new XDocument();

            XElement map = new XElement("map");
            map.SetAttributeValue("version", m_tileMap.Version);
            //map.SetAttributeValue("name", m_tileMap.Name);
            map.SetAttributeValue("tilewidth", m_tileMap.TileWidth);
            map.SetAttributeValue("tileheight", m_tileMap.TileHeight);
            map.SetAttributeValue("width", m_tileMap.Width);
            map.SetAttributeValue("height", m_tileMap.Height);
            map.SetAttributeValue("orientation", m_tileMap.Orientation);
            
            foreach (TileSet ts in m_tileMap.TileSets)
            {
                XElement tileset = new XElement("tileset");
                tileset.SetAttributeValue("firstgid", ts.FirstId);
                tileset.SetAttributeValue("name", ts.Name);
                tileset.SetAttributeValue("tilewidth", ts.TileWidth);
                tileset.SetAttributeValue("tileheight", ts.TileHeight);

                XElement imagesource = new XElement("image");
                imagesource.SetAttributeValue("source", ts.Image.Name);
                imagesource.SetAttributeValue("width", ts.Image.Width);
                imagesource.SetAttributeValue("height", ts.Image.Height);

                tileset.Add(imagesource);
                map.Add(tileset);
            }

            // Parse Layers

            foreach (MapLayer l in m_tileMap.Layers)
            {
                if (l.Type == LayerType.TILEMAP)
                {
                    XElement layer = new XElement("layer");
                    layer.SetAttributeValue("name", l.Name);
                    layer.SetAttributeValue("width", l.Width);
                    layer.SetAttributeValue("height", l.Height);

                    XElement data = new XElement("data");

                    TileMapLayer tileMapLayer = (TileMapLayer)l;


                    for (int j = 0; j < tileMapLayer.Height; j++ )
                    {
                        for (int i = 0; i < tileMapLayer.Width; i++)
                        {
                            int tileId = tileMapLayer.TileIds[i, j];
                            XElement tile = new XElement("tile");
                            tile.SetAttributeValue("gid", tileId);
                            data.Add(tile);        
                        }
                    }
                    //foreach (int tileId in tileMapLayer.TileIds)
                    //{
                    //    XElement tile = new XElement("tile");
                    //    tile.SetAttributeValue("gid", tileId);
                    //    data.Add(tile);
                    //}

                    layer.Add(data);
                    map.Add(layer);
                }
                else if (l.Type == LayerType.OBJECT)
                {
                    TileObjectGroup objectgroup = (TileObjectGroup)l;

                    XElement objectgroupElem = new XElement("objectgroup");
                    objectgroupElem.SetAttributeValue("name", objectgroupElem.Name);

                    foreach (TileObject tiobj in objectgroup.Objects)
                    {
                        XElement objElem = new XElement("object");
                        objElem.SetAttributeValue("name", tiobj.Name);
                        if (tiobj.Type != "")
                        {
                            objElem.SetAttributeValue("type", tiobj.Type);
                        }
                        objElem.SetAttributeValue("x", tiobj.Position.X);
                        objElem.SetAttributeValue("y", tiobj.Position.Y);
                        if (tiobj.Size.Width != -1 && tiobj.Size.Height != -1)
                        {
                            objElem.SetAttributeValue("width", tiobj.Size.Width);
                            objElem.SetAttributeValue("height", tiobj.Size.Height);
                        }
                        if (tiobj.ObjectType == TileObjectType.POLYLINE)
                        {
                            XElement polylineElem = new XElement("polyline");
                            polylineElem.SetAttributeValue("points", tiobj.Data.ToString());
                            objElem.Add(polylineElem);
                        }

                        objectgroupElem.Add(objElem);
                    }
                    map.Add(objectgroupElem);
                }
            }


            m_tmxDocument.Add(map);
        }

      



        //public TileMap GetTileMap()
        //{
        //    //if(!m_isValid)
        //    //    return null;

        //    //XElement mapElement = m_tmxDocument.Element("map");
        //    //List<TileSet> tileSets = _parseListTileSets(mapElement.Elements("tileset"));
        //    //List<MapLayer> layers = _parseMapLayers(mapElement);

        //    //TileMap parseTileMap = _parseTileMapInfo(mapElement);
        //    //parseTileMap.Path = m_tmxFilePath;
        //    //parseTileMap.Layers = layers;
        //    //parseTileMap.TileSets = tileSets;
        //    //TileMapSize size = new TileMapSize(parseTileMap.Width, parseTileMap.Height, parseTileMap.TileWidth, parseTileMap.TileHeight);
        //    //parseTileMap.Size = size;

        //    //return parseTileMap;
        //}

        //private TileMap _parseTileMapInfo(XElement map)
        //{
        //    string version = map.Attribute("version").Value;
        //    string orientation = map.Attribute("orientation").Value;
        //    int width = int.Parse(map.Attribute("width").Value);
        //    int height = int.Parse(map.Attribute("height").Value);
        //    int tilewidth = int.Parse(map.Attribute("tilewidth").Value);
        //    int tileheight = int.Parse(map.Attribute("tileheight").Value);

        //    TileMap tileMap = new TileMap();
        //    tileMap.Version = version;
        //    tileMap.Orientation = orientation;
        //    tileMap.Width = width;
        //    tileMap.Height = height;
        //    tileMap.TileWidth = tilewidth;
        //    tileMap.TileHeight = tileheight;
        //    return tileMap;
        //}

        //private List<MapLayer> _parseMapLayers(XElement mapElement)
        //{
        //    string layerName;
        //    int layerWidth, layerHeight, gid, mapWidth, mapHeight;

        //    mapWidth = int.Parse(mapElement.Attribute("width").Value);
        //    mapHeight = int.Parse(mapElement.Attribute("height").Value);

        //    List<MapLayer> layerList = new List<MapLayer>();

        //    IEnumerable<XElement> mapLayerElements = mapElement.Elements("layer");

        //    foreach (XElement element in mapLayerElements)
        //    {
        //        layerName = element.Attribute("name").Value;
        //        layerWidth = int.Parse(element.Attribute("width").Value);
        //        layerHeight = int.Parse(element.Attribute("height").Value);
        //        MapLayer layer = new MapLayer(layerName, layerWidth, layerHeight);
        //        layer.Type = LayerType.TILEMAP;

        //        XElement dataElement = element.Element("data");

        //        List<XElement> tileElements = dataElement.Elements("tile").ToList();

        //        int[,] tileIds = new int[layerWidth, layerHeight ];

        //        for (int j = 0; j < layerHeight; j++)
        //        {
        //            for (int i = 0; i < layerWidth; i++)
        //            {
        //                gid = int.Parse((tileElements.First()).Attribute("gid").Value);
        //                tileIds[i, j] = gid;
        //                tileElements.RemoveAt(0);
        //            }
        //        }

        //        layer.TileIds = tileIds;

        //        layerList.Add(layer);
        //    }

        //    XElement objectLayerElement = mapElement.Element("objectgroup");
        //    if (objectLayerElement != null)
        //    {
        //        MapLayer objectLayer = new MapLayer("Object Layer", mapWidth, mapHeight);
        //        objectLayer.Type = LayerType.OBJECT;
        //        layerList.Add(objectLayer);
        //    }


        //    return layerList;
        //}

        //public List<TileSet> GetTileSets()
        //{
        //    if (!m_isValid)
        //        return null;
        //    return null;
        //}

        //private List<TileSet> _parseListTileSets(IEnumerable<XElement> tilesetElements)
        //{
        //    List<TileSet> list = new List<TileSet>();
        //    int firstId, lastId, tileWidth, tileHeight, imageWidth, imageHeight;
        //    string tilesetName, imageSource;

        //    foreach (XElement element in tilesetElements)
        //    {
        //        TileSet tileSet = new TileSet();
        //        tilesetName = element.Attribute("name").Value;
        //        tileWidth = int.Parse(element.Attribute("tilewidth").Value);
        //        tileHeight = int.Parse(element.Attribute("tileheight").Value);
        //        firstId = int.Parse(element.Attribute("firstgid").Value);

        //        XElement sourceElement = element.Element("image");
        //        imageSource = sourceElement.Attribute("source").Value;
        //        imageWidth = int.Parse(sourceElement.Attribute("width").Value);
        //        imageHeight = int.Parse(sourceElement.Attribute("height").Value);

        //        lastId = firstId + (imageWidth / tileWidth) * (imageHeight / tileHeight) -1;

        //        TileSetImage tilesetImage = new TileSetImage(imageSource, imageWidth, imageHeight);
        //        tileSet.FirstId = firstId;
        //        tileSet.LastId = lastId;
        //        tileSet.Name = tilesetName;
        //        tileSet.TileWidth = tileWidth;
        //        tileSet.TileHeight = tileHeight;
        //        tileSet.Image = tilesetImage;

        //        list.Add(tileSet);
        //    }

        //    return list;
        //}

        //public List<MapLayer> GetMapLayers()
        //{
        //    if (!m_isValid)
        //        return null;
        //    return null;
        //}
    }
}

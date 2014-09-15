using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TiledMapDemo1.Model;

namespace TiledMapDemo1
{
    public interface IFormatMapWriter
    {
        void saveTileMap(string filePath);
    }

    public class TmxFormat : IFormatMapWriter
    {
        public TileMap TileMap { get; private set; }

        public XDocument TileMapDoc { get; private set; }

        public TmxFormat(TileMap tileMap)
        {
            TileMap = tileMap;
        }

        private XElement createLayerElements(TileMap tileMap)
        {
            XElement mapLayerElement = new XElement("layer",
                        new XAttribute("name", tileMap.Layers[0].Name),
                        new XAttribute("width", tileMap.Size.Dimension.X),
                        new XAttribute("height", tileMap.Size.Dimension.Y)                
            );
            XElement mapData = new XElement("data");

            for(int i = 0; i < tileMap.Size.Dimension.Y; i++)
                for (int j = 0; j < tileMap.Size.Dimension.X; j++)
                {
                    XElement tileElement = new XElement("tile",
                                                new XAttribute("gid", tileMap.Layers[0].TileIds[i, j]));
                    mapData.Add(tileElement);
                }
            mapLayerElement.Add(mapData);
            return mapLayerElement;
        }

        private List<XElement> createTileSetListElements(IList<TileSet> listTileSet)
        {
            List<XElement> tileSetElements = new List<XElement>();

            foreach (TileSet tileSet in listTileSet)
            {
                XElement tileSetElement = new XElement("tileset",
                            new XAttribute("firstgid", tileSet.FirstId),
                            new XAttribute("name", tileSet.Name),
                            new XAttribute("tilewidth", tileSet.TileWidth),
                            new XAttribute("tileheight", tileSet.TileHeight)
                );
                XElement tileSetImage = this.createTileSetImageElement(tileSet);
                tileSetElement.Add(tileSetImage);

                tileSetElements.Add(tileSetElement);
            }
            return tileSetElements;
        }

        private XElement createTileSetImageElement(TileSet tileSet)
        {
            return new XElement("image",
                        new XAttribute("source", tileSet.Image.Name),
                        new XAttribute("width", tileSet.Image.Width),
                        new XAttribute("height", tileSet.Image.Height)
            );
        }

        private XElement createTileMapElement(TileMap tileMap)
        {
            XElement mapElement = new XElement("map",
                        new XAttribute("version", "1.0"),
                        new XAttribute("orientation", "orthogonal"),
                        new XAttribute("width", tileMap.Size.Dimension.X),
                        new XAttribute("height", tileMap.Size.Dimension.Y),
                        new XAttribute("tilewidth", tileMap.Size.TileWidth),
                        new XAttribute("tileheight", tileMap.Size.TileHeight)
            );

            List<XElement> tileSetElements = createTileSetListElements(TileMap.TileSets);
            foreach (XElement elem in tileSetElements)
            {
                mapElement.Add(elem);
            }

            XElement layerElement = createLayerElements(TileMap);

            mapElement.Add(layerElement);

            return mapElement;
        }


        public void setTileMap(TileMap tileMap)
        {
            TileMap = tileMap;
        }

        public void saveTileMap(string filePath)
        {
            XElement tileMap = this.createTileMapElement(TileMap);
            tileMap.Save(filePath);
        }


    }
}

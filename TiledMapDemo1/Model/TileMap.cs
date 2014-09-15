using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledMapDemo1.Model
{
    public class TileMap
    {
        public TileMapSize Size { get; set; }

        public IList<TileSet> TileSets { get; set; }

        public IList<MapLayer> Layers { get; set; }

        public string Path { get; set; }


        /*New Info*/
        public String Version { get; set; }
        public String Orientation { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }
        /**/

        public TileMap(TileMapSize size, IList<TileSet> tileSets, IList<MapLayer> layers, string path)
        {
            if (size == null)
            {
                throw new ArgumentNullException("size");
            }

            if (tileSets == null)
            {
                throw new ArgumentNullException("tileSets");
            }

            if (layers == null)
            {
                throw new ArgumentNullException("layers");
            }
            Path = path;

            Size = size;

            TileSets = tileSets;

            Layers = layers;
        }
        public TileMap()
        {
            Version = null;
            Orientation = null;
            Width = -1;
            Height = -1;
            TileWidth = -1;
            TileHeight = -1;
            Path = null;
            Size = null;
            TileSets = null;
            Layers = null;
        }
    }
}

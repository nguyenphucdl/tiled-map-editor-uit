using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledMapDemo1.Model
{
    public class TileSet
    {
        /*
         * The first global tile ID of this tileset (this global ID maps to the first tile in this tileset).
         */
        [CategoryAttribute("TileSet Info")]
        public int FirstId { get; set; }

        [CategoryAttribute("TileSet Info")]
        public int LastId { get; set; }

        [CategoryAttribute("TileSet Info")]
        public string Name { get; set; }

        [CategoryAttribute("TileSet Info")]
        public int TileWidth { get; set; }

        [CategoryAttribute("TileSet Info")]
        public int TileHeight { get; set; }

        [BrowsableAttribute(false)]
        public TileSetImage Image { get; set; }

        [CategoryAttribute("TileSet Info")]
        public int Count { 
            get {
                return LastId - FirstId + 1;
            }
        }

        [CategoryAttribute("TileSet Info")]
        public int Width
        {
            get { return Image.Width; }
        }

        [CategoryAttribute("TileSet Info")]
        public int Height
        {
            get { return Image.Height; }
        }

        [CategoryAttribute("TileSet Info")]
        public Point Dimension
        {
            get
            {
                return new Point(Width / TileWidth, Height / TileHeight);
            }
        }
        public TileSet(int firstId, string name, int tileWidth, int tileHeight, TileSetImage image)
        {
            FirstId = firstId;

            Name = name;

            TileWidth = tileWidth;

            TileHeight = tileHeight;

            Image = image;

            LastId = FirstId + (Image.Width / tileWidth) * (Image.Height / tileHeight) - 1;
        }

        public TileSet()
        {
            FirstId = -1;
            LastId = -1;
            Name = null;
            TileWidth = -1;
            TileHeight = -1;
            Image = null;
        }

        public bool ContainsTile(int tileId)
        {
            return (tileId >= FirstId && tileId <= LastId);
        }

    }
}

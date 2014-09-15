using System;
using System.Collections.Generic;
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
        public int FirstId { get; set; }

        public int LastId { get; set; }

        public string Name { get; set; }

        public int TileWidth { get; set; }

        public int TileHeight { get; set; }

        public TileSetImage Image { get; set; }

        public int Count { 
            get {
                return LastId - FirstId + 1;
            }
        }

        public int Width
        {
            get { return Image.Width; }
        }

        public int Height
        {
            get { return Image.Height; }
        }

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

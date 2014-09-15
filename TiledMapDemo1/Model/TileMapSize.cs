using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledMapDemo1.Model
{
    public class TileMapSize
    {
        public int Width { get; private set; }

        public int Height { get; private set; }

        public int TileWidth { get; private set; }

        public int TileHeight { get; private set; }

        public Point Dimension { get; private set; }

        public TileMapSize(int width, int height, int tileWidth, int tileHeight)
        {
            Width = width;

            Height = height;

            TileWidth = tileWidth;

            TileHeight = tileHeight;

            Dimension = new Point(width / tileWidth, height / tileHeight);
        }
    }
}

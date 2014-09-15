using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledMapDemo1.Model
{
    public class TileSetImage
    {
        public string Name { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public TileSetImage(string name, int width, int height)
        {
            Name = name;

            Width = width;

            Height = height;
        }
    }
}

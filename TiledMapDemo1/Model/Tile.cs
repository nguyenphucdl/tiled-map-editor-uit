using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledMapDemo1.Model
{
    public class Tile
    {
        public int Id { get; set; }

        public Rectangle Region { get; set; }

        public TileSetImage Image { get; set; }

        public Tile(int id, TileSetImage image, Rectangle region)
        {
            Id = id;

            Region = region;

            Image = image;
        }
    }
}

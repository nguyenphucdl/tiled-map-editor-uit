using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledMapDemo1.Model
{
    public class MapLayer
    {
        public string Name { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int[,] TileIds { get; set; }

        public bool Visible { get; set; }

        public MapLayer(string name, int width, int height)
        {
            Name = name;

            Width = width;

            Height = height;

            TileIds = null;

            Visible = true;
        }

        public MapLayer()
        {
            Name = null;

            Width = -1;

            Height = -1;

            TileIds = null;

            Visible = true;
        }

        public MapLayer(MapLayer mapLayer)
        {
            Name = mapLayer.Name;

            Width = mapLayer.Width;

            Height = mapLayer.Height;

            TileIds = mapLayer.TileIds;

            Visible = true;
        }
    }
}

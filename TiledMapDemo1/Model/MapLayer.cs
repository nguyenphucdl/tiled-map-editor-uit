using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledMapDemo1.Model
{
    public enum LayerType
    {
        NONE,
        TILEMAP,
        OBJECT
    }

    public class MapLayer
    {
        [CategoryAttribute("Layer Info")]
        public string Name { get; set; }

        [CategoryAttribute("Layer Info")]
        public LayerType Type { get; set; }

        [CategoryAttribute("Layer Info"),
        ReadOnlyAttribute(true)]
        public int Width { get; set; }

        [CategoryAttribute("Layer Info"),
        ReadOnlyAttribute(true)]
        public int Height { get; set; }

        //[BrowsableAttribute(false)]
        //public int[,] TileIds { get; set; } // OLD

        [CategoryAttribute("Layer Info")]
        public bool Visible { get; set; }

        public MapLayer(string name, int width, int height)
        {
            Name = name;

            Width = width;

            Height = height;

            Visible = true;

            Type = LayerType.NONE;
        }

        public MapLayer()
        {
            Name = null;

            Width = -1;

            Height = -1;

            Visible = true;

            Type = LayerType.NONE;
        }

        public MapLayer(MapLayer mapLayer)
        {
            Name = mapLayer.Name;

            Width = mapLayer.Width;

            Height = mapLayer.Height;

            Visible = true;

            Type = LayerType.NONE;
        }
    }
}

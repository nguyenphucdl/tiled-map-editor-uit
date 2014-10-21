using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledMapDemo1.Model
{

    public class TileMapLayer : MapLayer
    {

        [BrowsableAttribute(false)]
        public int[,] TileIds { get; set; }


        public TileMapLayer(string name, int width, int height)
        {
            Name = name;

            Width = width;

            Height = height;

            TileIds = null;

            Visible = true;

            Type = LayerType.NONE;
        }

        public TileMapLayer()
        {
            Name = null;

            Width = -1;

            Height = -1;

            TileIds = null;

            Visible = true;

            Type = LayerType.NONE;
        }
    }
}

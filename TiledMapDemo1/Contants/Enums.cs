using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledMapDemo1.Contants
{
    enum ObjectTypes
    {
        BLOCK_OBJECT = 0,
        END_SCENE = 1,
        SPAWNLOCATION = 2,
        RANGE_OF_MOMENT = 3,
        STAIRWAY_OBJECT = 4,
        DYNAMIC_OBJECT = 5
    }

    enum ViewportTypes
    {
        TOP_LEFT,
        TOP_RIGHT,
        BOTTOM_LEFT,
        BOTTOM_RIGHT
    }
}

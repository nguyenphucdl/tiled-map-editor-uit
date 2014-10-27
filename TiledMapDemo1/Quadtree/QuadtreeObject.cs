using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapDemo1.Model;

namespace TiledMapDemo1.Quadtree
{
    class QuadtreeObject
    {
        private Rectangle m_bound;

        public Rectangle Bound
        {
            get { return m_bound; }
            set { m_bound = value; }
        }

        private TileObject m_target;

        public TileObject Target
        {
            get { return m_target; }
            set { m_target = value; }
        }

        public QuadtreeObject(Rectangle bound, TileObject target)
        {
            m_bound = bound;
            m_target = target;
        }
    }
}




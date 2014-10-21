using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledMapDemo1
{
    public class DrawingLine : DrawingShape
    {
        private Point m_pt1;
        private Point m_pt2;
        private DrawingType m_type;

        public DrawingType Type
        {
            get { return m_type; }
            set { m_type = value; }
        }


        public DrawingLine(Point x, Point y)
        {
            m_pt1 = x;
            m_pt2 = y;
        }

        public override void Draw(Graphics graphics)
        {
            base.Draw(graphics);

            if(Selected)
                graphics.DrawLine(SelectedPen, m_pt1, m_pt2);
            else
                graphics.DrawLine(DrawingPen, m_pt1, m_pt2);
        }

        public override Rectangle GetBound()
        {
            int minX, minY, maxX, maxY;
            if (m_pt1.X > m_pt2.X)
            {
                minX = m_pt2.X;
                maxX = m_pt1.X;
            }
            else
            {
                minX = m_pt1.X;
                maxX = m_pt2.X;
            }
            if (m_pt1.Y > m_pt2.Y)
            {
                minY = m_pt2.Y;
                maxY = m_pt1.Y;
            }
            else
            {
                minY = m_pt1.Y;
                maxY = m_pt2.Y;
            }

            return new Rectangle(minX, minY, maxX - minX, maxY - minY);
        }
    }
}

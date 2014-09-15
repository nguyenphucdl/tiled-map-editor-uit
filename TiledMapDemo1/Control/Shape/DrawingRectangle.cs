using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledMapDemo1
{
    public class DrawingRectangle : DrawingShape
    {
        private int m_x;
        private int m_y;
        private int m_width;
        private int m_height;

        public DrawingRectangle(int x, int y, int width, int height)
        {
            m_x = x;
            m_y = y;
            m_width = width;
            m_height = height;
        }

        public override void Draw(Graphics graphics)
        {
            base.Draw(graphics);
            Rectangle drawRegion = new Rectangle(m_x, m_y, m_width, m_height);
            
            if (Fill)
                graphics.FillRectangle(DrawingBrush, drawRegion);
            if (Selected)
                graphics.DrawRectangle(SelectedPen, drawRegion);
            else
                graphics.DrawRectangle(DrawingPen, drawRegion);

        }

        public override Rectangle GetBound()
        {
            return new Rectangle(m_x, m_y, m_width, m_height);
        }
    }
}

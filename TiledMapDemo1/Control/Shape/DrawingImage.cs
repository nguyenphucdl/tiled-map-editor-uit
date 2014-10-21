using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledMapDemo1
{
    public class DrawingImage : DrawingShape
    {
        private Rectangle m_dest;
        private Rectangle m_src;
        private Image m_source;
        

        public DrawingImage(Image source,Rectangle src, Rectangle dest)
        {
            m_dest = dest;
            m_src = src;
            m_source = source;
        }

        

        public override void Draw(Graphics graphics)
        {
            base.Draw(graphics);

            

            if (m_source == null)
                graphics.FillRectangle(Brushes.LightGray, m_dest);
            else
                graphics.DrawImage(m_source, m_dest, m_src, GraphicsUnit.Pixel);

            if (Selected)
                graphics.DrawRectangle(SelectedPen, m_dest);
        }

        public override Rectangle GetBound()
        {
            return m_src;
        }
    }
}

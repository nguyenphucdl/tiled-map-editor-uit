using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledMapDemo1
{
    public class GridLayer : Layer
    {
        #region Fields
        private Size m_cellSize;
        private Size m_gridSize;
        private Graphics m_graphics;
        private Point m_drawPoint = new Point(-1, -1);
        #endregion

        #region Constructor
        public GridLayer(LayerContext container, String id, int index,Size cellSize, Size gridSize) : base(container, id, index)
        {
            m_cellSize = cellSize;
            m_gridSize = gridSize;
        }
        #endregion

        #region Functions
        public override void Draw(Graphics graphics)
        {
            base.Draw(graphics);
        }

        public void DrawGridLine(Graphics graphics)
        {
            int m = m_gridSize.Width / m_cellSize.Width;
            int n = m_gridSize.Height / m_cellSize.Height;
            //float[] dashValues = { 2, 2, 2, 2 };
            //Pen blackPen = new Pen(Color.LightCyan, 1);
            //blackPen.DashPattern = dashValues;
            Pen newPen = new Pen(Brushes.DarkCyan, 1);
            for (int i = 0; i < m; i++)
            {
                graphics.DrawLine(Pens.DarkCyan, new Point(i * m_cellSize.Width, 0), new Point(i * m_cellSize.Width, n * m_cellSize.Height));
            }
            for (int j = 0; j < n; j++)
            {
                graphics.DrawLine(newPen, new Point(0, j * m_cellSize.Height), new Point(m * m_cellSize.Width, j * m_cellSize.Height));
            }
        }

        public void HightLight(int i, int j, float zoom)
        {
            if (m_drawPoint.X == i && m_drawPoint.Y == j)
                return;
            m_drawPoint = new Point(i, j);
            Rectangle selection = new Rectangle((int)(m_drawPoint.X * m_cellSize.Width * zoom), (int)(m_drawPoint.Y * m_cellSize.Height * zoom),(int)( m_cellSize.Width * zoom),(int)(m_cellSize.Height * zoom));
            m_graphics.DrawRectangle(Pens.Red, selection);
        }

        public void DrawDirect(Image source, Rectangle src, Rectangle dest, float zoom)
        {

            Rectangle newRect = new Rectangle((int)(src.X * zoom), (int)(src.Y * zoom),(int)(src.Width * zoom),(int)(src.Height * zoom));
            m_graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
           

            m_graphics.DrawImage(source, newRect, dest, GraphicsUnit.Pixel);
        }

        public void DrawRectangleDirect(int x, int y, int width, int height, float zoom)
        {
            Rectangle region = new Rectangle((int)(x * zoom),(int)(y * zoom),(int)(width * zoom),(int)(height * zoom));
            m_graphics.DrawRectangle(Pens.Cyan, region);
        }

        public void DrawLineDirect(int x1, int y1, int x2, int y2, float zoom)
        {
            Pen pen = Pens.Cyan;
            m_graphics.DrawLine(pen, new Point((int)(x1 * zoom), (int)(y1 * zoom)), new Point((int)(x2 * zoom), (int)(y2 * zoom)));
        }

        public void SetGraphicsContext(Graphics graphics)
        {
            m_graphics = graphics;
        }

        public void RemoveRegion(Rectangle region)
        {
            DrawingShape shape = Shapes.Where(e => e.GetBound() == region).FirstOrDefault();
            if(shape != null)
                RemoveDrawingShape(shape);
        }
        #endregion
    }
}

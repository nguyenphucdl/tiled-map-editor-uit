using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledMapDemo1
{
    public abstract class DrawingShape
    {
        #region Fields
        protected String m_uid;
        protected bool m_selected = false;
        protected Pen m_pen = Pens.Aqua;
        protected Pen m_selectedPen = Pens.Red;
        protected Brush m_brush;
        protected bool m_isFill = false;
        #endregion 

        #region Setter & Getter
        public Brush DrawingBrush
        {
            get {  return m_brush; }
            set { m_brush = value; }
        }
        public Pen DrawingPen
        {
            get { return m_pen;  }
            set { m_pen = value; }
        }
        public bool Fill
        {
            get { return m_isFill; }
            set { m_isFill = value; }
        }
        public bool Selected
        {
            get { return m_selected; }
            set { m_selected = value; }
        }
        protected String Uid
        {
            get { return m_uid; }
            set { m_uid = value; }
        }
        public Pen SelectedPen
        {
            get { return m_selectedPen; }
            set { m_selectedPen = value; }
        }
        #endregion

        #region Funtions
        protected DrawingShape(){
            m_uid = Guid.NewGuid().ToString("N");
        }

        public virtual void Draw(Graphics graphics)
        {
        }
        public virtual Rectangle GetBound()
        {
            return new Rectangle(-100, -100, 0, 0);
        }
        public virtual bool HitTest(Rectangle rect)
        {
            return false;
        }
        public virtual void Select()
        {
            m_selected = true;
        }
        public virtual void UnSelect()
        {
            m_selected = false;
        }
        #endregion
    }
}

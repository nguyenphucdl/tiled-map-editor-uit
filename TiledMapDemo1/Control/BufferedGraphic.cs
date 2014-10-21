using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace TiledMapDemo1
{
    public partial class BufferedGraphic : UserControl
    {
        #region Buffer Fields
        private bool m_Dirty;
        private float m_Zoom;
        private int m_OriginalWidth;
        private int m_OriginalHeight;
        private Graphics m_Graphics;

        public Size WorkplaceSize
        {
            get {
                return new Size(this.Width, this.Height);
            }
            set
            {
                if (value != null)
                {
                    Size temp = value;
                    if (temp.Width != this.Width || temp.Height != this.Height)
                    {
                        this.Width = temp.Width;
                        this.Height = temp.Height;
                        this.m_OriginalWidth = temp.Width;
                        this.m_OriginalHeight = temp.Height;
                    }
                }
            }
        }

        public float Zoom
        {
            get { return m_Zoom; }
            set
            {
                if (m_Zoom != value)
                {
                    if (value < 0.25f || value > 5.0f)
                        return;
                    m_Zoom = value;
                    this.Width = (int)(this.m_OriginalWidth * m_Zoom);
                    this.Height = (int)(this.m_OriginalHeight * m_Zoom);
                }
            }
        }
        
        public bool Dirty
        {
            get { return m_Dirty; }
            set
            {
                if (!value)
                    return;
                m_Dirty = true;
                Invalidate();
            }
        }
        private BufferedGraphicsContext m_BufferContext;
        private BufferedGraphics m_Buffer;
        #endregion

        #region Buffer Configuration
        private void ApdaptSizeGraphicsBuffer()
        {
            if (m_Buffer != null)
            {
                m_Buffer.Dispose();
                m_Buffer = null;
            }

            if (m_BufferContext == null)
                return;

            if (DisplayRectangle.Width <= 0)
                return;

            if (DisplayRectangle.Height <= 0)
                return;

            m_Graphics = CreateGraphics();
            m_Buffer = m_BufferContext.Allocate(m_Graphics, DisplayRectangle);

            Dirty = true;
        }
        private void InitBufferedGraphic()
        {
            m_BufferContext = new BufferedGraphicsContext();
            ApdaptSizeGraphicsBuffer();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, false);
            SetStyle(ControlStyles.DoubleBuffer, false);
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            /*Do nothing*/
        }
        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (m_Buffer != null)
                {
                    m_Buffer.Dispose();
                    m_Buffer = null;
                }

                if (m_BufferContext != null)
                {
                    m_BufferContext.Dispose();
                    m_BufferContext = null;
                }

                if (components != null)
                {
                    components.Dispose();
                }
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Contructors
        public BufferedGraphic()
        {
            // Default Zoom
            m_Zoom = 1.25f;
            InitializeComponent();
            InitBufferedGraphic();
        }
        #endregion 
        
        #region Events Delegate
        public Action<Graphics> OnDraw;
        public Action<MouseEventArgs> OnMove;
        public Action<EventArgs> OnSizeChange;
        public Action<MouseEventArgs> OnMouseDownAction;
        public Action<MouseEventArgs> OnMouseUpAction;
        #endregion

        #region Events
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (OnMouseDownAction != null)
                OnMouseDownAction(e);
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            ApdaptSizeGraphicsBuffer();
            if (OnSizeChange != null)
                OnSizeChange(e);
            base.OnSizeChanged(e);
        }

        public void Redraw()
        {
            m_Buffer.Render(m_Graphics);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (m_Buffer == null)
            {
                Draw(e.Graphics);
                return;
            }

            if (m_Dirty)
            {
                m_Dirty = false;
                Draw(m_Buffer.Graphics);
            }

            m_Buffer.Render(e.Graphics);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            xMove = e.X;
            yMove = e.Y;
            if (OnMove != null)
                OnMove(e);

        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (OnMouseUpAction != null)
                OnMouseUpAction(e);
        }
        int xMove = -1, yMove = -1;
        protected virtual void Draw(Graphics graphics)
        {
            graphics.Clear(Color.WhiteSmoke);
            Matrix matrans = new Matrix();
            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            matrans.Scale(m_Zoom, m_Zoom, MatrixOrder.Prepend);
            graphics.Transform = matrans;

            if (OnDraw != null)
                OnDraw(graphics);
        }
        #endregion
    }
}

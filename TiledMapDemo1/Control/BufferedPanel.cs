using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TiledMapDemo1
{
    public partial class BufferedPanel : Panel
    {
        #region Buffer Fields
        private bool m_Dirty;

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

        public Graphics Graphics
        {
            get { return m_Buffer.Graphics; }
        }
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

            using (Graphics graphics = CreateGraphics())
                m_Buffer = m_BufferContext.Allocate(graphics, DisplayRectangle);

            Dirty = true;
        }
        private void InitBufferedGraphic()
        {
            m_BufferContext = new BufferedGraphicsContext();
            ApdaptSizeGraphicsBuffer();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, false);
            SetStyle(ControlStyles.DoubleBuffer, false);
            //SetStyle(ControlStyles.UserPaint, true);
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //SetStyle(ControlStyles.DoubleBuffer, true);
            //SetStyle(ControlStyles.UserPaint, true);
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //SetStyle(ControlStyles.DoubleBuffer, true);
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

            //base.Dispose(disposing);
        }
        #endregion

        #region Constructor
        public BufferedPanel()
        {
            InitializeComponent();
            InitBufferedGraphic();
        }
        #endregion

        #region Delegates
        public Action<MouseEventArgs> OnMouseWheelAction;
        public Action<MouseEventArgs> OnMouseDownAction;
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
            base.OnSizeChanged(e);
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (OnMouseWheelAction != null)
                OnMouseWheelAction(e);
        }
        protected override Point ScrollToControl(Control activeControl)
        {
            // PREVENT: auto scroll to control when click to it
            //return base.ScrollToControl(activeControl);
            return DisplayRectangle.Location;
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
        protected virtual void Draw(Graphics graphics)
        {
            graphics.Clear(Color.WhiteSmoke);
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            this.Focus();
            base.OnMouseEnter(e);
        }
        #endregion
    }
}

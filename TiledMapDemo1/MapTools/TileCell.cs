using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledMapDemo1.MapTools
{
    public class TileCell
    {
        #region Fields
        private int m_i;
        private int m_j;
        private Image m_image;
        private int m_mapCode = 0;
        #endregion

        #region Constructor
        public TileCell(int i, int j, Image image)
        {
            m_i = i;
            m_j = j;
            m_image = image;
        }
        #endregion

        #region Getter & Setter
        public int I
        {
            get { return m_i; }
            set { m_i = value; }
        }

        public int J
        {
            get { return m_j; }
            set { m_j = value; }
        }

        public Image Image
        {
            get { return m_image; }
            set { m_image = value; }
        }

        public int MapCode
        {
            get { return m_mapCode; }
            set { m_mapCode = value; }
        }
        #endregion
    }
}

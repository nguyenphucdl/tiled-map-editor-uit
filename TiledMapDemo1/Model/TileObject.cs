using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledMapDemo1.Model
{
    public enum TileObjectType
    {
        NORMAL,
        ELLIPSE,
        POLYGON,
        POLYLINE
    }

    public class TileObject
    {
        private String m_name;

        public String Name
        {
            get { return m_name; }
            set { m_name = value; }
        }
        private String m_type;

        public String Type
        {
            get { return m_type; }
            set { m_type = value; }
        }
        private Point m_position;

        public Point Position
        {
            get { return m_position; }
            set { m_position = value; }
        }
        private Size m_size;

        public Size Size
        {
            get { return m_size; }
            set { m_size = value; }
        }
        private TileObjectType m_objType;

        public TileObjectType ObjectType
        {
            get { return m_objType; }
            set { m_objType = value; }
        }

        private Object m_data;

        public Object Data
        {
            get { return m_data; }
            set { m_data = value; }
        }



        public TileObject()
        {
            m_name = "";
            m_type = "";
            m_position = new Point(0, 0);
            m_size = new Size(1, 1);

            m_objType = TileObjectType.NORMAL;
        }

        public TileObject(String name, Point pos, Size size)
        {
            m_name = name;
            m_position = pos;
            m_size = size;
            m_type = "";

            m_objType = TileObjectType.NORMAL;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapDemo1.Utils;

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

        private Int64 m_id;

        public Int64 Id
        {
            get { return m_id; }
            set { m_id = value; }
        }

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

        private Color m_color;

        public Color Color
        {
            get { return m_color; }
            set { m_color = value; }
        }

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

        private Rectangle m_bound;

        public Rectangle Bound
        {
            get { return GetBound(); }
            private set { m_bound = value; }
        }

        public Rectangle GetBound()// get axis align bound
        {
            m_bound = new Rectangle(-1, -1, 1, 1); // invalid bound
            if (m_objType == TileObjectType.NORMAL)
            {
                m_bound = new Rectangle(m_position, m_size);
            }
            else if (m_objType == TileObjectType.POLYLINE)
            {
                string[] datapoints = ((String)m_data).Split(',', ' ');
                int[] boundPoints = new int[4] {9999, 9999, -1, -1};

                for (int i = 0; i < datapoints.Length; i += 2)
                {
                    int x = int.Parse(datapoints[i]) + m_position.X;
                    int y = int.Parse(datapoints[i + 1]) + m_position.Y;

                    boundPoints[0] = (x < boundPoints[0]) ? x : boundPoints[0];// Min x
                    boundPoints[1] = (y < boundPoints[1]) ? y : boundPoints[1];// Min y
                    boundPoints[2] = (x > boundPoints[2]) ? x : boundPoints[2];// Max x
                    boundPoints[3] = (y > boundPoints[3]) ? y : boundPoints[3];// Max y
                }
                m_bound = new Rectangle(boundPoints[0], boundPoints[1],
                                        boundPoints[2] - boundPoints[0],
                                        boundPoints[3] - boundPoints[1]);

            }
            else if (m_objType == TileObjectType.ELLIPSE)
            {
            }
            else if (m_objType == TileObjectType.POLYGON)
            {
            }
            return m_bound;
        }

        public TileObject()
        {
            m_id = IDGenerator.GetNextID();
            m_name = "";
            m_type = "";
            m_position = new Point(0, 0);
            m_size = new Size(1, 1);

            m_objType = TileObjectType.NORMAL;

            Color = Color.Pink;
        }

        public TileObject(String name, Point pos, Size size)
        {
            m_name = name;
            m_position = pos;
            m_size = size;
            m_type = "";

            m_objType = TileObjectType.NORMAL;

            Color = Color.Pink;
        }

    }
}

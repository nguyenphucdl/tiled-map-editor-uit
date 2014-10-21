using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledMapDemo1.Model
{
    public class TileObjectGroup : MapLayer
    {
        private String m_color;

        public String Color
        {
            get { return m_color; }
            set { m_color = value; }
        }

        private List<TileObject> m_objects = new List<TileObject>();
        private string p;
        private int mapWidth;
        private int mapHeight;

        public List<TileObject> Objects
        {
            get { return m_objects; }
            set { m_objects = value; }
        }

        public TileObjectGroup(String name)
        {
            Name = name;
            Color = "Black";
            Visible = true;
        }

        public TileObjectGroup(string name, int mapWidth, int mapHeight)
        {
            // TODO: Complete member initialization
            Name = name;
            Width = mapWidth;
            Height = mapHeight;
        }

        public void AddObject(TileObject obj)
        {
            int idx = m_objects.IndexOf(obj);

            if (idx != -1)
                return;

            m_objects.Add(obj);
        }

        public void RemoveObject(TileObject obj)
        {
            int idx = m_objects.IndexOf(obj);

            if (idx != -1)
                return;

            m_objects.Remove(obj);
        }
      
    }
}

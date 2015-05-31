using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapDemo1.Model;

namespace TiledMapDemo1.Quadtree
{

    public class Quadtree
    {
        public static int MAX_LEVEL = 5;
        public static int MAX_OBJECTs = 5;

        private int m_id; // Id generate by QuadtreeIDGenerator
        private int m_parentId;

        public int ParentId
        {
            get { return m_parentId; }
            set { m_parentId = value; }
        }

        public int Id
        {
            get { return m_id; }
            set { m_id = value; }
        }
        private int m_level;

        public int Level
        {
            get { return m_level; }
            set { m_level = value; }
        }
        private List<QuadtreeObject> m_listObject;

        internal List<QuadtreeObject> Objects
        {
            get { return m_listObject; }
            set { m_listObject = value; }
        }
        private Rectangle m_bound;

        public Rectangle Bound
        {
            get { return m_bound; }
            set { m_bound = value; }
        }

        private int m_count = 0;

        public int Count
        {
            get { return GetCount(this, ref m_count); }
            set { m_count = value; }
        }

        public Quadtree    NE;
        public Quadtree NW;
        public Quadtree SW;
        public Quadtree SE;

        public Quadtree(int level, Rectangle bound)
        {
            m_level = level;
            m_bound = bound;

            NE = NW = SW = SE = null;

            m_listObject = new List<QuadtreeObject>();
        }
        public void Clear()
        {
            m_listObject.Clear();
            if (NE != null)
            {
                NE.Clear();
                NE = null;

                NW.Clear();
                NW = null;

                SW.Clear();
                SW = null;

                SE.Clear();
                SE = null;
            }
        }
        void Quadrant()
        {
            int subWidth = m_bound.Width / 2;
            int subHeight = m_bound.Height / 2;

            Rectangle neBound = new Rectangle(m_bound.X, m_bound.Y, subWidth, subHeight);
            Rectangle nwBound = new Rectangle(m_bound.X + subWidth, m_bound.Y, subWidth, subHeight);
            Rectangle swBound = new Rectangle(m_bound.X + subWidth, m_bound.Y + subHeight, subWidth, subHeight);
            Rectangle seBound = new Rectangle(m_bound.X, m_bound.Y + subHeight, subWidth, subHeight);

            NE = new Quadtree(m_level + 1, neBound);
            NW = new Quadtree(m_level + 1, nwBound);
            SW = new Quadtree(m_level + 1, swBound);
            SE = new Quadtree(m_level + 1, seBound);
        }

        private bool IsExternal(Quadtree quadrant)
        {
            if (quadrant.NE != null)
                return false;
            return true;
        }

        public bool IsExternal()
        {
            if (NE != null)
                return false;
            return true;
        }

        public int GetCountObject()
        {
            return GetCount(this, ref m_count);
        }

        public int GetCount(Quadtree quadrant, ref int count)
        {
            if (IsExternal(quadrant))
            {
                count += quadrant.m_listObject.Count;
            }
            else
            {
                GetCount(quadrant.NE,ref count);
                GetCount(quadrant.NW, ref count);
                GetCount(quadrant.SE, ref count);
                GetCount(quadrant.SW, ref count);
            }
            return count;
        }

        public bool CheckIntersect(Rectangle rect1, Rectangle rect2)
        {
            if (rect1.X + rect1.Width < rect2.X || rect2.X + rect2.Width < rect1.X
            || rect1.Y + rect1.Height < rect2.Y || rect2.Y + rect2.Height < rect1.Y)
                return false;
            else
                return true;
        }

        public void Insert(TileObject obj)
        {
            // Clip object bound
            Rectangle objBound = obj.Bound;
            //Rectangle clip = Rectangle.Intersect(objBound, m_bound);// Need fix not call function
            if(!CheckIntersect(objBound, m_bound))
                return;
            //if (clip.IsEmpty)
            //    return;
            
            if (_IsNeedQuadrant())
            {
                // Quadrant Quadtree
                Quadrant();
                // Move all child object to quadrant
                foreach (QuadtreeObject childObj in m_listObject)
                {
                    _RecursiveInsertObject(childObj.Target);
                }
                m_listObject.Clear();
            }
            
            // Recursive Insert Object by child if not null
            if (NE != null)
            {
                _RecursiveInsertObject(obj);// not call all
            }
            else
            {
                Rectangle clip = Rectangle.Intersect(objBound, m_bound);
                QuadtreeObject qobj = new QuadtreeObject(clip, obj);

                // Add object to list
                m_listObject.Add(qobj);
            }            
        }

        private void _RecursiveInsertObject(TileObject obj)
        {
            NE.Insert(obj);
            NW.Insert(obj);
            SW.Insert(obj);
            SE.Insert(obj);
        }

        private bool _IsNeedQuadrant()
        {
            bool needQuadrant = true;
            if (NE != null)
                return false;
            if (m_listObject.Count < Quadtree.MAX_OBJECTs)
                return false;
            if (m_level >= Quadtree.MAX_LEVEL)
                return false;
            
            return needQuadrant;
        }
        public List<TileObject> QueryRange(Rectangle range, List<TileObject> listObjects)// Need  Test
        {
            Rectangle clip = Rectangle.Intersect(range, m_bound);
            if (clip.IsEmpty)
                return null;
            // Recursive Query range by child if not null
            if (NE != null)
            {
                _RecursiveQueryRange(range, listObjects);
            }
            else
            {
                foreach (QuadtreeObject childObj in m_listObject)
                {
                    listObjects.Add(childObj.Target);
                }
            }


            return listObjects;
        }

        private void _RecursiveQueryRange(Rectangle range, List<TileObject> listObjects)
        {
            NE.QueryRange(range, listObjects);
            NW.QueryRange(range, listObjects);
            SE.QueryRange(range, listObjects);
            SW.QueryRange(range, listObjects);
        }
    }
}

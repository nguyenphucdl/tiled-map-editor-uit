using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapDemo1.Quadtree;

namespace TiledMapDemo1.Utils
{
    public static class IDGenerator
    {
        public static int Code = 0;

        public static Int64 GetNextID()
        {
            return ++Code;
        }
    }

    public enum QuadrantID
    {
        RO = 0,
        NE = 1,
        NW = 2,
        SE = 3,
        SW = 4
    }

    public class QuadtreeIDGenerator
    {
        private int m_maxlevel;

        private Quadtree.Quadtree m_quadtree;

        private int m_codeStart = 0;

        public QuadtreeIDGenerator(Quadtree.Quadtree quadtree)
        {
            m_quadtree = quadtree;
        }

        public void GenerateId()
        {
            GenerateId(m_quadtree, QuadrantID.RO, null); // Generate Root ID
            _RecursiveGenerateId(m_quadtree);
        }

        private void _RecursiveGenerateId(Quadtree.Quadtree node)
        {
            if (node.IsExternal())
                return;
            GenerateId(node.NE, QuadrantID.NE, node);
            GenerateId(node.NW, QuadrantID.NW, node);
            GenerateId(node.SE, QuadrantID.SE, node);
            GenerateId(node.SW, QuadrantID.SW, node);

            _RecursiveGenerateId(node.NE);
            _RecursiveGenerateId(node.NW);
            _RecursiveGenerateId(node.SE);
            _RecursiveGenerateId(node.SW);
        }

        private void GenerateId(Quadtree.Quadtree child, QuadrantID quadrantId ,Quadtree.Quadtree parent)
        {
            //int exponent = Quadtree.Quadtree.MAX_LEVEL - child.Level;
            //int shift = 2 * exponent;
            //int presentId = (int)quadrantId;
            //int codeQuadrant = (presentId << shift);
            //if(parent != null)
            //{
            //    codeQuadrant |= parent.Id;
            //    child.ParentId = parent.Id;
            //}
            //child.Id = codeQuadrant;

            child.Id = m_codeStart++;
            if (parent != null)
            {
                child.ParentId = parent.Id;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapDemo1.Model;
using TiledMapDemo1.Utils;

namespace TiledMapDemo1.Quadtree
{
    class QuadtreeWriter
    {
        private WorkplaceModel m_workplaceModel;

        private TileMap m_tileMap;

        private Quadtree m_quadtree;

        public QuadtreeWriter(WorkplaceModel workplaceModel)
        {
            m_workplaceModel = workplaceModel;

            m_tileMap = m_workplaceModel.TileMap;
        }

        public void Prepare()
        {
            int mapWidth = m_tileMap.Width * m_tileMap.TileWidth;
            int mapHeight = m_tileMap.Height * m_tileMap.TileHeight;

            int boundQuadtree = (mapWidth > mapHeight) ? mapWidth : mapHeight;

            Rectangle quadtreeBound = new Rectangle(0, 0, boundQuadtree, boundQuadtree);// Quadtree square size

            m_quadtree = new Quadtree(0, quadtreeBound);

            TileObjectGroup objGroup = null;

            foreach (MapLayer l in m_tileMap.Layers)
            {
                if (l.Type == LayerType.OBJECT)
                {
                    objGroup = (TileObjectGroup)l;
                }
            }

            foreach (TileObject obj in objGroup.Objects)
            {
                m_quadtree.Insert(obj);
            }

            m_quadtree.GetCountObject();

            QuadtreeIDGenerator idGenerator = new QuadtreeIDGenerator(m_quadtree);
            idGenerator.GenerateId();
        }

        public void Save()
        {
            m_tileMap.Quadtree = m_quadtree;
        }
    }
}

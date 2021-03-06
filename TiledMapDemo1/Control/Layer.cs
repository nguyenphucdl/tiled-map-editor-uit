﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapDemo1.Model;


namespace TiledMapDemo1
{

    public class Layer
    {
        #region Fields
        protected String m_id;
        protected int m_index;
        protected bool m_enable = true;
        protected bool m_serializable = true;
        protected LayerType m_layerType = LayerType.NONE;
        protected LayerContext m_container;
        private List<DrawingShape> m_shapes = new List<DrawingShape>();

        public List<DrawingShape> Shapes
        {
            get { return m_shapes; }
            set { m_shapes = value; }
        }
        #endregion

        #region Getter & Setter
        public String Id
        {
            get { return m_id; }
            private set {  m_id = value; }
        }
        public int Index
        {
            get { return m_index; }
            set { m_index = value; }
        }
        public bool Enable
        {
            get { return m_enable; }
            set { m_enable = value; }
        }
        public bool Serializable
        {
            get { return m_serializable; }
            set { m_serializable = value; }
        }
        public LayerContext Container
        {
            get { return m_container; }
            set { m_container = value; }
        }
        public LayerType Type
        {
            get { return m_layerType; }
            set { m_layerType = value; }
        }
        #endregion

        #region Contructors
        public Layer(LayerContext container, String id, int index)
        {
            this.Id = id;
            this.Index = index;
            this.Container = container;
            this.Type = LayerType.NONE;
        }
        
        #endregion

        #region Events
        public virtual void Draw(Graphics graphics)
        {
            foreach (DrawingShape shape in m_shapes)
                shape.Draw(graphics);
        }
        #endregion

        #region Functions
        public void AddDrawingShape(DrawingShape shape)
        {
            m_shapes.Add(shape);
        }

        public void RemoveDrawingShape(DrawingShape shape)
        {
            m_shapes.Remove(shape);
        }
        #endregion
    }
}

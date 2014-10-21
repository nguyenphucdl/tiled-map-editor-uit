using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapDemo1.Model;

namespace TiledMapDemo1
{
    public class LayerContext
    {
        #region Fields
        private List<Layer> m_Layers;
        private int m_CurrentIndex;
        private Layer m_CurrentLayer;
       
        public int CurrentIndex
        {
            get { return m_CurrentIndex; }
            set {
                if (isValidIndex(value))
                    m_CurrentIndex = value;
                else
                    throw new Exception("Setting index layer out of range!");
            }
        }
        public Layer CurrentLayer
        {
            get {
                if (isValidIndex(m_CurrentIndex))
                    return m_Layers[m_CurrentIndex];
                return null;
            }
        }
        public string CurrentLayerId
        {
            get
            {
                if (isValidIndex(m_CurrentIndex))
                    return m_Layers[m_CurrentIndex].Id;
                return null;
            }
        }
        #endregion

        #region Contructors
        public LayerContext()
        {
            m_Layers = new List<Layer>();
            m_CurrentIndex = -1;
            m_CurrentLayer = null;
        }
        #endregion

        #region Events
        private bool isValidIndex(int index)
        {
            if (index < 0 || index >= m_Layers.Count)
                return false;
            return true;
        }
        
        public virtual void Draw(Graphics graphics)
        {
            var activeLayer = from l in m_Layers
                              where l.Enable == true
                              orderby l.Index ascending
                              select l;
            foreach (Layer l in activeLayer)
            {
                l.Draw(graphics);
            }
        }
        #endregion

        #region Functions
        public void HideAll()
        {
            foreach (Layer l in m_Layers)
            {
                l.Enable = false;
            }
        }
        public void Hide(int index)
        {
            if (isValidIndex(index))
            {
                m_Layers[index].Enable = false;
            }
        }
        public void Show(int index)
        {
            if (isValidIndex(index))
            {
                m_Layers[index].Enable = true;
            }
        }
        public bool AddLayer()
        {
            int defaultLayerIndex = m_Layers.Count + 1;
            string defaultLayerName = "Layer" + m_Layers.Count;

            return AddLayer(defaultLayerName, defaultLayerIndex);
        }
        public bool AddLayer(string name, int index)
        {
            return AddLayer(name, index, LayerType.NONE);
        }
        public bool AddLayer(string name, int index, LayerType type)
        {
            Layer newLayer = new Layer(this, name, index);
            newLayer.Type = type;
            Layer check = m_Layers.Where(e => e.Id == name).FirstOrDefault();
            if (check != null)
                return false;
            m_Layers.Add(newLayer);
            if (!isValidIndex(m_CurrentIndex))
                m_CurrentIndex = 0;
            return true;
        }
        public bool AddLayer(Layer layer)
        {
            if (layer == null)
                return false;
            m_Layers.Add(layer);
            return true;
        }
        public bool RemoveLayer(string name)
        {
            Layer layer = m_Layers.Where(e => e.Id == name).FirstOrDefault();
            if (layer != null)
            {
                m_Layers.Remove(layer);
                if (!isValidIndex(m_CurrentIndex))
                    m_CurrentIndex = 0;
                return true;
            }
            return false;
        }
        public void Clear()
        {
            m_Layers.Clear();
            m_CurrentIndex = -1;
            m_CurrentLayer = null;

        }

        public void DrawRectange(int x, int y, int width, int height)
        {
            DrawRectange(x, y, width, height, LayerType.TILEMAP);
        }

        private void _stwichLayerType(LayerType type)
        {
            //bool exist = false;
            //if (type == LayerType.OBJECT)
            //{
            //    foreach (Layer l in m_Layers)
            //    {
            //        if (l.Type == LayerType.OBJECT)
            //            exist = true;
            //    }
            //    if (!exist)
            //    {
            //        AddLayer("Object Layer", 10, LayerType.OBJECT);
            //    }
            //}

            foreach (Layer l in m_Layers)
            {
                if (l.Type == type)
                {
                    CurrentIndex = m_Layers.IndexOf(l);
                }
            }
        }

        public void DrawRectange(int x, int y, int width, int height, LayerType type)
        {
            if(!isValidIndex(m_CurrentIndex))
                return;
            _stwichLayerType(type);

            DrawingRectangle drawRect = new DrawingRectangle(x, y, width, height);
            CurrentLayer.AddDrawingShape(drawRect);
        }

        public void DrawLine(int x1, int y1, int x2, int y2)
        {
            DrawLine(x1, y1, x2, y2, LayerType.TILEMAP);
        }
        
        public void DrawLine(int x1, int y1, int x2, int y2, LayerType type)
        {
            if (!isValidIndex(m_CurrentIndex))
                return;

            _stwichLayerType(type);

            DrawingLine drawLine = new DrawingLine(new Point(x1, y1), new Point(x2, y2));
            CurrentLayer.AddDrawingShape(drawLine);
        }

        public void DrawImage(Image source, Rectangle src, Rectangle dest)
        {
            DrawImage(source, src, dest, LayerType.TILEMAP);
        }

        public void DrawImage(Image source, Rectangle src, Rectangle dest, LayerType type)
        {
            if (!isValidIndex(m_CurrentIndex))
                return;

            _stwichLayerType(type);

            DrawingImage image = new DrawingImage(source, src, dest);
            CurrentLayer.AddDrawingShape(image);
        }

        #endregion
    }
}

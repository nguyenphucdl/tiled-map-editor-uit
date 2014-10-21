using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapDemo1.Model;


namespace TiledMapDemo1.MapTools
{
    public static class TileMapFactory
    {
        #region Functions
        public static TileSet createTileSet(int firstId, string name, int tileWidth, int tileHeight, TileSetImage image)
        {
            return new TileSet(firstId, name, tileWidth, tileHeight, image);
        }

        public static List<T> packList<T>(T one)
        {
            List<T> list = new List<T>();
            list.Add(one);
            return list;
        }

        public static TileMap createTileMap(TileMapSize size, IList<TileSet> tileSets, IList<MapLayer> layers, string path)
        {
            return new TileMap(size, tileSets, layers, path);
        }

        public static TileMapSize createTileMapSize(int width, int height, int tileWidth, int tileHeight)
        {
            return new TileMapSize(width, height, tileWidth, tileHeight);
        }

        public static TileSetImage createTileSetImage(string filePath, int width, int height)
        {
            return new TileSetImage(filePath, width, height);
        }

        public static MapLayer createMapLayer(string name, int width, int height, int[,] tileIds)
        {
            TileMapLayer mapLayer = new TileMapLayer(name, width, height);
            mapLayer.TileIds = tileIds;
            return mapLayer;
        }
        #endregion
    }
}

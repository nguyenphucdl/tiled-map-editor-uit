using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledMapDemo1.Utils
{
    public static class FileUtility
    {
        public static void CreateFolderIfNotExist(string folder)
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
        }

        public static void DeleteFileIfExist(string fileName)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
        }
        public static string GetFileNameWithoutExtension(string filePath)
        {
            return Path.GetFileNameWithoutExtension(filePath);
        }

        public static string GetFileName(string filePath)
        {
            return Path.GetFileName(filePath);
        }
    }
}

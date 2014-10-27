using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TiledMapDemo1.Utils
{
    public static class CommonUtil
    {
        public static int SafeGetAttributeInt(XElement elem, String name)
        {
            XAttribute attr = elem.Attribute(name);
            if (attr == null)
                return -1;

            return int.Parse(attr.Value);
        }

        public static string SafeGetAttributeString(XElement elem, String name)
        {
            XAttribute attr = elem.Attribute(name);
            if (attr == null)
                return "";

            return attr.Value;
        }

        public static Color SafeGetAttributeColor(XElement elem, string name)
        {
            XAttribute attr = elem.Attribute(name);
            if (attr == null)
                return Color.Pink;
            return ColorTranslator.FromHtml(attr.Value.ToString());
        }
    }
}

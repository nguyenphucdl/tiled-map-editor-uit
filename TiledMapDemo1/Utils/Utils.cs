using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TiledMapDemo1
{
    public static class DemoUtils
    {
        private static Image m_cacheImage = null;
        public static Image CacheImage {
            get
            {
                return m_cacheImage;
            }
            set
            {
                m_cacheImage = value;
                Bitmap newBitmap = null;
                if(value != null)
                    newBitmap = new Bitmap(value);
                CacheBitMapImage = newBitmap;
            }
        }
        public static Bitmap CacheBitMapImage { get; set; }

        public static bool isTiledMapValid(Size tileSize, Image imageMap)
        {
            if (imageMap == null || tileSize.Width < 1 || tileSize.Height < 1)
                return false;
            if ((imageMap.Width % tileSize.Width != 0) || (imageMap.Height % tileSize.Height != 0))
                return false;

            return true;
        }

        public static Image Crop(Rectangle selection, Image imageMap)
        {
            return CacheBitMapImage.Clone(selection, CacheBitMapImage.PixelFormat);
        }

        public static bool CompareBitmaps(Image left, Image right)
        {
            if (object.Equals(left, right))
                return true;
            if (left == null || right == null)
                return false;
            if (!left.Size.Equals(right.Size) || !left.PixelFormat.Equals(right.PixelFormat))
                return false;

            Bitmap leftBitmap = left as Bitmap;
            Bitmap rightBitmap = right as Bitmap;
            if (leftBitmap == null || rightBitmap == null)
                return true;

            #region Optimized code for performance

            int bytes = left.Width * left.Height * (Image.GetPixelFormatSize(left.PixelFormat) / 8);

            bool result = true;
            byte[] b1bytes = new byte[bytes];
            byte[] b2bytes = new byte[bytes];

            BitmapData bmd1 = leftBitmap.LockBits(new Rectangle(0, 0, leftBitmap.Width - 1, leftBitmap.Height - 1), ImageLockMode.ReadOnly, leftBitmap.PixelFormat);
            BitmapData bmd2 = rightBitmap.LockBits(new Rectangle(0, 0, rightBitmap.Width - 1, rightBitmap.Height - 1), ImageLockMode.ReadOnly, rightBitmap.PixelFormat);

            Marshal.Copy(bmd1.Scan0, b1bytes, 0, bytes);
            Marshal.Copy(bmd2.Scan0, b2bytes, 0, bytes);

            for (int n = 0; n <= bytes - 1; n++)
            {
                if (b1bytes[n] != b2bytes[n])
                {
                    result = false;
                    break;
                }
            }

            leftBitmap.UnlockBits(bmd1);
            rightBitmap.UnlockBits(bmd2);

            #endregion

            return result;
        }
        

        //public static Bitmap ConvertToGrayScale(Bitmap original)
        //{
        //    if (original == null)
        //        return null;
        //    //create a blank bitmap the same size as original
        //    Bitmap newBitmap = new Bitmap(original.Width, original.Height);

        //    //get a graphics object from the new image
        //    Graphics g = Graphics.FromImage(newBitmap);

        //    //create the grayscale ColorMatrix
        //    ColorMatrix colorMatrix = new ColorMatrix(
        //       new float[][] 
        //      {
        //         new float[] {.3f, .3f, .3f, 0, 0},
        //         new float[] {.59f, .59f, .59f, 0, 0},
        //         new float[] {.11f, .11f, .11f, 0, 0},
        //         new float[] {0, 0, 0, 1, 0},
        //         new float[] {0, 0, 0, 0, 1}
        //      });

        //    //create some image attributes
        //    ImageAttributes attributes = new ImageAttributes();

        //    //set the color matrix attribute
        //    attributes.SetColorMatrix(colorMatrix);

        //    //draw the original image on the new image
        //    //using the grayscale color matrix
        //    g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
        //       0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

        //    //dispose the Graphics object
        //    g.Dispose();
        //    return newBitmap;
        //}

        //public static Bitmap MakeGrayscale2(Bitmap original)
        //{
        //    unsafe
        //    {
        //        //create an empty bitmap the same size as original
        //        Bitmap newBitmap = new Bitmap(original.Width, original.Height);

        //        //lock the original bitmap in memory
        //        BitmapData originalData = original.LockBits(
        //           new Rectangle(0, 0, original.Width, original.Height),
        //           ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

        //        //lock the new bitmap in memory
        //        BitmapData newData = newBitmap.LockBits(
        //           new Rectangle(0, 0, original.Width, original.Height),
        //           ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

        //        //set the number of bytes per pixel
        //        int pixelSize = 3;

        //        for (int y = 0; y < original.Height; y++)
        //        {
        //            //get the data from the original image
        //            byte* oRow = (byte*)originalData.Scan0 + (y * originalData.Stride);

        //            //get the data from the new image
        //            byte* nRow = (byte*)newData.Scan0 + (y * newData.Stride);

        //            for (int x = 0; x < original.Width; x++)
        //            {
        //                //create the grayscale version
        //                byte grayScale =
        //                   (byte)((oRow[x * pixelSize] * .11) + //B
        //                   (oRow[x * pixelSize + 1] * .59) +  //G
        //                   (oRow[x * pixelSize + 2] * .3)); //R

        //                //set the new image's pixel to the grayscale version
        //                nRow[x * pixelSize] = grayScale; //B
        //                nRow[x * pixelSize + 1] = grayScale; //G
        //                nRow[x * pixelSize + 2] = grayScale; //R
        //            }
        //        }

        //        //unlock the bitmaps
        //        newBitmap.UnlockBits(newData);
        //        original.UnlockBits(originalData);

        //        return newBitmap;
        //    }
        //}

        //[DllImport("msvcrt.dll")]
        //private static extern int memcmp(IntPtr b1, IntPtr b2, long count);

        //public static bool CompareMemCmp(Bitmap b1, Bitmap b2)
        //{
        //    if ((b1 == null) != (b2 == null)) return false;
        //    if (b1.Size != b2.Size) return false;

        //    var bd1 = b1.LockBits(new Rectangle(new Point(0, 0), b1.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
        //    var bd2 = b2.LockBits(new Rectangle(new Point(0, 0), b2.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

        //    try
        //    {
        //        IntPtr bd1scan0 = bd1.Scan0;
        //        IntPtr bd2scan0 = bd2.Scan0;

        //        int stride = bd1.Stride;
        //        int len = stride * b1.Height;

        //        return memcmp(bd1scan0, bd2scan0, len) == 0;
        //    }
        //    finally
        //    {
        //        b1.UnlockBits(bd1);
        //        b2.UnlockBits(bd2);
        //    }
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace OlegZhukov.CAR
{
    class BitmapToIPictureAdapter : IPicture
    {
        Bitmap bitmap;

        public BitmapToIPictureAdapter(Bitmap bitmap)
        {
            this.bitmap = bitmap;
        }

        public int GetPixel(int x, int y)
        {
            return bitmap.GetPixel(x, y).ToArgb();
        }

        public void SetPixel(int x, int y, int argb)
        {
            bitmap.SetPixel(x, y, Color.FromArgb(argb));
        }

        public int Width
        {
            get { return bitmap.Width; }
        }

        public int Height
        {
            get { return bitmap.Height; }
        }
    }
}

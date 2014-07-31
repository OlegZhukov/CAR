using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaintDotNet;

namespace OlegZhukov.CAR.Paint.NET
{
    class SurfaceToIPictureAdapter : IPicture
    {
        Surface surface;

        public SurfaceToIPictureAdapter(Surface surface)
        {
            this.surface = surface;
        }

        public int GetPixel(int x, int y)
        {
            return (int)surface[x, y].Bgra;
        }

        public void SetPixel(int x, int y, int argb)
        {
            surface[x, y] = ColorBgra.FromUInt32((uint)argb);
        }

        public int Width
        {
            get { return surface.Width; }
        }

        public int Height
        {
            get { return surface.Height; }
        }
    }
}

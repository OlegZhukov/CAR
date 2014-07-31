using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OlegZhukov.CAR
{
    interface IPicture
    {
        int GetPixel(int x, int y);
        void SetPixel(int x, int y, int argb);
        int Width { get; }
        int Height { get; }
    }
}

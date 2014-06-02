using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OlegZhukov.CAR
{
    class BrightnessGradientNorm : EnergyFunction
    {
        public override float value(int i, int x, int y)
        {
            float deltaX, deltaY;

            if (x == 0) deltaX = brightness(i + 1) - brightness(i);
            else if (x == n - 1) deltaX = brightness(i) - brightness(i - 1);
            else deltaX = (brightness(i + 1) - brightness(i - 1)) / 2;

            if (y == 0) deltaY = brightness(i + n) - brightness(i);
            else if (y == m - 1) deltaY = brightness(i) - brightness(i - n);
            else deltaY = (brightness(i + n) - brightness(i - n)) / 2;

            return (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }
    }
}

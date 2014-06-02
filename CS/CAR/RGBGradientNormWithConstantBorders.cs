using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace OlegZhukov.CAR
{
    class RGBGradientNormWithConstantBorders : EnergyFunction
    {
        const int BORDER_ENERGY = 195075;

        public override float value(int i, int x, int y)
        {
            if (x == 0 || x == n - 1 || y == 0 || y == m - 1) return BORDER_ENERGY;

            int upper = linearizedPicture[i - n], lower = linearizedPicture[i + n],
                  left = linearizedPicture[i - 1], right = linearizedPicture[i + 1];
            float rDeltaX = red(right) - red(left), rDeltaY =
                    red(lower) - red(upper), gDeltaX =
                    green(right) - green(left), gDeltaY =
                    green(lower) - green(upper), bDeltaX =
                    blue(right) - blue(left), bDeltaY =
                    blue(lower) - blue(upper);
            return (float)Math.Sqrt(rDeltaX * rDeltaX + rDeltaY * rDeltaY
                    + gDeltaX * gDeltaX + gDeltaY * gDeltaY
                    + bDeltaX * bDeltaX + bDeltaY * bDeltaY);
        }
    }
}

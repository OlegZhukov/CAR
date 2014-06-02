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

            Color upper = linearizedPicture[i - n], lower = linearizedPicture[i + n],
                  left = linearizedPicture[i - 1], right = linearizedPicture[i + 1];
            float rDeltaX = right.R - left.R, rDeltaY =
                    lower.R - upper.R, gDeltaX =
                    right.G - left.G, gDeltaY =
                    lower.G - upper.G, bDeltaX =
                    right.B - left.B, bDeltaY =
                    lower.B - upper.B;
            return (float)Math.Sqrt(rDeltaX * rDeltaX + rDeltaY * rDeltaY
                    + gDeltaX * gDeltaX + gDeltaY * gDeltaY
                    + bDeltaX * bDeltaX + bDeltaY * bDeltaY);
        }
    }
}

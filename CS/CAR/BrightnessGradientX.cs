﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OlegZhukov.CAR
{
    class BrightnessGradientX : EnergyFunction
    {
        public override float value(int i, int x, int y)
        {
            float deltaX;

            if (x == 0) deltaX = brightness(i + 1) - brightness(i);
            else if (x == n - 1) deltaX = brightness(i) - brightness(i - 1);
            else deltaX = (brightness(i + 1) - brightness(i - 1)) / 2;

            return Math.Abs(deltaX);
        }
    }
}

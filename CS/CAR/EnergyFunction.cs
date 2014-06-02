using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace OlegZhukov.CAR
{
    abstract class EnergyFunction
    {
        protected int n, m;
        protected Color[] linearizedPicture;

        public void decrementN()
        {
            this.n--;
        }

        public void init(int n, int m, Color[] linearizedPicture)
        {
            this.n = n;
            this.m = m;
            this.linearizedPicture = linearizedPicture;
        }

        public abstract float value(int i, int x, int y);

        protected float brightness(int i)
        {
            Color color = linearizedPicture[i];
            return (color.R + color.G + color.B) / (3f * 255f);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OlegZhukov.CAR
{
    class ShortestVerticalPath
    {
        int n;
        int nm;
        float[] energy;

        public readonly float[] distTo;
        public readonly int[] prev;

        public ShortestVerticalPath(float[] energy, int n, int nm, float[] distTo,
                int[] prev) {
            this.n = n;
            this.nm = nm;
            this.energy = energy;
            this.distTo = distTo;
            this.prev = prev;
        }

        public int doFind() {
            initDistTo();
            for (int minOfUpper3 = 0, minOfUpper2 = 0, upperRight = 1, upperRightmost =
                    n - 1, curr = n;; upperRight++, curr++) {
                if (distTo[upperRight] >= distTo[minOfUpper2]) {
                    minOfUpper3 = minOfUpper2;
                    if (minOfUpper2 != upperRight - 1) minOfUpper2 =
                            distTo[upperRight] < distTo[upperRight - 1] ? upperRight
                                    : upperRight - 1;
                }
                else minOfUpper3 = minOfUpper2 = upperRight;
                distTo[curr] = distTo[minOfUpper3] + energy[curr];
                prev[curr] = minOfUpper3;
                if (upperRight == upperRightmost) {
                    curr++;
                    distTo[curr] = distTo[minOfUpper2] + energy[curr];
                    prev[curr] = minOfUpper2;
                    if (curr == nm - 1) break;
                    minOfUpper3 = minOfUpper2 = upperRight = upperRightmost + 1;
                    upperRightmost += n;
                }
            }
            return getIndexOfMinBottomDistTo();
        }

        int getIndexOfMinBottomDistTo() {
            int result = nm - n;
            for (int i = result + 1; i < nm; i++)
                if (distTo[i] < distTo[result]) result = i;
            return result;
        }

        void initDistTo() {
            Array.Copy(energy, distTo, n);
        }
    }
}

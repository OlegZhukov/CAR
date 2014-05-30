package com.OlegZhukov.CAR;

import java.util.Arrays;

class ShortestVerticalPath {
    private int n;
    private int nm;
    private float[] energy;

    public final float[] distTo;
    public final int[] prev;

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
        for (int minOfUpper3 = n, minOfUpper2 = n, upperRight = n + 1, upperRightmost =
                2 * n - 1, curr = 2 * n;; upperRight++, curr++) {
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
        return getMinBottomDistToIndex();
    }

    private int getMinBottomDistToIndex() {
        int result = nm - n;
        for (int i = result + 1; i < nm; i++)
            if (distTo[i] < distTo[result]) result = i;
        return result;
    }

    private void initDistTo() {
        Arrays.fill(distTo, 0, n - 1, 0);
        for (int i = n; i < 2 * n; i++)
            distTo[i] = energy[i];
    }
}

package com.OlegZhukov.CAR;

import java.awt.Color;
import java.util.Arrays;

import edu.princeton.cs.introcs.Picture;

public class SeamCarver {
    private static final int BORDER_ENERGY = 195075;
    private Picture picture;
    private int[] energy;
    private int[] energyT;

    public SeamCarver(Picture picture) {
        this.picture = new Picture(picture);
        energy = createEnergyMatrix();
        energyT = transpose(energy);
    }

    private int[] createEnergyMatrix() {
        int n = width(), m = height(), nm = n * m;
        int[] result = new int[nm];
        Arrays.fill(result, 0, n + 1, BORDER_ENERGY);
        int i = n + 1, x = 1, y = 1, rightmost = 2 * n - 1;
        if (n > 2 && m > 2) for (;; i++, x++) {
            if (i == rightmost) {
                result[i++] = BORDER_ENERGY;
                rightmost += n;
                if (rightmost == nm - 1) break;
                result[i] = BORDER_ENERGY; // next leftmost
                x = 0;
                y++;
            }
            else result[i] = getEnergy(x, y);
        }
        Arrays.fill(result, i, nm, BORDER_ENERGY);
        return result;
    }

    private int getEnergy(int x, int y, boolean transpose) {
        return transpose ? getEnergy(y, x) : getEnergy(x, y);
    }

    private int getEnergy(int x, int y) {
        Color top = picture.get(x, y - 1), bottom = picture.get(x, y + 1);
        Color left = picture.get(x - 1, y), right = picture.get(x + 1, y);
        int rDeltaX = right.getRed() - left.getRed(), rDeltaY =
                bottom.getRed() - top.getRed(), gDeltaX =
                right.getGreen() - left.getGreen(), gDeltaY =
                bottom.getGreen() - top.getGreen(), bDeltaX =
                right.getBlue() - left.getBlue(), bDeltaY =
                bottom.getBlue() - top.getBlue();
        return rDeltaX * rDeltaX + rDeltaY * rDeltaY
                + gDeltaX * gDeltaX + gDeltaY * gDeltaY
                + bDeltaX * bDeltaX + bDeltaY * bDeltaY;
    }

    private int[] transpose(int[] e) {
        int n = e == this.energy ? width() : height(), m =
                e == this.energy ? height() : width(), nm = n * m;
        int[] result = new int[nm];
        int i = 0;
        for (int x = 0; x < n; x++)
            for (int j = x; j < nm; j += n)
                result[i++] = e[j];
        return result;
    }

    public Picture picture() {
        return this.picture;
    }

    public int width() {
        return this.picture.width();
    }

    public int height() {
        return this.picture.height();
    }

    public double energy(int x, int y) {
        if (x < 0 || y < 0 || x >= width() || y >= height()) {
            throw new IndexOutOfBoundsException();
        }
        return energy[x + y * width()];
    }

    public int[] findHorizontalSeam() {
        return findSeam(energyT, height());
    }

    public int[] findVerticalSeam() {
        return findSeam(energy, width());
    }

    private int[] findSeam(int[] e, int n) {
        int nm = e.length, m = nm / n;
        int[] result = new int[m];
        int[] distTo = new int[nm], prev = new int[nm];
        int spNode = new ShortestVerticalPath(e, n, distTo, prev).doFind();
        int rowLeftmost = nm - n;
        for (int i = m - 1; i >= 0; i--) {
            result[i] = spNode - rowLeftmost;
            rowLeftmost -= n;
            spNode = prev[spNode];
        }
        return result;
    }

    public void removeHorizontalSeam(int[] a) {
        int n = picture.width(), m = picture.height();
        Picture newPicture = new Picture(n, m - 1);
        for (int x = 0; x < n; x++) {
            int shift = 0;
            for (int y = 0; y < m; y++)
                if (y == a[x]) shift = 1;
                else newPicture.set(x, y - shift, picture.get(x, y));
        }
        picture = newPicture;

        energyT = cloneEnergySkippingSeam(energyT, a);
        correctEnergyAlongSeam(energyT, a);
        energy = transpose(energyT);
    }

    public void removeVerticalSeam(int[] a) {
        int n = picture.width(), m = picture.height();
        Picture newPicture = new Picture(n - 1, m);
        for (int y = 0; y < m; y++) {
            int shift = 0;
            for (int x = 0; x < n; x++)
                if (x == a[y]) shift = 1;
                else newPicture.set(x - shift, y, picture.get(x, y));
        }
        picture = newPicture;

        energy = cloneEnergySkippingSeam(energy, a);
        correctEnergyAlongSeam(energy, a);
        energyT = transpose(energy);
    }

    private void correctEnergyAlongSeam(int[] e, int[] a) {
        int nm = e.length, m = a.length, n = nm / m;
        boolean transp = e == energyT;
        for (int y = 1, offset = n; y < m - 1; y++, offset += n) {
            int x = a[y], pos = offset + x;
            if (x < n - 1 && x > 0) e[pos] = getEnergy(x, y, transp);
            else if (x == 0) e[pos] = BORDER_ENERGY;
            if (x < n && x > 1) e[pos - 1] = getEnergy(x - 1, y, transp);
            else if (x == n) e[pos - 1] = BORDER_ENERGY;
        }
    }

    private int[] cloneEnergySkippingSeam(int[] src, int[] a) {
        int nm = src.length, m = a.length, n = nm / m;
        int[] dest = new int[nm - m];
        System.arraycopy(src, 0, dest, 0, a[0]);
        int srcPos = a[0] + 1, destPos = a[0];
        for (int i = 0; i < m - 1; i++) {
            int length = a[i + 1] - a[i] + n - 1;
            System.arraycopy(src, srcPos, dest, destPos, length);
            srcPos += length + 1;
            destPos += length;
        }
        System.arraycopy(src, srcPos, dest, destPos, n - a[m - 1] - 1);
        return dest;
    }
}

package com.OlegZhukov.CAR;

import java.awt.Color;

import edu.princeton.cs.introcs.Picture;

public class SeamCarver {
    private Picture cachedPicture;
    private boolean removeVerticalSeams;
    private boolean reverseScan;
    private ProgressListener progressListener;
    private int n, m, nm;
    private EnergyFunction energyFunc;
    private int[] linearizedPicture;
    private float[] energy;
    private int[] lastFoundSeam;
    private float[] distTo;
    private int[] prev;

    public SeamCarver(Picture picture, boolean removeVerticalSeams,
            boolean reverseScan, EnergyFunction energyFunc,
            ProgressListener progressListener) {
        this.cachedPicture = picture;
        this.removeVerticalSeams = removeVerticalSeams;
        this.reverseScan = reverseScan;
        this.progressListener = progressListener;
        this.n = removeVerticalSeams ? picture.width() : picture.height();
        this.m = removeVerticalSeams ? picture.height() : picture.width();
        this.nm = n * m;

        initLinearizedPicture();

        this.energyFunc = energyFunc;
        this.energyFunc.init(n, linearizedPicture);

        initEnergy();

        this.lastFoundSeam = new int[m];
        this.distTo = new float[nm];
        this.prev = new int[nm];
    }

    public Picture picture() {
        if (cachedPicture == null) cachedPicture =
                createPictureByLinearizedPicture();
        return cachedPicture;
    }

    public void removeSeams(int count) {
        for (int i = 0; i < count; i++)
            removeSeam(findSeam());
    }

    private void initLinearizedPicture() {
        linearizedPicture = new int[nm];
        if (reverseScan) {
            if (removeVerticalSeams) for (int i = m - 1, k = 0; i >= 0; i--)
                for (int j = 0; j < n; j++, k++)
                    linearizedPicture[k] = cachedPicture.get(j, i).getRGB();
            else for (int i = m - 1, k = 0; i >= 0; i--)
                for (int j = 0; j < n; j++, k++)
                    linearizedPicture[k] = cachedPicture.get(i, j).getRGB();
        }
        else {
            if (removeVerticalSeams) for (int i = 0, k = 0; i < m; i++)
                for (int j = 0; j < n; j++, k++)
                    linearizedPicture[k] = cachedPicture.get(j, i).getRGB();
            else for (int i = 0, k = 0; i < m; i++)
                for (int j = 0; j < n; j++, k++)
                    linearizedPicture[k] = cachedPicture.get(i, j).getRGB();
        }
    }

    private Picture createPictureByLinearizedPicture() {
        int width = removeVerticalSeams ? n : m;
        int height = removeVerticalSeams ? m : n;
        Picture result = new Picture(width, height);
        if (reverseScan) {
            if (removeVerticalSeams) for (int i = m - 1, k = 0; i >= 0; i--)
                for (int j = 0; j < n; j++, k++)
                    result.set(j, i, new Color(linearizedPicture[k]));
            else for (int i = m - 1, k = 0; i >= 0; i--)
                for (int j = 0; j < n; j++, k++)
                    result.set(i, j, new Color(linearizedPicture[k]));
        }
        else {
            if (removeVerticalSeams) for (int i = 0, k = 0; i < m; i++)
                for (int j = 0; j < n; j++, k++)
                    result.set(j, i, new Color(linearizedPicture[k]));
            else for (int i = 0, k = 0; i < m; i++)
                for (int j = 0; j < n; j++, k++)
                    result.set(i, j, new Color(linearizedPicture[k]));
        }
        return result;
    }

    private void initEnergy() {
        energy = new float[nm];
        energy[0] = energyFunc.topLeftCornerValue();
        for (int i = 1; i < n - 1; i++)
            energy[i] = energyFunc.topBorderValue(i);
        energy[n - 1] = energyFunc.topRightCornerValue(n - 1);
        int i = n + 1, rightmost = 2 * n - 1;
        if (n > 2 && m > 2) for (;; i++) {
            if (i == rightmost) {
                energy[i++] = energyFunc.rightBorderValue(rightmost);
                rightmost += n;
                if (rightmost == nm - 1) break;
                energy[i] = energyFunc.leftBorderValue(i); // next leftmost
            }
            else energy[i] = energyFunc.nonBorderValue(i);
        }
        energy[i] = energyFunc.bottomLeftCornerValue(i);
        for (i++; i < nm - 1; i++)
            energy[i] = energyFunc.bottomBorderValue(i);
        energy[i] = energyFunc.bottomRightCornerValue(i);
    }

    private int[] findSeam() {
        int spNode =
                new ShortestVerticalPath(energy, n, nm, distTo, prev).doFind();
        int currRowLeftmost = nm - n;
        for (int i = m - 1; i >= 0; i--) {
            lastFoundSeam[i] = spNode - currRowLeftmost;
            currRowLeftmost -= n;
            spNode = prev[spNode];
        }
        return lastFoundSeam;
    }

    private void removeSeam(int[] seam) {
        cachedPicture = null;
        removeSeamFromLinearizedPictureAndEnergy(seam);
        correctEnergyAlongSeam(seam);
        if (progressListener != null) progressListener.notifySeamRemoved();
    }

    private void removeSeamFromLinearizedPictureAndEnergy(int[] seam) {
        System.arraycopy(energy, 0, energy, 0, seam[0]);
        System.arraycopy(linearizedPicture, 0, linearizedPicture, 0, seam[0]);
        int srcPos = seam[0] + 1, destPos = seam[0];
        for (int i = 0; i < m - 1; i++) {
            int length = seam[i + 1] - seam[i] + n - 1;
            System.arraycopy(energy, srcPos, energy, destPos, length);
            System.arraycopy(linearizedPicture, srcPos, linearizedPicture,
                    destPos, length);
            srcPos += length + 1;
            destPos += length;
        }
        System.arraycopy(energy, srcPos, energy, destPos, n - seam[m - 1] - 1);
        System.arraycopy(linearizedPicture, srcPos, linearizedPicture,
                destPos, n - seam[m - 1] - 1);
        n--;
        nm -= m;
        energyFunc.setN(n);
    }

    private void correctEnergyAlongSeam(int[] seam) {

        correctTopBorderEnergy(seam[0]);

        for (int y = 1, leftmost = n; y < m - 1; y++, leftmost += n) {
            int seamX = seam[y], pos = leftmost + seamX, rightmost =
                    leftmost + n - 1;
            if (seamX > 1 && seamX < n - 1) {
                energy[pos] = energyFunc.nonBorderValue(pos);
                energy[pos - 1] = energyFunc.nonBorderValue(pos - 1);
            }
            else if (seamX <= 1) {
                energy[leftmost] = energyFunc.leftBorderValue(leftmost);
                if (seamX == 1) energy[pos] = energyFunc.nonBorderValue(pos);
            }
            else if (seamX >= n - 1) {
                energy[rightmost] = energyFunc.rightBorderValue(rightmost);
                if (seamX == n - 1) energy[pos - 1] =
                        energyFunc.nonBorderValue(pos - 1);
            }
        }

        correctBottomBorderEnergy(seam[m - 1]);
    }

    private void correctTopBorderEnergy(int seamX) {
        if (seamX > 1 && seamX < n - 1) {
            energy[seamX] = energyFunc.topBorderValue(seamX);
            energy[seamX - 1] = energyFunc.topBorderValue(seamX - 1);
        }
        else if (seamX <= 1) {
            energy[0] = energyFunc.topLeftCornerValue();
            if (seamX == 1) energy[1] = energyFunc.topBorderValue(1);
        }
        else if (seamX >= n - 1) {
            energy[n - 1] = energyFunc.topRightCornerValue(n - 1);
            if (seamX == n - 1) energy[n - 2] =
                    energyFunc.topBorderValue(n - 2);
        }
    }

    private void correctBottomBorderEnergy(int seamX) {
        int bottomLeft = nm - n, bottomRight = nm - 1;
        int pos = bottomLeft + seamX;
        if (seamX > 1 && seamX < n - 1) {
            energy[pos] = energyFunc.bottomBorderValue(pos);
            energy[pos - 1] = energyFunc.bottomBorderValue(pos - 1);
        }
        else if (seamX <= 1) {
            energy[bottomLeft] = energyFunc.bottomLeftCornerValue(bottomLeft);
            if (seamX == 1) energy[pos] = energyFunc.bottomBorderValue(pos);
        }
        else if (seamX >= n - 1) {
            energy[bottomRight] =
                    energyFunc.bottomRightCornerValue(bottomRight);
            if (seamX == n - 1) energy[pos - 1] =
                    energyFunc.bottomBorderValue(pos - 1);
        }
    }
}

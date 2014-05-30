package com.OlegZhukov.CAR;

import java.awt.Color;

import edu.princeton.cs.introcs.Picture;

public class SeamCarver {
    private Picture cachedPicture;
    private boolean removeVerticalSeams;
    private ProgressListener progressListener;
    private int n, m, nm;
    private EnergyFunction energyFunc;
    private int[] linearizedPicture;
    private float[] energy;
    private int[] lastFoundSeam;
    private float[] distTo;
    private int[] prev;

    public SeamCarver(Picture picture, boolean removeVerticalSeams,
            EnergyFunction energyFunc, ProgressListener progressListener) {
        this.cachedPicture = picture;
        this.removeVerticalSeams = removeVerticalSeams;
        this.progressListener = progressListener;
        this.n = removeVerticalSeams ? picture.width() : picture.height();
        this.m = removeVerticalSeams ? picture.height() : picture.width();
        this.nm = n * m;

        initLinearizedPicture();

        this.energyFunc = energyFunc;
        this.energyFunc.init(n, m, linearizedPicture);

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
        if (removeVerticalSeams) for (int i = 0, k = 0; i < m; i++)
            for (int j = 0; j < n; j++, k++)
                linearizedPicture[k] = cachedPicture.get(j, i).getRGB();
        else for (int i = 0, k = 0; i < m; i++)
            for (int j = 0; j < n; j++, k++)
                linearizedPicture[k] = cachedPicture.get(i, j).getRGB();
    }

    private Picture createPictureByLinearizedPicture() {
        int width = removeVerticalSeams ? n : m;
        int height = removeVerticalSeams ? m : n;
        Picture result = new Picture(width, height);
        if (removeVerticalSeams) for (int i = 0, k = 0; i < m; i++)
            for (int j = 0; j < n; j++, k++)
                result.set(j, i, new Color(linearizedPicture[k]));
        else for (int i = 0, k = 0; i < m; i++)
            for (int j = 0; j < n; j++, k++)
                result.set(i, j, new Color(linearizedPicture[k]));
        return result;
    }

    private void initEnergy() {
        energy = new float[nm];
        for (int y = 0, k = 0; y < m; y++)
            for (int x = 0; x < n; x++, k++)
                energy[k] = energyFunc.value(k, x, y);
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
        updateEnergyAlongSeam(seam);
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
        energyFunc.decrementN();
    }

    private void updateEnergyAlongSeam(int[] seam) {
        for (int y = 0, leftmost = 0; y < m; y++, leftmost += n) {
            int x = seam[y], i = leftmost + x;
            if (x < n) energy[i] = energyFunc.value(i, x, y);
            if (x > 0) energy[i - 1] = energyFunc.value(i - 1, x - 1, y);
        }
    }
}

/*
 * Content-Aware Resizing Tool.
 *
 * Copyright © 2014, Oleg Zhukov (mailto:mail@OlegZhukov.com)
 *
 * This software is licensed under GPL 3.0 license.
 */
package com.OlegZhukov.CAR;

public abstract class EnergyFunction {
    protected int n, m;
    protected int[] linearizedPicture;

    public void decrementN() {
        this.n--;
    }

    public void init(int n, int m, int[] linearizedPicture) {
        this.n = n;
        this.m = m;
        this.linearizedPicture = linearizedPicture;
    }

    public abstract float value(int i, int x, int y);

    protected float red(int color) {
        return (color >> 16) & 0xFF;
    }

    protected float green(int color) {
        return (color >> 8) & 0xFF;
    }

    protected float blue(int color) {
        return (color >> 0) & 0xFF;
    }

    protected float brightness(int i) {
        int color = linearizedPicture[i];
        return (red(color) + green(color) + blue(color)) / (3 * 255);
    }
}

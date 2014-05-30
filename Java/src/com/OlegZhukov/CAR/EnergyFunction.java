package com.OlegZhukov.CAR;

public abstract class EnergyFunction {
    protected int n;
    protected int[] linearizedPicture;

    public void setN(int n) {
        this.n = n;
    }

    public void init(int n, int[] linearizedPicture) {
        this.n = n;
        this.linearizedPicture = linearizedPicture;
    }

    public abstract float nonBorderValue(int i);

    public abstract float topBorderValue(int i);

    public abstract float bottomBorderValue(int i);

    public abstract float leftBorderValue(int i);

    public abstract float rightBorderValue(int i);

    public abstract float topLeftCornerValue();

    public abstract float topRightCornerValue(int i);

    public abstract float bottomLeftCornerValue(int i);

    public abstract float bottomRightCornerValue(int i);

    protected float red(int color) {
        return (color >> 16) & 0xFF;
    }

    protected float green(int color) {
        return (color >> 8) & 0xFF;
    }

    protected float blue(int color) {
        return (color >> 0) & 0xFF;
    }

    protected float brightness(int color) {
        return (red(color) + green(color) + blue(color)) / 765;
    }
}

package com.OlegZhukov.CAR;

public class RGBGradientNormWithConstantBorders extends EnergyFunction {
    private static final int BORDER_ENERGY = 195075;

    @Override
    public float nonBorderValue(int i) {
        int upper = linearizedPicture[i - n], lower = linearizedPicture[i + n];
        int left = linearizedPicture[i - 1], right = linearizedPicture[i + 1];
        float rDeltaX = red(right) - red(left), rDeltaY =
                red(lower) - red(upper), gDeltaX =
                green(right) - green(left), gDeltaY =
                green(lower) - green(upper), bDeltaX =
                blue(right) - blue(left), bDeltaY =
                blue(lower) - blue(upper);
        return (float) Math.sqrt(rDeltaX * rDeltaX + rDeltaY * rDeltaY
                + gDeltaX * gDeltaX + gDeltaY * gDeltaY
                + bDeltaX * bDeltaX + bDeltaY * bDeltaY);
    }

    @Override
    public float topBorderValue(int i) {
        return BORDER_ENERGY;
    }

    @Override
    public float bottomBorderValue(int i) {
        return BORDER_ENERGY;
    }

    @Override
    public float leftBorderValue(int i) {
        return BORDER_ENERGY;
    }

    @Override
    public float rightBorderValue(int i) {
        return BORDER_ENERGY;
    }

    @Override
    public float topLeftCornerValue() {
        return BORDER_ENERGY;
    }

    @Override
    public float topRightCornerValue(int i) {
        return BORDER_ENERGY;
    }

    @Override
    public float bottomLeftCornerValue(int i) {
        return BORDER_ENERGY;
    }

    @Override
    public float bottomRightCornerValue(int i) {
        return BORDER_ENERGY;
    }

}

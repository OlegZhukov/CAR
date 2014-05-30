package com.OlegZhukov.CAR;

public class BrightnessGradientX extends EnergyFunction {

    @Override
    public float nonBorderValue(int i) {
        return gradX(linearizedPicture[i - 1], linearizedPicture[i + 1]);
    }

    @Override
    public float topBorderValue(int i) {
        return gradX(linearizedPicture[i - 1], linearizedPicture[i + 1]);
    }

    @Override
    public float bottomBorderValue(int i) {
        return gradX(linearizedPicture[i - 1], linearizedPicture[i + 1]);
    }

    @Override
    public float leftBorderValue(int i) {
        return grad2X(linearizedPicture[i], linearizedPicture[i + 1]);
    }

    @Override
    public float rightBorderValue(int i) {
        return grad2X(linearizedPicture[i - 1], linearizedPicture[i]);
    }

    @Override
    public float topLeftCornerValue() {
        return grad2X(linearizedPicture[0], linearizedPicture[1]);
    }

    @Override
    public float topRightCornerValue(int i) {
        return grad2X(linearizedPicture[i - 1], linearizedPicture[i]);
    }

    @Override
    public float bottomLeftCornerValue(int i) {
        return grad2X(linearizedPicture[i], linearizedPicture[i + 1]);
    }

    @Override
    public float bottomRightCornerValue(int i) {
        return grad2X(linearizedPicture[i - 1], linearizedPicture[i]);
    }

    private float gradX(int left, int right) {
        return Math.abs((brightness(right) - brightness(left)) / 2);
    }

    private float grad2X(int left, int right) {
        return Math.abs(brightness(right) - brightness(left));
    }
}

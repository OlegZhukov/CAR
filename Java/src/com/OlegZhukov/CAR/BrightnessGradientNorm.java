package com.OlegZhukov.CAR;

public class BrightnessGradientNorm extends EnergyFunction {

    @Override
    public float nonBorderValue(int i) {
        return grad(linearizedPicture[i - n], linearizedPicture[i + n],
                linearizedPicture[i - 1], linearizedPicture[i + 1]);
    }

    @Override
    public float topBorderValue(int i) {
        return grad2Y(linearizedPicture[i], linearizedPicture[i + n],
                linearizedPicture[i - 1], linearizedPicture[i + 1]);
    }

    @Override
    public float bottomBorderValue(int i) {
        return grad2Y(linearizedPicture[i - n], linearizedPicture[i],
                linearizedPicture[i - 1], linearizedPicture[i + 1]);
    }

    @Override
    public float leftBorderValue(int i) {
        return grad2X(linearizedPicture[i - n], linearizedPicture[i + n],
                linearizedPicture[i], linearizedPicture[i + 1]);
    }

    @Override
    public float rightBorderValue(int i) {
        return grad2X(linearizedPicture[i - n], linearizedPicture[i + n],
                linearizedPicture[i - 1], linearizedPicture[i]);
    }

    @Override
    public float topLeftCornerValue() {
        return grad2XY(linearizedPicture[0], linearizedPicture[n],
                linearizedPicture[0], linearizedPicture[1]);
    }

    @Override
    public float topRightCornerValue(int i) {
        return grad2XY(linearizedPicture[i], linearizedPicture[i + n],
                linearizedPicture[i - 1], linearizedPicture[i]);
    }

    @Override
    public float bottomLeftCornerValue(int i) {
        return grad2XY(linearizedPicture[i - n], linearizedPicture[i],
                linearizedPicture[i], linearizedPicture[i + 1]);
    }

    @Override
    public float bottomRightCornerValue(int i) {
        return grad2XY(linearizedPicture[i - n], linearizedPicture[i],
                linearizedPicture[i - 1], linearizedPicture[i]);
    }

    private float grad(int upper, int lower, int left, int right) {
        float deltaX = (brightness(right) - brightness(left)) / 2, deltaY =
                (brightness(lower) - brightness(upper)) / 2;
        return (float) Math.sqrt(deltaX * deltaX + deltaY * deltaY);
    }

    private float grad2X(int upper, int lower, int left, int right) {
        float deltaX = brightness(right) - brightness(left), deltaY =
                (brightness(lower) - brightness(upper)) / 2;
        return (float) Math.sqrt(deltaX * deltaX + deltaY * deltaY);
    }

    private float grad2Y(int upper, int lower, int left, int right) {
        float deltaX = (brightness(right) - brightness(left)) / 2, deltaY =
                brightness(lower) - brightness(upper);
        return (float) Math.sqrt(deltaX * deltaX + deltaY * deltaY);
    }

    private float grad2XY(int upper, int lower, int left, int right) {
        float deltaX = brightness(right) - brightness(left), deltaY =
                brightness(lower) - brightness(upper);
        return (float) Math.sqrt(deltaX * deltaX + deltaY * deltaY);
    }
}

/*
 * Content-Aware Resizing Tool.
 *
 * Copyright © 2014, Oleg Zhukov (mailto:mail@OlegZhukov.com)
 *
 * This software is licensed under GPL 3.0 license.
 */
package com.OlegZhukov.CAR;

public class RGBGradientNormWithConstantBorders extends EnergyFunction {
    private static final int BORDER_ENERGY = 195075;

    @Override
    public float value(int i, int x, int y) {

        if (x == 0 || x == n - 1 || y == 0 || y == m - 1) return BORDER_ENERGY;

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
}

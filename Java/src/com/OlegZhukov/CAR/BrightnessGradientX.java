/*
 * Content-Aware Resizing Tool.
 *
 * Copyright © 2014, Oleg Zhukov (mailto:mail@OlegZhukov.com)
 *
 * This software is licensed under GPL 3.0 license.
 */
package com.OlegZhukov.CAR;

public class BrightnessGradientX extends EnergyFunction {

    @Override
    public float value(int i, int x, int y) {
        float deltaX;

        if (x == 0) deltaX = brightness(i + 1) - brightness(i);
        else if (x == n - 1) deltaX = brightness(i) - brightness(i - 1);
        else deltaX = (brightness(i + 1) - brightness(i - 1)) / 2;

        return Math.abs(deltaX);
    }
}

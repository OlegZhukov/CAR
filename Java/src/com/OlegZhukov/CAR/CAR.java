/*
 * Content-Aware Resizing Tool.
 *
 * Copyright © 2014, Oleg Zhukov (mailto:mail@OlegZhukov.com)
 * 
 * This software is licensed under GPL 3.0 license.
 */
package com.OlegZhukov.CAR;

import java.io.File;

import edu.princeton.cs.introcs.Picture;
import edu.princeton.cs.introcs.Stopwatch;

public class CAR {

    private static String inputPictureFile;
    private static Picture inputPicture;
    private static EnergyFunction energyFunc;
    private static int verticalSeamCount;
    private static int horizontalSeamCount;
    private static int seamsRemoved;

    public static void main(String[] args) {
        if (!processInputArgs(args)) return;
        Picture picture = inputPicture;
        Stopwatch sw = new Stopwatch();
        if (verticalSeamCount > 0) {
            seamsRemoved = 0;
            SeamCarver sc =
                    new SeamCarver(picture, true, energyFunc,
                            () -> System.out.printf(
                                    "Changing width: %d%%\r",
                                    ++seamsRemoved * 100 / verticalSeamCount));
            sc.removeSeams(verticalSeamCount);
            picture = sc.picture();
            System.out.printf("Changing width: 100%% [%fsec]\n",
                    sw.elapsedTime());
        }
        if (horizontalSeamCount > 0) {
            seamsRemoved = 0;
            SeamCarver sc =
                    new SeamCarver(picture, false, energyFunc,
                            () -> System.out.printf(
                                    "Changing height: %d%%\r",
                                    ++seamsRemoved * 100 / horizontalSeamCount));
            sc.removeSeams(horizontalSeamCount);
            picture = sc.picture();
            System.out.printf("Changing height: 100%% [%fsec]\n",
                    sw.elapsedTime());
        }
        outputPicture(picture);
    }

    public static String getOutputPictureFile() {
        return insertCARBeforeExtension(inputPictureFile);
    }

    private static boolean processInputArgs(String[] args) {
        StringBuilder newWidth = new StringBuilder(), newHeight =
                new StringBuilder();
        energyFunc = new BrightnessGradientNorm();
        for (String argS : args) {
            StringBuilder arg = new StringBuilder(argS);
            if (cutAnyBeginning(arg, "-w=", "--width=")) newWidth = arg;
            else if (cutAnyBeginning(arg, "-h=", "--height=")) newHeight = arg;
            else if (cutAnyBeginning(arg, "-e=", "--energy=")) energyFunc =
                    parseEnergyFunc(arg);
            else if (new File(argS).isFile()) inputPicture =
                    new Picture(inputPictureFile = argS);
            else {
                System.out.printf("\nWrong option or file name: %s\n\n", argS);
                printUsage();
                return false;
            }
        }
        if (inputPicture == null || energyFunc == null
                || (newWidth.length() == 0 && newHeight.length() == 0)) {
            printUsage();
            return false;
        }
        return initSeamCount(newWidth.toString(), newHeight.toString());
    }

    private static boolean cutAnyBeginning(StringBuilder argSB,
            String... substrings) {
        for (String substr : substrings)
            if (argSB.indexOf(substr) == 0) {
                argSB.delete(0, substr.length());
                return true;
            }
        return false;
    }

    private static EnergyFunction parseEnergyFunc(StringBuilder arg) {
        switch (arg.toString()) {
        case "BrightGradNorm":
            return new BrightnessGradientNorm();
        case "BrightGradX":
            return new BrightnessGradientX();
        case "RGBGradNorm":
            return new RGBGradientNormWithConstantBorders();
        }
        return null;
    }

    private static void outputPicture(Picture picture) {
        picture.save(getOutputPictureFile());
    }

    private static String insertCARBeforeExtension(String str) {
        int extPos = str.lastIndexOf(".");
        if (extPos == -1) return str + "_CAR";
        return str.substring(0, extPos) + "_CAR" + str.substring(extPos);
    }

    private static void printUsage() {
        System.out.println("Usage: java ...CAR [OPTION]... IMAGE_FILE");
        System.out.println("Options:");
        System.out.println("  -w, --width=NUM[px|%]\t\t"
                + "Reduce image width to NUM px (or %)");
        System.out.println("  -h, --height=NUM[px|%]\t"
                + "Reduce image height to NUM px (or %)");
        System.out.println("  -e, --energy=ENERGY_FUNC\t"
                + "Specify the energy (pixel meaningfulness)\n\t\t\t\t"
                + "function. Possible values are:\n\t\t\t\t"
                + " BrightGradNorm - norm of brightness gradient,\n\t\t\t\t"
                + " BrightGradX - brightness gradient X component,\n\t\t\t\t"
                + " RGBGradNorm - norm of RGB gradient.\n\t\t\t\t"
                + "Default is BrightGradNorm.");
    }

    private static boolean
            initSeamCount(String newWidthStr, String newHeightStr) {
        int newWidth = parse(newWidthStr, inputPicture.width());
        int newHeight = parse(newHeightStr, inputPicture.height());
        if (newWidth < 3 || newWidth > inputPicture.width()) {
            System.out.println("New width should be between 3px and the "
                    + "original image width.");
            return false;
        }
        if (newHeight < 3 || newHeight > inputPicture.height()) {
            System.out.println("New height should be between 3px and the "
                    + "original image height.");
            return false;
        }
        verticalSeamCount = inputPicture.width() - newWidth;
        horizontalSeamCount = inputPicture.height() - newHeight;
        return true;
    }

    private static int parse(String valStr, int originalVal) {
        int val;
        try {
            if (!valStr.isEmpty()) val =
                    valStr.endsWith("%") ? Integer.parseInt(valStr
                            .replaceFirst("%", "")) * originalVal / 100
                            : Integer.parseInt(valStr.replaceFirst("px", ""));
            else val = originalVal;
        } catch (NumberFormatException e) {
            val = -1;
        }
        return val;
    }
}

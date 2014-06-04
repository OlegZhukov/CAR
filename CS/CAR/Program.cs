using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.IO;

namespace OlegZhukov.CAR
{
    public class Program
    {
        static String inputPictureFile;
        static Bitmap inputPicture;
        static EnergyFunction energyFunc;
        static int verticalSeamCount;
        static int horizontalSeamCount;

        public static void Main(String[] args)
        {
            if (!processInputArgs(args)) return;
            Bitmap picture = inputPicture;
            using (inputPicture)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                if (verticalSeamCount > 0) picture = removeSeams(picture, true);
                if (horizontalSeamCount > 0) picture = removeSeams(picture, false);
                Console.WriteLine("Elapsed time: {0:f}sec", sw.Elapsed.TotalSeconds);
            }
            outputPicture(picture);
        }

        public static String getOutputPictureFile()
        {
            return insertCARBeforeExtension(inputPictureFile);
        }

        static Bitmap removeSeams(Bitmap picture, bool vertical)
        {
            int seamCount = vertical ? verticalSeamCount : horizontalSeamCount,
                seamsRemoved = 0;
            string changedDimension = vertical ? "width" : "height";
            SeamCarver sc = new SeamCarver(picture, vertical, energyFunc,
                    () => Console.Write("Changing {0}: {1}%\r",
                    changedDimension, ++seamsRemoved * 100 / seamCount));
            sc.removeSeams(seamCount);
            Console.WriteLine("Changing {0}: 100%", changedDimension);
            return sc.picture();
        }

        static bool processInputArgs(String[] args)
        {
            StringBuilder newWidth = new StringBuilder(), newHeight =
                    new StringBuilder();
            energyFunc = new BrightnessGradientNorm();
            foreach (String argS in args)
            {
                StringBuilder arg = new StringBuilder(argS);
                if (cutAnyBeginning(arg, "-w=", "--width=")) newWidth = arg;
                else if (cutAnyBeginning(arg, "-h=", "--height=")) newHeight = arg;
                else if (cutAnyBeginning(arg, "-e=", "--energy=")) energyFunc =
                        parseEnergyFunc(arg);
                else if (File.Exists(argS)) inputPicture =
                        new Bitmap(inputPictureFile = argS);
                else
                {
                    Console.Write("\nWrong option or file name: {0}\n\n", argS);
                    printUsage();
                    return false;
                }
            }
            if (inputPicture == null || energyFunc == null
                || (newWidth.Length == 0 && newHeight.Length == 0))
            {
                printUsage();
                return false;
            }
            return initSeamCount(newWidth.ToString(), newHeight.ToString());
        }

        static bool cutAnyBeginning(StringBuilder argSB,
                params String[] substrings)
        {
            foreach (String substr in substrings)
                if (argSB.ToString().IndexOf(substr) == 0)
                {
                    argSB.Remove(0, substr.Length);
                    return true;
                }
            return false;
        }

        static EnergyFunction parseEnergyFunc(StringBuilder arg)
        {
            switch (arg.ToString())
            {
                case "BrightGradNorm":
                    return new BrightnessGradientNorm();
                case "BrightGradX":
                    return new BrightnessGradientX();
                case "RGBGradNorm":
                    return new RGBGradientNormWithConstantBorders();
            }
            return null;
        }

        static void outputPicture(Bitmap picture)
        {
            picture.Save(getOutputPictureFile());
        }

        static String insertCARBeforeExtension(String str)
        {
            int extPos = str.LastIndexOf(".");
            if (extPos == -1) return str + "_CAR";
            return str.Substring(0, extPos) + "_CAR" + str.Substring(extPos);
        }

        static void printUsage()
        {
            Console.WriteLine("Usage: CAR.exe [OPTION]... IMAGE_FILE");
            Console.WriteLine("Options:");
            Console.WriteLine("  -w, --width=NUM[px|%]\t\t"
                    + "Reduce image width to NUM px (or %)");
            Console.WriteLine("  -h, --height=NUM[px|%]\t"
                    + "Reduce image height to NUM px (or %)");
            Console.WriteLine("  -e, --energy=ENERGY_FUNC\t"
                    + "Specify the energy (pixel meaningfulness)\n\t\t\t\t"
                    + "function. Possible values are:\n\t\t\t\t"
                    + " BrightGradNorm - norm of brightness gradient,\n\t\t\t\t"
                    + " BrightGradX - brightness gradient X component,\n\t\t\t\t"
                    + " RGBGradNorm - norm of RGB gradient.\n\t\t\t\t"
                    + "Default is BrightGradNorm.");
        }

        static bool initSeamCount(String newWidthStr, String newHeightStr)
        {
            int newWidth = parse(newWidthStr, inputPicture.Width);
            int newHeight = parse(newHeightStr, inputPicture.Height);
            if (newWidth < 3 || newWidth > inputPicture.Width)
            {
                Console.WriteLine("New width should be between 3px and the "
                        + "original image width.");
                return false;
            }
            if (newHeight < 3 || newHeight > inputPicture.Height)
            {
                Console.WriteLine("New height should be between 3px and the "
                        + "original image height.");
                return false;
            }
            verticalSeamCount = inputPicture.Width - newWidth;
            horizontalSeamCount = inputPicture.Height - newHeight;
            return true;
        }

        static int parse(String valStr, int originalVal)
        {
            int val;
            try
            {
                if (valStr.Length != 0) val =
                        valStr.EndsWith("%") ? int.Parse(valStr
                                .Replace("%", "")) * originalVal / 100
                                : int.Parse(valStr.Replace("px", ""));
                else val = originalVal;
            }
            catch (FormatException e)
            {
                val = -1;
            }
            return val;
        }
    }
}

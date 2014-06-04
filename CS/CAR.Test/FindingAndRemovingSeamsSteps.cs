using System;
using TechTalk.SpecFlow;
using OlegZhukov.CAR;
using System.IO;
using NUnit.Framework;
using System.Drawing;

namespace CAR.Test
{
    [Binding]
    public class FindingAndRemovingSeamsSteps
    {
        String inputImageFile;
        String energyFunc;

        [BeforeFeature]
        public static void BeforeFeature()
        {
            Directory.SetCurrentDirectory(string.Format(
				"..{0}..{0}..{0}..{0}Features", Path.DirectorySeparatorChar));
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            energyFunc = "";
        }

        [Given(@"image (.*)")]
        public void GivenImage(String inputImageFile)
        {
            this.inputImageFile = inputImageFile;
        }

        [When(@"I use the energy function (.*)")]
        public void UseEnergyFunc(String energyFunc)
        {
            this.energyFunc = energyFunc;
        }

        [When(@"I resize the image to (.*) width and (.*) height")]
        public void resize(String v, String h)
        {
            if (energyFunc != "") energyFunc = " -e=" + energyFunc;
            Program.Main(String.Format("-w={0} -h={1}{2} {3}", v, h, energyFunc,
                    inputImageFile).Split(' '));
        }

        [Then(@"I should get image (.*)")]
        public void shouldGetImage(String expectedImageFile)
        {
            using (Bitmap expectedImage = new Bitmap(expectedImageFile),
                   actualImage = new Bitmap(Program.getOutputPictureFile()))
            {
                Assert.AreEqual(expectedImage.Width, actualImage.Width);
                Assert.AreEqual(expectedImage.Height, actualImage.Height);
                for (int x = 0; x < expectedImage.Width; x++)
                    for (int y = 0; y < actualImage.Height; y++)
                        if (expectedImage.GetPixel(x, y) != actualImage.GetPixel(x, y))
                            Assert.Fail();
                Assert.IsTrue(true);
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (File.Exists(Program.getOutputPictureFile()))
                File.Delete(Program.getOutputPictureFile());
        }
    }
}

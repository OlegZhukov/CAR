using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace OlegZhukov.CAR
{
    class SeamCarver
    {
        IPicture picture;
        Bitmap cachedBitmap;
        bool removeVerticalSeams;
        Action progressCallback;
        int n, m, nm;
        EnergyFunction energyFunc;
        int[] linearizedPicture;
        float[] energy;
        int[] lastFoundSeam;
        float[] distTo;
        int[] prev;

        public SeamCarver(Bitmap bitmap, bool removeVerticalSeams,
                EnergyFunction energyFunc, Action progressCallback)
            : this(new BitmapToIPictureAdapter(bitmap), removeVerticalSeams, energyFunc, progressCallback)
        { }

        public SeamCarver(IPicture picture, bool removeVerticalSeams,
                EnergyFunction energyFunc, Action progressCallback)
        {
            this.picture = picture;
            this.removeVerticalSeams = removeVerticalSeams;
            this.progressCallback = progressCallback;
            this.n = removeVerticalSeams ? picture.Width : picture.Height;
            this.m = removeVerticalSeams ? picture.Height : picture.Width;
            this.nm = n * m;

            initLinearizedPicture();

            this.energyFunc = energyFunc;
            this.energyFunc.init(n, m, linearizedPicture);

            initEnergy();

            this.lastFoundSeam = new int[m];
            this.distTo = new float[nm];
            this.prev = new int[nm];
        }

        public Bitmap bitmap()
        {
            if (cachedBitmap == null) cachedBitmap =
                    createBitmapByLinearizedPicture();
            return cachedBitmap;
        }

        public void removeSeam()
        {
            removeSeam(findSeam());
        }

        public void removeSeams(int count)
        {
            for (int i = 0; i < count; i++) removeSeam();
        }

        void initLinearizedPicture()
        {
            linearizedPicture = new int[nm];
            if (removeVerticalSeams) for (int i = 0, k = 0; i < m; i++)
                    for (int j = 0; j < n; j++, k++)
                        linearizedPicture[k] = picture.GetPixel(j, i);
            else for (int i = 0, k = 0; i < m; i++)
                    for (int j = 0; j < n; j++, k++)
                        linearizedPicture[k] = picture.GetPixel(i, j);
        }

        Bitmap createBitmapByLinearizedPicture()
        {
            int width = removeVerticalSeams ? n : m;
            int height = removeVerticalSeams ? m : n;
            Bitmap result = new Bitmap(width, height);
            fillPictureByLinearizedPicture(new BitmapToIPictureAdapter(result));
            return result;
        }

        public void fillPictureByLinearizedPicture(IPicture picture)
        {
            if (removeVerticalSeams) for (int i = 0, k = 0; i < m; i++)
                    for (int j = 0; j < n; j++, k++)
                        picture.SetPixel(j, i, linearizedPicture[k]);
            else for (int i = 0, k = 0; i < m; i++)
                    for (int j = 0; j < n; j++, k++)
                        picture.SetPixel(i, j, linearizedPicture[k]);
        }

        void initEnergy()
        {
            energy = new float[nm];
            for (int y = 0, k = 0; y < m; y++)
                for (int x = 0; x < n; x++, k++)
                    energy[k] = energyFunc.value(k, x, y);
        }

        int[] findSeam()
        {
            int spNode =
                    new ShortestVerticalPath(energy, n, nm, distTo, prev).doFind();
            int currRowLeftmost = nm - n;
            for (int i = m - 1; i >= 0; i--)
            {
                lastFoundSeam[i] = spNode - currRowLeftmost;
                currRowLeftmost -= n;
                spNode = prev[spNode];
            }
            return lastFoundSeam;
        }

        void removeSeam(int[] seam)
        {
            cachedBitmap = null;
            removeSeamFromLinearizedPictureAndEnergy(seam);
            updateEnergyAlongSeam(seam);
            if (progressCallback != null) progressCallback();
        }

        void removeSeamFromLinearizedPictureAndEnergy(int[] seam)
        {
            Array.Copy(energy, 0, energy, 0, seam[0]);
            Array.Copy(linearizedPicture, 0, linearizedPicture, 0, seam[0]);

            int srcPos = seam[0] + 1, destPos = seam[0];
            for (int i = 0; i < m - 1; i++)
            {
                int length = seam[i + 1] - seam[i] + n - 1;
                Array.Copy(energy, srcPos, energy, destPos, length);
                Array.Copy(linearizedPicture, srcPos, linearizedPicture,
                        destPos, length);
                srcPos += length + 1;
                destPos += length;
            }

            Array.Copy(energy, srcPos, energy, destPos, n - seam[m - 1] - 1);
            Array.Copy(linearizedPicture, srcPos, linearizedPicture,
                    destPos, n - seam[m - 1] - 1);
            n--;
            nm -= m;
            energyFunc.decrementN();
        }

        void updateEnergyAlongSeam(int[] seam)
        {
            for (int y = 0, leftmost = 0; y < m; y++, leftmost += n)
            {
                int x = seam[y], i = leftmost + x;
                if (x < n) energy[i] = energyFunc.value(i, x, y);
                if (x > 0) energy[i - 1] = energyFunc.value(i - 1, x - 1, y);
            }
        }
    }
}

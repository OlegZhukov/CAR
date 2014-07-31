using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaintDotNet;
using System.Threading;
using System.Windows.Forms;

namespace OlegZhukov.CAR.Paint.NET
{
    public class LayerResizeTask
    {
        Surface srcSurface;
        Surface dstSurface;
        CARDialog.EnergyFunction energyFunction;
        readonly bool removeVerticalSeams;
        
        public readonly int TotalSeamsToRemove;

        public int CurrentlyRemovedSeams { get; private set; }

        public LayerResizeTask(BitmapLayer src, BitmapLayer dst, CARDialog.EnergyFunction energyFunction)
        {
            this.energyFunction = energyFunction;
            srcSurface = src.Surface;
            dstSurface = dst.Surface;
            removeVerticalSeams = src.Width > dst.Width;
            TotalSeamsToRemove = removeVerticalSeams ? src.Width - dst.Width : src.Height - dst.Height;
        }

        public static bool ExecuteAll(Queue<LayerResizeTask> layerResizeTasks)
        {
            LayerResizeTask[] allTasks = layerResizeTasks.ToArray();
            CARProgressDialog progressDialog = new CARProgressDialog(allTasks);
            Thread[] resizingThreads = new Thread[Math.Min(layerResizeTasks.Count, Environment.ProcessorCount)];
            for (int i = 0; i < resizingThreads.Length; i++)
            {
                resizingThreads[i] = new Thread(() => ResizeThreadFunction(layerResizeTasks, progressDialog));
                resizingThreads[i].Start();
            }
            bool abortRequested = progressDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel;
            if (abortRequested)
                foreach (LayerResizeTask t in allTasks)
                    t.AbortRequested = true;
            return !abortRequested;
        }

        static void ResizeThreadFunction(Queue<LayerResizeTask> layerResizeTasks, CARProgressDialog progressDialog)
        {
            while (true)
            {
                LayerResizeTask currTask;
                lock (layerResizeTasks)
                {
                    if (layerResizeTasks.Count == 0) return;
                    currTask = layerResizeTasks.Dequeue();
                }
                SeamCarver seamCarver = new SeamCarver(new SurfaceToIPictureAdapter(currTask.srcSurface),
                    currTask.removeVerticalSeams, GetEnergyFunc(currTask.energyFunction), null);
                while (!currTask.AbortRequested && currTask.CurrentlyRemovedSeams < currTask.TotalSeamsToRemove)
                {
                    seamCarver.removeSeam();
                    currTask.CurrentlyRemovedSeams++;
                    progressDialog.UpdateProgress();
                }
                if (currTask.AbortRequested) return;
                seamCarver.fillPictureByLinearizedPicture(new SurfaceToIPictureAdapter(currTask.dstSurface));
            }
        }

        static EnergyFunction GetEnergyFunc(CARDialog.EnergyFunction energyFunction)
        {
            switch (energyFunction)
            {
                case CARDialog.EnergyFunction.BrightnessGradientNorm: return new BrightnessGradientNorm();
                case CARDialog.EnergyFunction.BrightnessGradientX: return new BrightnessGradientX();
                case CARDialog.EnergyFunction.RGBGradientNormWithConstantBorders: return new RGBGradientNormWithConstantBorders();
            }
            return new BrightnessGradientNorm();
        }

        public bool AbortRequested { get; set; }
    }
}

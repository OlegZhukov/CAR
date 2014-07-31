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
                resizingThreads[i] = new Thread(() => ResizeThreadWork(layerResizeTasks, progressDialog));
                resizingThreads[i].Start();
            }
            bool abortRequested = progressDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel;
            if (abortRequested)
                foreach (LayerResizeTask t in allTasks)
                    t.AbortRequested = true;
            return !abortRequested;
        }

        static void ResizeThreadWork(Queue<LayerResizeTask> layerResizeTasks, CARProgressDialog progressDialog)
        {
            while (true)
            {
                LayerResizeTask currTask;
                lock (layerResizeTasks)
                {
                    if (layerResizeTasks.Count == 0) return;
                    currTask = layerResizeTasks.Dequeue();
                }
                currTask.srcSurface[5, 6].
                while (!currTask.AbortRequested && currTask.CurrentlyRemovedSeams < currTask.TotalSeamsToRemove)
                {
                    currTask.CurrentlyRemovedSeams++;
                    progressDialog.UpdateProgress();
                }
            }
        }

        public bool AbortRequested { get; set; }
    }
}

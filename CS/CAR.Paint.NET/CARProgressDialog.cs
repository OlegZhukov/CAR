using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PaintDotNet.SystemLayer;
using OlegZhukov.CAR.Paint.NET.Properties;

namespace OlegZhukov.CAR.Paint.NET
{
    public partial class CARProgressDialog : PaintDotNet.PdnBaseForm
    {
        LayerResizeTask[] layerResizeTasks;
        readonly int totalSeamsToRemove;

        public CARProgressDialog(LayerResizeTask[] layerResizeTasks)
        {
            this.layerResizeTasks = layerResizeTasks;
            totalSeamsToRemove = layerResizeTasks.Sum(task => task.TotalSeamsToRemove);

            base.SuspendLayout();

            base.AutoHandleGlassRelatedOptimizations = true;
            base.IsGlassDesired = true;
            this.DoubleBuffered = true;
            this.InitializeComponent();
            this.Icon = Icon.FromHandle(Resources.Icon.GetHicon());
            this.ShowInTaskbar = false;

            base.ResumeLayout();
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            int scaledHeight = UI.ScaleHeight(8), scaledWidth = UI.ScaleWidth(8);
            int x = base.IsGlassEffectivelyEnabled ? -1 : scaledWidth,
                y = Math.Max(0, scaledHeight - base.ExtendedFramePadding.Bottom);
            this.cancelButton.PerformLayout();
            this.cancelButton.Location = new Point((base.ClientSize.Width - x) - this.cancelButton.Width, (base.ClientSize.Height - y) - this.cancelButton.Height);
            if (base.IsGlassEffectivelyEnabled)
                base.GlassInset = new Padding(0, 0, 0, base.ClientSize.Height - this.cancelButton.Top + 10);
            else
                base.GlassInset = new Padding(0);
            base.OnLayout(levent);
        }

        public void UpdateProgress()
        {
            try
            {
                BeginInvoke((MethodInvoker)delegate()
                {
                    if (DialogResult == DialogResult.Cancel) // If the Cancel button was hit don't do the update
                        return;
                    int progress = layerResizeTasks.Sum(task => task.CurrentlyRemovedSeams) * 100 / totalSeamsToRemove;
                    resizingLabel.Text = string.Format("Resizing... {0}%", progress); 
                    resizeProgressBar.Value = progress;
                    if (progress == 100)
                    {
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                });
            }
            catch (InvalidOperationException) // If BeginInvoke is called on already closed form
            {
                 // then do nothing
            }
        }
    }
}

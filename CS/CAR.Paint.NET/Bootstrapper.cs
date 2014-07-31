using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using OlegZhukov.CAR.Paint.NET.Properties;
using PaintDotNet;
using PaintDotNet.Effects;

namespace OlegZhukov.CAR.Paint.NET
{
    [EffectCategory(EffectCategory.DoNotDisplay)]
    internal class Bootstrapper : Effect
    {
        const string OperationName = "Content-Aware Resize";

        FieldInfo layerPropertiesField = typeof(Layer).GetField("properties", BindingFlags.Instance | BindingFlags.NonPublic);
        FieldInfo bitmapLayerPropertiesField = typeof(BitmapLayer).GetField("properties", BindingFlags.Instance | BindingFlags.NonPublic);
        Type replaceDocumentHistoryMementoClass = Type.GetType("PaintDotNet.HistoryMementos.ReplaceDocumentHistoryMemento, PaintDotNet");

        public Bootstrapper() : base("CAR", null, string.Empty, EffectFlags.None)
        {
            PdnMenuManager.AddToImageMenu(OperationName, 2, Resources.Icon, CARMenuItemClick);
        }

        void CARMenuItemClick(object sender, EventArgs e)
        {
            object activeDocWorkspace;
            Document activeDoc = PdnWorkspaceManager.GetActiveDocumentWithWorkspace(out activeDocWorkspace);
            if (activeDoc == null) return;
            CARDialog carDialog = new CARDialog(activeDoc.Width, activeDoc.Height);
            if (carDialog.ShowDialog() == DialogResult.OK)
                DoResize(activeDoc, activeDocWorkspace, carDialog.ChangedDimension,
                         carDialog.TargetSize, carDialog.EnergyFunc);
        }

        void DoResize(Document doc, object docWorkspace, CARDialog.Dimension changedDimension,
                      int targetSize, CARDialog.EnergyFunction energyFunction)
        {
            if (targetSize == (changedDimension == CARDialog.Dimension.Width ? doc.Width : doc.Height))
                return;
            Size newSize = changedDimension == CARDialog.Dimension.Width
                ? new Size(targetSize, doc.Height) : new Size(doc.Width, targetSize);
            Document newDoc = new Document(newSize);
            Queue<LayerResizeTask> layerResizeTasks = new Queue<LayerResizeTask>();
            for (int i = 0; i < doc.Layers.Count; i++)
            {
                BitmapLayer oldLayer = doc.Layers[i] as BitmapLayer,
                            newLayer = CreateEmptyLayer(ref newSize, oldLayer);
                newDoc.Layers.Add(newLayer);
                layerResizeTasks.Enqueue(new LayerResizeTask(oldLayer, newLayer, energyFunction));
            }
            if (!LayerResizeTask.ExecuteAll(layerResizeTasks)) return;
            object historyMemento = Activator.CreateInstance(replaceDocumentHistoryMementoClass,
                OperationName, ImageResource.FromImage(Resources.Icon), docWorkspace);
            PdnWorkspaceManager.SetWorkspaceDocument(docWorkspace, newDoc);
            PdnWorkspaceManager.PushMementoToHistory(docWorkspace, historyMemento);
        }

        private BitmapLayer CreateEmptyLayer(ref Size newSize, Layer layerToClonePropertiesFrom)
        {
            Surface layerSurface = new Surface(newSize);
            BitmapLayer result = new BitmapLayer(layerSurface);
            ICloneable clonedLayerProperties = layerPropertiesField.GetValue(layerToClonePropertiesFrom) as ICloneable,
                       clonedBitmapLayerProperties = bitmapLayerPropertiesField.GetValue(layerToClonePropertiesFrom) as ICloneable;
            layerPropertiesField.SetValue(result, clonedLayerProperties.Clone());
            bitmapLayerPropertiesField.SetValue(result, clonedBitmapLayerProperties.Clone());
            return result;
        }

        public override void Render(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs, Rectangle[] rois, int startIndex, int length)
        {
            // Stub override required because of the abstract base method
        }
    }
}
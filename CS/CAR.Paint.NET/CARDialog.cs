using System;
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
    public partial class CARDialog : PaintDotNet.PdnBaseForm
    {
        readonly int originalWidth, originalHeight;
        bool changingTargetSizeInternally = false;

        enum Unit
        {
            Percent,
            Pixel
        }

        public enum Dimension
        {
            Width,
            Height
        }

        public enum EnergyFunction
        {
            BrightnessGradientNorm,
            BrightnessGradientX,
            RGBGradientNormWithConstantBorders
        }

        public CARDialog(int originalWidth, int originalHeight)
        {
            this.TargetSize = this.originalWidth = originalWidth;
            this.originalHeight = originalHeight;

            base.SuspendLayout();
            
            base.AutoHandleGlassRelatedOptimizations = true;
            base.IsGlassDesired = true;
            this.DoubleBuffered = true;
            this.InitializeComponent();
            this.Icon = Icon.FromHandle(Resources.Icon.GetHicon());
            this.ShowInTaskbar = false;
            
            SetDefaultUIControlValues();
            
            base.ResumeLayout();

            AssignEventHandlers();
        }

        public int TargetSize { get; private set; }

        public Dimension ChangedDimension
        {
            get { return widthOrHeightComboBox.SelectedIndex == 0 ? Dimension.Width : Dimension.Height; }
        }

        public EnergyFunction EnergyFunc
        {
            get
            {
                switch (contentEnergyFunctionComboBox.SelectedIndex)
                {
                    case 0: return EnergyFunction.BrightnessGradientNorm;
                    case 1: return EnergyFunction.BrightnessGradientX;
                    case 2: return EnergyFunction.RGBGradientNormWithConstantBorders;
                }
                return EnergyFunction.BrightnessGradientNorm;
            }
        }

        void SetDefaultUIControlValues()
        {
            widthOrHeightComboBox.SelectedIndex =
                percentOrPixelComboBox.SelectedIndex =
                contentEnergyFunctionComboBox.SelectedIndex = 0;
            targetSizeUpDownControl.Minimum = MinimumSizeInPercent;
        }

        void AssignEventHandlers()
        {
            widthOrHeightComboBox.SelectedIndexChanged += widthOrHeightComboBox_SelectedIndexChanged;
            targetSizeUpDownControl.ValueChanged += targetSizeUpDownControl_ValueChanged;
            percentOrPixelComboBox.SelectedIndexChanged += percentOrPixelComboBox_SelectedIndexChanged;
        }

        int OriginalSize
        {
            get { return ChangedDimension == Dimension.Width ? originalWidth : originalHeight; }
        }

        int MinimumSizeInPercent
        {
            get { return (int)Math.Ceiling(3f * 100 / OriginalSize); }
        }

        Unit CurrUnit
        {
            get { return percentOrPixelComboBox.SelectedIndex == 0 ? Unit.Percent : Unit.Pixel; }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            int scaledHeight = UI.ScaleHeight(8), scaledWidth = UI.ScaleWidth(8);
            int x = base.IsGlassEffectivelyEnabled ? -1 : scaledWidth,
                y = Math.Max(0, scaledHeight - base.ExtendedFramePadding.Bottom);
            this.cancelButton.PerformLayout();
            this.okButton.PerformLayout();
            this.cancelButton.Location = new Point((base.ClientSize.Width - x) - this.cancelButton.Width, (base.ClientSize.Height - y) - this.cancelButton.Height);
            this.okButton.Location = new Point((this.cancelButton.Left - scaledWidth) - this.okButton.Width, (base.ClientSize.Height - y) - this.okButton.Height);
            if (base.IsGlassEffectivelyEnabled)
                base.GlassInset = new Padding(0, 0, 0, base.ClientSize.Height - this.okButton.Top + 10);
            else
                base.GlassInset = new Padding(0);
            base.OnLayout(levent);
        }

        void widthOrHeightComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            changingTargetSizeInternally = true;
            try
            {
                if (CurrUnit == Unit.Percent)
                    TargetSize = (int)Decimal.Round(targetSizeUpDownControl.Value * OriginalSize / 100, 0);
                else if (targetSizeUpDownControl.Value > OriginalSize)
                    targetSizeUpDownControl.Value = TargetSize = OriginalSize;
            }
            finally
            {
                changingTargetSizeInternally = false;
            }
        }

        void targetSizeUpDownControl_ValueChanged(object sender, EventArgs e)
        {
            if (changingTargetSizeInternally) return;

            decimal newValue = targetSizeUpDownControl.Value;
            TargetSize = CurrUnit == Unit.Pixel ? (int)newValue
                                                : (int)(newValue * OriginalSize / 100);
        }

        void percentOrPixelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            changingTargetSizeInternally = true;
            try
            {
                if (CurrUnit == Unit.Percent)
                {
                    targetSizeUpDownControl.Maximum = 100;
                    targetSizeUpDownControl.Minimum = MinimumSizeInPercent;
                    int targetSizeInPercent = (int)Decimal.Round((decimal)TargetSize * 100 / OriginalSize, 0);
                    if (targetSizeInPercent < MinimumSizeInPercent)
                    {
                        targetSizeInPercent = MinimumSizeInPercent;
                        TargetSize = (int)Decimal.Round(targetSizeInPercent * OriginalSize / 100, 0);
                    }
                    targetSizeUpDownControl.Value = targetSizeInPercent;
                }
                else
                {
                    targetSizeUpDownControl.Maximum = OriginalSize;
                    targetSizeUpDownControl.Minimum = 3;
                    targetSizeUpDownControl.Value = TargetSize;
                }
            }
            finally
            {
                changingTargetSizeInternally = false;
            }
        }
    }
}

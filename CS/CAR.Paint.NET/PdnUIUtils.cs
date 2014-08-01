using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace OlegZhukov.CAR.Paint.NET
{
    class PdnUIUtils
    {
        static MethodInfo scaleHeightMethod, scaleWidthMethod;

        static PdnUIUtils()
        {
            Type uiUtilClass = Type.GetType("PaintDotNet.SystemLayer.UI, PaintDotNet.SystemLayer");
            if (uiUtilClass == null)
                uiUtilClass = Type.GetType("PaintDotNet.SystemLayer.UIUtil, PaintDotNet.SystemLayer");
            scaleHeightMethod = uiUtilClass.GetMethod("ScaleHeight", new Type[] { typeof(int) });
            scaleWidthMethod = uiUtilClass.GetMethod("ScaleWidth", new Type[] { typeof(int) });
        }

        public static int ScaleHeight(int height)
        {
            return (int)scaleHeightMethod.Invoke(null, new object[] { height });
        }

        public static int ScaleWidth(int width)
        {
            return (int)scaleWidthMethod.Invoke(null, new object[] { width });
        }
    }
}

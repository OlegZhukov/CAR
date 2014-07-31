using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using OlegZhukov.CAR.Paint.NET.Properties;
using System.Drawing;

namespace OlegZhukov.CAR.Paint.NET
{
    class PdnMenuManager
    {
        static HashSet<string> itemsAlreadyInImageMenu = new HashSet<string>();

        public static void AddToImageMenu(string itemName, int pos, Image icon, EventHandler clickHandler)
        {
            if (itemsAlreadyInImageMenu.Contains(itemName)) return;

            FieldInfo toolBarField = PdnWorkspaceManager.AppWorkspace.GetType().GetField("toolBar", BindingFlags.Instance | BindingFlags.NonPublic);
            object toolBar = toolBarField.GetValue(PdnWorkspaceManager.AppWorkspace);
            FieldInfo mainMenuField = toolBar.GetType().GetField("mainMenu", BindingFlags.Instance | BindingFlags.NonPublic);
            object mainMenu = mainMenuField.GetValue(toolBar);
            FieldInfo imageMenuField = mainMenu.GetType().GetField("imageMenu", BindingFlags.Instance | BindingFlags.NonPublic);
            ToolStripMenuItem imageMenu = imageMenuField.GetValue(mainMenu) as ToolStripMenuItem;
            imageMenu.DropDownItems.Insert(pos, new ToolStripMenuItem(itemName, icon, clickHandler));

            itemsAlreadyInImageMenu.Add(itemName);
        }
    }
}

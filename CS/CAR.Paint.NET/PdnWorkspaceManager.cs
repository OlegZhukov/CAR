using System.Reflection;
using System.Windows.Forms;
using PaintDotNet;
using System;

namespace OlegZhukov.CAR.Paint.NET
{
    class PdnWorkspaceManager
    {
        static object appWorkspace;
        static PropertyInfo activeDocumentWorkspaceProperty,
                            documentProperty,
                            historyProperty;
        static MethodInfo pushNewMementoMethod;

        static PdnWorkspaceManager()
        {
            documentProperty = Type.GetType("PaintDotNet.Controls.DocumentView, PaintDotNet").GetProperty("Document");
            historyProperty = Type.GetType("PaintDotNet.Controls.DocumentWorkspace, PaintDotNet").GetProperty("History");
            pushNewMementoMethod = Type.GetType("PaintDotNet.HistoryStack, PaintDotNet").GetMethod("PushNewMemento");
        }
        
        public static object AppWorkspace
        {
            get
            {
                if (appWorkspace == null)
                {
                    Form mainForm = FindMainForm();
                    FieldInfo appWorkspaceField = mainForm.GetType().GetField("appWorkspace", BindingFlags.Instance | BindingFlags.NonPublic);
                    appWorkspace = appWorkspaceField.GetValue(mainForm);
                    activeDocumentWorkspaceProperty = appWorkspace.GetType().GetProperty("ActiveDocumentWorkspace");
                }
                return appWorkspace;
            }
        }

        public static Document GetActiveDocumentWithWorkspace(out object activeDocWorkspace)
        {
            activeDocWorkspace = activeDocumentWorkspaceProperty.GetValue(AppWorkspace, null);
            if (activeDocWorkspace == null) return null;
            return (Document)documentProperty.GetValue(activeDocWorkspace, null);
        }

        public static void SetWorkspaceDocument(object docWorkspace, Document newDoc)
        {
            documentProperty.SetValue(docWorkspace, newDoc, null);
        }

        public static void PushMementoToHistory(object docWorkspace, object historyMemento)
        {
            object history = historyProperty.GetValue(docWorkspace, null);
            pushNewMementoMethod.Invoke(history, new object[] { historyMemento });
        }

        static Form FindMainForm()
        {
            foreach (Form form in Application.OpenForms)
                if (form.GetType().Name == "MainForm")
                    return form;
            return null;
        }
    }
}

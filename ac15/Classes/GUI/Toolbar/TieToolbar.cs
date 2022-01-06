using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;

using AcApp = Autodesk.AutoCAD.ApplicationServices.Application;

using Autodesk.Windows;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Interop;
using System.IO;
using Autodesk.AutoCAD.Interop.Common;


namespace TIExCAD.Generic
{
    public class TieToolbar : ITieToolbar
    {
        private AcadToolbar acadToolbar = null;
        readonly AcadApplication acadApp = (AcadApplication)AcApp.AcadApplication;

        public void ToolbarCreate(string toolbarName, List<ToolbarButtItem> listButtons)
        {
            //throw new NotImplementedException();
            if (!IsExistToolbarInMenubar(toolbarName))
            {
                acadToolbar = acadApp.MenuGroups.Item(0).Toolbars.Add(toolbarName);
                AcadToolbarItem btn;

                foreach (ToolbarButtItem Btn in listButtons)
                {
                   btn = acadToolbar.AddToolbarButton(
                       Btn.Index,
                       Btn.Name,
                       Btn.HelpString,
                       Btn.Macros);

                    btn.SetBitmaps(Btn.SmallIcon, Btn.LargeIcon);
                }
                // Center toolbar on AutoCAD Window
                int x = acadApp.WindowLeft + (acadApp.Width - acadToolbar.Width) / 2,
                y = acadApp.WindowTop + (acadApp.Height - acadToolbar.Height) / 2;
                acadToolbar.Float(y, x, 1);
            }

        }

        internal bool IsExistToolbarInMenubar(string toolbarName)
        {
            bool isTbar = false;

            foreach (AcadToolbar toolbar in acadApp.MenuBar)
            {
                if (toolbar.Name == toolbarName)
                {
                    isTbar = true;
                    break;
                }

            }
            return isTbar;
        }

        public void ToolbarRemove(string toolbarName)
        {
            // throw new NotImplementedException();
        }

    }
}

/*
IAcadApplication app =
(IAcadApplication) Autodesk.AutoCAD.ApplicationServices.Application.AcadApplication;
IAcadMenuGroups groups = app.MenuGroups;
IAcadMenuGroup group = groups.Item(0);
IAcadToolbars toolbars = group.Toolbars;
// Adding new toolbar
IAcadToolbar toolbar = toolbars.Add("My toolbar");
// Adding button to toolbar
IAcadToolbarItem button = toolbar.AddToolbarButton(
0, // Index of button
"My button", // Name of button
"Helpstring button", // Helpstring of button
"\x1b\x1b_.OPTIONS\n", // Macro command
false // Flyout
);
// Setting path to small and large icons
button.SetBitmaps("C:\\SmallIcon.bmp","C:\\LargeIcon.bmp");
// Center toolbar on AutoCAD Window
int x = app.WindowLeft + (app.Width - toolbar.Width)/2,
y = app.WindowTop + (app.Height - toolbar.Height)/2;
toolbar.Float(y,x,1);
*/
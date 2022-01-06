
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

namespace TIExCAD.Generic
{
    interface ITieToolbar 
    {
        // структура панели 
        // url manual
        // Dim toolbar As Object = Application.MenuGroups.item(0).Toolbars.add("Field Engineering ToolBar")

        // Через .NET 
        // https://forums.autodesk.com/t5/net/create-toolbar/m-p/1687527#M3911

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toolbarName">имя панели инструментов</param>
        /// <param name="listButtons"> 
        /// button = index(int), name, help-string, macros(str), smallIcon(path), largeIcon(path) - отдельная структура</param>
        void ToolbarCreate(string toolbarName, List<ToolbarButtItem> listButtons);
        void ToolbarRemove(string toolbarName);
         
    }

    public struct ToolbarButtItem
    {
        public int Index;
        public string Name;
        public string HelpString;
        public string Macros;
        public string LargeIcon;
        public string SmallIcon;
    }

}

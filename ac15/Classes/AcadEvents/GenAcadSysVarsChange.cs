using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Acad
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.Windows;
using AdW = Autodesk.Windows;
using acadApp = Autodesk.AutoCAD.ApplicationServices.Application;


namespace TIExCAD.Generic
{
    class AcadSysVarsChange
    {
        public void acadApp_SystemVariableChanged(object sender, SystemVariableChangedEventArgs e)
        {
            //if (e.Name.Equals("WSCURRENT")) BuildRibbonTab();
            // 
        }


    }


}

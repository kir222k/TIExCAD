using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;

[assembly: CommandClass(typeof(TIExCAD.AcadDocReport))]

namespace TIExCAD
{
    public class AcadDocReport
    {
        // ПОЛЯ
        /// <value>doc - ссылка  на активный открытый чертеж AutoCAD</value>
        public Document doc = Application.DocumentManager.MdiActiveDocument;
        // Поле ed - ссылка на Editor активного чертежа
        private Editor ed;

        //[CommandMethod("TIExCADdocreport")]
        public void AcadDocGetReport()
        {
            if (doc != null)
            {
                ed = doc.Editor;

                AcadSendMess AcSM = new AcadSendMess();
                AcSM.SendStringDebugStars(new List<string>
                {
                    //"Шаблон текущего чертежа:",
                    //{doc.},
                    "Полный путь к файлу текущего чертежа:",
                    {doc.Database.Filename.ToString()}
                });

                //ed.Command("_.purge"); // 

                //Application.ShowAlertDialog("OK");
            }
        }


    }
}

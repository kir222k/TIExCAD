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

[assembly: CommandClass(typeof(TIExCAD.TestInACAD))]

namespace TIExCAD
{
    class TestInACAD
    {
        /// <summary>
        /// Проверка метода для отправки сообщений в ком строку AutoCAD 
        /// </summary>
        [CommandMethod ("TIExAcSend")]
        public void TIExAcSend()
        {
            //AcadSendMessExt AcSM = new AcadSendMessExt();
            AcadSendMessExt AcSM = new AcadSendMessExt("TIExAcSend", $"{this}");
            AcSM.SendString("Тест!!!");

        }




    }
}

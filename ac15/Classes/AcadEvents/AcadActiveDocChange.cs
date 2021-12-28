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

    public static class AcadActiveDocChange
    {
        // Определяем делегат
        public delegate void ActiveDocChangeHandler();

        // Событие, кот. доп. активируется в методе-обработчике
        public static event ActiveDocChangeHandler ActiveDocumentChanged;


        // Подключение метода-обработчика для события активации документа (1 вызов из другого метода)
        public static void DocumentChanged_ConnectHandler()
        {
            Application.DocumentManager.DocumentActivated += new DocumentCollectionEventHandler(DocumentChanged_Handler);
        }

        // Сам метод-обработчик
        private static void DocumentChanged_Handler(object sender, DocumentCollectionEventArgs e)
        {
            // событие, можно подписаться и прикрутить свой делегат.
            ActiveDocumentChanged?.Invoke();
        }
    }
}

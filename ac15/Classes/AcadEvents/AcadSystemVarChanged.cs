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
    /// <summary>
    /// Работа с событием изменения сист переменных.
    /// </summary>
    public static class AcadSystemVarChanged
    {
        // СОБЫТИЯ

        /// <summary>
        /// событие изменения сист переменной WSCURRENT, можно подписаться и подключить свой делегат типа Action.
        /// </summary>
        public static event Action AcadSysVarChangeEvent_WSCURRENT;
        /// <summary>
        /// событие изменения сист переменной, любой, кроме тех, на кот уже есть свои события, 
        /// можно подписаться и подключить свой делегат типа EventHandler.
        /// </summary>
        public static event EventHandler AcadSysVarChangeEvent_ANYVAR;

        // МЕТОДЫ

        /// <summary>
        /// Подключение обоработчика к событию изменения системных переменных, 
        /// в т.ч., для переподключения нашей вкладки при смене раб. пр-ва.
        /// </summary>
        public static void AcadSystemVariableChanged_ConnectHandler()
        {
            acadApp.SystemVariableChanged +=
                new SystemVariableChangedEventHandler(AcadSysVarChangedEventHandler);
        }

        // МЕТОДЫ-ОБРАБОТЧИКИ

        /// <summary>
        /// Обработчик события изменения системных переменных.
        /// </summary>
        internal static void AcadSysVarChangedEventHandler(object sender, Autodesk.AutoCAD.ApplicationServices.SystemVariableChangedEventArgs e)
        {
            //  Нужно разобраться, что это.
            string cmdNames =
            (string)Autodesk.AutoCAD.ApplicationServices.Application.
                GetSystemVariable(
                    "CMDNAMES");
            //  Нужно разобраться, что это: 
            // if the QUICKCUI or CUI command is active, returns
            if (cmdNames.ToUpper().IndexOf("QUICKCUI") >= 0 ||
                cmdNames.ToUpper().IndexOf("CUI") >= 0)
                return;

            // Если команды QUICKCUI и CUI не активны (см. выше), то:
            // e.Name  - проверим имя переменной
            switch (e.Name)
            {
                case "WSCURRENT": // название текущего раб. простраства.
                    AcadSysVarChangeEvent_WSCURRENT?.Invoke(); // событие, можно подписаться и прикрутить свой делегат типа Action.
                    break;
                //case 2:
                //    ;
                //    break;
                default:
                    AcadSysVarChangeEvent_ANYVAR?.Invoke(e.Name, null); // событие, можно подписаться и прикрутить свой делегат типа EventHandler.
                    break;
            }
        }

    }

}

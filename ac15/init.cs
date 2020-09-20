//  Файл инициализации библиотеки

using System;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;

// This line is not mandatory, but improves loading performances, чтобы это не значило(!)
[assembly: ExtensionApplication(typeof(TIExCAD.InitSelf))]

namespace TIExCAD
{
    /// <summary>
    /// Запускаемый класс - точка входа.
    /// При загрузке данной dll в AutoCAD выполняется код в ac15.InitSelf.Initialize
    /// </summary>
    public class InitSelf : IExtensionApplication
    {
        /// <summary>
        /// Инициализация.
        /// для запуска своих методов при загрузке dll в acad
        /// через команду _netload дописать здесь свой код 
        /// </summary>
        void IExtensionApplication.Initialize()
        {
            // Сообщение в ком строку AutoCAD
            AcadSendMessDebug AcSM = new AcadSendMessDebug("IExtensionApplication.Initialize", $"{this}");
            AcSM.SendStringDebug("Пока никакой код здесь не выполняется");
            //

        }

        /// <summary>
        /// Метод, выполняемый при выгрузке плагина
        /// в нашем случае, при выгрузке экземляра acad.exe
        /// </summary>
        void IExtensionApplication.Terminate()
        {


        }

    }

}

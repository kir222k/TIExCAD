//  Файл инициализации библиотеки

using System;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using System.Collections.Generic;
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
            AcadSendMess AcSM = new AcadSendMess();
            AcSM.SendStringDebugStars(new List<string> 
            {
                "TIExCAD 2020", 
                "Интерфейсные инструменты для работы с API .NET AutoCAD",
                "Применяется в сборке DDECAD-MZ",
                //"-----",
                //"Для регистрации в реестре (автозагрузка) выполните команду AppCadReg"
            });

            // Регистрация сборки.
            RegTools RegT = new RegTools("TIExCAD");
            RegT.RegisterMyApp();
            // Информация о сборках
            RegT.GetRegistryKeyMyApps();


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

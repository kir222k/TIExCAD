/*! \file Файл инициализации библиотеки
 * \todo подумать, что добавить в автозагрузку dll    
 */
using System;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;

[assembly: ExtensionApplication(typeof(TIExCAD.InitSelf))]

namespace TIExCAD
{
    /// <summary>
    /// \brief Запускаемый класс - точка входа
    /// \details 
    /// </summary>
    public class InitSelf : IExtensionApplication
    {
        /// <summary>
        /// \brief Инициализация
        /// \details для запуска своих методов при загрузке dll в acad
        /// через команду _netload дописать здесь свой код 
        /// </summary>
        void IExtensionApplication.Initialize()
        {
            // MyCommands myC = new MyCommands();

        }

        void IExtensionApplication.Terminate()
        {

        }

    }

}

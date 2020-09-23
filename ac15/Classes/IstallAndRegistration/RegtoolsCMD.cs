using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

using System.Reflection;
using Autodesk.AutoCAD.Runtime;
using AcRt = Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;


[assembly: CommandClass(typeof(TIExCAD.RegtoolsCMD))]

namespace TIExCAD
{

    /// <summary>
    /// Работа с регистрацией приложения.
    /// Разработан для вызова методов работы с реестром по команде из AutoCAD.
    /// </summary>
    public static class RegtoolsCMD
    {
        
        RegTools RegT = new RegTools();

        /// <summary>
        /// Регистрирует сборку (dll файл) приложения в реестре для ее автозапуска при старте AutoCAD.
        /// Команда AppCadReg
        /// </summary>
        [CommandMethod("AppCadReg")]
        public void RegisterMyAppCMD()
        {
            RegT.NameCustomApp = Constantes.ConstNameCustomApp; RegT.PathAssembly = Assembly.GetExecutingAssembly().Location;
            RegT.RegisterMyApp();
        }


        /// <summary>
        /// Отмена регистрации сборки.
        /// Команда AppCadUnReg
        /// </summary>
        [CommandMethod("AppCadUnReg")]
        public void UnregisterMyAppCMD()
        {
            RegT.NameCustomApp = Constantes.ConstNameCustomApp; RegT.PathAssembly = Assembly.GetExecutingAssembly().Location;
            RegT.UnregisterMyApp();
        }

        /// <summary>
        /// Вывод данных о зарег. сборках.
        /// Команда AppCadViewReg
        /// </summary>
        [CommandMethod("AppCadViewReg")]
        public void GetRegistryKeyMyAppsCMD()
        {
            RegT.NameCustomApp = Constantes.ConstNameCustomApp; RegT.PathAssembly = Assembly.GetExecutingAssembly().Location;
            RegT.GetRegistryKeyMyApps();
        }
    }


}

﻿//using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Win32;

using System.Reflection;
using Autodesk.AutoCAD.Runtime;
//using AcRt = Autodesk.AutoCAD.Runtime;
//using Autodesk.AutoCAD.ApplicationServices;
//using Autodesk.AutoCAD.DatabaseServices;

using TIExCAD.Generic;

// Если строку ниже закомментировать, методы [CommandMethod] из AutoCAD недоступны.
[assembly: CommandClass(typeof(TIExCAD.Generic.RegtoolsCMDF))]

namespace TIExCAD.Generic
{

    /// <summary>
    /// Работа с регистрацией приложения.
    /// Разработан для вызова методов работы с реестром по команде из AutoCAD.
    /// Методы информируют о своей работе кратко. В отличие от класса RegtoolsCMD, 
    /// не требуется создание промежуточного класса RegTools. Код оптимизирован!
    /// </summary>
     public class RegtoolsCMDF
    {
        // ПОЛЯ

        string appName;

        // СВОЙСТВА

        public string  AppName 
        {
            set { appName = value; }
        }

        // КОНСТРУКТОРЫ

        RegtoolsCMDF (string appName) { this.appName=appName;}

        /// <summary>
        /// Регистрирует сборку (dll файл) приложения в реестре для ее автозапуска при старте AutoCAD.
        /// Команда AppCadRegF
        /// </summary>
        [CommandMethod("AppCadRegF")]
        public void RegisterMyAppCMD()
        {
            RegGeneric RegGen = new RegGeneric();
            AcadSendMess AcSM = new AcadSendMess();

            //string appName 

            if (RegGen.GetRegisterCustomApp(appName, Assembly.GetExecutingAssembly().Location))
            {
                AcSM.SendStringDebugStars($"Регистрация {appName} выполнена.");
            }
            else
            {
                AcSM.SendStringDebugStars($"Регистрации {appName} не требуется. Запись в реестре существует.");
            }

        }

        /// <summary>
        /// Отмена регистрации сборки.
        /// Команда AppCadUnReg
        /// </summary>
        [CommandMethod("AppCadUnRegF")]
        public void UnregisterMyAppCMD()
        {
            RegGeneric RegGen = new RegGeneric();
            AcadSendMess AcSM = new AcadSendMess();
            //string appName = Constantes.ConstNameCustomApp;

            if (RegGen.GetUnRegisterCustomApp(appName))
            {
                AcSM.SendStringDebugStars($"Регистрация {appName} отменена");
            }
            else
            {
                AcSM.SendStringDebugStars($"Ошибка отмены регистрации {appName}. Запись в реестре отсутствует.");
            }
        }

        //public void UnregisterMyAppCMD(string appName)
        //{
        //    RegGeneric RegGen = new RegGeneric();
        //    AcadSendMess AcSM = new AcadSendMess();
        //    //string appName = Constantes.ConstNameCustomApp;
        //    RegGen.GetUnRegisterCustomApp(appName);
        //}


        /// <summary>
        /// Вывод данных о зарег. сборках.
        /// Команда AppCadViewReg
        /// </summary>
        [CommandMethod("AppCadViewRegF")]
        public void GetRegistryKeyAppsCMD()
        {
            RegGeneric RegGen = new RegGeneric();
            AcadSendMess AcSM = new AcadSendMess();

            List<string> listKeys = RegGen.GetRegisteredApps();
            listKeys.Insert(0, "Зарегистрированные в реестре приложения AutoCAD:");
            listKeys.Insert(1, "------------------------------------------------");

            AcSM.SendStringDebugStars(listKeys);
        }

        [CommandMethod("AppCadUnRegForm")]
        public void UnregisterMyAppForm()
        {
            //string appName;
            //System.Console.WriteLine("Input aplication name for unregistration:");
            //appName = System.Console.ReadLine();
            //UnregisterMyAppCMD(appName);
            AdmReg AR = new AdmReg();
            AR.ShowDialog();
        }
    }

}

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
    /// Регистрирует сборку (dll файл) приложения в реестре для ее автозапуска при старте AutoCAD.
    /// </summary>
    public static class RegtoolsCMD
    {
        /// <value>Имя приложения для регистрации, может быть любое.</value>
        public const string NameCustomApp = "TIExCAD";

        // МЕТОДЫ

        /// <summary>
        /// Регистрация сборки.
        /// </summary>
        /// <remarks>Доступен по команде AppCadReg из AutoCAD.</remarks>
        [CommandMethod("AppCadReg")]
        public static void RegisterMyApp()
        {
            string PathAssembly = Assembly.GetExecutingAssembly().Location;
            RegGeneric RegGen = new RegGeneric();
            bool isReg = RegGen.RegirterCustomApp(NameCustomApp, PathAssembly);
            AcadSendMess AcSM = new AcadSendMess();

            if (isReg == true)
            {
                AcSM.SendStringDebugStars(new List<string> {
                $"Приложение {NameCustomApp} зарегистрированно в реестре" ,
                $"{NameCustomApp} будет автоматически загружаться при последующих запусках AutoCAD",
                "Для отмены регистрации выполните команду AppCadUnreg",
                "Для просмотра зарегистрированных приложений выполните команду AppCadViewReg"});
            }
            else
            {
                AcSM.SendStringDebugStars($"Приложение {NameCustomApp} уже зарегистрированно.");
            }
        }

        /// <summary>
        /// Отмена регистрации сборки.
        /// </summary>
        /// <remarks>Доступен по команде AppCadUnReg из AutoCAD.</remarks>
        //
        [CommandMethod("AppCadUnReg")]
        public static void UnregisterMyApp()
        {
            string PathAssembly = Assembly.GetExecutingAssembly().Location;
            RegGeneric RegGen = new RegGeneric();
            bool isReg = RegGen.UnRegisterCustomApp(NameCustomApp);
            AcadSendMess AcSM = new AcadSendMess();

            if (isReg == true)
            {
                AcSM.SendStringDebugStars(new List<string> {
                $"Регистрация приложения {NameCustomApp} в реестре отменена" ,
                $"Автоматическая загрузка {NameCustomApp} при последующих запусках AutoCAD выполняться не будет",
                "Для повторной регистрации загрузите приложение при помощи команды _.netload и выполните команду AppCadReg",
                "Для просмотра зарегистрированных приложений выполните команду AppCadViewReg"});
            }
            else
            {
                AcSM.SendStringDebugStars(new List<string> {
                    $"Информации о приложении {NameCustomApp} в реестре не найдено",
                    "Отмена регистрации не выполнена."
                });
            }
        }

        /// <summary>
        /// Вывод информации о сборках, кот. зарег. в реестре для автозапуска.
        /// </summary>
        /// <remarks>Доступен по команде AppCadViewReg из AutoCAD.</remarks>
        [CommandMethod("AppCadViewReg")]
        public static  void GetRegistryKeyMyApps()
        {
            List<string> listKeys = new List<string>();

            RegGeneric RegGen = new RegGeneric();
            listKeys = RegGen.GetRegisteredApps();

            listKeys.Insert(0, "Зарегистрированные в реестре приложения AutoCAD:");
            listKeys.Insert(1, "---->");

            // Выведем в ком строку AutoCAD данные по зарег в реестре приложениям под AutoCAD
            TIExCAD.AcadSendMess AcSM = new AcadSendMess();
            AcSM.SendStringDebugStars(listKeys);
        }
    }

}

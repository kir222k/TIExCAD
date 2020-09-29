/* Пример громоздкого исполнения через несколько промеж классов,
 * что в данном случае нецелесообразно.
 * Правильное исполнение см. RegGeneric <- RegToolsCMDF
 */

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

// Если строку ниже закомментировать, методы [CommandMethod] из AutoCAD недоступны.
//[assembly: CommandClass(typeof(TIExCAD.RegtoolsCMD))]

namespace TIExCAD
{
    /// <summary>
    /// Регистрирует сборку (dll файл) приложения в реестре для ее автозапуска при старте AutoCAD.
    /// Разработан для работы с реестром из кода сборки. 
    /// </summary>
    public class RegTools
    {
        // ПОЛЯ

        /// <value>Имя приложения для регистрации, может быть любое.</value>
        public string NameCustomApp { get; set; }
        /// <value>Путь к dll приложения. Нужно указывать путь именно к той dll, кот. должна быть в автозапуске AutoCAD.</value>
        public string PathAssembly { get; set; }

        // КОНСТРУКТОРЫ

        /// <summary>
        /// Регистрирует сборку (dll файл) приложения в реестре для ее автозапуска при старте AutoCAD.
        /// </summary>
        /// <remarks>После создания экземпляра необходимо задать иям приложения и путь регистрации.</remarks>
        public RegTools() { }

        /// <summary>
        /// Регистрирует сборку (dll файл) приложения в реестре для ее автозапуска при старте AutoCAD.
        /// </summary>
        /// <remarks>Путь к сборке задется как путь к dll, которая содержит класс RegTools</remarks>
        /// <param name="nameCustomApp">Имя приложения для регистрации, может быть любое.</param>
        public RegTools(string nameCustomApp) { NameCustomApp = nameCustomApp; PathAssembly = Assembly.GetExecutingAssembly().Location; }

        /// <summary>
        /// Регистрирует сборку (dll файл) приложения в реестре для ее автозапуска при старте AutoCAD.
        /// </summary>
        /// <param name="nameCustomApp">Имя приложения для регистрации, может быть любое.</param>
        /// <param name="pathAssembly">Путь к dll приложения. Нужно указывать путь именно к той dll, кот. должна быть в автозапуске AutoCAD.</param>
        public RegTools(string nameCustomApp, string pathAssembly) { NameCustomApp = nameCustomApp; PathAssembly = pathAssembly; }

        // МЕТОДЫ

        /// <summary>
        /// Регистрация сборки.
        /// </summary>
        public void RegisterMyApp()
        {
            if (NameCustomApp != null && PathAssembly != null)
            {

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

                    AcSM.SendStringDebugStars("Приложение работает в следующих релизах AutoCAD:");
                    AcSM.SendStringDebugStars(StaticObjAcadRev.GetListAcadReleaseVersions());
                }
                else
                {
                    AcSM.SendStringDebugStars($"Приложение {NameCustomApp} уже зарегистрированно.");
                }
            }
        }

        /// <summary>
        /// Отмена регистрации сборки.
        /// </summary>
        public void UnregisterMyApp()
        {
            // Проверим, задано ли поле NameCustomApp
            if (NameCustomApp != null)
            {
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
        }

        /// <summary>
        /// Вывод информации о сборках, кот. зарег. в реестре для автозапуска.
        /// </summary>
        /// <remarks>Доступен по команде AppCadViewReg из AutoCAD.</remarks>
        //
        public void GetRegistryKeyMyApps()
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

    /// <summary>
    /// Работа с регистрацией приложения.
    /// Разработан для вызова методов работы с реестром по команде из AutoCAD.
    /// </summary>
    public class RegtoolsCMD
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

//  Файл инициализации библиотеки

// ПОЛЯ
// СВОЙЙСТВА
// КОНСТРУКТОРЫ
// МЕТОДЫ


using System;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using System.Reflection;
using Autodesk.Windows;
using acadApp = Autodesk.AutoCAD.ApplicationServices.Application;



// This line is not mandatory, but improves loading performances, чтобы это не значило(!)
[assembly: ExtensionApplication(typeof(TIExCAD.InitSelf))]

namespace TIExCAD
{
    /// <summary>
    /// Запускаемый класс - точка входа.
    /// При загрузке данной dll в AutoCAD выполняется код в методе IExtensionApplication.Initialize()
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
            // Вывод данных о приложении в ком строку AutoCAD
            InitThis.InitOne();

            // Загрузка интерфейса
            InitThis.LoadUserInterface();

        }

        /// <summary>
        /// Метод, выполняемый при выгрузке плагина
        /// в нашем случае, при выгрузке экземляра acad.exe
        /// </summary>
        void IExtensionApplication.Terminate()
        {


        }


        /*
        public void ComponentManager_ItemInitialized(object sender, Autodesk.Windows.RibbonItemEventArgs e)
        {
            // Проверяем, что лента загружена
            if (Autodesk.Windows.ComponentManager.Ribbon != null)
            {
                // Строим нашу вкладку
                BuildRibbonTab();
                //и раз уж лента запустилась, то отключаем обработчик событий
                Autodesk.Windows.ComponentManager.ItemInitialized -=
                    new EventHandler<RibbonItemEventArgs>(ComponentManager_ItemInitialized);
            }
        }
        // Построение вкладки
        public void BuildRibbonTab()
        {
            ExampleRibbon ExRib = new ExampleRibbon();

            // Если лента еще не загружена
            if (!ExRib.isLoaded())
            {
                // Строим вкладку
                ExRib.CreateRibbonTab();
                // Подключаем обработчик событий изменения системных переменных
                //acadApp.SystemVariableChanged += 
                //    new SystemVariableChangedEventHandler(acadApp_SystemVariableChanged);
            }
        }
        */

    }


    internal static class InitThis
    {
        internal static void InitOne()
        {
            // Сообщение в ком строку AutoCAD
            AcadSendMess AcSM = new AcadSendMess();
            AcSM.SendStringDebugStars(new List<string>
            {
                "TIExCAD - Загружено",
                "С# 8.0, VS2019, API .NET AutoCAD",
                "Применяется в сборке DDECAD-MZ",
            });

            // Регистрация сборок в автозагрузке AutoCAD.
            RegtoolsCMDF RegCMD = new RegtoolsCMDF();

            // Проверка регистрации сборки в автозагрузке AutoCAD.
            RegGeneric RegGen = new RegGeneric();
            // Вызывается регистрация сборки: 
            if (RegGen.GetRegirterCustomApp(Constantes.ConstNameCustomApp,
                Assembly.GetExecutingAssembly().Location)) // true
                                                           // если регистрация прошла успешно, то уведомляем
            {
                AcSM.SendStringDebugStars("Приложение зарегистрировано. " +
                    "\nПри следуюющем запуске AutoCAD будет загружно автоматически!");
                // выведем список зарег приложений, кот в автозагрузке AutoCAD.
                RegCMD.GetRegistryKeyAppsCMD();

            }
            // Иначе ничего не делаем, т.к. наше приложение уже есть в автозагрузке AutoCAD.
        }

        internal static  void LoadUserInterface()
        {

            // если файла usercadr.ini нет в папке /sys, то загрузка в соотв. с настройками cadr.ini (кот. исп. при инсталяции)
            // usercadr.ini создается при первой запуске окна настроек, или при "сбросить" в онке настроек (заново создается)

            // Лента
            ////ExampleRibbon ExRib = new ExampleRibbon();

            //////AcadSendMess AcSM = new AcadSendMess();
            //////AcSM.SendStringDebugStars("Загрузить вкладку на ленту можно командой RibCreate");
            ////ExRib.CreateRibbonTab();

            //ExRib.ComponentManager_ItemInitialized()

            // Меню

            // Другие элементы интерфейса
        }

    }
}

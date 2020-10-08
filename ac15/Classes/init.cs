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

using TIExCAD.Generic;



// This line is not mandatory, but improves loading performances, чтобы это не значило(!)
[assembly: ExtensionApplication(typeof(TIExCAD.InitSelf))]

namespace TIExCAD
{
    /// <summary>
    /// Запускаемый класс - точка входа.
    /// При загрузке данной dll в AutoCAD выполняется код в методе IExtensionApplication.Initialize()
    /// </summary>
    internal  class InitSelf : IExtensionApplication
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
            // Подключение обработчиков основных событий.
            InitThis.BasicEventHadlerlersConnect();
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

        /// <summary>
        /// Дествия при загрузки сборки.
        /// </summary>
        /// <remarks>Подключение обработчиков основных событий, 
        /// Загрузка интерфейса пользователя,
        /// чтение ini-файлов, выполнение затем каких-то настроек и др.</remarks>
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
                if (RegGen.GetRegisterCustomApp(Constantes.ConstNameCustomApp,
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

            /// <summary>
            /// Подключение обработчиков основных событий.
            /// </summary>
            internal static void BasicEventHadlerlersConnect()
            {
                // 
                // Подключим автосоздание вкладки ленты.
                AcadComponentManagerInit.AcadComponentManagerInit_ConnectHandler();

                // ИЗМЕНЕНИЯ СИСТЕМНЫХ ПЕРЕМЕННЫХ
                // Подключим пересоздание вкладки ленты.
                // В случае вкладки ленты, отслеживается переменная WSCURRENT.
                AcadSystemVarChanged.AcadSystemVariableChanged_ConnectHandler();


            }

            internal static void LoadUserInterface()
            {

                // если файла usercadr.ini нет в папке /sys, то загрузка в соотв. с настройками cadr.ini (кот. исп. при инсталяции)
                // usercadr.ini создается при первой запуске окна настроек, или при "сбросить" в онке настроек (заново создается)

                #region ЛЕНТА
                // Загрузка выполняется в методе 
                // AcadComponentManager_ItemInitialized
                // Перезагрузка при смене раб. пр. - в методе
                // AcadSysVarChangedEvHr_WSCURRENT
                #endregion


                // Меню

                // Другие элементы интерфейса
            }

        }
    }
}

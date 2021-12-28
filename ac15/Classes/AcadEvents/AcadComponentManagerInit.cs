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
    /// Подключение обработчика для автозагрузки вкладки н ленту при загрузке сборки в AutoCAD.
    /// </summary>
    /// <remarks>1. Подписаться на событие RibbonInitializedEvent - подключить делегат типа Action к этому событию.
    /// 2. вызвать метод AcadComponentManagerInit.AcadComponentManagerInit_ConnectHandler</remarks>
    public static  class AcadComponentManagerInit
    {
        // СОБЫТИЯ

        /// <summary>
        /// Объявление события "Можно загружать вкладку ленты", на кот. может подписаться делегат типа Action,
        /// т.е. делегат, методы кот. не имеют  арггументов и кот. ничего не возращает.
        /// </summary>
        public static event Action RibbonInitializedEvent;


        /// <summary>
        /// Подключение обоработчика к событию создания ленты, для автоподключения нашей вкладки
        /// </summary>
        public static void AcadComponentManagerInit_ConnectHandler()
        {
            Autodesk.Windows.ComponentManager.ItemInitialized +=
              new EventHandler<RibbonItemEventArgs>(AcadComponentManager_ItemInitialized);

            
        }


        // ОБРАБОТЧИКИ

        /// <summary>
        /// Автосоздание вкладки ленты.
        /// </summary>
        internal static void AcadComponentManager_ItemInitialized(object sender, Autodesk.Windows.RibbonItemEventArgs e)
        {
            // Создать и загрузить вкладку
            // создадим событие, при подписке на которое создается и загружается вкладка.
            RibbonInitializedEvent?.Invoke();


            // Отключить обработчик загрузки ленты, т.к. он вызвается
            // только 1 раз при инициализации DLL.
            ComponentManager.ItemInitialized -=
                new EventHandler<RibbonItemEventArgs>(AcadComponentManager_ItemInitialized);
        }
    }

}

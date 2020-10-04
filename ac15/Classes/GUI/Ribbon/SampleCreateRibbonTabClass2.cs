using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

using TIExCAD.Generic.Ribbon;
// Acad
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.Windows;
using AdW = Autodesk.Windows;
using acadApp = Autodesk.AutoCAD.ApplicationServices.Application;

[assembly: CommandClass(typeof(TIExCAD.SampleCreateRibbonTabClass2))]

namespace TIExCAD

{


    /// <summary>
    /// Пример создания вкладки на ленте с кнопками
    /// </summary>
    public partial class SampleCreateRibbonTabClass2
    {
        /// <summary>
        /// Создание вкладки на ленте. Тестовый пример.
        /// </summary>
        [CommandMethod("TiexTestRibCreate2")]
        public void TiexTestRibCreate()
        {

            // Класс для создания вкладки на ленте, содержит методы создания кннопок, панелей, вкладок ленты
            RibbonCreateEasy RibCrEsy = new RibbonCreateEasy();

            string ribbonTabTitle = "TIExCAD";
            string ribbonTabID = "tiexid";

            if (!RibCrEsy.GetIsRibbonLoaded(ribbonTabTitle, ribbonTabID)) // если вкладки еще нет
            {


                // Экз делегата для события кнопки
                DelegateRibButtonHandler delegateRibBtnMetod = new DelegateRibButtonHandler(ShowAdminWindow);

                // Получаем доступ к ленте
                RibbonControl RibAcControl = Autodesk.Windows.ComponentManager.Ribbon;


                #region КНОПКА 1
                // Класс для создания кнопки.
                RibbonButton RbB1 = new RibbonButton();

                // Вызываем метод, создающий кнопку.
                RbB1 = RibCrEsy.GetRibButton("Новая кнопка", true, RibbonItemSize.Large, Orientation.Vertical, delegateRibBtnMetod, true);

                // Привязка метода к экз делегата
                delegateRibBtnMetod = new DelegateRibButtonHandler(ShowAdminWindow);

                // Класс для привязки метода на кнопку 1
                RibBtnHdlrDel RibBH1 = new RibBtnHdlrDel(delegateRibBtnMetod);
                // Привяжем.
                RbB1.CommandHandler = RibBH1;
                #endregion

                #region КНОПКА 2
                // Класс для создания кнопки.
                RibbonButton RbB2 = new RibbonButton();
                RibCrEsy.PathImgFolder = "u:/dev/TIExCAD/distr/png/";
                // Метод, создающий кнопку.
                RbB2 = RibCrEsy.GetRibButton("Вторая кнопка", true, RibbonItemSize.Large, Orientation.Vertical,
                    delegateRibBtnMetod,
                    true, "image_02_32.png", "image_02_16.png");
                /*
                // Класс для привязки метода на кнопку 2.
                RibBtnHdlrDel RibBH2 = new RibBtnHdlrDel(delegateRibBtnMetod);
                // Привяжем.
                RbB2.CommandHandler = RibBH2; */
                #endregion


                #region ПАНЕЛЬ ДЛЯ КНОПОК
                RibbonPanel RbPan = RibCrEsy.GetRibPanel("Панель для кнопок");
                // Вставим кнопки в панель.
                RbPan.Source.Items.Add(RbB1);
                RbPan.Source.Items.Add(RbB2);  //RbPan.Source.Items.Add(RbB1); RbPan.Source.Items.Add(RbB2);
                #endregion

                #region ВКЛАДКА НА ЛЕНТЕ
                RibbonTab RbTab = RibCrEsy.GetRibTab(ribbonTabTitle, ribbonTabID);
                // Вставим панель во вкладку.
                RbTab.Panels.Add(RbPan);
                #endregion

                // Добавляем вкладку в ленту
                RibAcControl.Tabs.Add(RbTab);

                // сделаем кнопки на всю панель
                RbB1.Height = RbPan.RibbonControl.ActualHeight;
                RbB2.Height = RbPan.RibbonControl.ActualHeight;
                RbB1.MinWidth = 50;
                RbB2.MinWidth = 50;

                RibAcControl.UpdateLayout();
            }
        }
    }

    public partial class SampleCreateRibbonTabClass2
    {
        [CommandMethod("TiexTestRibCreate3")]
        public void TiexTestRibCreate3()
        {
            List<RibButtonMyShort> listBtn = new List<RibButtonMyShort>();

            RibButtonMyShort btn1 = new RibButtonMyShort();
            btn1.ribButtonText = "Кнопка1"; btn1.ribButtonSize = RibbonItemSize.Large; btn1.delegateRibBtnEv = GetStaticInfo.SendMessToAcad;
            listBtn.Add(btn1);

            listBtn.Add(new RibButtonMyShort()
            {
                ribButtonText = "Кнопка2",
                ribButtonSize = RibbonItemSize.Large,
                delegateRibBtnEv = GetStaticInfo.SendMessToAcad
            }) ;

            listBtn.Add(new RibButtonMyShort()
            {
                ribButtonText = "Кнопка3",
                ribButtonSize = RibbonItemSize.Large,
                delegateRibBtnEv = GetStaticInfo.SendMessToAcad
            });



            //TIExCAD.Generic.Ribbon.CreateRibTabSpeed CrTabSpeed =
            //    new CreateRibTabSpeed("TIExCAD-2", "tiexcad2", listBtn);

            //CrTabSpeed.CreateOrModifityRibbonTab();

        }

    }


    public partial class SampleCreateRibbonTabClass2
    {
        internal void ShowAdminWindow()
        {

            AcadSendMess AcSM = new AcadSendMess();
            AcSM.SendStringDebugStars(new List<string> { "Обработчик", "Кнопка на ленте" });
        }
    }

    public static class GetStaticInfo
    {
        public static void SendMessToAcad ()
        {

            AcadSendMess AcSM = new AcadSendMess();
            AcSM.SendStringDebugStars(new List<string> {"Метод привязанный к делегату", "delegateRibBtnEv" });
        }
    }
}

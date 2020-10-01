using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Controls;


// Acad
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.Windows;
using AdW = Autodesk.Windows;
using acadApp = Autodesk.AutoCAD.ApplicationServices.Application;

[assembly: CommandClass(typeof(TIExCAD.SampleCreateRibbonTabClass2))]

namespace TIExCAD

{
    public delegate void DelegateRibBtnEvents();

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
            // Экз делегата для события кнопки
            DelegateRibBtnEvents RibBtnMetod = new DelegateRibBtnEvents(ShowAdminWindow);

            // Получаем доступ к ленте
            RibbonControl RibAcControl = Autodesk.Windows.ComponentManager.Ribbon;

            // Класс для создания вкладки на ленте, содержит методы создания кннопок, панелей, вкладок ленты
            TIExCAD.RibbonCreateEasy RibCrEsy = new RibbonCreateEasy();


            #region КНОПКА 1
            // Класс для создания кнопки.
            RibbonButton RbB1 = new RibbonButton();

            // Метод, создающий кнопку.
            RbB1 = RibCrEsy.GetRibButton("Новая кнопка", true, RibbonItemSize.Large, Orientation.Vertical, true);

            // Класс для привязки метода на кнопку 1
            RibBtnHdlrDel RibBH1 = new RibBtnHdlrDel(RibBtnMetod);
            // Привяжем.
            RbB1.CommandHandler = RibBH1;
            #endregion

            #region КНОПКА 2
            // Класс для создания кнопки.
            RibbonButton RbB2 = new RibbonButton();
            RibCrEsy.PathImgFolder = "u:/dev/TIExCAD/distr/png/";
            // Метод, создающий кнопку.
            RbB2 = RibCrEsy.GetRibButton("Вторая кнопка", true, RibbonItemSize.Large, Orientation.Vertical,
                true, "image_02_32.png", "image_02_16.png");
            // Класс для привязки метода на кнопку 2.
            RibBtnHdlrDel RibBH2 = new RibBtnHdlrDel(RibBtnMetod);
            // Привяжем.
            RbB2.CommandHandler = RibBH2;
            #endregion


            #region ПАНЕЛЬ ДЛЯ КНОПОК
            RibbonPanel RbPan = RibCrEsy.GetRibPanel("Панель для кнопок");
            // Вставим кнопки в панель.
            RbPan.Source.Items.Add(RbB1);
            RbPan.Source.Items.Add(RbB2);  //RbPan.Source.Items.Add(RbB1); RbPan.Source.Items.Add(RbB2);
            #endregion

            #region ВКЛАДКА НА ЛЕНТЕ
            RibbonTab RbTab = RibCrEsy.GetRibTab("Заголовок панели", "panID");
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

    public partial class SampleCreateRibbonTabClass2
    {
        internal void ShowAdminWindow()
        {
            AdminAcadCmd AdmWindow = new AdminAcadCmd();
            AdmWindow.ShowDialog();
        }
    }
    public class RibBtnHdlrDel : TIExCAD.RibButtonHandler
    {
        private DelegateRibBtnEvents DelegateRibBtnEv;
        public RibBtnHdlrDel() { }
        public RibBtnHdlrDel(DelegateRibBtnEvents delegateRibBtnEv)
        {
            DelegateRibBtnEv = delegateRibBtnEv;
        }
        public override void Execute(object parameter)
        {
            DelegateRibBtnEv();
        }
    }

}

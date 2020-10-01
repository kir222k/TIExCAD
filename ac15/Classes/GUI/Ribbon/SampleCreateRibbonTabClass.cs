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

[assembly: CommandClass(typeof(TIExCAD.SampleCreateRibbonTabClass))]

namespace TIExCAD
{
    /// <summary>
    /// Пример создания вкладки на ленте с кнопками
    /// </summary>
    class SampleCreateRibbonTabClass
    {
        /// <summary>
        /// Создание вкладки на ленте. Тестовый пример.
        /// </summary>
        [CommandMethod("TiexTestRibCreate")]
        public void TiexTestRibCreate()
        {
            // Получаем доступ к ленте
            RibbonControl RibAcControl = Autodesk.Windows.ComponentManager.Ribbon;

            // Класс для создания вкладки на ленте, содержит методы создания кннопок, панелей, вкладок ленты
            TIExCAD.RibbonCreateEasy RibCrEsy = new RibbonCreateEasy();


            #region КНОПКА 1, с картинками
            // Класс для создания кнопки.
            RibbonButton RbB1 = new RibbonButton();
            // Uri uri1 = new Uri("C:/test/path/file.txt") // Implicit file path.
            // Если файл рисунка лежит рядом с dll: Assembly.GetExecutingAssembly().Location.
            // или
            // ribButtonBig.LargeImage = LoadImage(My.Resources.question) - но нужен еще метод https://adndevblog.typepad.com/autocad/2012/05/arrange-ribbon-buttons-into-columns.html
            // Метод, создающий кнопку.
            RbB1 = RibCrEsy.GetRibButton("ОДИН ОДИНОДИН ОДИН ОДИН ", true, true,
                new BitmapImage(new Uri("u:/dev/TIExCAD/distr/lib/ac15/icon_32.png")), // путь к большой картинке
                new BitmapImage(new Uri("u:/dev/TIExCAD/distr/lib/ac15/icon_16.png")),  // путь к мал. картинке
                RibbonItemSize.Large, // большая кнопка
                Orientation.Vertical
                );
            // Класс для привязки метода на кнопку 1
            RibBtnHdlr1 RibBH1 = new RibBtnHdlr1();
            // Привяжем.
            RbB1.CommandHandler = RibBH1;
            #endregion

            #region КНОПКА 2, только текст
            // Класс для создания кнопки.
            RibbonButton RbB2 = new RibbonButton();
            // Метод, создающий кнопку.
            RbB2 = RibCrEsy.GetRibButton("ДВА", RibbonItemSize.Large, Orientation.Horizontal);
            // Класс для привязки метода на кнопку 2.
            RibBtnHdlr2 RibBH2 = new RibBtnHdlr2();
            // Привяжем.
            RbB2.CommandHandler = RibBH2;
            #endregion


            #region ПАНЕЛЬ ДЛЯ КНОПОК
            RibbonPanel RbPan = RibCrEsy.GetRibPanel("Панель для кнопок");
            // Вставим кнопки в панель.
            RbPan.Source.Items.Add(RbB1);
            RbPan.Source.Items.Add(RbB2);  RbPan.Source.Items.Add(RbB1); RbPan.Source.Items.Add(RbB2);
            #endregion

            #region ВКЛАДКА НА ЛЕНТЕ
            RibbonTab RbTab = RibCrEsy.GetRibTab("Заголовок паанели", "panID");
            // Вставим панель во вкладку.
            RbTab.Panels.Add(RbPan);
            #endregion

            // Добавляем вкладку в ленту
            RibAcControl.Tabs.Add(RbTab);

            // сделаем кнопки на всю панель
            RbB1.Height = RbB2.Height = RbPan.RibbonControl.ActualHeight;
            RbB1.MinWidth = RbB2.MinWidth = 50; //RbPan.RibbonControl.ActualWidth / 2;

            //RbB1.ResizeStyle = RibbonItemResizeStyles.ChangeSize;
            RibAcControl.UpdateLayout();


            AcadSendMess AcSM = new AcadSendMess();
            AcSM.SendStringDebugStars(new List<string> {
                "Ribbon Panel ActualWidth = "   ,
                $"{RbPan.RibbonControl.ActualWidth}",
                "Ribbon Panel Width = ",
                $"{RbPan.RibbonControl.Width}"
            });

        }
    }

    public class RibBtnHdlr1 : TIExCAD.RibButtonHandler
    {
        public override void Execute(object parameter)
        {
            Document doc = acadApp.DocumentManager.MdiActiveDocument;
            if (parameter is RibbonButton)
            {
                RibbonButton button = parameter as RibbonButton;
                doc.Editor.WriteMessage(
                  "\nМетод на кнопку 1: " + button.Text + " переопределен\n");
            }
        }
    }

    public class RibBtnHdlr2 : TIExCAD.RibButtonHandler
    {
        public override void Execute(object parameter)
        {
            Document doc = acadApp.DocumentManager.MdiActiveDocument;
            if (parameter is RibbonButton)
            {
                RibbonButton button = parameter as RibbonButton;
                doc.Editor.WriteMessage(
                  "\nМетод на кнопку 2: " + button.Text + " переопределен\n");
            }
        }
    }

}

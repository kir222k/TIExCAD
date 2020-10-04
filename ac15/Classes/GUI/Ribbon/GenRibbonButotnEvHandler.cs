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

namespace TIExCAD.Generic.Ribbon
{
    /* Объявление делегата, для использования в привязке  методов к кнопкам на ленте. 
     * 
     * В методе, где настраивается кнопка: 
     * 
     * 1) создать экз DelegateRibButtonHandler, для каждой кнопки:
     *             DelegateRibButtonHandler RibBtnMetod;
     * 2) подключить к экз DelegateRibButtonHandler методы:
     *             RibBtnMetod = new DelegateRibButtonHandler(ShowAdminWindow);
     * 3) создать экз RibBtnHdlrDel (см. ниже), аргумент - экз. DelegateRibButtonHandler:
     *             RibBtnHdlrDel RibBH2 = new RibBtnHdlrDel(RibBtnMetod);
     * 4) привязать экз RibBtnHdlrDel к свойству CommandHandler кнопки:
     *             RbB1.CommandHandler = RibBH1;
     * */
    public delegate void DelegateRibButtonHandler();

    /// <summary>
    /// Подключение 
    /// </summary>
    internal partial class RibbonGenericEvents
    {
    }



    /// <summary>
    /// Содержит переопределяемый метод, кот. выполняется при нажатии кнопки на ленте.
    /// Экз. данного класса (или его наследника) присваивается свойству CommandHandler кнопки ленты.
    /// Для использования в основном коде, чтобы использовать свои методы как обработчики, 
    /// нужно создать класс-наследник от RibButtonHandler, переопределить метод Execute
    /// </summary>
    public class RibButtonHandler : System.Windows.Input.ICommand
    {
        /// <summary>
        /// Определен в интерфейсе  System.Windows.Input.ICommand, не используется.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }
        /// <summary>
        /// Событие на нажатие кнопки
        /// </summary>
        public event EventHandler CanExecuteChanged;

        public virtual void Execute(object parameter)
        {
            Document doc = acadApp.DocumentManager.MdiActiveDocument;

            if (parameter is RibbonButton)
            {
                RibbonButton button = parameter as RibbonButton;
                doc.Editor.WriteMessage(
                  "\nRibbonButton Executed: " + button.Text + "\n");
            }
        }
    }

    /// <summary>
    /// К нажатию кнопки привызывается свой метод
    /// </summary>
    public class RibBtnHdlrDel : RibButtonHandler
    {
        private DelegateRibButtonHandler DelegateRibBtnEv;
        public RibBtnHdlrDel() { }
        public RibBtnHdlrDel(DelegateRibButtonHandler delegateRibBtnEv)
        {
            DelegateRibBtnEv = delegateRibBtnEv;
        }
        public override void Execute(object parameter)
        {
            DelegateRibBtnEv();
        }
    }


}

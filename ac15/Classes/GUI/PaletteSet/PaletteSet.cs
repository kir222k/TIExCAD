
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Windows;
using System.Drawing;
//using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
//using AcadApp = Autodesk.AutoCAD.ApplicationServices.Application;
using Autodesk.AutoCAD.DatabaseServices;
//using KR.CAD.NET.DDECAD.MZ;
//using acad = Autodesk.AutoCAD;
//using System.Windows;
using System.Runtime.Remoting;
using System.Runtime.InteropServices;



namespace TIExCAD.Generic
{

    /// <summary>
    /// Создание, настройка и отображение палитры.
    /// </summary>
    public class CustomPaletteSetAcad : IPaletteSetCustom
    {
        #region ПОЛЯ
        private PaletteSet paletteSetAcad;
        private string paletteSetAcadName;
        private Guid paletteSetGuid;
        private readonly UserControl paletteControl;
        private string paletteControlName;
        private int widthPaletteSet;
        private int heigthPaletteSet;
        #endregion

        #region СВОЙСТВА
        // Имя палитры
        /// <inheritdoc/>
        public string PaletteSetAcadName { get => paletteSetAcadName; set => paletteSetAcadName = value; }
        // GUID  Палитры
        /// <inheritdoc/>
        public Guid PaletteSetGuid { get => paletteSetGuid; set => paletteSetGuid = value; }
        // Имя контрола палитры
        /// <inheritdoc/>
        public string PalettteControlName { get => paletteControlName; set => paletteControlName = value; }
        #endregion

        
        /// <summary>
        /// Создает или открывает скрытую палитру.
        /// </summary>
        /// <param name="paletteSetAcadName">Название палитры (заголовок)</param>
        /// <param name="paletteSetGuid">идентификатор</param>
        /// <param name="widthPaletteSet">Ширина палитры</param>
        /// <param name="heigthPaletteSet">Высота палитры</param>
        /// <param name="paletteControl">контрол, кот. вставлен в палитру</param>
        /// <param name="paletteControlName">имя контрола в палитре</param>
        public CustomPaletteSetAcad( string paletteSetAcadName, Guid paletteSetGuid, WidthPaletteSet widthPaletteSet, HeigthPaletteSet heigthPaletteSet,  UserControl paletteControl, string paletteControlName) 
        {
           // this.paletteSetAcad = paletteSetAcad;
            this.paletteSetAcadName = paletteSetAcadName;
            this.paletteSetGuid = paletteSetGuid;
            this.paletteControl = paletteControl;
            this.paletteControlName = paletteControlName;
            this.widthPaletteSet = (int)widthPaletteSet;
            this.heigthPaletteSet = (int)heigthPaletteSet;
        }

        /// <inheritdoc/>
        public void PaletteSetCreate()
        {
            //throw new NotImplementedException("Create PaletteSet");

            //AcadSendMess AcSM = new AcadSendMess();
            //AcSM.SendStringDebugStars(new List<string> { "Create PaletteSet" });

            if (paletteSetAcad == null)
            {
                paletteSetAcad = new PaletteSet(PaletteSetAcadName, paletteSetGuid);

                // вставка экз. контрола в палитру
                ElementHost host = new ElementHost
                {
                    //// настройка какого-то объекта
                    AutoSize = true,
                    Dock = System.Windows.Forms.DockStyle.Fill,
                    //// вставка в какой-то объект нашего контрола 
                    Child = paletteControl
                }; //  какой-то объект

                // Теперь свставим host в палитру.
                // добавление какого-то объекта с нашим контролом в палитру
                paletteSetAcad.Add(paletteControlName, host);
                PaletteSetSetting(ref paletteSetAcad, widthPaletteSet, heigthPaletteSet);

            }

            PaletteSetShow(ref paletteSetAcad);
            
        }
        
        // Настроить палитру.
        /// <inheritdoc/>
        public void PaletteSetSetting(ref PaletteSet paletteAc, int widthPaletteSet, int heigthPaletteSet)
        {
            //throw new NotImplementedException("Setting PaletteSet");
            paletteAc
                .DockEnabled = (DockSides)((int)DockSides.Left + (int)DockSides.Right);
            paletteAc
                .RolledUp = true;
            paletteAc
                .Style = PaletteSetStyles.NoTitleBar |
                PaletteSetStyles.ShowCloseButton |
                PaletteSetStyles.ShowPropertiesMenu |
                PaletteSetStyles.ShowAutoHideButton;

            // объект размера
            Size PaletteSetSize = new Size { Width = (int)WidthPaletteSet.WidthMin - 2, Height = (int)HeigthPaletteSet.HeightMin - 2 };
            Size PaletteSetSizeMin = new Size { Width = (int)WidthPaletteSet.WidthMin, Height = (int)HeigthPaletteSet.HeightMin };
            // передача размеров палитре
            //paletteAc.MinimumSize = new System.Drawing.Size(413,252);
            paletteAc.MinimumSize = PaletteSetSizeMin;
            paletteAc.SetSize(PaletteSetSize);

            //paletteAc.SizeChanged += PaletteAc_SizeChanged;
        }

        //private void PaletteAc_SizeChanged(object sender, PaletteSetSizeEventArgs e)
        //{
        //    //throw new NotImplementedException();
        //    if ((paletteSetAcad.Size.Width < paletteSetAcad.MinimumSize.Width) | (paletteSetAcad.Size.Height < paletteSetAcad.MinimumSize.Height))
        //    {
        //        Size PaletteSetSizeMin = new Size { Width = (int)WidthPaletteSet.WidthMin, Height = (int)HeigthPaletteSet.HeightMin };
        //        paletteSetAcad.SetSize (PaletteSetSizeMin);
        //    }
        //}

        /// <inheritdoc/>
        public void PaletteSetSetting()
        {
            throw new NotImplementedException();
        }


        // Показать палитру.
        /// <inheritdoc/>
        public void PaletteSetShow(ref PaletteSet paletteAc)
        {
            if (paletteAc != null )
                paletteAc.Visible = true;
        }
        /// <inheritdoc/>
        public void PaletteSetShow()
        {
            throw new NotImplementedException();
        }

    }
}

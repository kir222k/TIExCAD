using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIExCAD.Generic
{
    /// <summary>
    /// Создание, настройка и отображение палитры.
    /// </summary>
    interface IPaletteSetCustom
    {
        /// <summary>
        /// Имя палитры.
        /// </summary>
        string PaletteSetAcadName { get; }
        /// <summary>
        /// GUD палитры.
        /// </summary>
        System.Guid PaletteSetGuid { get; }
        
        /// <summary>
        /// Имя внедряемого в палитру контрола.
        /// </summary>
        string PalettteControlName { get; }
        /// <summary>
        /// Создание палитры.
        /// </summary>
        void PaletteSetCreate();
        /// <summary>
        /// Настройка палитры.
        /// </summary>
        void PaletteSetSetting();
        /// <summary>
        /// Отображение палитры.
        /// </summary>
        void PaletteSetShow();

       // void PaletteSetResize();

    }

}

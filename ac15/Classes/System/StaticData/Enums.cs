
namespace TIExCAD.Generic
{
    /// <summary>
    /// Дни недели.
    /// </summary>
     public enum  WeekDays : int
    {
        /// <summary>
        /// Понедельник
        /// </summary>
        Monday =  1,
        /// <summary>
        /// Вторник
        /// </summary>
        Tuesday = 2,
        /// <summary>
        /// Среда
        /// </summary>
        Wednesday=3,
        /// <summary>
        /// Четверг
        /// </summary>
        Thursday=4,
        /// <summary>
        /// Пятница
        /// </summary>
        Friday=5,
        /// <summary>
        /// Суббота
        /// </summary>
        Saturday=6,
        /// <summary>
        /// Воскресенье
        /// </summary>
        Sunday=7
    }

    // Size sz = new Size { Width = 310, Height = 500 };

    /// <summary>
    /// Ширина контрола палитры
    /// </summary>
public enum WidthPaletteSet : int
    {
        /// <summary>
        /// Мин. ширина
        /// </summary>
        WidthMin = 406,
        /// <summary>
        /// Средняя ширина
        /// </summary>
        WidthMiddle = 600,
        /// <summary>
        /// Макс. ширина
        /// </summary>
        WidthBig = 800,
    }

    /// <summary>
    /// Высота контрола палитры
    /// </summary>
    public enum HeigthPaletteSet : int
    {
        /// <summary>
        /// Мин. высота
        /// </summary>
        HeightMin = 256,
        /// <summary>
        /// Средняя высота
        /// </summary>
        HeightMiddle = 800,
        /// <summary>
        /// Макс. высота
        /// </summary>
        HeightBig = 1000,

    }
}

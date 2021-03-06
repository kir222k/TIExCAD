
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

    //             Size sz = new Size { Width = 310, Height = 500 };

public enum WidthPaletteSet : int
    {
        WidthMin = 400,
        WidthMiddle = 600,
        WidthBig = 800,

        //HeightMin = 600,
        //HeightMiddle = 800,
        //HeightBig = 1000,

    }

    public enum HeigthPaletteSet : int
    {
        //WidthMin = 400,
        //WidthMiddle = 600,
        //WidthBig = 800,

        HeightMin = 600,
        HeightMiddle = 800,
        HeightBig = 1000,

    }


}

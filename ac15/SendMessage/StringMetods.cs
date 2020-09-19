//using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace TIExCAD
{
    static public class StringMetods
    {
        /// <summary>
        /// Из списка строк выбирает самую длинную, возращает ее длину
        /// </summary>
        /// <returns>lenMaxStr</returns>
        static public byte GetLenMaxStringOfList(List<string> listString)
        {
            byte lenMaxStr = 0;
            // по списку
            foreach (string sitem in listString)
            {
                lenMaxStr = ((byte)sitem.Length > lenMaxStr) ? (byte)sitem.Length : lenMaxStr;
            }
            //Console.WriteLine();
            return lenMaxStr;
        }

    }
}

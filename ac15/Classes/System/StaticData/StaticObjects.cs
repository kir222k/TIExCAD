using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIExCAD.Generic
{
    /// <summary>
    /// Информация о версиях AutoCAD.
    /// Разработан для хранения необходимых статических данных в теле сборки.
    /// </summary>
    public static class StaticObjAcadRev
    {
        /// <summary>
        /// AutoCAD 2015
        /// </summary>
        public static readonly string Acad2015 = "20.0";
        /// <summary>
        /// AutoCAD 2016
        /// </summary>
        public static readonly string Acad2016 = "20.1";
        /// <summary>
        /// AutoCAD 2017
        /// </summary>
        public static readonly string Acad2017 = "21.0";
        /// <summary>
        /// AutoCAD 2018
        /// </summary>
        public static readonly string Acad2018 = "22.0";
        /// <summary>
        /// AutoCAD 2019
        /// </summary>
        public static readonly string Acad2019 = "23.0";
        /// <summary>
        /// AutoCAD 2020
        /// </summary>
        public static readonly string Acad2020 = "23.1";
        /// <summary>
        /// AutoCAD 2021
        /// </summary>
        public static readonly string Acad2021 = "24.0";

        /// <summary>
        /// Информация о версиях AutoCAD.
        /// </summary>
        /// <returns>Словарь: ключ - "Acad20xx", значение - строка с номером версии</returns>
        public static Dictionary<string, string> GetDictionaryAcadReleaseVersions()
        {
            Dictionary<string, string> dictVersion = new Dictionary<string, string>
            {
                { "Acad2015", "20.0" },
                { "Acad2016", "20.1" },
                { "Acad2017", "21.0" },
                { "Acad2018", "22.0" },
                { "Acad2019", "23.0" },
                { "Acad2020", "23.1" },
                { "Acad2021", "24.0" }

            };

            return dictVersion;
        }

    }
}

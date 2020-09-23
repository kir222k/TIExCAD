using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIExCAD
{
    /// <summary>
    /// Содержит метод, кот возращет список строк: класс, метод, сообщение c обрамлением *.
    /// </summary>
        public class StringDebugFromClass
    {
        // ПОЛЯ (статические)
        // строка Класс
        private static string strClass = "\n* Класс:     ";
        // строка Метод
        private static string strMetod = "\n* Метод:     ";
        // строка Сообщение
        private static string strMess = "\n* Сообщение:  ";

        // ПОЛЯ
        private string nameSourceClass;
        private string nameSourceMetod;
        private string messText;

        // КОНСТРУКТОРЫ
        /// <summary>
        /// Принимает в конструктор имя класса, метода, откуда вызывается и само сообщение, напр.  из AcadSendMess
        /// Содержит метод, возращающий строку 
        /// </summary>
        /// <param name="NameSourceClass"></param>
        /// <param name="NameSourceMetod"></param>
        /// <param name="MessText"></param>
        public StringDebugFromClass(string NameSourceClass, string NameSourceMetod, string MessText)
        {
            nameSourceClass = NameSourceClass;
            nameSourceMetod = NameSourceMetod;
            messText = MessText;
        }


        // Найдем строку с максимальной длиной

        // добавим необходимое кол-во звездочек в остальные строки

        // добавим строку нужной длины из звездочек 

        // соберем всю строку в список, 

        // МЕТОДЫ
        /// <summary>
        /// Собирает строку из 
        /// </summary>
        /// <returns></returns>
        public string GetStringStars()
        {
                // строка Класс+
         string strClassFull = strClass + nameSourceClass;
        // строка Метод+
         string strMetodFull = strMetod + nameSourceMetod;
        // строка Сообщение+
         string strMessFull = strMess + messText;

            // создаем экз StringDebug,
            StringDebug StrDeb = new StringDebug (new List<string> { strClassFull , strMetodFull, strMessFull });
            // вызываем GetStringLikeStars
            
            string resStr = StrDeb.GetStringLikeStars();

            return (resStr != "") ? resStr : ".\nСбой в отправке отладочного сообщения\n.";

        }


    }






    /// <summary>
    /// Содержит метод, кот. возращает форматированную строку с переносами, получается блок строк,  обрамленных * , принимает список строк в конструктор
    /// Вызвается из класса StringDebugFromClass или напрямую из AcadSendMess 
    /// </summary>
    public class StringDebug
    {
        // ПОЛЯ
         private List<string> listText;

        // СВОЙСТВА
        public List<string> ListText
        {
            //get { }
            set { listText = value; }
        }

        // КОНСТРУКТОРЫ 
        public StringDebug( List<string> listString) { ListText = listString; }


        // 
        public string GetStringLikeStars()
        {
            // МЕТОДЫ

            // отправим сообщение

            //string strFull = 
            //            $"\n." +
            //            $"\n*******************************" +
            //            $"\n*****  D   E   B   U   G  *****" +
            //            $"\n*******************************" +
            //            $"\n* {strClassFull}" +
            //            $"\n* {strMetodFull}" +
            //            $"\n* {strMessFull}" +
            //            $"\n*******************************" +
            //            $"\n.");
            return "!";
        }

}

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;



// [assembly: CommandClass(typeof(TIExCAD.AcadSendMess))]
//
namespace TIExCAD
{
    /// <summary>
    /// Класс для работы с сообщениями в коммандной строке AutoCAD. Отправка простого сообщения.
    /// </summary>
    public class AcadSendMess
    {
        // ПОЛЯ
        // Поле doc - ссылка  на активный открытый чертеж AutoCAD
        public readonly Document doc = Application.DocumentManager.MdiActiveDocument;
        // Поле ed - ссылка на Editor активного чертежа
        private  Editor ed;
        
        // МЕТОДЫ
        /// <summary>
        /// Отправка простого сообщения в ком.строку AutoCAD. Метод можно  переопределить
        /// </summary>
        public virtual void SendStringDebug(string messText)
        {
            // Проверим, что ссылка на текущий чертеж реальная 
            if (doc != null)
            {
                // получаем в ed Editor текущего чертежа
                ed = doc.Editor;
                // Шлем сообщение
                ed.WriteMessage(messText);
            }
        }
    }

    /// <summary>
    /// Класс наследован от AcadSendMess, используется для отправки составного сообщения: 
    /// используется информация о блоке кода, откуда вызываются методы данного класса.
    /// Предназначен для отладки. Пример создания экземпляра: 
    /// <code> AcadSendMessExt AcSM = new AcadSendMessExt("TIExAcSend", $"{this}"); </code>
    /// или
    /// <code>  AcadSendMessExt AcSM = new AcadSendMessExt($"{this}"); </code>
    /// </summary>
    public class AcadSendMessExt : AcadSendMess
    {
        // СВОЙСТВА
        /// <summary>
        /// Имя метода, откуда посылается сообщение
        /// </summary>
        public string NameSourceMetod { get; set; }
        /// <summary>
        /// Имя класса, откуда посылается сообщение
        /// </summary>
        public string NameSourceClass { get; set; }

        // КОНСТРУКТОРЫ
        /// <summary>
        /// При создании экз класса имя класса=Неизвестно, имя метода=Неизвестно
        /// </summary>
        public AcadSendMessExt() : this("Неизвестно") { }
        /// <summary>
        /// При создании экз класса требуется задать имя класса источника, имя метода=Неизвестно
        /// </summary>
        /// <param name="mn"></param>
        public AcadSendMessExt(string cn) : this( "Неизвестно", cn) { }
        /// <summary>
        /// При создании экз класса требуется задать имя метода, имя класса источника сообщения
        /// </summary>
        /// <param name="cn"></param>
        /// <param name="mn"></param>
        public AcadSendMessExt(string mn, string cn) {  NameSourceMetod = mn; NameSourceClass = cn; }

        // МЕТОДЫ
        /// <summary>
        /// Отправка  сообщения в ком.строку AutoCAD. Метод переопределен, добавлена информация, откуда шлется сообщение: Класс, Метод
        /// </summary>
        public override void SendStringDebug(string messText)
        {
            // public readonly Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc != null)
            {
                StringDebugFromClass StrDeb = new StringDebugFromClass(NameSourceClass, NameSourceMetod, messText);
                //// строка Класс
                //string strClass = "\n* Класс:     ";
                //// строка Метод
                //string strMetod = "\n* Метод:     ";
                //// строка Сообщение
                //string strMess = "\n* Сообщение:  ";

                //// строка Класс+
                //string strClassFull = strClass + NameSourceClass;
                //// строка Метод+
                //string strMetodFull = strMetod + NameSourceMetod;
                //// строка Сообщение+
                //string strMessFull = strMess + messText;

                //// Найдем строку с максимальной длиной

                //// добавим необходимое кол-во звездочек в остальные строки

                //// добавим строку нужной длины из звездочек 

                //// соберем всю строку

                // отправим сообщение

                doc.Editor.WriteMessage(StrDeb.GetStringStars());
                //    $"\n*******************************" +
                //    $"\n*****  D   E   B   U   G  *****" +
                //    $"\n*******************************" +
                //    $"\n* {strClassFull}" +
                //    $"\n* {strMetodFull}" +
                //    $"\n* {strMessFull}" +
                //    $"\n*******************************" +
                //    $"\n.");
            }
        }
    }
}

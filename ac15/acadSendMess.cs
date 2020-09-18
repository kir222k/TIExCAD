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
        public virtual void SendString(string messText)
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
        public AcadSendMessExt(string mn, string cn) {  NameSourceMetod = mn; NameSourceClass = cn; }

        // МЕТОДЫ
        /// <summary>
        /// Отправка  сообщения в ком.строку AutoCAD. Метод переопределен, добавлена информация, откуда шлется сообщение: Класс, Метод
        /// </summary>
        // /// <param name="messText"></param>
        public override void SendString(string messText)
        {
            if (doc != null)
            {
                doc.Editor.WriteMessage($"\n." +
                    $"\n****** D * E * B * U * G ******" +
                    $"\n* Класс: {NameSourceClass} " +
                    $"\n* Метод: {NameSourceMetod} " +
                    $"\n* Сообщение: {messText} " +
                    $"\n*******************************" +
                    $"\n.");
            }
        }
    }
}

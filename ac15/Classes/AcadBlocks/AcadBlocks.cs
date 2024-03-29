﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.IO;
using System.Reflection;

// Acad
using Autodesk.Windows;
using AdW = Autodesk.Windows;
using acadApp = Autodesk.AutoCAD.ApplicationServices.Application;


[assembly: CommandClass(typeof(TIExCAD.Generic.AcadDocBlocksTest))]

namespace TIExCAD.Generic
{
    /// <summary>
    /// Работа с определениями блоков. Поиск в БД  чертежа блоков (определений блоков), считывание их свойств, атрибутов и др. с пом. различных методов.
    /// </summary>
    public static class AcadBlocksDef
    {
        /// <summary>
        /// список имен блоков - будет возрашен каким-то методом. или как-то еще использован в классе.
        /// </summary>
        //private static List<string> blockNames;
        //public static List<string> BlockNames
        //{ 
        //    get { return blockNames; }
        // //   set { blockNames = value; }
        //}

        // БД чертежа. Можем использовать как БД активного чертежа, так и другого. По умолчанию - БД активного чертежа.

        /// <summary>
        /// База данных чертежа. по умолчанию - БД текущего активного чертежа.
        /// </summary>
        //public static Database DwgDB { get => dwgDB; set => dwgDB = value; }
        //private static Database dwgDB = HostApplicationServices.WorkingDatabase;

        // КОНСТРУКТОРЫ - нет в стат. классе


        // МЕТОДЫ

        // Получим определения блоков в чертеже.
        public static List<string> GetBlockNames(Database dwgDB)
        {
            List<string> blockNames = new List<string>();



            using (Transaction trans = (Transaction)dwgDB.TransactionManager.StartTransaction())
            {
                try
                {
                    // вызов ошибки када.
                    //throw new Autodesk.AutoCAD.Runtime.Exception(ErrorStatus.BadDwgHeader, "Ошибка када");

                    // Получим таблицу блоков из БД чертежа.
                    BlockTable blockTable = (BlockTable)trans.GetObject(dwgDB.BlockTableId, OpenMode.ForRead);

                    foreach (ObjectId idBlock in blockTable)
                    {
                        // Получим запись блока.
                        BlockTableRecord btRecord = (BlockTableRecord)trans.GetObject(idBlock, OpenMode.ForRead);
                        // Добавим в список блоков.
                        blockNames.Add(btRecord.Name);
                    }
                }
                catch (Autodesk.AutoCAD.Runtime.Exception ex)
                {
                    acadApp.ShowAlertDialog(ex.ToString());
                }

            }


            return blockNames;
        }
        // Получим список имен блоков.

        // Вывод данных о классе в строку:
        //

        // Вывод данных о классе в консоль.
        // public void Print()=> Console.WriteLine($"Name: {name}  Age: {age}");

    }


    public static class AcadDocBlocksTest
    {
        [CommandMethod("TIECADGETBLOCKNAMES")]
        public static void AcadDocBlockTestGetBlocks()
        {
            AcadSendMess AcMs = new AcadSendMess();
            AcMs.SendStringDebugStars(AcadBlocksDef.GetBlockNames(HostApplicationServices.WorkingDatabase));

            //System.Windows.MessageBoxResult msresult = 
            //    System.Windows.MessageBox.Show("Is it References", "References",
            //    System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question);
            System.Windows.MessageBox.Show("тест сообщений");



        }
    }



}

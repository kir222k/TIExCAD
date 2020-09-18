/* Файл сохранен из AutoCAD_2021_dotnet_wizards
 * как пример работы с методами, кот. вызываются
 * командой из AutoCAD. 
 * Файл не следует использовать в проектах как рабочий,
 * оставлен для изучения и освоения конструкций [CommandMethod ...]
 */

using System;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;

// This line is not mandatory, but improves loading performances, чтобы это не значило(!)
[assembly: CommandClass(typeof(TIExCAD.MyCommands))]

namespace TIExCAD
{

    // This class is instantiated by AutoCAD for each document when
    // a command is called by the user the first time in the context
    // of a given document. In other words, non static data in this class
    // is implicitly per-document!

    /// <summary>
    /// Пример применения CommandMethod от Autodesk Wizard
    /// в различных случаях
    /// </summary>
    public class MyCommands
    {
        // The CommandMethod attribute can be applied to any public  member 
        // function of any public class.
        // The function should take no arguments and return nothing.
        // If the method is an intance member then the enclosing class is 
        // intantiated for each document. If the member is a static member then
        // the enclosing class is NOT intantiated.
        //
        // NOTE: CommandMethod has overloads where you can provide helpid and
        // context menu.

        // Modal Command with localized name

        /// <summary>
        /// Отправляет сообщение  в ком. строку AutoCAD
        /// </summary>
        [CommandMethod("MyGroup", "MyCommand", "MyCommandLocal", CommandFlags.Modal)]
        public void MyCommand(string strCommand) // This method can have any name
        {
            // Put your command code here
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor ed;
            if (doc != null)
            {
                ed = doc.Editor;
                //ed.WriteMessage("Hello, this is your first command.");
                ed.WriteMessage(strCommand);

            }


        }

        /// <summary>
        /// Выделение примитива/объекта в чертеже
        /// </summary>
        [CommandMethod("MyGroup", "MyPickFirst", "MyPickFirstLocal", CommandFlags.Modal | CommandFlags.UsePickSet)]
        public void MyPickFirst() // This method can have any name
        {
            PromptSelectionResult result = Application.DocumentManager.MdiActiveDocument.Editor.GetSelection();
            if (result.Status == PromptStatus.OK)
            {
                // There are selected entities
                // Put your command using pickfirst set code here
            }
            else
            {
                // There are no selected entities
                // Put your command code here
            }
        }

        /// <summary>
        /// Application Session Command with localized name.
        /// Непонятно что это. Нужно разобраться
        /// </summary>
        [CommandMethod("MyGroup", "MySessionCmd", "MySessionCmdLocal", CommandFlags.Modal | CommandFlags.Session)]
        public void MySessionCmd() // This method can have any name
        {
            // Put your command code here
        }

        /// <summary>
        /// LispFunction is similar to CommandMethod but it creates a lisp 
        /// callable function. Many return types are supported not just string
        /// or integer.
        /// Видимо, создание и вызов Lisp функции. Тоже нужно разбираться
        /// </summary>
        [LispFunction("MyLispFunction", "MyLispFunctionLocal")]
        public int MyLispFunction(ResultBuffer args) // This method can have any name
        {
            // Put your command code here

            // Return a value to the AutoCAD Lisp Interpreter
            return 1;
        }

    }

}

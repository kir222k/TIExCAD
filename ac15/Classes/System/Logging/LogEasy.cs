using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Autodesk.AutoCAD.Runtime;
using AcEx = Autodesk.AutoCAD.Runtime.Exception;

[assembly: CommandClass(typeof(TIExCAD.Generic.LogTesting))]

namespace TIExCAD.Generic
{
    
    public static  class LogEasy
    {
        public static void  WriteLog(string eventName, string path)
        {
            
            if ((eventName != string.Empty) && (path != string.Empty))
            {
                //FileInfo FileInf = new FileInfo(path);

                //if (FileInf.Exists == true )
                //{

                    using (StreamWriter logger = new StreamWriter(path, true))
                    {
                        logger.WriteLine(DateTime.Now.ToLongTimeString() + " - " + eventName);
                    }
                //}
            }
        }

        private static void CreateFileLog(string pathLog)
        {
            
        }

        private static void DeleteFileLog(string pathLog)
        {

        }
    }

    public static class LogTesting
    {
        [CommandMethod("LogTestErr")]
        public static void LogTestErr()
        {
            // internal static readonly string PathLog = GetPathApp() + "\\<название приложения>.log";

            try
            {   
                // вызовем исключение из AutoCAD
                throw new AcEx(ErrorStatus.HandleExists,"Generated Exception from AutoCAD. Texting");
            }
            catch (AcEx e)
            {
                string eventThis =
                    "AcadExeption " + "LogTestErr " + e.Message.ToString();
                LogEasy.WriteLog(eventThis, Pathes.PathLog);
                throw;
            }
            catch (System.Exception e )
            {

                throw;
            }

        }
    }

}

using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIExCAD.Generic
{
    /// <summary>
    /// Работа с информацией об определении блока
    /// </summary>
    internal interface IBlockDefinitionInfo
    {
        // 
    }

    public  interface IAppData
    {
      //  List<string> BlockNames { get; }
        Database DwgDB { get; set; } 


    }
}

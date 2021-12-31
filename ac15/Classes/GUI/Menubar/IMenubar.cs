        // структура  меню
        // url manual

        // Загрузка из CUIX 
        // https://forums.autodesk.com/t5/net/problem-while-adding-menu-into-autocad-menu-bar-using-c/td-p/2437903

        // Создание через .NET
        // https://forums.autodesk.com/t5/net/custom-menu-and-ribbon-with-code/td-p/10511529

//#if DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Interop;

namespace TIExCAD.Generic
{
     interface IMenubar
    {
        //void MenubarCreatePopupMenu(string menuName, string menuItemName, string menuMacros);
        void MenubarCreatePopupMenu(string menuName, List<MenuPopItem> listMenuPopItems);

         void MenubarDeletePopupMenu(string menuName);
    }
}

//#endif
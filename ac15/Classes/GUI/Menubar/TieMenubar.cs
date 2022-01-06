
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;

using AcApp = Autodesk.AutoCAD.ApplicationServices.Application;

using Autodesk.Windows;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Interop;
using System.IO;

#if DEBUG
[assembly: CommandClass(typeof(TIExCAD.Generic.MenubarPopMenuTest))]
#endif

//#error version

namespace TIExCAD.Generic
{
  

    public  class TieMenubar : ITieMenubar
    {
        public TieMenubar() { }

        //private  AcadApplication acadApp = (AcadApplication)AcApp.AcadApplication;

        private AcadPopupMenu acadPopMenu = null;
        readonly AcadApplication acadApp = (AcadApplication)AcApp.AcadApplication;
        //public AcadPopupMenu AcadPopMenu { get => acadPopMenu;  } 

        public void MenubarCreatePopupMenu(string menuName, List<MenuPopItem> listMenuPopItems) //string menuItemName , string menuMacros)
        {
            if (acadPopMenu == null)
            { 
                bool isExist = false;
                        
                foreach (AcadPopupMenu menu in acadApp.MenuBar)
                {
                    if (menu.Name == menuName)
                    {
                        isExist = true;
                        break;
                    }
                }

                if (!isExist)
                {
                    bool isName = false;
                    foreach (AcadPopupMenu menux in acadApp.MenuGroups.Item(0).Menus)
                    {
                        //listTxt.Add(menux.Name);
                        if (menux.Name == menuName)
                        {
                            isName = true;
                            menux.InsertInMenuBar(acadApp.MenuBar.Count);
                            break;
                        }
                    }

                    if (!isName)
                    {
                        acadPopMenu = acadApp.MenuGroups.Item(0).Menus.Add(menuName);

                        foreach (MenuPopItem menuItem in listMenuPopItems)
                        {
                            acadPopMenu.AddMenuItem(acadPopMenu.Count, menuItem.Name, menuItem.Macros);
                        }

                        acadPopMenu.InsertInMenuBar(acadApp.MenuBar.Count); 
                    }
                }
            }
        }




        public void MenubarDeletePopupMenu(string menuName)
        {
            AcadApplication acadApp = (AcadApplication)AcApp.AcadApplication;

            foreach (AcadPopupMenu menu in acadApp.MenuBar)
            {
                if (menu.Name == menuName)
                {
                    menu.RemoveFromMenuBar();
                }
            }

        }


    }

#if DEBUG
    public static  class MenubarPopMenuTest
    {
        [CommandMethod("TIExMenuLoadTest")]
        public static void MenubarPopMenuTestLoad()
        {
            var Mbar = new Menubar();

            var listMI = new List<MenuPopItem>();
            listMI.Add(new MenuPopItem { Name = "MenuItem1", Macros = "_.pline " });
            listMI.Add(new MenuPopItem { Name = "MenuItem2", Macros = "_.circle " });
            listMI.Add(new MenuPopItem { Name = "MenuItem3", Macros = "_.circle " });
            listMI.Add(new MenuPopItem { Name = "MenuItem4", Macros = "_.circle " });
            listMI.Add(new MenuPopItem { Name = "MenuItem5", Macros = "_.circle " });
            listMI.Add(new MenuPopItem { Name = "MenuItem6", Macros = "_.circle " });

            Mbar.MenubarCreatePopupMenu("TestMenu", listMI);
        }

        [CommandMethod("TIExMenuRemoveTest")]
        public static void MenubarPopMenuTestRemove()
        {
            var Mbar = new Menubar();
            Mbar.MenubarDeletePopupMenu("TestMenu");
        }

        [CommandMethod("TIExMenuListTest")]
        public static void MenubarPopMenuList()
        {
            AcadSendMess AcSM = new AcadSendMess();
            var listTxt = new List<string>();
            AcadApplication acadApp = (AcadApplication)AcApp.AcadApplication;
            foreach (AcadPopupMenu menux in acadApp.MenuGroups.Item(0).Menus)
            {
                listTxt.Add(menux.Name);
            }
                AcSM.SendStringDebugStars(listTxt);
        }

    }
#endif

}


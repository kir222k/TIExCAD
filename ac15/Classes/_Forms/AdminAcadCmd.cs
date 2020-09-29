using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Win32;

using System.Reflection;
using Autodesk.AutoCAD.Runtime;
using AcRt = Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;

[assembly: CommandClass(typeof(TIExCAD.ShowdminCmd))]


namespace TIExCAD
{
    public partial class AdminAcadCmd : Form
    {
        public AdminAcadCmd()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegGeneric RegGen = new RegGeneric();
            RegGen.GetUnRegisterCustomApp(txtNameApp.Text);
        }

        private void RegisterApp_Click(object sender, EventArgs e)
        {
            //RegGeneric RegGen = new RegGeneric();
            //RegGen.GetRegisteredApps(txtNameApp.Text,);
        }
    }


    public class ShowdminCmd
    {
        [CommandMethod("ShowAdmin")]
        public void ShowAdmin()
        {
            AdminAcadCmd FormAdmin = new AdminAcadCmd();
            FormAdmin.ShowDialog();
        }

        //[CommandMethod("ShowAdmin")]
        //public void ShowAdmin()
        //{
        //    AdminAcadCmd FormAdmin = new AdminAcadCmd();
        //    FormAdmin.ShowDialog();
        //}

    }
}

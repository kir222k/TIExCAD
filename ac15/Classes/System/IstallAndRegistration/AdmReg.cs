using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TIExCAD.Generic
{
    public partial class AdmReg : Form
    {
        public AdmReg()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string appName;
            //System.Console.WriteLine("Input aplication name for unregistration:");
            //appName = System.Console.ReadLine();
            TIExCAD.Generic.RegGeneric RegGen = new Generic.RegGeneric();
            appName = this.textBox1.Text;
            if (appName != String.Empty)
                RegGen.GetUnRegisterCustomApp(appName);

        }
    }
}

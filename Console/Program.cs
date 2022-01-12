using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIExCAD.Generic;

using System.IO;



namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nFile path: ");
            string? strPath = Console.ReadLine();

            Console.WriteLine(LogEasy.GetFileSizeMb(strPath).ToString());


            Console.ReadKey();
        }
    }
}

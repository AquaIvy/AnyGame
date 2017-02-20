using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AnsiToUtf8
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 2)
            {
                Console.WriteLine("参数数量错误。应输入：（参数1：检索目录    参数2：检索通配符）");
                return;
            }

            string path = Application.StartupPath;
            string filter = "*.*";

            //参数赋值
            if (args.Length == 2)
            {
                path = args[0];
                filter = args[1];
            }
            else if (args.Length == 1)
            {

            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("path     {0}", path);
            Console.WriteLine("filter   {0}", filter);
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.ReadKey();
        }
    }
}

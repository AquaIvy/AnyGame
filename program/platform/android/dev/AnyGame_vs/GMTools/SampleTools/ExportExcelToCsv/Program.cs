using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExportExcelToCsv
{
    class Program
    {
        static void Main(string[] args)
        {
            //if (args.Length != 2)
            //{
            //    Console.WriteLine(@"参数数量错误。应输入：（参数1：检索目录    参数2：检索通配符）   检索目录参数可为/或.表示当前目录");
            //    return;
            //}

            string sourcePath = AppDomain.CurrentDomain.BaseDirectory;
            string targetPath = string.Empty;
            string sheetName = "";

            //参数赋值
            if (args.Length == 2)
            {
                sourcePath = args[0];

                sourcePath = sourcePath == "/" ? "." : sourcePath;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("path     {0}", sourcePath);
            Console.WriteLine("filter   {0}", targetPath);
            Console.ForegroundColor = ConsoleColor.Gray;

            ExcelHelper excel = new ExcelHelper();
            excel.Open(@"D:\Unity\Projects\AnyGame\plan\W文档\Card.xlsx");
            excel.SaveAs(@"D:\Unity\Projects\AnyGame\plan\W文档\Card.csv", XlFileFormat.xlCSV);
            excel.Close();

            Console.ReadKey();
        }
    }
}

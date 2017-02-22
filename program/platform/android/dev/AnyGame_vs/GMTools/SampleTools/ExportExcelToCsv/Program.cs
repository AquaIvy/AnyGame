using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ExportExcelToCsv
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine(@"参数数量错误。应输入：（参数1：sourcePath    参数2：targetPath）");
                return;
            }

            //Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;

            string sourcePath = string.Empty;
            string targetPath = string.Empty;
            string sheetName = "";

            //参数赋值
            sourcePath = args[0];
            targetPath = args[1];

            if (Directory.Exists(targetPath))
            {
                //文件夹
                targetPath = Path.Combine(targetPath, Path.GetFileNameWithoutExtension(sourcePath) + ".csv");
            }
            else if (File.Exists(targetPath))
            {
                //文件
            }

            sourcePath = Path.GetFullPath(sourcePath);
            targetPath = Path.GetFullPath(targetPath);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("sourcePath   {0}", sourcePath);
            Console.WriteLine("targetPath   {0}", targetPath);
            Console.ForegroundColor = ConsoleColor.Gray;

            try
            {
                ExcelHelper excel = new ExcelHelper();
                excel.Open(sourcePath);
                excel.SaveAs(targetPath, XlFileFormat.xlCSV);
                excel.Close();

                ConvertFileEncoding(targetPath, targetPath, Encoding.UTF8);
            }
            catch (Exception)
            {

                throw;
            }

            //Console.ReadKey();
        }

        /// <summary>
        /// 文件编码转换
        /// </summary>
        /// <param name="sourceFile">源文件</param>
        /// <param name="destFile">目标文件，如果为空，则覆盖源文件</param>
        /// <param name="targetEncoding">目标编码</param>
        public static void ConvertFileEncoding(string sourceFile, string destFile, Encoding targetEncoding)
        {
            destFile = string.IsNullOrEmpty(destFile) ? sourceFile : destFile;
            File.WriteAllText(destFile, File.ReadAllText(sourceFile, Encoding.Default), targetEncoding);
        }
    }
}

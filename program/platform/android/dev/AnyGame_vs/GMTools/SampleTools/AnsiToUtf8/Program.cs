using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AnsiToUtf8
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine(@"参数数量错误。应输入：（参数1：检索目录    参数2：检索通配符）   检索目录参数可为/或.表示当前目录");
                return;
            }

            string path = Application.StartupPath;
            string pattern = "*.*";

            //参数赋值
            if (args.Length == 2)
            {
                path = args[0];
                pattern = args[1];

                path = path == "/" ? "." : path;
            }
            //else if (args.Length == 1)
            //{
            //    if (args[0].Length > 2 &&)
            //    {

            //    }
            //}

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("path     {0}", path);
            Console.WriteLine("filter   {0}", pattern);
            Console.ForegroundColor = ConsoleColor.Gray;


            var allfiles = Directory.GetFiles(path, pattern, SearchOption.AllDirectories);
            foreach (var file in allfiles)
            {
                if (File.Exists(file))
                {
                    ConvertFileEncoding(file, file, Encoding.UTF8);
                    Console.WriteLine("convert  {0}", file);
                }
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

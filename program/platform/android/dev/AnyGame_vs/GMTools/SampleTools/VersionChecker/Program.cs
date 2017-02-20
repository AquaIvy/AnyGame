using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace VersionChecker
{
    class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("versionChange fileName version");
                return;
            }
            var file = args[0];

            if (args.Length > 1)
                int.TryParse(args[1], out inputVersion);

            UpdateFileVersion(new FileInfo(file));
        }


        private Version v = new Version("0.0.0.0");

        /// <summary>
        /// 输入的版本号
        /// </summary>
        private static int inputVersion = -1;

        static string regStr2 = "new Version\\(\"+\\d+\\.\\d+\\.\\d+\\.\\d+\"\\)";
        static string subRegStr = "\\d+\\.\\d+\\.\\d+\\.\\d+";


        static void UpdateFileVersion(FileInfo file)
        {
            try
            {
                if (!file.Exists)
                {
                    Console.WriteLine("not fin file" + file.FullName);
                    return;
                }

                var content = File.ReadAllText(file.FullName);
                bool hasChange = false;


                var m11 = Regex.Match(content, regStr2);
                if (m11.Success)
                {
                    var m2 = Regex.Match(m11.Value, subRegStr);
                    if (m2.Success)
                    {
                        var numArray = m2.Value.Split('.');

                        int builderVersion = int.Parse(numArray[2]);
                        if (inputVersion == -1)
                            inputVersion = builderVersion;

                        int fixVersion = int.Parse(numArray[3]) + 1;
                        if (builderVersion == inputVersion)
                            fixVersion++;
                        else
                        {
                            //  如果输入的参数是-2，则说明需要提升一个编译版本号
                            if (inputVersion == -2)
                                inputVersion = builderVersion + 1;

                            fixVersion = 0;
                        }

                        var s = string.Format("{0}.{1}.{2}.{3}", numArray[0], numArray[1], inputVersion.ToString(),
                            fixVersion.ToString());
                        Console.WriteLine("version {0}", s);
                        if (m2.Value != s)
                        {
                            content = content.Replace(m2.Value, s);
                            hasChange = true;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("not find version data");
                }

                if (hasChange)
                {
                    File.WriteAllText(file.FullName, content);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("错误0：{0}", e.Message);
            }
        }
    }
}

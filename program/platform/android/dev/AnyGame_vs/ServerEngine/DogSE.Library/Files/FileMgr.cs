using DogSE.Library.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DogSE.Library.Files
{
    /// <summary>
    /// 
    /// </summary>
    public class FileMgr
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool CreateFile(string path, string info)
        {
            FileInfo t = null;
            StreamWriter sw = null;

            try
            {
                t = new FileInfo(path);
                DeleteFile(path);

                sw = t.CreateText();
                //以行的形式写入信息 
                sw.WriteLine(info);

                return true;
            }
            catch (Exception ex)
            {
                Logs.Error("创建失败：{0}\n{1}", path, ex.ToString());
                return false;
            }
            finally
            {
                sw.Close();
                sw.Dispose();
            }
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="path"></param>
        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// 创建目录(是否先删除原目录)
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isDeleteFirst"></param>
        public static void CreateDirectory(string path, bool isDeleteFirst)
        {
            if (isDeleteFirst)
            {
                Directory.Delete(path);
            }
            CreateDirectory(path);
        }



        /// <summary>
        /// 保存文件到本地
        /// </summary>
        /// <param name="path">要保存的路径</param>
        /// <param name="bytes">文件内容</param>
        /// <returns></returns>
        public static bool SaveFile(string path, byte[] bytes)
        {
            Stream sw = null;
            FileInfo info = new FileInfo(path);

            try
            {
                info.Delete();
                sw = info.Create();

                sw.Write(bytes, 0, bytes.Length);
                //File.WriteAllBytes(path, bytes);

                Logs.Info("保存文件: {0}", info.FullName);
                return true;

            }
            catch (Exception ex)
            {
                Logs.Error("保存文件失败: {0} \n{1}", info.FullName, ex.Message);
                return false;
            }
            finally
            {
                sw.Close();
                sw.Dispose();
            }
        }

        /// <summary>
        /// 获取文件夹下所有文件大小(KB)
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public int GetAllFileSize(string dir)
        {
            int sumSize = 0;
            if (!Directory.Exists(dir))
            {
                return 0;
            }

            DirectoryInfo dti = new DirectoryInfo(dir);

            FileInfo[] fi = dti.GetFiles();

            foreach (FileInfo f in fi)
            {
                sumSize += Convert.ToInt32(f.Length / 1024);
            }

            DirectoryInfo[] di = dti.GetDirectories();

            if (di.Length > 0)
            {
                for (int i = 0; i < di.Length; i++)
                {
                    sumSize += GetAllFileSize(di[i].FullName);
                }
            }

            return sumSize;
        }

        /// <summary>
        /// 获取指定文件大小(KB)
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public int GetFileSize(string path)
        {
            int size = 0;

            FileInfo info = new FileInfo(path);
            if (info.Exists)
            {
                size = Convert.ToInt32(info.Length / 1024);
            }

            return size;
        }

        /// <summary>
        /// 获得某一目录下的所有文件
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="noneExtension"></param>
        /// <returns></returns>
        public static List<string> GetAllFiles(string dir, string noneExtension)
        {
            List<string> allfiles = new List<string>();
            if (!Directory.Exists(dir))
            {
                return allfiles;
            }
            var extensions = noneExtension.Replace("*", "").Split('|');

            GetAllFiles(dir, allfiles, extensions);
            return allfiles;
        }

        private static void GetAllFiles(string dir, List<string> allfiles, string[] noneExtension)
        {
            DirectoryInfo dti = new DirectoryInfo(dir);

            FileInfo[] fi = dti.GetFiles();

            foreach (FileInfo f in fi)
            {
                if (noneExtension.FirstOrDefault(o => o == f.Extension) == null)
                {
                    allfiles.Add(f.FullName);
                }
            }

            DirectoryInfo[] di = dti.GetDirectories();

            if (di.Length > 0)
            {
                for (int i = 0; i < di.Length; i++)
                {
                    GetAllFiles(di[i].FullName, allfiles, noneExtension);
                }
            }
        }

        /// <summary>
        /// 文件（夹）重命名
        /// </summary>
        /// <param name="oldname"></param>
        /// <param name="newname"></param>
        public static void Rename(string oldname, string newname)
        {
            File.Move(oldname, newname);
        }


        /// <summary>
        /// 反射程序集
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Assembly LoadAssembly(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }

            var fs = new FileStream(path, FileMode.Open);
            var bytes = new byte[fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            fs.Dispose();
            Assembly assembly = Assembly.Load(bytes);

            return assembly;
        }


        /// <summary>
        /// 读取文本文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<string> LoadFile(string path)
        {
            List<string> lstString = new List<string>();
            if (!File.Exists(path))
            {
                Logs.Error("未找到文件：{0}", path);
                return lstString;
            }

            StreamReader sr = null;
            try
            {
                sr = File.OpenText(path);

                string line = string.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    //一行一行的读取
                    lstString.Add(line);
                }

                return lstString;
            }
            catch (Exception ex)
            {
                Logs.Warn("读取失败： {0}", ex.ToString());
                return null;
            }
            finally
            {
                sr.Close();
                sr.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool SaveFile(string path, List<string> info)
        {
            try
            {
                DeleteFile(path);
                using (StreamWriter sw = new StreamWriter(path))
                {
                    foreach (var item in info)
                    {
                        sw.WriteLine(item);
                        sw.Flush();
                    }
                }
                Logs.Info("保存成功 {0}", path);

                return true;
            }
            catch (Exception ex)
            {
                Logs.Info("保存失败 {0}\n{1}", path, ex.ToString());

                return false;
            }


        }


        /// <summary>
        /// 返回单个汉字的位编码
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string GetChineseCharCode(char c)
        {
            var uintCode = (uint)c;
            string charCode = "0x" + uintCode.ToString("X4");
            return charCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static byte[] ReadFile(string path)
        {
            if (!File.Exists(path))
            {
                return new byte[0];
            }

            var fs = new FileStream(path, FileMode.Open);
            var bytes = new byte[fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            fs.Dispose();

            return bytes;
        }

    }

}

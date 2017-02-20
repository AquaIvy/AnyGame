using System;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class Log
    {
        public static string LOCAL_LOG_PATH = @"";
        public static LogLevel Level = LogLevel.Info;

        private static void Display(LogLevel lev, string msg)
        {
            if ((int)lev >= (int)(Level))
            {
                switch (lev)
                {
                    case LogLevel.None:
                    case LogLevel.Info:
                    case LogLevel.Debug:
                        Debug.Log(msg);
                        break;
                    case LogLevel.Warning:
                        Debug.LogWarning(msg);
                        break;
                    case LogLevel.Error:
                        Debug.LogError(msg);
                        break;
                    default:
                        break;
                }
            }
        }

        public static void Info(string msg)
        {
            Display(LogLevel.Info, msg);
        }

        public static void Info(string format, params object[] param)
        {
            Info(string.Format(format, param));
        }

        public static void Warning(string msg)
        {
            Display(LogLevel.Warning, msg);
        }

        public static void Warning(string format, params object[] param)
        {
            Warning(string.Format(format, param));
        }

        public static void Error(string msg)
        {
            Display(LogLevel.Error, msg);
        }

        public static void Error(string format, params object[] param)
        {
            Error(string.Format(format, param));
        }

        public static bool AppendLogFile(string msg)
        {
            if (string.IsNullOrEmpty(LOCAL_LOG_PATH))
            {

                LOCAL_LOG_PATH = Application.dataPath + @"\Log\GameLog.log";

                //LOCAL_LOG_PATH = System.Environment.CurrentDirectory + @"\Log\GameLog.log";

            }

            FileStream fs = null;
            FileInfo fi = new FileInfo(LOCAL_LOG_PATH);
            if (!fi.Exists)
            {
                fs = fi.Create();
            }

            try
            {
                fs = File.OpenWrite(LOCAL_LOG_PATH);
                fs.Position = fs.Length;
                string msgfull = string.Format("{0} {1}\n", DateTime.Now, msg);
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(msgfull);
                fs.Write(bytes, 0, bytes.Length);

                return true;
            }
            catch (Exception ex)
            {
                Warning("日志记录出错 {0}", ex.ToString());
                return false;
            }
            finally
            {
                //即使前面有return   这里也会执行
                fs.Close();
            }
        }

        public static bool AppendLogFile(string format, params object[] param)
        {
            return AppendLogFile(string.Format(format, param));
        }
    }

}
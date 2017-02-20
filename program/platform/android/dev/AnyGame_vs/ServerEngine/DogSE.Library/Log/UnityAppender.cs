using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DogSE.Library.Log
{
    /// <summary>
    /// unity输出适配器
    /// </summary>
    public class UnityAppender : ILogAppender
    {
        /// <summary>
        /// 当前需要处理的集合
        /// </summary>
        private static readonly Queue<LogInfo> s_LogInfoQueue = new Queue<LogInfo>();

        /// <summary>
        /// 日志操作的锁
        /// </summary>
        private static object s_LockLogInfoQueue = new object();

        /// <summary>
        /// （全局，现成安全）是否加锁
        /// </summary>
        private static volatile bool s_IsLock;

        #region IAppender 成员

        /// <summary>
        /// 写日志（可以多线程操作）
        /// </summary>
        /// <param name="info"></param>
        public void Write(LogInfo info)
        {
            //  日志过滤
            if (info.MessageFlag < level)
                return;

            bool bIsLock = false;

            lock (s_LockLogInfoQueue)
            {
                s_LogInfoQueue.Enqueue(info);

                // 检测是否有其它的线程已在处理中，如在使用就退出，否则开始锁定
                if (s_IsLock == false)
                    bIsLock = s_IsLock = true;
            }


            // 如果有其它的线程在处理就退出
            if (bIsLock == false)
                return;

            LogInfo[] logInfoArray;

            do
            {
                logInfoArray = null;

                lock (s_LockLogInfoQueue)
                {
                    if (s_LogInfoQueue.Count > 0)
                    {
                        logInfoArray = s_LogInfoQueue.ToArray();
                        s_LogInfoQueue.Clear();
                    }
                    else
                        s_IsLock = false; // 没有数据需要处理,释放锁定让其它的程序来继续处理
                }

                if (logInfoArray == null)
                    break;

                for (int iIndex = 0; iIndex < logInfoArray.Length; iIndex++)
                {
                    LogInfo logInfo = logInfoArray[iIndex];

                    if (logInfo.Parameter == null)
                        InternalWriteLine(logInfo.MessageFlag, logInfo.Format);
                    else
                        InternalWriteLine(logInfo.MessageFlag, logInfo.Format, logInfo.Parameter);
                }

            } while (true);
        }

        #endregion


        /// <summary>
        /// 日志等级
        /// </summary>
        private LogMessageType level = LogMessageType.MSG_INFO;

        /// <summary>
        /// 日志等级
        /// </summary>
        public LogMessageType Level
        {
            get { return level; }
            set { level = value; }
        }



        private void InternalWriteLine(LogMessageType messageFlag, string format, object[] parameter)
        {
            InternalWriteLine(messageFlag, string.Format(format, parameter));
        }

        private void InternalWriteLine(LogMessageType messageFlag, string format)
        {
            switch (messageFlag)
            {
                case LogMessageType.MSG_NONE:
                    Debug.Log("[NONE]: " + format);
                    break;
                case LogMessageType.MSG_STATUS:
                    Debug.Log("[STATUS]: " + format);
                    break;
                case LogMessageType.MSG_SQL:
                    Debug.Log("[SQL]: " + format);
                    break;
                case LogMessageType.MSG_DEBUG:
                    Debug.Log("[DEBUG]: " + format);
                    break;
                case LogMessageType.MSG_INFO:
                    Debug.Log("[INFO]: " + format);
                    break;
                case LogMessageType.MSG_NOTICE:
                    Debug.LogWarning("[NOTICE]: " + format);
                    break;
                case LogMessageType.MSG_WARNING:
                    Debug.LogWarning("[WARNING]: " + format);
                    break;
                case LogMessageType.MSG_ERROR:
                    Debug.LogError("[ERROR]: " + format);
                    break;
                case LogMessageType.MSG_FATALERROR:
                    Debug.LogError("[FATALERROR]: " + format);
                    break;
                default:
                    Debug.LogErrorFormat("[{0}]: {1}", messageFlag, format);
                    break;
            }
        }

    }
}


using DogSE.Library.Log;
using System;
using System.Collections;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace AnyGame.Content
{
    /// <summary>
    /// 下载任务
    /// </summary>
    public class DownloadTask : IDisposable
    {
        /// <summary>
        /// 下载任务
        /// </summary>
	    public DownloadTask()
        {
            StartTime = DateTime.Now;
        }

        private WWW m_www;
        //private Stopwatch m_sw;

        /// <summary>
        /// 下载的资源
        /// </summary>
        public WWW WWW { get { return m_www; } }

        /// <summary>
        /// 是否完成
        /// </summary>
        public bool IsDone { get; private set; }

        /// <summary>
        /// 如果失败则由消息，
        /// 没失败则是空
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// 下载的数据
        /// </summary>
        public byte[] Bytes { get; private set; }

        /// <summary>
        /// 已经下载字节大小
        /// </summary>
        public int LoadedBytes { get { return m_www.bytesDownloaded; } }


        /// <summary>
        /// 下载直接大小
        /// </summary>
        public int TotalBytes { get { return m_www.size; ; } }

        /// <summary>
        /// 预设下载超时时间
        /// </summary>
        public int Timeout { get; private set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 是否超时
        /// </summary>
        public bool IsTimeOut
        {
            get
            {
                if (Timeout == 0)
                    return false;

                return (DateTime.Now - StartTime).TotalSeconds > Timeout;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="timeout">暂时无用</param>
        public DownloadTask(string path, int timeout = 0)
        {
            Timeout = timeout;
            Game.instance.StartCoroutine(DownloadFile(path));
        }

        public IEnumerator DownloadFile(string path, byte[] postData = null)
        {
            if (postData == null)
                m_www = new WWW(path);
            else
                m_www = new WWW(path, postData);

            yield return m_www;

            IsDone = true;
            if (string.IsNullOrEmpty(m_www.error))
            {
                Bytes = m_www.bytes;
                if (OnLoaded != null)
                    OnLoaded(this, EventArgs.Empty);
            }
            else
            {
                ErrorMessage = m_www.error;
                if (m_www.url.IndexOf("ClientLog.aspx") > -1)
                {
                    Debug.LogError(m_www.error + m_www.url);
                    //Unity3DDebugAppender.EnableWebLogReport = false;
                }
                else
                    Logs.Error(m_www.error + m_www.url);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {

        }

        /// <summary>
        /// 下载完成
        /// </summary>
        public event EventHandler OnLoaded;

        /// <summary>
        /// 只向网站提交数据就ok了，不需要等返回值
        /// </summary>
        /// <param name="path"></param>
        /// <param name="postData"></param>
	    static internal void PostData(string path, byte[] postData = null)
        {
            var task = new DownloadTask();
            Game.instance.StartCoroutine(task.DownloadFile(path, postData));
        }
    }
}
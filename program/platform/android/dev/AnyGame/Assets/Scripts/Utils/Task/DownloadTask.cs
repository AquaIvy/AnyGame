using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    /// <summary>
    /// 下载任务
    /// </summary>
    public class DownloadTask : IDisposable
    {
        private WWW www;
        private Stopwatch m_stopwatch;

        public WWW WWW { get { return www; } }

        public bool IsDone { get; private set; }

        public string ErrorMessage { get; private set; }

        public byte[] Bytes { get; private set; }

        public int LoadedBytes { get { return www.bytesDownloaded; } }

        public int TotalBytes { get { return www.size; ; } }

        public int Timeout { get; private set; }

        public DownloadTask(string path, int timeout = 0)
        {
            Timeout = timeout;
            Loader.Instance.StartCoroutine(DownloadFile(path));
        }

        IEnumerator DownloadFile(string path)
        {
            www = new WWW(path);

            yield return www;

            IsDone = true;
            if (string.IsNullOrEmpty(www.error))
            {
                Bytes = www.bytes;
                if (OnLoaded != null)
                    OnLoaded(this, EventArgs.Empty);
            }
            else
            {
                ErrorMessage = www.error;
                Log.Error(www.error + " " + www.url);
            }
        }

        public void Dispose()
        {

        }

        public event EventHandler OnLoaded;
    }

}
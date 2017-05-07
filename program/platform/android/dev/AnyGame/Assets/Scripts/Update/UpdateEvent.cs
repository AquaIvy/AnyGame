using Assets.Scripts.Utils;
using System;

namespace Assets.Scripts.Update
{
    public class UpdateEvent
    {
        public void Log(LogLevel level, string text)
        {
            if (OnLog != null)
            {
                OnLog(this, new LogArgs() { Level = level, Text = text });
            }
        }

        public void DownloadOneItem(OpState state, int curNum, int totalNum)
        {
            if (OnDownloadOneItem != null)
            {
                OnDownloadOneItem(this, new ComplateOneItemArgs { State = state, CurNum = curNum, TotalNum = totalNum });
            }
        }

        public void DownloadError(OpState state, string msg, Action retryAction)
        {
            if (OnDownloadError != null)
            {
                OnDownloadError(this, new DownloadErrorArgs() { State = state, Message = msg, RetryAction = retryAction });
            }
        }

        public void DownloadFinish()
        {
            if (OnDownloadFinish != null)
            {
                OnDownloadFinish(this, null);
            }
        }

        /// <summary>
        /// 触发日志事件
        /// </summary>
        public event EventHandler<LogArgs> OnLog;

        /// <summary>
        /// 触发 下载/拷贝 完成一个文件
        /// </summary>
        public event EventHandler<ComplateOneItemArgs> OnDownloadOneItem;

        /// <summary>
        /// 触发 下载/拷贝 出错
        /// </summary>
        public event EventHandler<DownloadErrorArgs> OnDownloadError;

        /// <summary>
        /// 所有文件下载完成，准备开始反射流程
        /// </summary>
        public event EventHandler<EventArgs> OnDownloadFinish;

    }



    public class LogArgs : EventArgs
    {
        public LogLevel Level { get; internal set; }

        public string Text { get; internal set; }
    }


    public class ComplateOneItemArgs : EventArgs
    {
        public OpState State { get; internal set; }
        public int CurNum { get; internal set; }
        public int TotalNum { get; internal set; }
    }


    /// <summary>
    /// 拷贝、下载时出错的参数
    /// </summary>
    public class DownloadErrorArgs : EventArgs
    {
        public OpState State { get; internal set; }
        public string Message { get; internal set; }
        public Action RetryAction { get; internal set; }
    }

    /// <summary>
    /// 操作状态
    /// </summary>
    public enum OpState
    {
        /// <summary>
        /// 下载matchlist.txt
        /// </summary>
        Matchlist = 0,

        /// <summary>
        /// 拷贝StreamingAssets
        /// </summary>
        CopyStream,

        /// <summary>
        /// 下载资源文件
        /// </summary>
        Download,
    }

}
using Assets.Scripts.Utils;
using System;

namespace Assets.Scripts.Update
{
    public partial class UpdateController
    {
        #region 事件监听

        /// <summary>
        /// 触发日志事件
        /// </summary>
        public event EventHandler<LogArgs> OnLog;

        /// <summary>
        /// 触发拷贝完成一个文件
        /// </summary>
        public event EventHandler<ComplateOneItemArgs> OnCopyOneItem;

        /// <summary>
        /// 触发下载完成一个文件
        /// </summary>
        public event EventHandler<ComplateOneItemArgs> OnDownloadOneItem;

        /// <summary>
        /// 触发下载出错
        /// </summary>
        public event EventHandler<DownloadErrorArgs> OnDownloadError;

        /// <summary>
        /// 所有文件下载完成，准备开始反射流程
        /// </summary>
        public event EventHandler<EventArgs> OnDownloadFinish;

        #endregion

        #region 事件触发

        public void FireLog(LogLevel level, string text)
        {
            if (OnLog != null)
            {
                OnLog(this, new LogArgs() { level = level, text = text });
            }
        }

        public void FireCopyOneItem(int curNum, int totalNum)
        {
            if (OnCopyOneItem != null)
            {
                OnCopyOneItem(this, new ComplateOneItemArgs { curNum = curNum, totalNum = totalNum });
            }
        }

        public void FireDownloadOneItem(int curNum, int totalNum)
        {
            if (OnDownloadOneItem != null)
            {
                OnDownloadOneItem(this, new ComplateOneItemArgs { curNum = curNum, totalNum = totalNum });
            }
        }

        public void FireDownloadError(OpState state, string msg, Action retryAction)
        {
            if (OnDownloadError != null)
            {
                OnDownloadError(this, new DownloadErrorArgs() { State = state, Msg = msg, RetryAction = retryAction });
            }
        }

        public void FireDownloadFinish()
        {
            if (OnDownloadFinish != null)
            {
                OnDownloadFinish(this, null);
            }
        }

        #endregion
    }

    #region 事件返回参数


    public class LogArgs : EventArgs
    {
        public LogLevel level { get; internal set; }

        public string text;
    }


    public class ComplateOneItemArgs : EventArgs
    {
        public int curNum { get; internal set; }
        public int totalNum { get; internal set; }
    }


    /// <summary>
    /// 拷贝、下载时出错的参数
    /// </summary>
    public class DownloadErrorArgs : EventArgs
    {
        public OpState State { get; internal set; }
        public string Msg { get; internal set; }
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

        /// <summary>
        /// 版本验证
        /// </summary>
        VersionAuth

    }

    #endregion

}
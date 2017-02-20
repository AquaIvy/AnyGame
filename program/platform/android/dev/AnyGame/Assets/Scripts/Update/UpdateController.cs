using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Update
{
    /// <summary>
    /// 更新管理器
    /// </summary>
    public partial class UpdateController
    {

        //IIS服务端 需要加入相关MIME映射
        //< mimeMap fileExtension = ".apk" mimeType = "application/octet-stream" />
        //< mimeMap fileExtension = ".assetbundle" mimeType = "application/octet-stream" />
        //< mimeMap fileExtension = ".pkm" mimeType = "application/octet-stream" />
        //< mimeMap fileExtension = ".pvr" mimeType = "application/octet-stream" />
        //< mimeMap fileExtension = ".tpsheet" mimeType = "text/xml" />
        //< mimeMap fileExtension = ".uiconfig" mimeType = "text/xml" />
        //< mimeMap fileExtension = ".ctrlconfig" mimeType = "text/xml" />

        private static UpdateController m_instance = null;
        public static UpdateController Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new UpdateController();
                }
                return m_instance;
            }
        }

        /// <summary>
        /// 下载时触发的各种事件
        /// </summary>
        //public DownloadEvent Evt = null;
        private Loader loader = null;

        private int curUpdateCount = 0;                         //当前已经更新的文件数量
        private int maxUpdateCount = 0;                         //需要更新的文件总数量
        private int errorUpdateCount = 0;                       //更新出错的文件数量
        private List<FileUpdateItem> oldList = null;            //本地  文件列表
        private List<FileUpdateItem> newList = null;            //需要更新的  文件列表
        private List<FileUpdateItem> errorList = null;          //出错的  文件列表

        //private Action onFinish = null;     //当下载完成时执行的逻辑

        public UpdateController()
        {
            //Evt = new DownloadEvent();
            loader = Loader.Instance;
            errorList = new List<FileUpdateItem>();
        }


        /// <summary>
        /// 开始更新的入口函数.
        /// 1.复制缓存文件
        /// 2.下载最新文件
        /// 3.进入游戏
        /// </summary>
        public void Start()
        {
            //是否清除本地缓存
            if (Directory.Exists(LGlobalInfo.CLIENT_ROOT_PATH)
                && LGlobalInfo.IsAlwaysClearStream)
            {
                Directory.Delete(LGlobalInfo.CLIENT_ROOT_PATH, true);
            }

            //第一次安装
            if (!Directory.Exists(LGlobalInfo.CLIENT_ROOT_PATH))
            {
                //从StreamingAssets中拷贝数据到沙盒中
                FireLog(LogLevel.Info, string.Format("首次运行，创建目录： {0}", LGlobalInfo.CLIENT_ROOT_PATH));
                Directory.CreateDirectory(LGlobalInfo.CLIENT_ROOT_PATH);
                CopyStreamingFiles();
            }
            else
            {
                VersionAuthenticate();
            }
        }

        /// <summary>
        /// 所有文件下载完成，该开始进入反射流程
        /// </summary>
        private void DownloadFinish()
        {
            FireDownloadFinish();
        }

        /// <summary>
        /// 版本验证
        /// </summary>
        private void VersionAuthenticate()
        {
            FireLog(LogLevel.Info, "开始版本验证");

            string channel = Loader.LoginProxy.Name;
            string phoneType = Application.platform.ToString();
            string version = LGlobalInfo.EngineVersion.ToString();

            var ver_auth_url = string.Format(LGlobalInfo.VERSION_AUTH_PATH, channel, phoneType, version);
            DownloadTask authTask = null;
            Task.Invoke(k =>
            {
                if (authTask == null)
                {
                    authTask = new DownloadTask(ver_auth_url);
                }

                if (authTask.IsDone)
                {
                    if (string.IsNullOrEmpty(authTask.ErrorMessage))
                    {
                        var result = Encoding.UTF8.GetString(authTask.Bytes);
                        Debug.LogWarning("版本验证 " + result);

                        if (result == "sucess")
                        {
                            DownloadFiles();
                        }
                        else
                        {
                            FireDownloadError(OpState.VersionAuth, result, null);
                        }
                    }
                    else
                    {
                        Debug.LogError("版本验证 " + authTask.ErrorMessage);
                        FireDownloadError(OpState.VersionAuth, authTask.ErrorMessage, null);
                    }

                    return true;
                }

                return false;
            });
        }

        /// <summary>
        /// （重新）拷贝缓存文件到本地
        /// </summary>
        public void CopyStreamingFiles()
        {
            loader.StartCoroutine(DownloadMatchList(LGlobalInfo.STREAM_MATCHLIST_PATH,
            true,
            () =>
            {
                loader.StartCoroutine(DownloadFiles(OpState.CopyStream));
            },
            null));
        }

        /// <summary>
        /// （重新）下载文件
        /// </summary>
        public void DownloadFiles()
        {
            loader.StartCoroutine(DownloadMatchList(LGlobalInfo.SERVER_MATCHLIST_PATH,
            false,
            () =>
            {
                loader.StartCoroutine(DownloadFiles(OpState.Download));
            },
            (error) =>
            {
                FireDownloadError(OpState.Matchlist, error, DownloadFiles);
            }));
        }

        /// <summary>
        /// 下载匹配列表文件
        /// </summary>
        /// <param name="url">资源路径</param>
        /// <param name="ignoreError">是否忽略错误继续执行</param>
        /// <param name="callback">成功回调</param>
        /// <param name="error">出错回调</param>
        /// <returns></returns>
        private IEnumerator<WWW> DownloadMatchList(string url, bool ignoreError, Action callback, Action<string> error)
        {
            url = System.Uri.EscapeUriString(url.Replace(@"\", "/"));

            //FireLog(LogLevel.Info, string.Format("开始下载matchlist.txt   {0}", url));

            using (WWW www = new WWW(url))
            {
                yield return www;

                if (www.error != null)
                {
                    FireLog(LogLevel.Error, string.Format("下载 【{0}】 错误， {1}", url, www.error));

                    if (ignoreError)
                    {
                        callback();
                    }
                    else
                    {
                        error(www.error);
                    }

                    yield break;
                }

                if (www.isDone)
                {
                    var b = www.bytes;
                    var ms = new MemoryStream();
                    ms.Write(b, 0, b.Length);
                    ms.Position = 0;
                    var sr = new StreamReader(ms, Encoding.UTF8);

                    newList = LoadUpdateList(sr);

                    FireLog(LogLevel.Debug, string.Format("matchlist.txt 下载成功, 共{0}个文件", newList.Count));

                    sr.Close();
                    sr.Dispose();
                    ms.Close();
                    ms.Dispose();

                    callback();
                }
            }
        }

        /// <summary>
        /// 对比新旧文件列表，下载所需资源
        /// </summary>
        private IEnumerator<int> DownloadFiles(OpState state)
        {
            if (newList == null)
            {
                newList = new List<FileUpdateItem>();
            }

            string oldListPath = LGlobalInfo.CLIENT_MATCHLIST_PATH;

            oldList = LoadUpdateList(oldListPath);

            List<FileUpdateItem> lstNeedUpdate = new List<FileUpdateItem>();
            List<FileUpdateItem> lstNeedDelete = new List<FileUpdateItem>();

            #region  先遍历oldList，看newList中是否更新
            if (oldList != null)
            {
                foreach (var oldItem in oldList)
                {
                    var isFind = newList.FirstOrDefault(o => o.path == oldItem.path);
                    //找到，对比md5
                    if (isFind != null)
                    {
                        //md5相同，无视
                        if (oldItem.md5 == isFind.md5)
                        {
                            continue;
                        }
                        //md5不同，更新
                        else
                        {
                            lstNeedUpdate.Add(isFind);
                        }
                    }
                    //未找到，删除旧的
                    else
                    {
                        lstNeedDelete.Add(oldItem);
                    }
                }
            }
            else
            {
                //下载所有newList中的
                lstNeedUpdate = newList;
            }
            #endregion

            #region 再遍历newList，找新加入的资源
            if (oldList != null)
            {
                foreach (var newItem in newList)
                {
                    var isFind = oldList.FirstOrDefault(o => o.path == newItem.path);
                    //找到，无视
                    if (isFind != null)
                    {
                        continue;
                    }
                    //未找到，加入所需更新列表
                    else
                    {
                        lstNeedUpdate.Add(newItem);
                    }
                }
            }
            #endregion

            curUpdateCount = 0;
            maxUpdateCount = lstNeedUpdate.Count;

            FireLog(LogLevel.Notice, string.Format("{0} 更新{1} ,删除{2}", state, maxUpdateCount, lstNeedDelete.Count));

            if (oldList == null)
            {
                oldList = new List<FileUpdateItem>();
            }

            foreach (var item in lstNeedDelete)
            {
                FileUtils.DeleteFile(LGlobalInfo.CLIENT_ROOT_PATH + item.path);

                FireLog(LogLevel.Info, string.Format("{0}时删除无用文件：{1}", state, item.path));

                oldList.Remove(item);

                yield return 1;
            }

            SaveUpdateList(oldListPath, oldList);

            FireLog(LogLevel.Notice, string.Format("准备开始{0}文件：{1}个", state, maxUpdateCount));

            string root_path = state == OpState.CopyStream ? LGlobalInfo.STREAM_ROOT_PATH : LGlobalInfo.SERVER_ROOT_PATH;
            foreach (var updateItem in lstNeedUpdate)
            {
                //需要进制转换
                if (state == OpState.CopyStream && LGlobalInfo.IsHexBinDecOct)
                {
                    var pathSplit = updateItem.path.Split('/');     //把正常目录分割出来
                    string asciiPath = "";
                    for (int i = 0; i < pathSplit.Length - 1; i++)
                    {
                        asciiPath += string_to_ascii(pathSplit[i]) + "/";
                    }
                    asciiPath += string_to_ascii(Path.GetFileNameWithoutExtension(updateItem.path)) + Path.GetExtension(updateItem.path);

                    loader.StartCoroutine(DownloadAndSaveToLocal(
                        root_path + asciiPath,
                        LGlobalInfo.CLIENT_ROOT_PATH + updateItem.path,
                        updateItem,
                        state,
                        OnWriteFile
                        ));

                }
                else
                {
                    //不需要进制转换，直接下载
                    loader.StartCoroutine(DownloadAndSaveToLocal(
                        root_path + updateItem.path,
                        LGlobalInfo.CLIENT_ROOT_PATH + updateItem.path,
                        updateItem,
                        state,
                        OnWriteFile
                        ));
                }

                yield return 1;
            }

            if (maxUpdateCount <= 0)
            {
                if (state == OpState.CopyStream) { VersionAuthenticate(); }
                else if (state == OpState.Download) { DownloadFinish(); }
            }
        }

        /// <summary>
        /// 触发下载【写入文件】完成回调
        /// </summary>
        /// <param name="isSucess"></param>
        /// <param name="fileUpdateItem"></param>
        private void OnWriteFile(bool isSucess, OpState state, FileUpdateItem fileUpdateItem)
        {
            Action retry = null;
            if (state == OpState.CopyStream) { retry = CopyStreamingFiles; }
            else if (state == OpState.Download) { retry = DownloadFiles; }

            if (!isSucess)
            {
                errorUpdateCount++;
                errorList.Add(fileUpdateItem);

                CheckDownloadError(state, retry);
                return;
            }

            curUpdateCount++;

            //将刚下载好的文件添加入matchlist.txt
            //判断一下是update还是add
            bool isFind = false;
            for (int i = 0; i < oldList.Count; i++)
            {
                if (oldList[i].path == fileUpdateItem.path)
                {
                    isFind = true;
                    oldList[i] = fileUpdateItem;
                    break;
                }
            }
            if (!isFind)
            {
                oldList.Add(fileUpdateItem);
            }

            //保存matchlist.txt
            SaveUpdateList(LGlobalInfo.CLIENT_MATCHLIST_PATH, oldList);
            //打印日志
            FireLog(LogLevel.Info, string.Format("{0}完成：{1}", state, fileUpdateItem.path));
            //触发下载完成事件，通知View层展现
            if (state == OpState.CopyStream) { FireCopyOneItem(curUpdateCount, maxUpdateCount); }
            else if (state == OpState.Download) { FireDownloadOneItem(curUpdateCount, maxUpdateCount); }

            //检测是否有下载失败
            CheckDownloadError(state, retry);

            if (curUpdateCount >= maxUpdateCount)
            {
                if (state == OpState.CopyStream) { VersionAuthenticate(); }
                else if (state == OpState.Download) { DownloadFinish(); }
            }
        }

        /// <summary>
        /// 如果有下载失败的，则触发下载失败事件
        /// </summary>
        private void CheckDownloadError(OpState state, Action retryAction)
        {
            if (curUpdateCount + errorUpdateCount >= maxUpdateCount
                && errorUpdateCount > 0)
            {
                FireDownloadError(state, "", retryAction);

                //这里似乎不能赋值给newList
                newList = errorList;

                errorUpdateCount = 0;
                errorList.Clear();
            }
        }

        /// <summary>
        /// 加载 matchlist.txt 文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private List<FileUpdateItem> LoadUpdateList(string path)
        {
            if (!File.Exists(path))
            {
                //FireLog(LogLevel.Error, string.Format("未找到:{0}", path));

                return null;
            }

            return LoadUpdateList(new StreamReader(path));
        }


        /// <summary>
        /// 加载 matchlist.txt 文件
        /// </summary>
        /// <param name="sr"></param>
        /// <returns></returns>
        private static List<FileUpdateItem> LoadUpdateList(StreamReader sr)
        {
            List<FileUpdateItem> lstFileUpdate = new List<FileUpdateItem>();
            while (sr.Peek() >= 0)
            {
                var s = sr.ReadLine().Split(',');
                if (s.Length != 3)
                {
                    continue;
                }

                lstFileUpdate.Add(new FileUpdateItem()
                {
                    path = s[0],
                    md5 = s[1],
                    size = s[2]
                });
            }

            sr.Close();
            sr.Dispose();
            return lstFileUpdate;
        }


        /// <summary>
        /// 保存文件更新列表
        /// </summary>
        /// <param name="path"></param>
        /// <param name="info"></param>
        private static void SaveUpdateList(string path, List<FileUpdateItem> info)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            FileUtils.DeleteFile(path);
            info = info.OrderBy(o => o.path).ToList();
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (var item in info)
                {
                    sw.WriteLine(string.Format("{0},{1},{2}", item.path.Replace(@"\", "/"), item.md5, item.size));
                    sw.Flush();
                }
            }
        }


        /// <summary>
        /// 下载并保存文件到本地
        /// </summary>
        /// <param name="url">服务端地址</param>
        /// <param name="savepath">要保存到的本地地址</param>
        /// /// <param name="item">用于传给回调的参数</param>
        /// <param name="callback">回调方法</param>
        /// <returns></returns>
        private IEnumerator<WWW> DownloadAndSaveToLocal(string url, string savepath, FileUpdateItem item, OpState state, Action<bool, OpState, FileUpdateItem> callback)
        {
            url = System.Uri.EscapeUriString(url.Replace(@"\", "/"));

            using (WWW w = new WWW(url))
            {
                yield return w;

                if (w.error != null)
                {
                    FireLog(LogLevel.Error, string.Format("下载 [{0}] 错误， {1}", url, w.error));
                    if (callback != null)
                    {
                        callback(false, state, item);
                    }
                    yield break;
                }

                if (w.isDone)
                {
                    byte[] bytes = w.bytes;

                    FileInfo fi = new FileInfo(savepath);
                    if (!Directory.Exists(fi.DirectoryName))
                    {
                        Directory.CreateDirectory(fi.DirectoryName);
                    }

                    //写入模型到本地
                    bool ret = FileUtils.SaveFile(savepath, bytes);
                    if (callback != null)
                    {
                        callback(ret, state, item);
                    }
                }
            }
        }

        /// <summary>
        /// 字符串转ascii码
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string string_to_ascii(string name)
        {
            string ascii = "";

            for (int i = 0; i < name.Length; i++)
            {
                var c = name[i];
                ascii += ((uint)c).ToString("X4");
            }

            return ascii;
        }

        /// <summary>
        /// ascii转string
        /// </summary>
        /// <param name="asc"></param>
        /// <returns></returns>
        public static string ascii_to_string(string asc)
        {
            if (asc.Length % 4 != 0)
            {
                Debug.LogError("ASC长度错误 " + asc);

                return asc;
            }

            string str = "";

            var chararray = SplitByLen(asc, 4);
            for (int i = 0; i < chararray.Length; i++)
            {
                char c = (char)(Convert.ToInt32((chararray[i]), 16));
                str += c;
            }

            return str;
        }

        /// <summary>  
        /// 按长度切分字符串成数组  
        /// </summary>  
        /// <param name="str">原字符串</param>  
        /// <param name="separatorCharNum">切分长度</param>  
        /// <returns>字符串数组</returns>  
        public static string[] SplitByLen(string str, int separatorCharNum)
        {
            //http://blog.csdn.net/yenange/article/details/39637211

            if (string.IsNullOrEmpty(str) || str.Length <= separatorCharNum)
            {
                return new string[] { str };
            }
            string tempStr = str;
            List<string> strList = new List<string>();
            int iMax = Convert.ToInt32(Math.Ceiling(str.Length / (separatorCharNum * 1.0)));//获取循环次数  
            for (int i = 1; i <= iMax; i++)
            {
                string currMsg = tempStr.Substring(0, tempStr.Length > separatorCharNum ? separatorCharNum : tempStr.Length);
                strList.Add(currMsg);
                if (tempStr.Length > separatorCharNum)
                {
                    tempStr = tempStr.Substring(separatorCharNum, tempStr.Length - separatorCharNum);
                }
            }
            return strList.ToArray();
        }
    }

}

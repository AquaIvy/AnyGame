using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Update
{
    /// <summary>
    /// 更新列表item
    /// </summary>
    public class FileUpdateItem
    {
        /// <summary>
        /// 路径，非全路径
        /// </summary>
        public string path;

        /// <summary>
        /// 文件md5
        /// </summary>
        public string md5;

        /// <summary>
        /// 文件大小
        /// </summary>
        public string size;
    }
}

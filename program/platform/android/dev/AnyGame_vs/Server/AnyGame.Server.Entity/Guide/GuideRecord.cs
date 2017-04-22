using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnyGame.Server.Entity.Guide
{
    /// <summary>
    /// 新手引导的数据
    /// </summary>
    public class GuideRecord
    {
        /// <summary>
        /// 新手引导的类型
        /// </summary>
        public GuideTypes Type { get; set; }

        /// <summary>
        /// 是否通过
        /// </summary>
        public bool IsPass { get; set; }
    }
}

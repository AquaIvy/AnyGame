using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    /// <summary>
    /// 主线程回调任务
    /// </summary>
    public class Task
    {
        /// <summary>
        /// 累计时间
        /// </summary>
        public int Timer;

        /// <summary>
        /// 当前执行过的帧
        /// </summary>
        public int Frame;

        /// <summary>
        /// 需要延迟多少帧执行
        /// </summary>
        public int DelayFrame;

        /// <summary>
        /// 关联数据
        /// </summary>
        public object Tag;

        /// <summary>
        /// 执行函数
        /// 注意，timer的时间为时间启动后的时间，而不是间隔时间
        /// </summary>
        public Func<int, bool> Func { get; set; }

        /// <summary>
        /// 是否完成
        /// </summary>
        public volatile bool IsDone;

        /// <summary>
        /// 是否初始化
        /// </summary>
        public volatile bool IsInited;

        /// <summary>
        /// 错误次数
        /// </summary>
        public int ErrorCount;


        /// <summary>
        /// 释放这个任务
        /// </summary>
        public void Release()
        {
            Release(this);
        }

        #region 静态方法

        private static List<Task> m_invokeActions = new List<Task>();
        private static List<Task> m_willAddInvokeActions = new List<Task>();

        /// <summary>
        /// 压入一个执行函数
        /// 如果函数执行完成返回true
        /// 如果返回false，则再下一帧还会继续调用
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Task Invoke(Func<int, bool> action, int delayFrame = 0, object tag = null)
        {
            var r = new Task();
            r.Func = action;
            r.DelayFrame = delayFrame;
            r.Tag = tag;
            m_willAddInvokeActions.Add(r);
            return r;
        }

        public static void Update(int elapseTime)
        {
            if (m_willAddInvokeActions.Count > 0)
            {
                m_invokeActions.AddRange(m_willAddInvokeActions);
                m_willAddInvokeActions.Clear();
            }

            if (m_invokeActions.Count > 0)
            {
                for (var i = 0; i < m_invokeActions.Count; i++)
                {
                    m_invokeActions[i].Timer += elapseTime;
                    m_invokeActions[i].Frame++;
                    if (m_invokeActions[i].Frame >= m_invokeActions[i].DelayFrame)
                    {
                        try
                        {
                            m_invokeActions[i].IsDone = m_invokeActions[i].Func(m_invokeActions[i].Timer);

                            if (m_invokeActions[i].IsDone)
                            {
                                m_invokeActions.RemoveAt(i);
                                i--;
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.LogErrorFormat("run game.invoke fail. {0}", ex.Message);
                            //  这里记录错误次数，超过3次就放弃这个任务
                            if (m_invokeActions[i].ErrorCount++ > 3)
                            {
                                m_invokeActions.RemoveAt(i);
                                i--;
                            }
                        }

                    }
                }
            }
        }


        private static void Release(Task task)
        {
            m_willAddInvokeActions.Remove(task);
            m_invokeActions.Remove(task);
        }

        public static void ReleaseAll()
        {
            m_willAddInvokeActions.ForEach(o => o.Release());
            m_invokeActions.ForEach(o => o.Release());
        }

        #endregion
    }

}

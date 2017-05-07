
using DogSE.Library.Log;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AnyGame.Content.Manager
{
    public enum CanvasType
    {
        Control = 0,
        Tips,
        Anim,
        Announcement,
        FadeInOut,
    }

    public class CanvasMgr
    {
        #region 交互映射文件
        private static Dictionary<string, Dictionary<string, string>> interactionMap = new Dictionary<string, Dictionary<string, string>>();

        /// <summary>
        /// 获得一个控件的路径映射
        /// </summary>
        /// <param name="fileName">控件所在文件,如Login Main Bag</param>
        /// <param name="ctrlName">控件名</param>
        /// <returns></returns>
        //public static string GetPathMap(string fileName, string ctrlName)
        //{
        //    if (interactionMap.ContainsKey(fileName))
        //    {
        //        if (interactionMap[fileName].ContainsKey(ctrlName))
        //        {
        //            return interactionMap[fileName][ctrlName];
        //        }
        //        else
        //        {
        //            Logs.Error("CanvasMgr映射不存在，美术是不是又干了什么好事。{0}   {1}", fileName, ctrlName);
        //            return string.Empty;
        //        }
        //    }

        //    string relativeMap = SceneMgr.RelativeMap(fileName + "_ab");
        //    relativeMap = relativeMap == "" ? "" : relativeMap + "/";
        //    var newmap = LoadMapFile(GlobalInfo.RES_GAME_UI + relativeMap + fileName + ".ctrlconfig");
        //    interactionMap[fileName] = newmap;

        //    if (interactionMap[fileName].ContainsKey(ctrlName))
        //    {
        //        return interactionMap[fileName][ctrlName];
        //    }
        //    else
        //    {
        //        Logs.Error("CanvasMgr映射不存在，美术是不是又干了什么好事。{0}   {1}", fileName, ctrlName);
        //        return string.Empty;
        //    }

        //}

        /// <summary>
        /// 加载映射文件（游戏物体名称与路径结构的映射）
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static Dictionary<string, string> LoadMapFile(string filepath)
        {
            Dictionary<string, string> newmap = new Dictionary<string, string>();

            var jsInfo = JsonMgr.Load(filepath);
            foreach (var item in jsInfo)
            {
                var name = item["name"].ToString();
                var path = item["Path"].ToString();
                newmap[name] = path;
            }

            return newmap;
        }

        #endregion


        #region Canvas查找
        public static string basicControl = "/Canvas_Control";
        public static string basicTips = "/Canvas_Tips";
        public static string basicAnim = "/Canvas_Anim";
        public static string basicAnnouncement = "/Canvas_Announcement";
        public static string basicFadeInOut = "/Canvas_FadeInOut";


        public static GameObject FindControl(string name)
        {
            return GameObject.Find(basicControl + "/" + name);
        }

        public static GameObject FindTips(string name)
        {
            return GameObject.Find(basicTips + "/" + name);
        }

        public static GameObject FindAnim(string name)
        {
            return GameObject.Find(basicAnim + "/" + name);
        }

        public static GameObject FindAnnouncement(string name)
        {
            return GameObject.Find(basicAnnouncement + "/" + name);
        }

        public static GameObject FindFadeInOut(string name)
        {
            return GameObject.Find(basicFadeInOut + "/" + name);
        }

        #endregion

        public static Transform Control { get; private set; }
        public static Transform Tips { get; private set; }
        public static Transform Anim { get; private set; }
        public static Transform Announcement { get; private set; }
        public static Transform FadeInOut { get; private set; }

        public static void Init()
        {
            var tmpControl = GameObject.Find(basicControl);
            if (tmpControl != null)
            {
                Control = tmpControl.transform;
            }

            var tmpTips = GameObject.Find(basicTips);
            if (tmpTips != null) { Tips = tmpTips.transform; }

            var tmpAnim = GameObject.Find(basicAnim);
            if (tmpAnim != null) { Anim = tmpAnim.transform; }

            var tmpAnnouncement = GameObject.Find(basicAnnouncement);
            if (tmpAnnouncement != null) { Announcement = tmpAnnouncement.transform; }

            var tmpFadeInOut = GameObject.Find(basicFadeInOut);
            if (tmpFadeInOut != null)
            {
                FadeInOut = tmpFadeInOut.transform;
            }
        }

        /// <summary>
        /// （协程）清空一个画布下所有物体
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="onFinish"></param>
        /// <returns></returns>
        public static IEnumerator<int> Clear(Transform canvas, Action onFinish = null)
        {
            if (canvas == null)
            {
                Logs.Error("canvas == null {0}", canvas.name);
                yield break;
            }

            var canvasChildsCount = canvas.childCount;
            for (int i = 0; i < canvasChildsCount; i++)
            {
                var child = canvas.transform.GetChild(i);
                if (child != null)
                {
                    GameObject.Destroy(child.gameObject);
                    Logs.Info("Canvas Clear  {0}", child.name);

                    //一般来说一个画布下只有三两个物体，所以每次都返回问题也不大
                    yield return i;
                }
            }

            if (onFinish != null)
            {
                onFinish();
            }
        }

        /// <summary>
        /// 清空一个画布下所有物体 
        /// （如果Clear后还有其他操作，必须配合Game.Invoke使用，不建议使用本函数）
        /// </summary>
        /// <param name="canvas"></param>
        public static void Clear(Transform canvas)
        {
            if (canvas == null)
            {
                Logs.Error("canvas == null {0}", canvas.name);
                return;
            }

            var canvasChildsCount = canvas.childCount;
            for (int i = 0; i < canvasChildsCount; i++)
            {
                var child = canvas.transform.GetChild(i);
                if (child != null)
                {
                    GameObject.Destroy(child.gameObject);
                }
            }
        }
    }
}

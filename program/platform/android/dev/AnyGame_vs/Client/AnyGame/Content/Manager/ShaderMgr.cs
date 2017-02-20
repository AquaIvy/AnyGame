
using DogSE.Library.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AnyGame.Content.Manager
{
    class ShaderMgr
    {
        public static Shader uietc1 { get; private set; }
        public static Shader uipvrtc { get; private set; }


        public static void Init()
        {
            uietc1 = Load("UIETC");
            //uipvrtc = Load("UIPVRTC");
        }


        public static Shader Load(string name)
        {
            string path = GlobalInfo.RES_SHADER + name + @".assetbundle";
            var obj = SceneMgr.LoadObject(path, name);
            if (obj == null)
            {
                Logs.Error("Shader 【{0}】==null", name);
                return null;
            }

            var shader = (Shader)obj;

            return shader;
        }


    }
}

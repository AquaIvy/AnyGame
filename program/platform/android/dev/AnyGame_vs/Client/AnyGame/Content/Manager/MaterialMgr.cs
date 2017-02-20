
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AnyGame.Content.Manager
{
    public class MaterialMgr
    {
        public static Dictionary<string, Material> allMaterials = new Dictionary<string, Material>();

        public static void Init()
        {
            Load("");
        }

        public static Material Load(string name)
        {
            if (allMaterials.ContainsKey(name))
            {
                return allMaterials[name];
            }

            string path = GlobalInfo.RES_MATERIAL + name + @".assetbundle";
            var mat = ContentMgr.LoadObject<Material>(path, name);
            if (mat == null) { return null; }
            allMaterials[name] = mat;

            return mat;
        }
    }
}

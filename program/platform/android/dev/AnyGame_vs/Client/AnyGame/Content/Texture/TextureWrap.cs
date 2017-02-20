using AnyGame.Content.Manager;

using DogSE.Library.Log;
using LitJson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AnyGame.Content.Texture
{
    /// <summary>
    /// Texture包装类
    /// </summary>
    public class TextureWrap
    {
        /// <summary>
        /// 文件名  例：ui/texture_0
        /// </summary>
        public string name { get; private set; }

        /// <summary>
        /// 纹理名  ui  card  battle
        /// </summary>
        public string textureName { get; private set; }

        /// <summary>
        /// 纹理路径
        /// </summary>
        public string texturePath { get; private set; }

        /// <summary>
        /// tpsheet路径
        /// </summary>
        public string tpsheetPath { get; private set; }

        //public TextureFormat format { get; private set; }
        public string format { get; private set; }

        /// <summary>
        /// 纹理宽
        /// </summary>
        public int width { get; private set; }

        /// <summary>
        /// 纹理高
        /// </summary>
        public int height { get; private set; }

        public Texture2D mainTex { get; private set; }
        public Texture2D alphaTex { get; private set; }

        public Material material { get; private set; }
        public Shader shader { get; private set; }

        public Dictionary<string, SpriteSample> dicSample { get; private set; }
        public Dictionary<string, SpriteWrap> dicSpriteWrap { get; private set; }



        public bool isLoaded
        {
            get
            {
                return mainTex != null;
            }
        }

        public bool isAutoGC = true;        //引用为0时是否自动释放

        public TextureWrap(string texturePath)
        {
            this.texturePath = Path.ChangeExtension(texturePath, TextureMgr.ext_texture);
            this.tpsheetPath = texturePath;

            dicSample = new Dictionary<string, SpriteSample>();
            dicSpriteWrap = new Dictionary<string, SpriteWrap>();

            LoadTpsheet();
        }

        /// <summary>
        /// 加载该纹理所有Sample裁切信息
        /// </summary>
        public void LoadTpsheet()
        {
            string path = tpsheetPath;
            if (!File.Exists(path)) { return; }

            var info = File.ReadAllLines(path, Encoding.UTF8);
            //string combinname = string.Empty;

            //把string信息分离成对象
            for (int i = 0; i < info.Length; i++)
            {
                string str = info[i];

                //设置头信息（第一行）
                if (i == 0)
                {
                    if (str[0] != '#')
                    {
                        Logs.Error("Tpsheet 【{0}】 str[0] != '#'", path);
                        return;
                    }

                    JsonData headInfo = JsonMapper.ToObject(str.TrimStart('#'));
                    this.textureName = headInfo["TextureName"].ToString();
                    this.name = this.textureName + "/" + headInfo["Name"].ToString();
                    this.format = headInfo["Format"].ToString();
                    this.width = Convert.ToInt32(headInfo["Width"].ToString());
                    this.height = Convert.ToInt32(headInfo["Height"].ToString());
                }
                //剩下的都是内容信息
                else
                {
                    if (string.IsNullOrEmpty(str)
                        || str[0] == '#'
                        || str[0] == ':')
                    {
                        Logs.Error("Tpsheet 【{0}】 第{1}行信息可能错误", path, (i + 1).ToString());
                        continue;
                    }

                    var s = str.Split('\t');
                    var rect = s[1].Split(',');
                    var source = s[2].Split(',');
                    var offset = s[3].Split(',');

                    //前7位，基本信息
                    SpriteSample sample = new SpriteSample()
                    {
                        name = s[0],
                        textureName = this.name,
                        rect = new Rect
                        {
                            x = Convert.ToInt32(rect[0]),
                            y = Convert.ToInt32(rect[1]),
                            width = Convert.ToInt32(rect[2]),
                            height = Convert.ToInt32(rect[3])
                        },
                        sourceSize = new Vector2
                        {
                            x = Convert.ToInt32(source[0]),
                            y = Convert.ToInt32(source[1])
                        },
                        offset = new Vector2
                        {
                            x = Convert.ToInt32(offset[0]),
                            y = Convert.ToInt32(offset[1])
                        }
                    };

                    sample.size = new Vector2(sample.rect.width, sample.rect.height);

                    dicSample[sample.name] = sample;
                    TextureMgr.AddSpriteMap(sample.name, sample.textureName);
                }

            }

        }

        /// <summary>
        /// 创建一个精灵
        /// </summary>
        /// <param name="spriteName"></param>
        /// <returns></returns>
        public SpriteWrap CreateSprite(string spriteName, Vector4 border)
        {
            if (!dicSample.ContainsKey(spriteName))
            {
                Logs.Error("【{0}】 未包含sample 【{1}】", name, spriteName);
                return null;
            }

            LoadTexture();

            SpriteWrap sprite = new SpriteWrap(this, dicSample[spriteName], border);

            return sprite;
        }

        public void LoadTexture()
        {
            if (isLoaded)
            {
                return;
            }

            if (Application.platform == RuntimePlatform.WindowsEditor
                || Application.platform == RuntimePlatform.OSXEditor)
            {
                LoadPngTexture(name, this);
            }
            else
            {
                if (format == "Platform")
                {
                    LoadPngTexture(name, this);
                }
                else if (format == "ETC")
                {
                    LoadPkmTexture(name, this);
                }
                else if (format == "PVR")
                {
                    LoadPvrTexture(name, this);
                }
            }

        }

        public int quote { get; private set; }
        public void AddRefCounter()
        {
            quote++;
        }

        public void DecRefCounter()
        {
            quote--;
            if (quote < 0)
            {
                Logs.Error("{0}引用计数小于0", name);
            }
        }

        #region 加载纹理

        private static void LoadPngTexture(string textureName, TextureWrap textureWrap)
        {
            string mainPath = Path.GetFullPath(GlobalInfo.RES_IMAGE + textureName + @".png");

            var mainBytes = File.ReadAllBytes(mainPath);

            textureWrap.mainTex = CreateTexture2DByBytes(mainPath, textureWrap.width, textureWrap.height, TextureFormat.RGBA32, mainBytes);
            textureWrap.mainTex.name = textureName + @".png";

            textureWrap.material = null;
        }

        private static void LoadPkmTexture(string textureName, TextureWrap textureWrap)
        {
            string mainPath = Path.GetFullPath(GlobalInfo.RES_IMAGE + textureName + @".pkm");
            string alphaPath = Path.GetFullPath(GlobalInfo.RES_IMAGE + textureName + @"_alpha.pkm");

            var mainBytes = File.ReadAllBytes(mainPath);
            var alphaBytes = File.ReadAllBytes(alphaPath);

            textureWrap.mainTex = CreateTexture2DByBytes(mainPath, textureWrap.width, textureWrap.height, TextureFormat.ETC_RGB4, mainBytes);
            textureWrap.alphaTex = CreateTexture2DByBytes(alphaPath, textureWrap.width, textureWrap.height, TextureFormat.ETC_RGB4, alphaBytes);
            textureWrap.mainTex.name = textureName + @".pkm";
            textureWrap.alphaTex.name = textureName + @"_alpha.pkm";

            Shader shader = ShaderMgr.uietc1;
            Material material = new Material(shader);
            material.SetTexture("_MainTex", textureWrap.mainTex);
            material.SetTexture("_AlphaTex", textureWrap.alphaTex);

            textureWrap.shader = shader;
            textureWrap.material = material;
        }

        private static void LoadPvrTexture(string textureName, TextureWrap textureWrap)
        {
            string mainPath = Path.GetFullPath(GlobalInfo.RES_IMAGE + textureName + @".pvr");

            var mainBytes = File.ReadAllBytes(mainPath);

            textureWrap.mainTex = CreateTexture2DByBytes(mainPath, textureWrap.width, textureWrap.height, TextureFormat.PVRTC_RGBA4, mainBytes);
            textureWrap.mainTex.name = textureName + @".pvr";

            //Shader shader = ShaderMgr.uipvrtc;
            //Material material = new Material(shader);
            //material.SetTexture("_MainTex", textureWrap.mainTex);

            //textureWrap.shader = shader;
        }

        public static Texture2D CreateTexture2DByBytes(string path, int width, int height, TextureFormat format, byte[] bytes)
        {
            var ext = Path.GetExtension(path);
            if (ext == ".png")
            {
                Texture2D t2d = new Texture2D(width, height);
                t2d.LoadImage(bytes);

                return t2d;
            }
            else if (ext == ".pkm")
            {
                int headerSize = 16;
                byte[] buffer = new byte[bytes.Length - headerSize];
                System.Buffer.BlockCopy(bytes, headerSize, buffer, 0, bytes.Length - headerSize);
                Texture2D t2d = new Texture2D(width, height, format, false, true);
                t2d.LoadRawTextureData(buffer);
                t2d.Apply();

                return t2d;
            }
            else if (ext == ".pvr")
            {
                if (bytes[0] == 0x11)
                {
                    Logs.Error("bytes[0] == 0x11");
                }

                int headerSize = 52;
                byte[] buffer = new byte[bytes.Length - headerSize];
                System.Buffer.BlockCopy(bytes, headerSize, buffer, 0, bytes.Length - headerSize);
                Texture2D t2d = new Texture2D(width, height, format, false, true);
                t2d.LoadRawTextureData(buffer);
                t2d.wrapMode = TextureWrapMode.Clamp;
                t2d.filterMode = FilterMode.Point;
                t2d.Apply();

                return t2d;
            }
            else
            {
                Logs.Error("未知扩展名 {0}", ext);
                throw new Exception();
            }
        }

        #endregion

        #region 释放纹理

        public void Dispose()
        {
            //if (atlasItem.shader != null)
            //{
            //    Shader.Destroy(atlasItem.shader);
            //}

            if (this.material != null)
            {
                Material.Destroy(this.material);
            }

            //if (this.dicSample != null)
            //{
            //    this.dicSample.Clear();
            //}

            if (this.mainTex != null)
            {
                Texture2D.Destroy(this.mainTex);
            }

            if (this.alphaTex != null)
            {
                Texture2D.Destroy(this.alphaTex);
            }

            //GC.Collect();
        }

        #endregion

    }
}

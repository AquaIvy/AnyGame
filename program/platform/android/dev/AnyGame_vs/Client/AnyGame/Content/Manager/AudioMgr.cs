
using DogSE.Library.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AnyGame.Content.Manager
{
    public class AudioMgr
    {
        private static Dictionary<string, AudioClip> allAudioClip = new Dictionary<string, AudioClip>();
        private static GameObject audioParent = null;

        public static void Init()
        {
            //创建AudioMgr 物体
            if (GameObject.Find("/AudioMgr") == null)
            {
                GameObject go = new GameObject("AudioMgr");
                AudioMgr.audioParent = go;
            }
        }

        public static IEnumerator<WWW> Load_WWW(string name, Action onFinish)
        {

            string path = GlobalInfo.RES_AUDIO + name + @".mp3";
            if (!File.Exists(path))
            {
                Logs.Info("Audio 【{0}】 is null.", name);
                yield break;
            }


            using (WWW www = new WWW(path))
            {
                yield return www;

                if (www.error != null)
                {
                    Logs.Info("www download error. 【{0}】  {1}.", name, www.error);

                    yield break;
                }

                if (www.isDone)
                {
                    AudioClip audioClip = www.GetAudioClip(true, true, AudioType.MPEG);
                    Logs.Info("www.isDone " + www.isDone);
                    audioClip.name = name;
                    allAudioClip[name] = audioClip;

                    GameObject go = new GameObject(name);
                    go.transform.parent = audioParent.transform;

                    var audioSource = go.AddComponent<AudioSource>();
                    audioSource.clip = audioClip;
                    audioSource.Play();
                    Logs.Info("audioClip.length  {0}  {1}", name, audioClip.length);
                }
                else
                {
                    Logs.Error("no  done??");
                }
            }
        }

        public static AudioClip Load_WWW(string name)
        {
            if (allAudioClip.ContainsKey(name))
            {
                return allAudioClip[name];
            }

            string path = GlobalInfo.RES_AUDIO + name + @".mp3";
            if (!File.Exists(path))
            {
                Logs.Info("Audio 【{0}】 is null.", name);
                return null;
            }

            WWW www = new WWW(path);
            AudioClip audioClip = www.GetAudioClip(true, true, AudioType.MPEG);
            Logs.Info("www.isDone " + www.isDone);
            audioClip.name = name;
            allAudioClip[name] = audioClip;

            return audioClip;
        }

        public static AudioClip Load(string name)
        {
            if (allAudioClip.ContainsKey(name))
            {
                return allAudioClip[name];
            }

            string path = GlobalInfo.RES_AUDIO + name + @".assetbundle";
            var obj = SceneMgr.LoadObject(path, name);
            if (obj == null)
            {
                Logs.Info("Audio 【{0}】 is null.", name);
                return null;
            }

            AudioClip audioClip = (AudioClip)obj;
            audioClip.name = name;
            allAudioClip[name] = audioClip;

            return audioClip;
        }

        public static void PlayEffect(string name)
        {
            AudioClip audioClip = Load(name);
            if (audioClip == null)
            {
                Logs.Info("Audio 【{0}】 is null.", name);
                return;
            }

            GameObject go = new GameObject(name);
            go.transform.parent = audioParent.transform;

            var audioSource = go.AddComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.Play();
            Logs.Info("audioClip.length  {0}  {1}", name, audioClip.length);
            //GameObject.Destroy(go, audioClip.length);
        }

        public static void PlayBGM(string name)
        {
            AudioClip audioClip = Load(name);
            if (audioClip == null)
            {
                Logs.Info("Audio 【{0}】 is null.", name);
                return;
            }

            GameObject go = new GameObject("_BGM");
            go.transform.parent = audioParent.transform;

            var audioSource = go.AddComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.loop = true;
            audioSource.Play();
        }

        public static void Stop(string name)
        {
            var bgm = GameObject.Find("/AudioMgr/_BGM");
            if (bgm == null)
            {
                return;
            }

            var audioSource = bgm.GetComponent<AudioSource>();
            audioSource.Stop();
        }

        public static void Dispose(string name)
        {
            if (!allAudioClip.ContainsKey(name))
            {
                return;
            }

            Stop(name);
            AudioClip.DestroyImmediate(allAudioClip[name], true);
        }
    }
}

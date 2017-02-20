using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace Assets.Script.LoginProxy
{
    /// <summary>
    /// 服务器状态信息
    /// </summary>
    class LGameServerStatus
    {
        public LGameServerStatus()
        {
            GameServers = new List<GameServer>();
        }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// 登录令牌(通用于登录所有服务器）
        /// </summary>
        public string LoginTokon { get; set; }

        /// <summary>
        /// 公告
        /// </summary>
        public string Notice { get; set; }

        /// <summary>
        /// 公告版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 是否每次都显示
        /// </summary>
        public bool IsShowEveryTime { get; set; }


        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }


        /// <summary>
        /// 平台用户id
        /// </summary>
        public string PlatformAccountId { get; set; }

        /// <summary>
        /// 服务器列表
        /// </summary>
        public List<GameServer> GameServers { get; private set; }

        /// <summary>
        /// 从xml文件里获得服务器状态信息
        /// </summary>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static LGameServerStatus FromXmlString(string xmlString)
        {
            var ret = new LGameServerStatus();
            try
            {
                XElement root = XElement.Parse(xmlString);

                //var notice = root.Element("LoginResult");
                var notice = root;

                ret.Error = notice.GetString("Error");
                ret.LoginTokon = notice.GetString("LoginTakon");
                ret.IsShowEveryTime = notice.GetBool("IsShowEveryTime", true);
                ret.Notice = notice.GetString("Notice");
                ret.Version = notice.GetString("Version");
                ret.NickName = notice.GetString("NickName");
                ret.PlatformAccountId = notice.GetString("PlatformAccountId");

                foreach (var serverNode in root.Element("GameServers").Elements("GameServer"))
                {
                    var server = new GameServer
                    {
                        GameZoneId = serverNode.GetInt("GameZoneId"),
                        Name = serverNode.GetString("Name"),
                        Status = (ServerStatus)Enum.Parse(typeof(ServerStatus), serverNode.GetString("Status", ServerStatus.Maintain.ToString())),
                        Host = serverNode.GetString("Host"),
                        Port = serverNode.GetInt("Port", 4530),
                        CharacterName = serverNode.GetString("CharacterName"),
                        //Recommend = serverNode.GetAttrBool("Recommend")
                    };
                    ret.GameServers.Add(server);
                }

            }
            catch (Exception ex)
            {
                Debug.LogError("init GameServerStatus xml fail." + ex.ToString());
            }

            return ret;
        }

    }

    /// <summary>
    /// Xml的一些扩展方法
    /// </summary>
    public static class XmlExtend
    {
        /// <summary>
        /// 获得一个xml节点的bool值
        /// </summary>
        /// <param name="node"></param>
        /// <param name="elementsName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool GetBool(this XElement node, string elementsName, bool defaultValue = false)
        {
            var valueNode = node.Element(elementsName);
            if (valueNode == null)
                return defaultValue;
            bool retValue;
            if (bool.TryParse(valueNode.Value, out retValue))
                return retValue;
            return defaultValue;
        }

        /// <summary>
        /// 获得一个xml节点的int值
        /// </summary>
        /// <param name="node"></param>
        /// <param name="elementsName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetAttrInt(this XElement node, string elementsName, int defaultValue = 0)
        {
            var valueNode = node.Attribute(elementsName);
            if (valueNode == null)
                return defaultValue;
            int retValue;
            if (int.TryParse(valueNode.Value, out retValue))
                return retValue;
            return defaultValue;
        }

        /// <summary>
        /// 获得一个xml节点的bool值
        /// </summary>
        /// <param name="node"></param>
        /// <param name="elementsName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool GetAttrBool(this XElement node, string elementsName, bool defaultValue = false)
        {
            var valueNode = node.Attribute(elementsName);
            if (valueNode == null)
                return defaultValue;
            bool retValue;
            if (bool.TryParse(valueNode.Value, out retValue))
                return retValue;
            return defaultValue;
        }

        /// <summary>
        /// 获得一个xml节点的字符串数据
        /// </summary>
        /// <param name="node"></param>
        /// <param name="elementsName"></param>
        /// <param name="defaultValue">如果默认值是null，实际返回的是string.Empty</param>
        /// <returns></returns>
        public static string GetAttrString(this XElement node, string elementsName, string defaultValue = null)
        {
            if (defaultValue == null)
                defaultValue = string.Empty;
            var valueNode = node.Attribute(elementsName);
            if (valueNode == null)
                return defaultValue;

            return valueNode.Value;
        }


        /// <summary>
        /// 获得一个xml节点的字符串数据
        /// </summary>
        /// <param name="node"></param>
        /// <param name="elementsName"></param>
        /// <param name="defaultValue">如果默认值是null，实际返回的是string.Empty</param>
        /// <returns></returns>
        public static string GetString(this XElement node, string elementsName, string defaultValue = null)
        {
            if (defaultValue == null)
                defaultValue = string.Empty;
            var valueNode = node.Element(elementsName);
            if (valueNode == null)
                return defaultValue;

            return valueNode.Value;
        }

        /// <summary>
        /// 获得一个xml节点的字符串数据
        /// </summary>
        /// <param name="node"></param>
        /// <param name="elementsName"></param>
        /// <param name="defaultValue">如果默认值是null，实际返回的是string.Empty</param>
        /// <returns></returns>
        public static int GetInt(this XElement node, string elementsName, int defaultValue = 0)
        {
            var valueNode = node.Element(elementsName);
            if (valueNode == null)
                return defaultValue;

            int ret;
            if (int.TryParse(valueNode.Value, out ret))
                return ret;
            return defaultValue;
        }
    }

    /// <summary>
    /// 游戏服务器
    /// </summary>
    class GameServer
    {
        /// <summary>
        /// 服务器id
        /// </summary>
        public int GameZoneId { get; set; }

        /// <summary>
        /// 服务器名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 服务器状态
        /// </summary>
        public ServerStatus Status { get; set; }

        /// <summary>
        /// 服务器地址
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 服务器端口号
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 是否为当前的推荐服务器
        /// </summary>
        public bool Recommend { get; set; }

        /// <summary>
        /// 玩家在服务器里的角色名
        /// </summary>
        public string CharacterName { get; set; }


#if DEBUG
        public override string ToString()
        {
            return string.Format("ServerId={0}  Name={1}    Status={2}  ServerAddress={3}   Port={4}", GameZoneId, Name, Status, Host, Port);
        }
#endif
    }

    /// <summary>
    /// 服务器状态
    /// </summary>
    public enum ServerStatus
    {
        /// <summary>
        /// 维护中
        /// </summary>
        Maintain = 0,

        /// <summary>
        /// 开放中
        /// </summary>
        Open = 1,

        /// <summary>
        /// 繁忙
        /// </summary>
        Busy = 2,

        /// <summary>
        /// 新服
        /// </summary>
        New = 3,
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DogSE.Tools.CodeGeneration.Client.Unity3d
{
    class Utils
    {

        /// <summary>
        /// 给方法名之前加一个 On
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetFixBeCallProxyName(string name)
        {
            string ret = name;
            if (name.Substring(2).ToLower() != "on")
            {
                ret = "On" + name;
            }

            //if (name.ToLower().EndsWith("result"))
            //{
            //    ret = ret.Substring(0, ret.Length - "result".Length);
            //}

            return ret;
        }

        /// <summary>
        /// 取消方法名前面的 On
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetFixCallProxyName(string name)
        {
            if (name.Substring(0, 2).ToLower() == "on")
                return name.Substring(2, name.Length - 2);
            return name;
        }

        /// <summary>
        /// 取消接口前面的 i
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetFixInterfaceName(string name)
        {
            if (name[0].ToString().ToLower() == "i")
                return name.Substring(1, name.Length - 1);
            return name;
        }

        /// <summary>
        /// 将 .Server. 替换为 .Client.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetFixFullTypeName(string name)
        {
            return name.Replace(".Server.", ".Client.");
        }

        /// <summary>
        /// 返回Type的基础类型关键字，
        /// 如：int byte
        /// </summary>
        /// <param name="type"></param>
        /// <param isNeedTrans="isNeedTrans">是否需要将 ".Server." 转换为 ".Client."</param>
        /// <returns></returns>
        public static string GetBaseTypeName(Type type, bool isNeedTrans = false)
        {
            HashSet<string> usingCode = new HashSet<string>();
            return GetBaseTypeName(type, ref usingCode, isNeedTrans);
        }

        /// <summary>
        /// 返回Type的基础类型关键字，
        /// 如：int byte
        /// </summary>
        /// <param name="type"></param>
        /// <param name="usingCode">所使用到的命名空间</param>
        /// <param isNeedTrans="isNeedTrans">是否需要将 ".Server." 转换为 ".Client."</param>
        /// <returns></returns>
        public static string GetBaseTypeName(Type type, ref HashSet<string> usingCode, bool isNeedTrans = false)
        {
            if (type == typeof(bool))           //4字节   true false
            {
                return "bool";
            }
            else if (type == typeof(byte))      //2字节   0 ~ 255
            {
                return "byte";
            }
            else if (type == typeof(sbyte))     //1字节   -128 ~ 127
            {
                return "sbyte";
            }
            else if (type == typeof(short))     //2字节   -32768 ~ 32767
            {
                return "short";
            }
            else if (type == typeof(ushort))    //2字节   0 ~ 65535
            {
                return "ushort";
            }
            else if (type == typeof(int))       //4字节   -2,147,483,648 ~ 2,147,483,647
            {
                return "int";
            }
            else if (type == typeof(uint))      //4字节   0~4,294,967,295
            {
                return "uint";
            }
            else if (type == typeof(long))      //8字节   -922万万亿 ~ 922万万亿
            {
                return "long";
            }
            else if (type == typeof(ulong))     //8字节   0 ~ 1844万万亿
            {
                return "ulong";
            }
            else if (type == typeof(float))     //4字节
            {
                return "float";
            }
            else if (type == typeof(double))    //8字节
            {
                return "double";
            }
            else if (type == typeof(string))
            {
                return "string";
            }
            else if (type == typeof(DateTime))
            {
                return "DateTime";
            }
            else if (type.IsEnum || type.IsLayoutSequential || type.IsArray || type.IsClass)
            {
                if (isNeedTrans)
                {
                    string ns = Utils.GetFixFullTypeName(type.Namespace);
                    if (!usingCode.Contains(ns))
                        usingCode.Add(ns);

                    return Utils.GetFixFullTypeName(type.Name);
                }
                else
                {
                    string ns = type.Namespace;
                    if (!usingCode.Contains(ns))
                        usingCode.Add(ns);

                    return type.Name;
                }
            }
            //else if (type.IsLayoutSequential)
            //{
            //    if (isNeedTrans)
            //        return Utils.GetFixFullTypeName(type.Name);
            //    else
            //        return type.Name;
            //}
            //else if (type.IsArray)
            //{
            //    if (isNeedTrans)
            //        return Utils.GetFixFullTypeName(type.Name);
            //    else
            //        return type.Name;
            //}
            //else if (type.IsClass)
            //{
            //    if (isNeedTrans)
            //        return Utils.GetFixFullTypeName(type.Name);
            //    else
            //        return type.Name;
            //}

            return string.Empty;
        }
    }
}

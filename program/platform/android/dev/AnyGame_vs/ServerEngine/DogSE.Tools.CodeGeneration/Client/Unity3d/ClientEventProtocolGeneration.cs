using DogSE.Library.Log;
using DogSE.Server.Core.Net;
using DogSE.Server.Core.Protocol;
using DogSE.Tools.CodeGeneration.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DogSE.Tools.CodeGeneration.Client.Unity3d
{
    /// <summary>
    /// 创建 *.Logic.cs 里的事件代码
    /// </summary>
    public class CreateEventCode
    {
        /// <summary>
        /// 系统模块
        /// </summary>
        private readonly Type classType;

        /// <summary>
        /// 该模块的功能文档
        /// </summary>
        private List<FunItem> funcDoc;

        /// <summary>
        /// 创建事件代码
        /// </summary>
        /// <param name="type">模块</param>
        /// <param name="doc">文档注释</param>
        public CreateEventCode(Type type, List<FunItem> doc)
        {
            classType = type;
            funcDoc = doc;
        }


        /// <summary>
        /// 调用代码
        /// </summary>
        private readonly StringBuilder callCode = new StringBuilder();

        /// <summary>
        /// 事件代码
        /// </summary>
        private readonly StringBuilder eventCode = new StringBuilder();

        /// <summary>
        /// 事件参数代码
        /// </summary>
        private readonly StringBuilder eventArgsCode = new StringBuilder();


        private HashSet<string> usingCode = new HashSet<string>();

        public string GetUsedNameSpace()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in usingCode)
            {
                sb.AppendFormat("using {0};\r\n", item);
            }

            return sb.ToString();
        }


        /// <summary>
        /// 添加一个方法
        /// </summary>
        /// <param name="att"></param>
        /// <param name="methodinfo"></param>
        void AddMethod(NetMethodAttribute att, MethodInfo methodinfo)
        {
            var param = methodinfo.GetParameters();
            if (param.Length < 1)
            {
                Logs.Error(string.Format("{0}.{1} 不支持 {2} 个参数", classType.Name, methodinfo.Name, param.Length.ToString()));
                return;
            }

            if (param[0].ParameterType != typeof(NetState))
            {
                Logs.Error("{0}.{1} 的第一个参数必须是 NetState 对象", classType.Name, methodinfo.Name);
                return;
            }

            if (att.MethodType == NetMethodType.PacketReader)
            {
                Logs.Error("客户端代理类不支持这种模式 {0}", att.MethodType.ToString());
                return;
            }

            if (att.MethodType == NetMethodType.ProtocolStruct)
            {
                Logs.Error("客户端代理类暂时不支持这种模式 {0}", att.MethodType.ToString());
                return;

                #region ProtocolStruct

                /*
                Type parameterType = param[1].ParameterType;

                if (!parameterType.IsClass)
                {
                    Logs.Error("{0}.{1} 的第二个参数必须是class类型。", classType.Name, methodinfo.Name);
                    return;
                }

                if (parameterType.GetInterface(typeof(IPacketWriter).FullName) == null)
                {
                    //  自己实现一个对对象的协议写入类
                    AddWriteProxy(parameterType);

                    string methodName = methodinfo.Name;
                    StringBuilder methonNameCode = new StringBuilder();
                    StringBuilder methodBodyCode = new StringBuilder();
                    methonNameCode.AppendFormat("public void {0}({1} obj)",
                        methodName, parameterType.FullName);

                    methodBodyCode.AppendLine("{");
                    methodBodyCode.AppendFormat("var pw = PacketWriter.AcquireContent({0});", att.OpCode);
                    methodBodyCode.AppendLine();
                    methodBodyCode.AppendFormat(
                        @"            PacketProfile packetProfile = PacketProfile.GetOutgoingProfile( {0} );
            if ( packetProfile != null )
                packetProfile.RegConstruct();
                ", att.OpCode);

                    methodBodyCode.AppendFormat("{0}WriteProxy.Write(obj, pw);", parameterType.Name);

                    methodBodyCode.AppendLine("NetState.Send(pw);");
                    methodBodyCode.AppendLine(" if ( packetProfile != null ) packetProfile.Record(pw.Length);");
                    methodBodyCode.AppendLine("PacketWriter.ReleaseContent(pw);");

                    methodBodyCode.AppendLine("}");

                    methonNameCode.Remove(methonNameCode.Length - 1, 1);
                    methonNameCode.Append(")");

                    callCode.AppendLine(methonNameCode.ToString());
                    callCode.AppendLine(methodBodyCode.ToString());
                }
                else
                {
                    //  如果对象实现了 IPacketWriter 接口，则直接使用，否则则自己生成协议代码
                    string methodName = methodinfo.Name;
                    StringBuilder methonNameCode = new StringBuilder();
                    StringBuilder methodBodyCode = new StringBuilder();
                    methonNameCode.AppendFormat("public void {0}({1} obj)",
                        methodName, parameterType.FullName);

                    methodBodyCode.AppendLine("{");
                    methodBodyCode.AppendFormat("var pw = PacketWriter.AcquireContent({0});", att.OpCode);
                    methodBodyCode.AppendLine();
                    methodBodyCode.AppendFormat(
                        @"            PacketProfile packetProfile = PacketProfile.GetOutgoingProfile( {0} );
            if ( packetProfile != null )
                packetProfile.RegConstruct();
                ", att.OpCode);

                    methodBodyCode.AppendLine("obj.Write(pw);");

                    methodBodyCode.AppendLine("NetState.Send(pw);");
                    methodBodyCode.AppendLine(" if ( packetProfile != null ) packetProfile.Record(pw.Length);");
                    methodBodyCode.AppendLine("PacketWriter.ReleaseContent(pw);");
                    methodBodyCode.AppendLine("}");

                    methonNameCode.Remove(methonNameCode.Length - 1, 1);
                    methonNameCode.Append(")");

                    callCode.AppendLine(methonNameCode.ToString());
                    callCode.AppendLine(methodBodyCode.ToString());
                }

                */

                #endregion

            }

            if (att.MethodType == NetMethodType.SimpleMethod)
            {
                #region SimpleMethod

                string methodName = Utils.GetFixCallProxyName(methodinfo.Name);

                StringBuilder methodNameCode = new StringBuilder();         //方法名
                StringBuilder methodBodyCode = new StringBuilder();         //方法体
                StringBuilder commentCode = new StringBuilder();            //xml注释

                StringBuilder argsClassNameCode = new StringBuilder();      //事件xxEventArgs的类名
                StringBuilder argsClassBodyCode = new StringBuilder();      //事件xxEventArgs的类成员

                Console.WriteLine(classType.FullName + "." + methodinfo.Name);
                var doc = funcDoc.FirstOrDefault(o => o.Name.IndexOf("M:" + classType.FullName + "." + methodinfo.Name) == 0);
                if (doc == null)
                    doc = new FunItem();


                //事件
                eventCode.AppendFormat(@"        /// <summary>
                /// {0}
                /// </summary>",
                doc.SummaryWorked);

                eventCode.AppendLine();
                if (param.Length > 1)
                    eventCode.AppendFormat("public event EventHandler<{0}EventArgs> {0}Event;\r\n", methodName);
                else
                    eventCode.AppendFormat("public event EventHandler<EventArgs> {0}Event;\r\n", methodName);
                eventCode.AppendLine();

                //事件方法
                methodNameCode.AppendFormat("internal override void On{0}(", methodName);
                methodBodyCode.AppendLine("{");
                if (param.Length > 1)
                {
                    methodBodyCode.AppendFormat("{0}Event?.Invoke(this, new {0}EventArgs\r\n", methodName);
                    methodBodyCode.AppendLine("{");
                }
                else
                {
                    methodBodyCode.AppendFormat("{0}Event?.Invoke(this, new EventArgs());\r\n", methodName);
                }

                //事件参数xxEventArgs
                if (param.Length > 1)
                {
                    eventArgsCode.AppendFormat(@"        /// <summary>
                    /// {0} 【参数】
                    /// </summary>",
                    doc.SummaryWorked);

                    eventArgsCode.AppendLine();
                    eventArgsCode.AppendFormat("public class {0}EventArgs : EventArgs\r\n", methodName);
                    eventArgsCode.AppendLine("{");
                }


                //参数
                for (int i = 1; i < param.Length; i++)
                {
                    var p = param[i];

                    //方法 名字
                    string basetype = Utils.GetBaseTypeName(p.ParameterType, ref usingCode, true);
                    if (!string.IsNullOrEmpty(basetype))
                    {
                        methodNameCode.AppendFormat("{0} {1},", basetype, p.Name);
                    }
                    else
                    {
                        Logs.Error(string.Format("{0}.{1} 存在不支持的参数 {2}，类型未：{3}",
                            classType.Name, methodinfo.Name, p.Name, p.ParameterType.Name));
                    }

                    //方法体
                    methodBodyCode.AppendFormat("{0} = {1},\r\n", p.Name.GetInitialCharUpper(), p.Name);

                    //事件参数
                    eventArgsCode.AppendFormat(@"        /// <summary>
                    /// {0}
                    /// </summary>",
                    doc.GetParamSummary(p.Name));

                    eventArgsCode.AppendLine();
                    eventArgsCode.AppendFormat("public {0} {1} {{ get; internal set; }}\r\n", basetype, p.Name.GetInitialCharUpper());
                    eventArgsCode.AppendLine();
                }

                if (param.Length > 1)
                {
                    methodNameCode.Remove(methodNameCode.Length - 1, 1);

                    methodBodyCode.Remove(methodBodyCode.Length - 1, 1);
                    methodBodyCode.AppendLine("});");

                    eventArgsCode.Remove(eventArgsCode.Length - 2, 2);      //删掉最后一个空行
                    eventArgsCode.AppendLine("}");
                    eventArgsCode.AppendLine();
                }

                methodNameCode.Append(")");
                methodBodyCode.Append("}");

                callCode.AppendLine(commentCode.ToString());
                callCode.AppendLine(methodNameCode.ToString());
                callCode.AppendLine(methodBodyCode.ToString());

                #endregion
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetCode()
        {
            var ret = new StringBuilder();

            ret.Append(CodeBase);
            ret.Replace("#ClassName#", Utils.GetFixInterfaceName(classType.Name));
            ret.Replace("#FullClassName#", classType.FullName);
            ret.Replace("#CallMethod#", callCode.ToString());
            ret.Replace("#EventCode#", eventCode.ToString());
            ret.Replace("#EventArgsCode#", eventArgsCode.ToString());
            ret.Replace("`", "\"");

            return ret.ToString();
        }


        /// <summary>
        /// 创建代码并进行编译
        /// </summary>
        /// <returns></returns>
        public string CreateCode()
        {
            foreach (var method in classType.GetMethods())
            {
                var attributes = method.GetCustomAttributes(typeof(NetMethodAttribute), true);
                if (attributes.Length == 0)
                    continue;

                AddMethod(attributes[0] as NetMethodAttribute, method);
            }

            var code = GetCode();
            return code;
        }

        /// <summary>
        /// 编译后生成的组件
        /// </summary>
        public Assembly CompiledAssembly { get; set; }

        private const string CodeBase = @"
    /// <summary>
    /// #ClassName#
    /// </summary>
    public partial class #ClassName#Controller : Base#ClassName#Controller
    {
        private readonly GameController controller;

        public #ClassName#Controller(GameController gc, NetController nc)
            : this(nc)
        {
            controller = gc;
        }

        #CallMethod#
        #EventCode#
    }

    #EventArgsCode#
";
    }


    /// <summary>
    /// 生成客户端的 .Logic.cs ，
    /// 如果已经有了，则不重新生成
    /// </summary>
    public class ClientEventProtocolGeneration
    {
        public static void CreateCode(string dllFile, string outFolder, string nameSpace)
        {
            StringBuilder interfaceCode = new StringBuilder();

            var dll = Assembly.LoadFrom(dllFile);
            StringBuilder proxyregBuilder = new StringBuilder();

            var xmlFile = dllFile.Replace(".dll", ".xml");
            var xmlDoc = CodeCommentUtils.LoadXmlDocument(xmlFile);

            foreach (var type in dll.GetTypes())
            {
                if (type.IsInterface)
                {
                    var i = type.GetCustomAttributes(typeof(ClientInterfaceAttribute), true);
                    if (i.Length > 0)
                    {
                        //例：把 IBag 转成 Bag
                        string typeName = Utils.GetFixInterfaceName(type.Name);

                        string fileName = Path.Combine(outFolder, string.Format(@"Controller\{0}\{0}Controller.Logic.cs", typeName));

                        Console.WriteLine(typeName);

                        //  生成客户端【收到服务器的响应事件】代码并写入对应文件
                        var cec = new CreateEventCode(type, xmlDoc);
                        var code = cec.CreateCode();

                        var context = FileCodeBase
                        .Replace("#code#", code)
                        .Replace("#namespace#", nameSpace)
                        .Replace("#ClassName#", typeName)
                        .Replace("#using#", cec.GetUsedNameSpace())
                        .Replace("`", "\"");

                        var fi = new FileInfo(fileName);
                        if (!fi.Directory.Exists)
                            fi.Directory.Create();

                        // *.Logic.cs文件基本上一定会被手工修改，所以这里只是在没有文件时才生成
                        if (!File.Exists(fileName))
                        {
                            File.WriteAllText(fileName, context, Encoding.UTF8);
                        }

                        //这里在另一个地方刻意再存储一份
                        //因为有时候改了接口，好用这里的代码来赋值粘贴
                        fileName = Path.Combine("D:\\生成AnyGame的logic代码", string.Format(@"Controller\{0}\{0}Controller.Logic.cs", typeName));
                        fi = new FileInfo(fileName);
                        if (!fi.Directory.Exists)
                            fi.Directory.Create();

                        File.WriteAllText(fileName, context, Encoding.UTF8);
                    }
                }
            }
        }


        const string FileCodeBase = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DogSE.Client.Core;
using DogSE.Client.Core.Net;
using DogSE.Client.Core.Task;
#using#
namespace #namespace#.Controller.#ClassName#
{
    #code#
}
";
    }

}

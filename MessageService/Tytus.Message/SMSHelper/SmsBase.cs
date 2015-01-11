using System;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.IO;
using System.Configuration;
using System.Web.Services.Description;
using System.Xml.Serialization;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
using Microsoft.Win32;
namespace Tytus.B2B.Core.SMSHelper
{
    public abstract class SmsBase
    {
        public Assembly asm = null;
        WebClient client = new WebClient();
        public WebProxy proxy = null;
        bool ReCreateState = false;
        public SmsBase(bool flag = false)
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["SMSWebProxyOnOff"].ToString()))
            {
                proxy = getProxy(ConfigurationManager.AppSettings["SMSWebProxyHost"]
                                 , Convert.ToInt32(ConfigurationManager.AppSettings["SMSWebProxyPort"])
                                 , ConfigurationManager.AppSettings["SMSWebProxyUserName"]
                                 , ConfigurationManager.AppSettings["SMSWebProxyUserPwd"]
                                 , ConfigurationManager.AppSettings["SMSWebProxyDomain"]
                                 );
                client.Proxy = proxy;
            }
            ReCreateState = flag;
            InvokeMethod();
        }

        /// <summary>
        /// 查询余额实体
        /// </summary>
        public SMS QueryAccountModel { get; set; }

        /// <summary>
        /// 发送信息实体
        /// </summary>
        public SMSMessage SendSmsModel { get; set; }

        /// <summary>
        /// 修改密码实体
        /// </summary>
        public EditPwd EditPwdModel { get; set; }

        /// <summary>
        /// 发送信息方法
        /// </summary>
        /// <returns>string</returns>
        public abstract string SendMsg();

        /// <summary>
        /// 修改密码方法
        /// </summary>
        /// <returns>string</returns>
        public abstract string EditPwd();

        /// <summary>
        /// 查询余额方法
        /// </summary>
        /// <returns>string</returns>
        public abstract string QueryAccount();

        #region webservice的动态创建
        /// <summary>
        /// 根据webservice地址动态创建
        /// </summary>
        public void InvokeMethod()
        {

            string dll = AppDomain.CurrentDomain.BaseDirectory + "/bin/" + "Tytus.B2B.Core.SMSHelperTemp.dll";
            if (!File.Exists(dll) || ReCreateState)
            {
                String url = ConfigurationManager.AppSettings["SMSWebServiceAddress"];//这个地址可以写在Config文件里面，这里取出来就行了
                Stream stream = client.OpenRead(url);
                ServiceDescription description = ServiceDescription.Read(stream);
                ServiceDescriptionImporter importer = new ServiceDescriptionImporter();//创建客户端代理代理类。
                importer.ProtocolName = "Soap"; //指定访问协议。   
                importer.Style = System.Web.Services.Description.ServiceDescriptionImportStyle.Client; //生成客户端代理。   
                importer.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties | CodeGenerationOptions.GenerateNewAsync;
                importer.AddServiceDescription(description, null, null); //添加WSDL文档。   

                CodeNamespace nmspace = new System.CodeDom.CodeNamespace(); //命名空间   
                nmspace.Name = ConfigurationManager.AppSettings["SMSWebServiceNameSpace"];
                CodeCompileUnit unit = new CodeCompileUnit();
                unit.Namespaces.Add(nmspace);
                ServiceDescriptionImportWarnings warning = importer.Import(nmspace, unit);
                CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
                CompilerParameters parameter = new CompilerParameters();
                parameter.GenerateExecutable = false;
                parameter.OutputAssembly = dll;//输出程序集的名称   
                parameter.ReferencedAssemblies.Add("System.dll");
                parameter.ReferencedAssemblies.Add("System.XML.dll");
                parameter.ReferencedAssemblies.Add("System.Web.Services.dll");
                parameter.ReferencedAssemblies.Add("System.Data.dll");
                CompilerResults result = provider.CompileAssemblyFromDom(parameter, unit);
            }
            asm = Assembly.LoadFrom(dll);//加载前面生成的程序集 
        }

        /// <summary>
        /// 获取服务所在安装路径 4 
        /// </summary>
        /// <param name="ServiceName">服务名</param>
        /// <returns>服务安装路径</returns>
        public static string GetWindowsServiceInstallPath(string ServiceName)
        {
            string key = @"SYSTEM\CurrentControlSet\Services\" + ServiceName;
            string path = Registry.LocalMachine.OpenSubKey(key).GetValue("ImagePath").ToString();
            path = path.Replace("\"", string.Empty);//替换掉双引号
            FileInfo fi = new FileInfo(path);
            return fi.Directory.ToString();
        }
        #endregion

        #region 创建一个代理
        /// <summary>
        /// 创建一个代理
        /// </summary>
        /// <param name="server">服务器</param>
        /// <param name="port">端口</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>WebProxy</returns>
        private WebProxy getProxy(string server, int port, string userName, string password)
        {
            WebProxy _proxy = null;
            if (server != null && port > 0)
            {
                _proxy = new WebProxy(server, port);
                if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
                {
                    _proxy.Credentials = new NetworkCredential(userName, password);
                    _proxy.BypassProxyOnLocal = true;
                }
            }
            return _proxy;
        }
        /// <summary>
        /// 创建一个代理
        /// </summary>
        /// <param name="server">服务器</param>
        /// <param name="port">端口</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="WebProxyDomain">域</param>
        /// <returns>WebProxy</returns>
        private WebProxy getProxy(string server, int port, string userName, string password, string WebProxyDomain)
        {
            WebProxy _proxy = null;
            if (server != null && port > 0)
            {
                _proxy = new WebProxy(server, port);
                if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(WebProxyDomain))
                {
                    _proxy.Credentials = new NetworkCredential(userName, password, WebProxyDomain);
                    _proxy.BypassProxyOnLocal = true;
                }
            }
            return _proxy;
        }
        #endregion

    }
}
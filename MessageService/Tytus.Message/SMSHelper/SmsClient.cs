using System;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.Text;
using System.Reflection;
using System.Configuration;
namespace Tytus.B2B.Core.SMSHelper
{
    public class SmsClient:SmsBase
    {

        #region 构造函数
        public SmsClient(bool flag = false):base(flag) { }
        #endregion

        /// <summary>
        /// webservice命名名间
        /// </summary>
        private string NameSplace
        {
            get
            {
                return ConfigurationManager.AppSettings["SMSWebServiceNameSpace"];
            }
        }
        /// <summary>
        /// webservice类名
        /// </summary>
        private string ClassName
        {
            get
            {
                return ConfigurationManager.AppSettings["SMSWebServiceClass"];
            }
        }
        
        #region 发送信息
        /// <summary>
        /// 发送信息方法
        /// </summary>
        /// <returns>string</returns>
        public override string SendMsg()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in SendSmsModel.Mobiles)
            {
                sb.Append(",").Append(item);
            }
            if (sb.ToString().Length > 0)
                sb = new StringBuilder(sb.ToString().Remove(0, 1));
            List<string> arr = new List<string>();
            arr.Add(SendSmsModel.Ecode);
            arr.Add(SendSmsModel.Extno);
            arr.Add(SendSmsModel.UserName);
            arr.Add(SendSmsModel.UserPwd);
            arr.Add(sb.ToString());
            arr.Add(SendSmsModel.Content);
            arr.Add(SendSmsModel.Seq);
            arr.Add(SendSmsModel.SendTime);
            arr.Add(SendSmsModel.Task);
            Type t = asm.GetType(NameSplace + "." + ClassName);
            object o = Activator.CreateInstance(t);
            if (proxy != null)
            {
                PropertyInfo info = t.GetProperty("Proxy");
                info.SetValue(o, proxy, null);
            }
            MethodInfo method = t.GetMethod("smsSend");
            object resutl = method.Invoke(o,arr.ToArray());
            return ErrorMsg(resutl.ToString());
        }
        #endregion
      
        #region 修改密码
        /// <summary>
        /// 修改密码方法
        /// </summary>
        /// <returns>string</returns>
        public override string EditPwd()
        {
            List<string> arr = new List<string>();
            arr.Add(EditPwdModel.Ecode);
            arr.Add(EditPwdModel.UserName);
            arr.Add(EditPwdModel.UserPwd);
            arr.Add(EditPwdModel.NewPassword);
            Type t = asm.GetType(NameSplace + "." + ClassName);
            object o = Activator.CreateInstance(t);
            if (proxy != null)
            {
                PropertyInfo info = t.GetProperty("Proxy");
                info.SetValue(o, proxy, null);
            }
            MethodInfo method = t.GetMethod("updatePassword");
            object resutl = method.Invoke(o, arr.ToArray());
            return ErrorMsg(resutl.ToString());           
        }
        #endregion

        #region 查看余额
        /// <summary>
        /// 查询余额方法
        /// </summary>
        /// <returns>string</returns>
        public override string QueryAccount()
        {
            List<string> arr = new List<string>();
            arr.Add(QueryAccountModel.Ecode);
            arr.Add(QueryAccountModel.UserName);
            arr.Add(QueryAccountModel.UserPwd);
            Type t = asm.GetType(NameSplace + "." + ClassName);
            object o = Activator.CreateInstance(t);
            if (proxy != null)
            {
                PropertyInfo info = t.GetProperty("Proxy");
                info.SetValue(o, proxy, null);
            }
            MethodInfo method = t.GetMethod("account");
            object resutl = method.Invoke(o, arr.ToArray());
            return ErrorMsg(resutl.ToString());
        }
        #endregion

        #region 返回错误代码
        /// <summary>
        /// 返回状态码对应的消息
        /// </summary>
        /// <param name="ErrorCode">状态码</param>
        /// <returns>string</returns>
        private string ErrorMsg(string ErrorCode)
        {
            string Msg = string.Empty;
            switch (ErrorCode)
            {
                case "1":
                    Msg = "发送成功";
                    break;
                case "-1":
                    Msg = "不能初始化SOCKT";
                    break;
                case "-2":
                    Msg = "网络不通";
                    break;
                case "-3":
                    Msg = "一次发送的手机号码过多";
                    break;
                case "-4":
                    Msg = "内容包含不合法关键字";
                    break;
                case "-5":
                    Msg = "登录账户错误";
                    break;
                case "-6":
                    Msg = "通信数据传送错误";
                    break;
                case "-7":
                    Msg = "没有进行参数校验";
                    break;
                case "-8":
                    Msg = "扩展号码长度错误";
                    break;
                case "-9":
                    Msg = "手机号码不合法";
                    break;
                case "-10":
                    Msg = "号码太长";
                    break;
                case "-11":
                    Msg = "内容太长";
                    break;
                case "-12":
                    Msg = "内部错误";
                    break;
                case "-13":
                    Msg = "余额不足";
                    break;
                case "-14":
                    Msg = "扩展号不正确";
                    break;
                case "-15":
                    Msg = "扩展号不正确";
                    break;
                default:
                    Msg = ErrorCode;
                    break;
            }
            return Msg;
        }
        #endregion

      
    }
}
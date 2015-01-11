using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;

namespace Tytus.B2B.Core.SMSHelper
{
    public class SMS
    {
        ///<summary>
        /// 企业代码
        /// </summary>
        public string Ecode { get { return ConfigurationManager.AppSettings["SMSEcode"];} }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get { return ConfigurationManager.AppSettings["SMSUserName"];} }
        /// <summary>
        /// 密码
        /// </summary>
        public string UserPwd { get { return ConfigurationManager.AppSettings["SMSUserPwd"];} }
    }
    public class SMSMessage : SMS
    {
        /// <summary>
        /// 拓展号(没有则为空)
        /// </summary>
        public string Extno { get; set; }
        /// <summary>
        /// 发送手机号码列表（群发最大100个，号码间以,分隔）
        /// </summary>
        public List<string> Mobiles { get; set; }
        /// <summary>
        /// 发送内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 自定义唯一的消息ID，长度6位，消息流水号,顺序累加,步长为1,循环使用
        /// </summary>
        public string Seq { get {return Guid.NewGuid().ToString(); } }
        /// <summary>
        /// 发送时间
        /// </summary>
        public string SendTime{ get; set; }
        /// <summary>
        /// 任务ID(默认为空)
        /// </summary>
        public string Task { get; set; }
    }
    public class EditPwd : SMS
    {
        /// <summary>
        /// 拓展号(没有则为空)
        /// </summary>
        public string Extno { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPassword { get; set; }
    }
}
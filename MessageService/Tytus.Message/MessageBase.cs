using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tytus.Message
{
    /// <summary>
    /// 系统消息提醒基类
    /// </summary>
    public abstract class MessageBase
    {
        #region 基础属性

        /// <summary>
        /// 消息主题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        public abstract string To { get; set; }

        /// <summary>
        /// 收件人称谓
        /// </summary>
        public string Receiver { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public MessagePriority Priority { get; set; }

        /// <summary>
        /// 供子类Override使用
        /// </summary>
        protected string to;

        #endregion

        /// <summary>
        /// 发送消息
        /// </summary>
        public abstract void Send();

        /// <summary>
        /// 创建发邮件实例
        /// </summary>
        /// <returns></returns>
        public static MessageBase CreatMailEntity()
        {
            return new Mail.MessageMail();
        }

        public static MessageBase CreatSMSEntity()
        {
            return new MessageSMS();
        }

      
    }

    /// <summary>
    ///  指定 Tytus.Mail.MessageBase 的优先级。
    ///  与 System.Net.Mail.MailMessage 的优先级一致。
    /// </summary>
    public enum MessagePriority
    {
        /// <summary>
        /// 此消息具有正常优先级。
        /// </summary>     
        Normal = 0,

        /// <summary>
        /// 此消息具有低优先级。
        /// </summary>
        Low = 1,

        /// <summary>
        ///  此消息具有高优先级。
        /// </summary>
        High = 2,
    }

    /// <summary>
    /// 消息类型
    /// </summary>
    public enum Enum_MessageType
    {
        /// <summary>
        /// 缺失类型
        /// </summary>
        None,
        /// <summary>
        /// 邮件
        /// </summary>
        Mail,
        /// <summary>
        /// 短信
        /// </summary>
        SMS

    }
   
}

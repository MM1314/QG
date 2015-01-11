using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace Tytus.Message.Mail
{
    /// <summary>
    /// 扩展MessageBase
    /// 提供以SmtpClient为基础发送邮件消息的服务
    /// </summary>
    public class MessageMail : MessageBase
    {
        #region 邮件属性

        //TO, CC, BCC(收件人、抄送、密送)

        /// <summary>
        /// 收件人
        /// 群发邮件以 ";"隔开
        /// </summary>
        public override string To { get; set; }

        /// <summary>
        /// 抄送
        /// 多人以 ";"隔开
        /// </summary>
        public string CC { get; set; }

        /// <summary>
        /// 密送
        ///  多人以 ";"隔开
        /// </summary>
        public string BCC { get; set; }

        #endregion

        #region 初始化

        /// <summary>
        /// 以SMTP协议发送邮件
        /// </summary>
        public MessageMail()
        {
        }

        /// <summary>
        ///   以SMTP协议发送邮件
        /// </summary>
        /// <param name="To">收件人：以分号隔开</param>
        /// <param name="Subject">邮件主题</param>
        public MessageMail(string To, string Subject)
            : this()
        {
            this.To = To;
            this.Subject = Subject;
        }

        /// <summary>
        ///   以SMTP协议发送邮件
        /// </summary>
        /// <param name="To">收件人：以分号隔开</param>
        /// <param name="Subject">邮件主题</param>
        /// <param name="Body">邮件内容</param>
        public MessageMail(string To, string Subject, string Body)
            : this(To, Subject)
        {
            this.Body = Body;
        }

        /// <summary>
        ///   以SMTP协议发送邮件
        /// </summary>
        /// <param name="To">收件人：以分号隔开</param>
        /// <param name="Subject">邮件主题</param>
        /// <param name="Priority">邮件优先级</param>
        public MessageMail(string To, string Subject, MessagePriority Priority)
            : this(To, Subject)
        {
            this.Priority = Priority;
        }

        #endregion

        #region 邮件接收人信息处理

        /// <summary>
        /// 初始化各类接收人
        /// 收件人
        /// 抄送
        /// 密送
        /// </summary>
        /// <param name="mailAddress"></param>
        /// <param name="strReceivers"></param>
        private void InitReceivers(MailAddressCollection mailAddress, string strReceivers)
        {
            //参数验证
            if (string.IsNullOrEmpty(strReceivers))
            {
                return;
            }
            if (mailAddress == null)
            {
                mailAddress = new MailAddressCollection();
            }

            //邮件接收人
            string[] receivers = strReceivers.Trim(';').Split(';');
            foreach (string receiver in receivers)
            {
                mailAddress.Add(new MailAddress(receiver));
            }
        }

        #endregion

        #region 发送邮件

        /// <summary>
        /// 发送邮件
        /// </summary>
        public override void Send()
        {
            if (VerifyMail())
            {
                MailMessage msg = new MailMessage();
                msg.IsBodyHtml = true;

                //加载邮件接收人
                InitReceivers(msg.To, this.To);
                InitReceivers(msg.CC, this.CC);
                InitReceivers(msg.Bcc, this.BCC);

                msg.Subject = Subject;
                msg.Body = Body;
                msg.Priority = (MailPriority)Priority;

                try
                {
                    MailClient.Send(msg);
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format(MailConst.Err_SendFailed, ex.Message));
                }
            }
        }

        #endregion

        #region 邮件验证

        /// <summary>
        /// 验证邮件必须信息
        /// </summary>
        /// <returns></returns>
        private bool VerifyMail()
        {
            if (string.IsNullOrEmpty(To))
            {
                throw new Exception("收件人不能为空！");
            }
            if (string.IsNullOrEmpty(Subject))
            {
                Subject = "系统提醒！";
            }
            if (string.IsNullOrEmpty(Body))
            {
                throw new Exception("邮件内容不能为空！");
            }
            return true;
        }

        #endregion

    }

}

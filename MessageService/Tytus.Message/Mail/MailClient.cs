using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Configuration;

namespace Tytus.Message.Mail
{
    /// <summary>
    /// 发送邮件组件
    /// 
    /// 提供采用协议SMTP的Send(Msg)方法
    /// 
    /// 所需信息： 邮箱服务器地址 | 已授权的邮箱帐号 | 邮箱密码    
    /// 配置方法：Web.Config 中
    /// --邮件服务器地址--
    ///  key="MailServer" value="mail.sanyou-medical.com"
    /// --邮箱帐号--
    ///  key="MailAddress" value="logistics.test@sanyou-medical.com"
    /// --邮箱密码--
    ///  key="MailPassword" value="p@ssw0rd"
    /// --邮箱显示名 非必填--
    ///  key="MailDisplayName" value="拓腾"
    /// --邮件发送调用超时时间 单位毫秒 默认1000--
    ///  key="MailTimeOut" value="10000"
    /// </summary>
    public class MailClient
    {
        #region SmtpClient 配置属性

        /// <summary>
        /// smtp邮件发送客户端
        /// </summary>
        private static readonly SmtpClient client;
        /// <summary>
        /// 邮件发送调用超时时间 单位毫秒 默认1000
        /// </summary>
        private static int TimeOut;
        /// <summary>
        /// 邮箱服务器地址
        /// </summary>
        private static string mailServer { get; set; }
        /// <summary>
        /// 邮箱帐号
        /// </summary>
        private static string mailAddress { get; set; }
        /// <summary>
        /// 邮箱用户名
        /// </summary>
        private static string user { get; set; }
        /// <summary>
        /// 系统邮箱密码        
        /// </summary>
        private static string pwd { get; set; }

        /// <summary>
        /// 系统邮箱显示名
        /// </summary>
        private static string DisplayName { get; set; }

        /// <summary>
        /// 系统邮箱为发件箱
        /// </summary>
        private static string From { get; set; }

        #endregion

        #region SmtpClient静态初始化

        /// <summary>
        /// 设置smtp邮件发送客户端信息
        /// </summary>
        static MailClient()
        {
            client = new SmtpClient();

            //读取邮件服务器配置
            try
            {
                mailServer = ConfigurationManager.AppSettings["MailServer"].ToString();
                mailAddress = ConfigurationManager.AppSettings["MailAddress"].ToString();
                pwd = ConfigurationManager.AppSettings["MailPassword"].ToString();
                DisplayName = ConfigurationManager.AppSettings["MailDisplayName"];

                if (!int.TryParse(ConfigurationManager.AppSettings["MailTimeOut"], out TimeOut))
                {
                    TimeOut = MailConst.DefaultTimeOut;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(MailConst.Err_SmtpClientInit, MailConst.ErrInfo_ConfigFileRead));
            }
            //设置smtp邮件发送客户端信息
            try
            {
                From = mailAddress;
                user = mailAddress.Split('@')[0];

                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = true;
                client.Host = mailServer;
                client.Timeout = TimeOut;
                client.Credentials = new System.Net.NetworkCredential(user, pwd);

            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(MailConst.Err_SmtpClientInit, ex.Message));
            }
        }

        #endregion

        #region 发送邮件

        /// <summary>
        /// 通过SmtpClient 发送邮件
        /// </summary>
        /// <param name="msg">MailMessage 邮件</param>
        public static void Send(MailMessage msg)
        {
            if (string.IsNullOrEmpty(DisplayName))
            {
                msg.From = new MailAddress(From);
            }
            else
            {
                msg.From = new MailAddress(From, DisplayName);
            }

            client.Send(msg);
        }

        #endregion

    }

}

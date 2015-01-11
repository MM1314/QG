using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tytus.Message.Mail
{
    ///  邮件相关常量
    /// </summary>
    public class MailConst
    {

        #region 默认值

        /// <summary>
        /// / 邮件发送调用超时时间 单位毫秒 默认1000
        /// </summary>
        public const int DefaultTimeOut = 1000;


        #endregion

        #region 异常提示


        /// <summary>
        /// SmtpClient初始化失败
        /// </summary>
        public const string Err_SmtpClientInit = "邮件发送组件SmtpClient初始化失败。详细信息:{0}";

        /// <summary>
        /// 邮件服务器配置信息读取失败
        /// </summary>
        public static string ErrInfo_ConfigFileRead = "邮件服务器配置信息读取失败，请检查配置文件。";

        /// <summary>
        /// mail Send failed
        /// </summary>
        public const string Err_SendFailed = "邮件发送失败。详细信息：{0}";



        #endregion

    }

}

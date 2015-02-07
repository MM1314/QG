using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using MessageService.Model;
using MessageService.DAL;

namespace Tytus.Message
{
    /// <summary>
    /// 服务发送消息封装
    /// </summary>
    public class SMessage
    {
        #region 暂不调用 通过配置文件中MsgSentType节点配置。 支持单时钟同时发送邮件、短信

        const string type_Mail = "Mail";

        /// <summary>
        /// 指定消息发送方式：邮件Mail；短信SMS；以分号隔开
        /// </summary>
        public static string MsgSentType = ConfigurationManager.AppSettings["MsgSentType"] ?? "";

        /// <summary>
        /// 多种发送方式的Client
        /// </summary>
        private static List<MessageBase> Clients;

        static SMessage()
        {
            string[] types = MsgSentType.Trim(';').Split(';');
            if (types != null && types.Count() > 0)
            {
                Clients = new List<MessageBase>();
                foreach (string type in types)
                {
                    if (type.Equals(type_Mail))
                    {
                        Clients.Add(new Mail.MessageMail());
                    }
                }
            }
        }

        /// <summary>
        /// 以配置的发送方式发送消息。
        /// </summary>
        /// <param name="Msg"></param>
        public static void Send(MessageEntity Msg)
        {
            foreach (MessageBase client in Clients)
            {
                if (client.GetType() == typeof(Mail.MessageMail))
                {
                    client.To = Msg.EMails;
                }

                client.Subject = Msg.Title;
                client.Body = Msg.Content;
                client.Receiver = Msg.Receiver;
                client.Priority = MessagePriority.High;

                try
                {
                    client.Send();
                    Msg.IsSended = true;
                    SLog.WriteLogWithDefaultTime(client.To + client.Body + "发送成功");

                }
                catch (Exception ex)
                {
                    Msg.IsSended = false;
                    SLog.WriteLogWithDefaultTime(ex.Message);
                    continue;
                }
            }
        }

        /// <summary>
        /// 以配置的发送方式批量发送消息。
        /// </summary>
        /// <param name="Msg"></param>
        public static void Send(List<MessageEntity> Msgs)
        {

            //多种发送方式
            foreach (MessageBase client in Clients)
            {
                bool isMail = client.GetType() == typeof(Mail.MessageMail);
                //多条消息
                foreach (MessageEntity msg in Msgs)
                {
                    if (isMail)
                    {
                        client.To = msg.EMails;
                    }
                 
                    client.Subject = msg.Title;
                    client.Body = msg.Content;
                    client.Receiver = msg.Receiver;
                    client.Priority = MessagePriority.High;

                    try
                    {
                        client.Send();
                        msg.IsSended = true;
                        SLog.WriteLogWithDefaultTime(client.To + client.Body + "发送成功");

                    }
                    catch (Exception ex)
                    {
                        msg.IsSended = false;
                        SLog.WriteLogWithDefaultTime(ex.Message);
                        continue;
                    }
                }

            }
        }


        #endregion

        #region 扩展 支持邮件|短信独立发送  create 2012.3.14 mjp

        private static MessageBase MailClient = MessageBase.CreatMailEntity();
        

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="Msg"></param>
        public static void SendMail(MessageEntity msg)
        {
            msg.IsSended = false;

            MailClient.To = msg.EMails;
            MailClient.Subject = msg.Title;
            MailClient.Body = msg.Content;
            MailClient.Receiver = msg.Receiver;
            MailClient.Priority = MessagePriority.High;

            MailClient.Send();
            msg.IsSended = true;

        }


        #endregion

    }
}

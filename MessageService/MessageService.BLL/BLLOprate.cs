using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessageService.DAL;
using MessageService.Model;
using Tytus.Message;
using System.Threading;

namespace MessageService.BLL
{
    public class BLLOprate
    {
        /// <summary>
        /// 发送邮件提醒，并更新消息状态
        /// </summary>
        public void SendMail()
        {
            List<MessageEntity> messages = DBOperate.LoadAlertMessage(Enum_MessageType.Mail.ToString());
            //异步发送
            foreach (MessageEntity msg in messages)
            {
                //带返回值
                SendMailDelegate fh = new SendMailDelegate(this.delegate_SendMail);
                IAsyncResult ar = fh.BeginInvoke(msg, null, fh);
                while (!ar.IsCompleted)
                {
                    Thread.Sleep(100);
                }
                msg.IsSended = fh.EndInvoke(ar);
            }

            //DBOperate.UpdateMessageStatus(messages);
        }

        /// <summary>
        /// 发送短信提醒，并更新消息状态
        /// </summary>
        public void SendSMS()
        {
            List<MessageEntity> messages = DBOperate.LoadAlertMessage(Enum_MessageType.SMS.ToString());
            //异步发送
            foreach (MessageEntity msg in messages)
            {
                //无返回值，已实现
                //ThreadPool.QueueUserWorkItem(delegateMethord, msg);
                //WaitCallback delegateMethord= new WaitCallback(SendMethod);

                //带返回值
                SendSMSDelegate fh = new SendSMSDelegate(this.delegate_SendSMS);
                IAsyncResult ar = fh.BeginInvoke(msg, null, fh);
                while (!ar.IsCompleted)
                {
                    Thread.Sleep(100);
                }
                msg.IsSended = fh.EndInvoke(ar);
            }

            //DBOperate.UpdateMessageStatus(messages);

        }

        /// <summary>
        /// 执行经销商Job
        /// </summary>
        public void ExcuteDealerJob()
        {
            DBOperate.DelerJob();
        }


        #region 委托 发短信|发邮件

        /// <summary>
        /// 委托：发短信
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public delegate bool SendSMSDelegate(MessageEntity msg);
        /// <summary>
        /// 委托：发送邮件
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public delegate bool SendMailDelegate(MessageEntity msg);


        /// <summary>
        /// 委托实现：发短信
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool delegate_SendSMS(MessageEntity msg)
        {
            try
            {
                SMessage.SendSMS(msg);

                DBOperate.SaveMessageLog(msg, Enum_MessageType.SMS.ToString(), null);
                SLog.WriteLogWithDefaultTime(msg.Mobiles + "发送成功");

                return msg.IsSended;
            }
            catch (Exception ex)
            {
                DBOperate.SaveMessageLog(msg, Enum_MessageType.SMS.ToString(), ex.Message);
                SLog.WriteLogWithDefaultTime(string.Format("发送失败：{0}", ex.Message));
                return false;
            }


        }

        /// <summary>
        /// 委托实现：发送邮件
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool delegate_SendMail(MessageEntity msg)
        {
            try
            {
                SMessage.SendMail(msg);

                DBOperate.SaveMessageLog(msg, Enum_MessageType.Mail.ToString(), null);
                SLog.WriteLogWithDefaultTime(msg.EMails + "发送成功");

                return msg.IsSended;
            }
            catch (Exception ex)
            {
                DBOperate.SaveMessageLog(msg, Enum_MessageType.Mail.ToString(), ex.Message);
                SLog.WriteLogWithDefaultTime(string.Format("发送失败：{0}", ex.Message));
                return false;
            }

        }

        #endregion

        /// <summary>
        /// 执行自动收货Job
        /// </summary>
        public void ExcuteAutoReceivedJob(string defaultWarehouseName,string defaultStoreName)
        {
            List<PO_OutInvOrder> shouldConfirmOutInvOrder = DBOperate.shouldConifirmOutInvOrder();

            foreach (PO_OutInvOrder item in shouldConfirmOutInvOrder)
            {
                try
                {
                    DBOperate.ConfirmDealerReceiveParts(item, defaultWarehouseName, defaultStoreName);
                }
                catch (Exception ex)
                {
                    SLog.WriteLogWithDefaultTime(string.Format("自动收货Item更新失败：{0}", ex.Message));
                    continue;
                }
            }


        }

    }
}

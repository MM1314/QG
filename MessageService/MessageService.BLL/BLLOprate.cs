using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessageService.DAL;
using MessageService.Model;
using Tytus.Message;
using System.Threading;
using System.Threading.Tasks;

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

            //Parallel.ForEach(messages, msg =>
            //{
            //    delegate_SendMail(msg);                
            //});
            //异步发送
            foreach (MessageEntity msg in messages)
            {
                delegate_SendMail(msg);
                ////带返回值
                //SendMailDelegate fh = new SendMailDelegate(this.delegate_SendMail);
                //IAsyncResult ar = fh.BeginInvoke(msg, null, fh);
                //while (!ar.IsCompleted)
                //{
                //    Thread.Sleep(100);
                //}
                //msg.IsSended = fh.EndInvoke(ar);
            }

            //DBOperate.UpdateMessageStatus(messages);
        }


        #region 委托 发邮件

        /// <summary>
        /// 委托：发送邮件
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public delegate bool SendMailDelegate(MessageEntity msg);

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
                SLog.WriteLogWithDefaultTime(string.Format("发送失败：{0} 出现问题：{1}", msg.EMails,ex.Message));
                return false;
            }

        }

        #endregion
    }
}

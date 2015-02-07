using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessageService.Model;

namespace MessageService.DAL
{
    public class DBOperate
    {
        #region 提醒消息

        /// <summary>
        /// 获取待提醒信息
        /// </summary>
        /// <returns></returns>
        public static List<MessageEntity> LoadAlertMessage(string sendType)
        {
            using (MSGEntities dbContext = new MSGEntities())
            {
                //该发送类型sendType下的发送记录
                IQueryable<MD_MessageLog> logs = from log in dbContext.MD_MessageLog
                                                 where log.SendType.Equals(sendType)
                                                 select log;

                IQueryable<MessageEntity> query = from msg in dbContext.MD_Message
                                                  join log in logs on msg.ID equals log.MessageID into logsWithNull
                                                  from log in logsWithNull.DefaultIfEmpty()

                                                  //标记未发送;已发送但无记录;有此发送类型但发送失败；
                                                  where msg.IsSended.Value == false || log == null || log.IsSuccess == false

                                                  select new MessageEntity
                                                  {
                                                      ID = msg.ID,
                                                      Receiver = msg.Receiver,
                                                      Title = msg.Title,
                                                      Content = msg.Content,
                                                      EMails = msg.Mails,
                                                      Mobiles = msg.Mobiles,
                                                      IsSended = msg.IsSended.Value,
                                                  };
                //去重复
                IEnumerable<MessageEntity> result = query.ToList().Distinct<MessageEntity>(new MessageDistinct());
                return result.ToList();
            }
        }

        /// <summary>
        /// 更新消息状态，写入发送记录
        /// </summary>
        /// <param name="msg"></param>
        public static void SaveMessageLog(MessageEntity msg, string sendType, string exception)
        {
            using (MSGEntities db = new MSGEntities())
            {
                MD_Message md = db.MD_Message.SingleOrDefault(o => o.ID == msg.ID);
                if (md == null)
                {
                    throw new Exception(string.Format("消息{0}不存在。", msg.ID));
                }
                md.IsSended = md.IsSended.Value || msg.IsSended;

                MD_MessageLog log = db.MD_MessageLog.SingleOrDefault(o => o.MessageID == msg.ID && o.SendType.Equals(sendType));
                if (log == null)
                {
                    log = new MD_MessageLog();
                    log.ID = Guid.NewGuid();
                    log.MessageID = msg.ID;
                    log.SendType = sendType;

                    db.MD_MessageLog.AddObject(log);
                }

                log.IsSuccess = msg.IsSended;
                log.Exception = exception;
                log.SendTime = DateTime.Now;


                db.SaveChanges();
            }
        }

        /// <summary>
        /// 批量更新消息状态 暂未启用
        /// </summary>
        /// <param name="messages"></param>
        public static void UpdateMessageStatus(List<MessageEntity> messages)
        {
            using (MSGEntities dbContext = new MSGEntities())
            {
                foreach (MessageEntity msg in messages)
                {
                    var record = dbContext.MD_Message.SingleOrDefault(o => o.ID == msg.ID);
                    if (record != null)
                    {
                        //已发送则保持状态
                        record.IsSended = record.IsSended.Value || msg.IsSended;
                    }
                }

                dbContext.SaveChanges();

            }
        }

        #endregion

    }
}

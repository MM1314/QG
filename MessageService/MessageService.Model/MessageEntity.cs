using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using System.Collections;

namespace MessageService.Model
{
    /// <summary>
    /// 提醒消息实体
    /// </summary>
    public class MessageEntity
    {
        /// <summary>
        /// 邮件接收人
        /// 多人时用；隔开
        /// </summary>
        public string EMails { get; set; }

        /// <summary>
        /// 短信收件人
        /// 多人时用；隔开
        /// </summary>
        public string Mobiles { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime MessageDate { get; set; }
        public DateTime SendDate { get; set; }

        /// <summary>
        /// 发送记录
        /// </summary>
        public bool IsSended { get; set; }

        /// <summary>
        /// 收件人称谓
        /// </summary>
        public string Receiver { get; set; }

        public Guid ID { get; set; }
    }

    /// <summary>
    /// 去重比较
    /// </summary>
    public class MessageDistinct : IEqualityComparer<MessageEntity>
    {
        public bool Equals(MessageEntity x, MessageEntity y)
        {
            return x.ID == y.ID;
        }

        public int GetHashCode(MessageEntity obj)
        {
            return obj.GetHashCode();
        }
    }


}

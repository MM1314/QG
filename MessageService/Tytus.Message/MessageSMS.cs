using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tytus.B2B.Core.SMSHelper;
namespace Tytus.Message
{
    /// <summary>
    /// 通过WebService发送短信
    /// 扩展MessageBase
    /// </summary>
    public class MessageSMS : MessageBase
    {
        public override string To { get; set; }

        public override void Send()
        {
            if (VerifyMsg())
            {
                string result;
                try
                {
                    SmsClient client = new SmsClient();
                    client.SendSmsModel.Content = Body;
                    client.SendSmsModel.Mobiles = GetMobiles(To);
                    client.SendSmsModel.SendTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    result = client.SendMsg();

                    if (!result.Equals("发送成功"))
                    {
                        throw new Exception(string.Format("短信发送失败：{0}", result));
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("短信发送失败：{0}", ex.Message));
                }
            }
        }

        /// <summary>
        /// 收信人列表
        /// </summary>
        /// <param name="To"></param>
        /// <returns></returns>
        private List<string> GetMobiles(string To)
        {
            List<string> lst_Mobiles = new List<string>();
            lst_Mobiles = To.Trim(';').Split(';').ToList();
            return lst_Mobiles;
        }

        /// <summary>
        /// 验证短信
        /// </summary>
        /// <returns></returns>
        private bool VerifyMsg()
        {
            try
            {
                RequestValidator.ValidateRequired("收信人", this.To);
                RequestValidator.ValidateRequired("短信内容", this.Body);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return true;
        }
    }
}

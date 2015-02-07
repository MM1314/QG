using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Tytus.Message;

using System.Configuration;
using System.Threading;

using MessageService.Model;

namespace MessageService
{
    public partial class TytusMessage : ServiceBase
    {
        #region 属性

        #region 时钟间隔设置

        /// <summary>
        /// 消息遍历间隔，默认60秒
        /// </summary>
        public string interval_Mail = ConfigurationManager.AppSettings["MsgMailInterval"] ?? "60000";

        public string interval_SMS = ConfigurationManager.AppSettings["MsgSMSInterval"] ?? "60000";
        /// <summary>
        /// Job 时钟间隔，用于精度设置
        /// </summary>
        public string interval_Job = ConfigurationManager.AppSettings["JobInterval"] ?? "120000";

        #endregion

        #region 消息发送方式设置

        const string type_Mail = "Mail";
        const string type_SMS = "SMS";

        /// <summary>
        /// 指定消息发送方式：邮件Mail；短信SMS；以分号隔开
        /// </summary>
        public string MsgSentTypes = ConfigurationManager.AppSettings["MsgSentType"] ?? "";

        #endregion


        #region Job执行设置

        /// <summary>
        /// 记录Job最近执行日期
        /// </summary>
        private DateTime lastExcuteDate { get; set; }


        private string defaultWarehouseName = ConfigurationManager.AppSettings["JobWarehouseName"]??"默认仓库";
        private string defaultStoreName = ConfigurationManager.AppSettings["JobStoreName"]??"默认储位";

        #endregion

        #endregion

        public BLL.BLLOprate serviceBLL = new BLL.BLLOprate();

        public TytusMessage()
        {
            InitializeComponent();

            lastExcuteDate = DateTime.Now.AddDays(-1).Date;
            //初始化时钟
            InitializeTimers();
        }

        /// <summary>
        /// 初始化时钟
        /// </summary>
        private void InitializeTimers()
        {
            try
            {
                tmMail.Interval = Convert.ToDouble(interval_Mail);
                
                SLog.WriteLog(string.Format("服务时钟时间间隔初始化：邮件服务{0}.单位：秒/一次。",
               tmMail.Interval / 1000));
            }
            catch (Exception ex)
            {
                SLog.WriteLogWithDefaultTime(ex.Message);
            }
        }

        protected override void OnStart(string[] args)
        {
            SLog.WriteLog("====================================================");

            try
            {
                #region 根据消息发送类型配置启动时钟

                tmMail.Enabled = false;

                if (MsgSentTypes.Contains(type_Mail))
                {
                    tmMail.Enabled = true;
                    tmMail.Start();
                }

                #endregion

            }
            catch (Exception ex)
            {
                SLog.WriteLogWithDefaultTime(string.Format("服务启动失败，详细：{0}", ex.Message));
            }

            SLog.WriteLogWithDefaultTime("服务正常启动。");

        }

        protected override void OnStop()
        {
            SLog.WriteLogWithDefaultTime("服务关闭。");
            SLog.WriteLog("====================================================");
        }


        #region 服务时钟： 短信

        /// <summary>
        /// 邮件服务时钟
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmMail_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                tmMail.Enabled = false;
                //发送邮件提醒
                serviceBLL.SendMail();
                tmMail.Enabled = true;
            }
            catch (Exception ex)
            {
                SLog.WriteLogWithDefaultTime(string.Format("邮件发送失败,详细：{0}", ex.Message));
            }

        }
        #endregion

        #region 备用方法：无返回值异步操作（线程队列）

        /// <summary>
        /// 异步调用队列
        /// </summary>
        /// <param name="msg"></param>
        void SendMethod(object msg)
        {
            MessageEntity entity = msg as MessageEntity;
            SMessage.Send(entity);
        }

        #endregion

    }
}

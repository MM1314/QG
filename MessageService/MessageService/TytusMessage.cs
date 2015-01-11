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
                tmSMS.Interval = Convert.ToDouble(interval_SMS);
                tmDealerJob.Interval = Convert.ToDouble(interval_Job);

                SLog.WriteLog(string.Format("服务时钟时间间隔初始化：邮件服务{0}；短信服务{1}.单位：秒/一次。",
               tmMail.Interval / 1000, tmSMS.Interval / 1000));
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

                tmSMS.Enabled = false;
                tmMail.Enabled = false;

                if (MsgSentTypes.Contains(type_Mail))
                {
                    tmMail.Enabled = true;
                    tmMail.Start();
                }

                if (MsgSentTypes.Contains(type_SMS))
                {
                    tmSMS.Enabled = true;
                    tmSMS.Start();
                }

                #endregion

                tmDealerJob.Start();
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


        #region 服务时钟： 短信 | 邮件 | 经销商Job

        /// <summary>
        /// 邮件服务时钟
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmMail_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                //发送邮件提醒
                serviceBLL.SendMail();
            }
            catch (Exception ex)
            {
                SLog.WriteLogWithDefaultTime(string.Format("邮件发送失败,详细：{0}", ex.Message));
            }

        }

        /// <summary>
        /// 短信服务时钟
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmSMS_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                //发送短信提醒
                serviceBLL.SendSMS();
            }
            catch (Exception ex)
            {
                SLog.WriteLogWithDefaultTime(string.Format("短信发送失败,详细：{0}", ex.Message));
            }
        }

        /// <summary>
        /// Job 时钟(负责数据库同步)
        /// 精度在配置文件中配置。Job每天执行一次，时机是：时钟能检测到的一天中最早的时候。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmJob_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (IsDealerJobExcutePoint())
            {
                try
                {
                    //执行经销商Job
                    serviceBLL.ExcuteDealerJob();
                    lastExcuteDate = DateTime.Now.Date;

                    SLog.WriteLogWithDefaultTime("经销商Job执行成功。");
                }
                catch (Exception ex)
                {
                    SLog.WriteLogWithDefaultTime(string.Format("经销商Job执行失败,详细：{0}", ex.Message));
                }
            }

            if (IsOutInvJobExcutePoint())
            {
                try
                {
                    //执行自动收货Job
                    serviceBLL.ExcuteAutoReceivedJob(defaultWarehouseName, defaultStoreName);
                   
                    SLog.WriteLogWithDefaultTime("自动收货Job执行成功。");
                }
                catch (Exception ex)
                {
                    SLog.WriteLogWithDefaultTime(string.Format("自动收货Job执行失败,详细：{0}", ex.Message));
                }
            }

        }

        /// <summary>
        /// 判断当前时间是否该执行经销商Job
        ///  </summary>
        /// <returns></returns>
        private bool IsDealerJobExcutePoint()
        {
            DateTime now_Date = DateTime.Now.Date;

            //当前日期!=最近执行日期，并且当前钟点处在执行点
            bool isExcutePoint = (now_Date != lastExcuteDate);

            return isExcutePoint;
        }


        /// <summary>
        /// 判断当前时间是否该执行出库确认Job
        ///  </summary>
        /// <returns></returns>
        private bool IsOutInvJobExcutePoint()
        {
            return true;
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

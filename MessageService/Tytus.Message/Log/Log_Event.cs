using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Tytus.Message
{
    public class Log_Event : LogBase
    {

        /// <summary>
        /// 文件源
        /// 默认Application
        /// </summary>
        private string sSource;

        /// <summary>
        /// 日志文件
        /// </summary>
        private string sLog;

        /// <summary>
        /// 日志事件类
        /// 默认目录：Application
        /// 日志源：应用程序名称 
        /// </summary>
        public Log_Event()
        {
            try
            {
                this.sLog = "Application";
                this.sSource = AppDomain.CurrentDomain.FriendlyName;

                EventLog log = new EventLog(sLog);

                if (!EventLog.SourceExists(sSource))
                    EventLog.CreateEventSource(sSource, sLog);
                log.Source = sSource;
                log.WriteEntry("开始记录", EventLogEntryType.Information);
            }
            catch (Exception ex)
            {

            }

        }

        /// <summary>
        /// 日志事件类
        /// </summary>
        /// <param name="sLog">独立日志目录名称</param>
        /// <param name="sSource">日志源名称</param>
        public Log_Event(string sLog, string sSource)
        {
            try
            {
                this.sLog = sLog;
                this.sSource = sSource;

                EventLog log = new EventLog(sLog);

                if (!EventLog.SourceExists(sSource))
                    EventLog.CreateEventSource(sSource, sLog);
                log.Source = sSource;
                log.WriteEntry("开始记录", EventLogEntryType.Information);

            }
            catch (Exception ex)
            {

            }

        }

        /// <summary>
        /// 将msg与系统时间同时写入日志文件
        /// </summary>
        /// <param name="msg">日志信息</param>
        public override void WriteLogWithDefaultTime(string msg)
        {
            try
            {
                EventLog.WriteEntry(sSource, msg);
            }
            catch (Exception ex)
            {

            }

        }

        /// <summary>
        /// 将msg写入日志文件
        /// </summary>
        /// <param name="msg">日志信息</param>
        public override void WriteLog(string msg)
        {
            try
            {
                EventLog.WriteEntry(sSource, msg);
            }
            catch (Exception ex)
            {

            }

        }

    }
}

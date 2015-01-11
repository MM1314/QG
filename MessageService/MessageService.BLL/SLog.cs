using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Diagnostics;

namespace Tytus.Message
{
    /// <summary>
    /// 服务日志封装
    /// </summary>
    public class SLog
    {
        const string const_EventLog = "EventLog";

        /// <summary>
        /// 日志输出类型
        /// 输出到文件:FileLog
        /// 输出到windows事件:EventLog(默认)
        /// </summary>
        public static string LogOutputType = ConfigurationManager.AppSettings["MsgLogOutput"] ?? const_EventLog;

        /// <summary>
        /// 日志源名称
        /// </summary>
        private static string sSource = ConfigurationManager.AppSettings["MsgLogSource"];

        /// <summary>
        /// 日志目录名称
        /// </summary>
        private static string sLog = ConfigurationManager.AppSettings["MsgLogName"];


        private static LogBase logInstance;

        /// <summary>
        /// 将msg与系统时间同时写入日志文件
        /// </summary>
        /// <param name="msg">日志信息</param>
        public static void WriteLogWithDefaultTime(string msg)
        {
            try
            {
                logInstance.WriteLogWithDefaultTime(msg);
            }
            catch (Exception ex)
            {
                new Log_File().WriteLogWithDefaultTime(ex.Message);
            }

        }

        /// <summary>
        /// 将msg写入日志文件
        /// </summary>
        /// <param name="msg">日志信息</param>
        public static void WriteLog(string msg)
        {
            try
            {
                logInstance.WriteLog(msg);
            }
            catch (Exception ex)
            {
                new Log_File().WriteLogWithDefaultTime(ex.Message);
            }

        }

        /// <summary>
        /// 根据LogOutputType 实例化对应日志对象
        /// </summary>
        static SLog()
        {
            if (logInstance == null)
            {
                if (LogOutputType.Equals(const_EventLog))
                {
                    try
                    {
                        logInstance = new Log_Event(sLog, sSource);
                    }
                    catch (Exception ex)
                    {
                        new Log_File().WriteLogWithDefaultTime(ex.Message);
                    }

                }
                else
                {
                    logInstance = new Log_File();
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
namespace Tytus.Message
{
    public class Log_File : LogBase
    {
        #region 常量

        /// <summary>
        /// 详细时间格式
        /// </summary>
        const string const_DetailTimeFormate = "yyyy-MM-dd hh:mm:ss";
        const string const_ShortTimeFormate = "yyyy-MM-dd";
        const string const_LogPath = "D:\\TytusMessageLog";

        #endregion

        /// <summary>
        /// 日志名称,带全路径
        /// </summary>
        private static string logFileName;

        private static string DirectoryPath = ConfigurationManager.AppSettings["MsgLogDirectory"] ?? const_LogPath;

        /// <summary>
        /// 将msg与系统时间同时写入日志文件
        /// </summary>
        /// <param name="msg">日志信息</param>
        public override void WriteLogWithDefaultTime(string msg)
        {

            using (StreamWriter sw = File.AppendText(logFileName))
            {
                sw.WriteLine(string.Format("{0}:{1}", DateTime.Now.ToString(const_DetailTimeFormate), msg));
            }
        }

        /// <summary>
        /// 将msg写入日志文件
        /// </summary>
        /// <param name="msg">日志信息</param>
        public override void WriteLog(string msg)
        {
            using (StreamWriter sw = File.AppendText(logFileName))
            {
                sw.WriteLine(msg);
            }
        }

        static Log_File()
        {
            //日志记录
            if (!Directory.Exists(string.Format(@"{0}\logs", DirectoryPath)))
            {
                Directory.CreateDirectory(string.Format(@"{0}\logs", DirectoryPath));
            }

            logFileName = string.Format(@"{0}\logs\{1}.txt", DirectoryPath,
                                       DateTime.Now.ToString(const_ShortTimeFormate));
            if (!File.Exists(logFileName))
            {
                using (StreamWriter logFile = File.CreateText(logFileName))
                {
                    logFile.WriteLine(string.Format("{0}:{1}", DateTime.Now.ToString(const_DetailTimeFormate), "开始记录"));
                }
            }
        }


    }
}

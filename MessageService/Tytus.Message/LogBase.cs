using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tytus.Message
{
    /// <summary>
    /// 日志记录抽象类
    /// </summary>
    public abstract class LogBase
    {
        /// <summary>
        /// 将msg与系统时间同时写入日志文件
        /// </summary>
        /// <param name="msg">日志信息</param>
        public abstract void WriteLogWithDefaultTime(string msg);

        /// <summary>
        /// 将msg写入日志文件
        /// </summary>
        /// <param name="msg">日志信息</param>
        public abstract void WriteLog(string msg);
    }
}

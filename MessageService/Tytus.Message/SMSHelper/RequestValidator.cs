using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Tytus.B2B.Core.SMSHelper
{
    public class RequestValidator
    {
        private const string ERR_MSG_PARAM_MISSING = "client-error:Missing required arguments:{0}";
        private const string ERR_MSG_PARAM_INVALID = "client-error:Invalid arguments:{0}";
        #region 验证参数是否为空
        /// <summary>
        /// 验证参数是否为空。
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="value">参数值</param>
        public static void ValidateRequired(string name, object value)
        {
            if (value == null)
            {
                throw new Exception(string.Format(ERR_MSG_PARAM_MISSING, name));
            }
            else
            {
                if (value.GetType() == typeof(string))
                {
                    string strValue = value as string;
                    if (string.IsNullOrEmpty(strValue))
                    {
                        throw new Exception(string.Format(ERR_MSG_PARAM_MISSING, name));
                    }
                }
            }
        }
        #endregion

        #region 验证字符串参数的最大长度
        /// <summary>
        /// 验证字符串参数的最大长度。
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="maxLength">最大长度</param>
        public static void ValidateMaxLength(string name, string value, int maxLength)
        {
            if (value != null && value.Length > maxLength)
            {
                throw new Exception(string.Format(ERR_MSG_PARAM_INVALID, name));
            }
        }

        #endregion

        #region 验证以逗号分隔的字符串参数的最大列表长度
        /// <summary>
        /// 验证以逗号分隔的字符串参数的最大列表长度。
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="maxSize">最大列表长度</param>
        public static void ValidateMaxListSize(string name, string value, int maxSize)
        {
            if (value != null)
            {
                string[] data = value.Split(',');
                if (data != null && data.Length > maxSize)
                {
                    throw new Exception(string.Format(ERR_MSG_PARAM_INVALID, name));
                }
            }
        }
        #endregion

        #region 验证以逗号分隔的字符串参数的最小列表长度
        /// <summary>
        /// 验证以逗号分隔的字符串参数的最小列表长度。
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="maxSize">最大列表长度</param>
        public static void ValidateMinListSize(string name, string value, int minSize)
        {
            if (value != null)
            {
                string[] data = value.Split(',');
                if (data != null && data.Length < minSize)
                {
                    throw new Exception(string.Format(ERR_MSG_PARAM_INVALID, name));
                }
            }
        }
        #endregion

        #region 验证字符串参数的最小长度
        /// <summary>
        /// 验证字符串参数的最小长度。
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="minLength">最小长度</param>
        public static void ValidateMinLength(string name, string value, int minLength)
        {
            if (value != null && value.Length < minLength)
            {
                throw new Exception(string.Format(ERR_MSG_PARAM_INVALID, name));
            }
        }
        #endregion
    }
}

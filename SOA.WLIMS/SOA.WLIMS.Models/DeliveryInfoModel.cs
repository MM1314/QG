using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SOA.WLIMS.Models
{
    /// <summary>
    /// 没有元数据文档可用。
    /// </summary>
  
    public class DeliveryInfoModel
    {
        #region 基元属性

        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>

        public global::System.Int64 ID
        { get; set; }
        [Display(Name = "订单号")]
        public global::System.String OrderCode
        { get; set; }
        [Display(Name = "所在仓库ID")]
        public Nullable<global::System.Int64> StorehouseID
        { get; set; }
        [Display(Name = "所在车辆ID")]
        public Nullable<global::System.Int64> VehicleID
        { get; set; }
        [Display(Name = "处理时间")]
        public global::System.DateTime HandleTime
        { get; set; }
        [Display(Name = "处理状态")]
        public global::System.String HandleStatus
        { get; set; }
        [Display(Name = "处理消息")]
        public global::System.String HandleMessage
        { get; set; }

        #endregion
        public override string ToString()
        {
            return string.Format("订单最新处理时间：{0} \r\n订单状态：{1} \r\n处理消息：{2}",
                HandleTime.ToShortDateString(), HandleStatus, HandleMessage);
        }

    }
}

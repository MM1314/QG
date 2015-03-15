using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SOA.WLIMS.Models
{
    public class VehicleModel
    {
        #region 基元属性
        public global::System.Int64 ID
        { get; set; }
         [Display(Name = "名称")]
        public global::System.String Name
        { get; set; }
         [Display(Name = "车牌号")]
        public global::System.String LicensePlateNumber
        { get; set; }
         [Display(Name = "状态")]
        public global::System.String Status
        { get; set; }
        [Display(Name = "微信号")]
        public global::System.String WXCode
        { get; set; }
        [Display(Name = "位置_X")]
        public global::System.String Location_X
        { get; set; }
        [Display(Name = "位置_Y")]
        public global::System.String Location_Y
        { get; set; }

        [Display(Name = "备注")]
        public global::System.String Remark
        { get; set; }

        #endregion

    }
}

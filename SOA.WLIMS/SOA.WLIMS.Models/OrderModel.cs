using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SOA.WLIMS.Models
{
    public class OrderModel
    {

        #region 基元属性

        public global::System.Int64 ID
        { get; set; }
        [Required]
        [Display(Name = "编号")]
        public global::System.String Code
        { get; set; }
        [Display(Name = "寄件人姓名")]
        public global::System.String SrcUserName
        { get; set; }
        [Display(Name = "寄件人电话")]
        public global::System.String SrcUserPhone
        { get; set; }
        [Display(Name = "寄件人地址")]
        public global::System.String SrcAddress
        { get; set; }
         [Display(Name = "寄件内容")]
        public global::System.String Contents
        { get; set; }
         [Display(Name = "寄件重量")]
        public global::System.Double ContentsWeight
        { get; set; }
         [Display(Name = "保价")]
        public Nullable<global::System.Decimal> ContentsValue
        { get; set; }
        [Required]
        [Display(Name = "收件人姓名")]
        public global::System.String ToUserName
        { get; set; }
        [Required]
        [Display(Name = "收件人电话")]
        public global::System.String ToUserPhone
        { get; set; }
        [Required]
        [Display(Name = "收件人地址")]
        public global::System.String ToAddress
        { get; set; }
         [Display(Name = "状态")]
        public global::System.String Status
        { get; set; }
         [Display(Name = "费用")]
        public Nullable<global::System.Decimal> Charges
        { get; set; }
         [Display(Name = "收件日期")]
        public global::System.DateTime PickupTime
        { get; set; }
         [Display(Name = "签收日期")]
        public Nullable<global::System.DateTime> SigninTime
        { get; set; }
        #endregion

    }
}

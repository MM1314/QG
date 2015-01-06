using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOA.WLIMS.Models
{
    public class OrderModel
    {

        #region 基元属性

        public global::System.Int64 ID
        { get; set; }
        public global::System.String Code
        { get; set; }
        public global::System.String SrcUserName
        { get; set; }
        public global::System.String SrcUserPhone
        { get; set; }
        public global::System.String SrcAddress
        { get; set; }
        public global::System.String Contents
        { get; set; }
        public global::System.Double ContentsWeight
        { get; set; }
        public Nullable<global::System.Decimal> ContentsValue
        { get; set; }
        public global::System.String ToUserName
        { get; set; }
        public global::System.String ToUserPhone
        { get; set; }
        public global::System.String ToAddress
        { get; set; }
        public global::System.String Status
        { get; set; }
        public Nullable<global::System.Decimal> Charges
        { get; set; }
        public global::System.DateTime PickupTime
        { get; set; }
        public Nullable<global::System.DateTime> SigninTime
        { get; set; }
        #endregion

    }
}

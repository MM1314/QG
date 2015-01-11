using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessageService.Model
{
    /// <summary>
    /// 采购单/销售单状态
    /// </summary>
    public enum EnumPOStatus
    {
        /// <summary>
        /// 未提交
        /// </summary>
        UnSubmit,
        /// <summary>
        /// 已提交
        /// </summary>
        Submit,
        /// <summary>
        /// 审核中（发货中)
        /// </summary>
        Approving,
        /// <summary>
        /// 审核通过
        /// </summary>
        Approved,
        /// <summary>
        /// 配货
        /// </summary>
        Picking,
        /// <summary>
        /// 拒绝
        /// </summary>
        Reject,
        /// <summary>
        /// 正常关闭
        /// </summary>
        Close,
        /// <summary>
        /// 强制关闭
        /// </summary>
        FClose,
    }

    /// <summary>
    /// 出库单状态
    /// </summary>
    public enum EnumOutInvStatus
    {
        /// <summary>
        /// 草稿
        /// </summary>
        Draft = 0,
        /// <summary>
        /// 已出库
        /// </summary>
        Out = 10,
        /// <summary>
        /// 采购订单：已收货
        /// 手术订单：已清点
        /// </summary>
        Check = 20,
    }

    /// <summary>
    /// 散件和套餐类型区别
    /// </summary>
    public enum EnumDataType
    {
        /// <summary>
        /// 套餐
        /// </summary>
        PackageData = 2,
        /// <summary>
        /// 散件
        /// </summary>
        PartsData = 1,
    }

}

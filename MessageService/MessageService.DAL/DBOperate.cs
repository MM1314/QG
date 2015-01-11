using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessageService.Model;

namespace MessageService.DAL
{
    public class DBOperate
    {
        /// <summary>
        /// 经销商授权审核通过
        /// </summary>
        public const string approved = "approved";

        /// <summary>
        /// 设置自动收货时限
        /// </summary>
        public const int autoReceiveInterval = 3;
        #region 供应商收货

        /// <summary>
        /// 供应商收货确认及订单状态修改
        /// </summary>
        /// <param name="storageID">供应商库位标识ID</param>
        /// <param name="outInvID">出库单标识ID</param>
        /// <param name="operatorUser">操作人</param>
        /// <returns></returns>
        public static void ConfirmDealerReceiveParts(PO_OutInvOrder outInvOrderID, string defaultWarehouseName, string defaultStoreName)
        {
            string operatorUser = "系统";

            using (TytusB2BEntities TDC = new TytusB2BEntities())
            {
                PO_OutInvOrder outInvOrder = TDC.PO_OutInvOrder.SingleOrDefault(o => o.OutInvID == outInvOrderID.OutInvID);

                // 更新状态
                outInvOrder.Status = "20";                                                  // 已收货

                #region 获取出库单中产品

                // 散件信息
                var queryPart = from a in TDC.PO_OutInvOrderDT
                                join b in TDC.MD_Product on a.ProductID equals b.ProductID
                                where a.OutInvID == outInvOrder.OutInvID
                                where a.DataType == (int)EnumDataType.PartsData
                                select new
                                {
                                    ProductID = a.ProductID,
                                    ProductCode = b.ProductCode,
                                    ProductName = b.ProductName,
                                    Model = b.Model,
                                    LotNo = a.LotNo,
                                    InvNum = a.DeliveryNum
                                };

                // 套装信息
                var queryPackage = from a in TDC.PO_OutInvOrderDT
                                   join b in TDC.MD_PrewiredPackageDT on a.ProductID equals b.PrewiredID
                                   join c in TDC.MD_Product on b.ProductID equals c.ProductID
                                   where a.OutInvID == outInvOrder.OutInvID
                                   where a.DataType == (int)EnumDataType.PackageData
                                   select new
                                   {
                                       ProductID = b.ProductID,
                                       ProductCode = c.ProductCode,
                                       ProductName = c.ProductName,
                                       Model = c.Model,
                                       LotNo = b.LotNo,
                                       InvNum = b.PickAmount
                                   };


                // 合并数据集获取产品列表
                var query = queryPart.Union(queryPackage);

                #endregion

                // 获取出库单对应的发货单
                PO_ShipOrder shipOrder = TDC.PO_ShipOrder.Where(x => x.ShipOrderID == outInvOrder.ShipOrderID).FirstOrDefault();

                #region 货物放入默认仓库


                // 根据库位标识ID获取供应商库房及库位信息

                // 供应商信息
                Dealer_DealerInfo dealerInfo = TDC.Dealer_DealerInfo.Where(x => x.DealerID == shipOrder.DealerID).FirstOrDefault();

                // 供应商默认库房信息
                MD_Warehouse warehouse = TDC.MD_Warehouse.Where(x => x.CompanyID == shipOrder.DealerID &&
                    x.WarehouseName == defaultWarehouseName).FirstOrDefault();

                if (warehouse == null)
                {
                    throw new Exception(string.Format("经销商{0}不存在默认仓库{1}", dealerInfo.DealerName, defaultWarehouseName));
                }

                // 供应商默认库位信息
                MD_WarehouseDT warehouseDT = TDC.MD_WarehouseDT.Where(x => x.WarehouseID == warehouse.WarehouseID &&
                    x.StorageName == defaultStoreName).FirstOrDefault();

                if (warehouseDT == null)
                {
                    throw new Exception(string.Format("经销商{0},仓库{1} 不存在默认储位{2}", dealerInfo.DealerName, defaultWarehouseName, defaultStoreName));
                }


                // 遍历产品列表
                foreach (var product in query)
                {
                    // 获取满足条件的供应商库存（产品ID、公司ID、库房ID、库位ID、批次ID）
                    Inv_InvInfo invInfo = TDC.Inv_InvInfo.Where(x => x.ProductID == product.ProductID &&
                        x.CompanyID == shipOrder.DealerID &&
                        x.WarehouseID == warehouse.WarehouseID &&
                        x.StorageID == warehouseDT.StorageID && x.LotNo == product.LotNo).FirstOrDefault();

                    // 如果返回NULL，则进行数据插入
                    if (invInfo == null)
                    {
                        // 创建新对象
                        invInfo = new Inv_InvInfo();

                        // 属性赋值
                        invInfo.InvID = Guid.NewGuid();                                                     // 库存标识ID
                        invInfo.CompanyID = shipOrder.DealerID;                                            // 供应商信息
                        invInfo.CompanyCode = shipOrder.DealerCode;                                        // 供应商代码
                        invInfo.CompanyName = shipOrder.DealerName;                                        // 供应商名称
                        invInfo.InvType = "DEALER";                                                         // 库存类型
                        invInfo.ProductID = product.ProductID;                                              // 产品标识ID
                        invInfo.ProductCode = product.ProductCode;                                          // 产品代码
                        invInfo.ProductName = product.ProductName;                                          // 产品名称
                        invInfo.Model = product.Model == null ? "" : product.Model;                         // 产品类型
                        invInfo.WarehouseID = warehouse.WarehouseID;                                        // 库房标识ID
                        invInfo.WarehouseCode = warehouse.WarehouseCode;                                    // 库房代码
                        invInfo.WarehouseName = warehouse.WarehouseName;                                    // 库房名称
                        invInfo.StorageID = warehouseDT.StorageID;                                          // 库位标识ID
                        invInfo.StorageCode = warehouseDT.StorageCode;                                      // 库位代码
                        invInfo.StorageName = warehouseDT.StorageName;                                      // 库位名称
                        invInfo.LotNo = product.LotNo;                                                      // 产品批次
                        invInfo.InvNum = product.InvNum.Value;                                              // 产品库存
                        invInfo.FreezeNum = 0;                                                              // 冻结库存
                        invInfo.IsDelete = false;                                                           // 
                        invInfo.IsDisable = false;                                                          //
                        invInfo.Creator = operatorUser;                                                     // 创建人
                        invInfo.CreateTime = DateTime.Now;                                                  // 创建日期
                        invInfo.Modifier = operatorUser;                                                    // 修改人
                        invInfo.ModifyTime = DateTime.Now;                                                  // 修改日期

                        // 表记录插入
                        TDC.Inv_InvInfo.AddObject(invInfo);
                    }

                    // 如果返回非NULL，则进行数据更新
                    else
                    {
                        invInfo.InvNum = invInfo.InvNum + product.InvNum.Value;                             // 库存增加

                        invInfo.Modifier = operatorUser;                                                    // 修改人
                        invInfo.ModifyTime = DateTime.Now;                                                  // 修改日期
                    }

                #endregion

                }

                // 是否更新采购订单状态
                bool isUpdate = true;

                #region 更新对应发货单状态


                // 如果发货单处于已出库状态
                if (shipOrder.ShipStatus.ToLower() == "outinv")
                {
                    // 获取当前发货单(除自身之外)是否存在处于10状态的出库单
                    List<PO_OutInvOrder> outInvOrds = TDC.PO_OutInvOrder.Where(x => x.ShipOrderID == shipOrder.ShipOrderID &&
                        x.Status.ToLower() == "10" && x.OutInvID != outInvOrder.OutInvID).ToList();

                    // 如果不存在，则更新发货单为已收货
                    if (outInvOrds.Count() == 0)
                    {
                        shipOrder.ShipStatus = "Received";
                    }
                }
                #endregion

                #region 判断是否可更新订单状态：订单不存在未出库发货单 & 订单所有出库单全部收货

                // 获取采购单
                PO_POrder porder = TDC.PO_POrder.Where(x => x.POID == shipOrder.POID).FirstOrDefault();

                // 未出库发货单
                List<PO_ShipOrder> shipOrders = TDC.PO_ShipOrder.Where(x => x.POID == porder.POID && x.ShipStatus.ToLower() == "ininv").ToList<PO_ShipOrder>();

                // 存在未出库的发货单则不更新对应订单状态
                if (shipOrders.Count() != 0)
                {
                    isUpdate = false;
                }

                // 订单所有出库单是否已经都收货
                else
                {
                    IQueryable<PO_OutInvOrder> queryOutInvOrder = from a in TDC.PO_OutInvOrder
                                                                  join b in TDC.PO_ShipOrder on a.ShipOrderID equals b.ShipOrderID
                                                                  join c in TDC.PO_POrder on b.POID equals c.POID
                                                                  where c.POID == porder.POID
                                                                  where a.OutInvID != outInvOrder.OutInvID
                                                                  where a.Status != EnumOutInvStatus.Check.ToString()
                                                                  select a;

                    // 如果存在未收货的单据
                    if (queryOutInvOrder.Count() != 0)
                    {
                        isUpdate = false;
                    }
                }

                #endregion

                #region 更新对应订单状态

                // 如果需要更新订单状态
                if (isUpdate == true)
                {
                    // 如果采购订单处于配货状态
                    if (porder.POStatus.ToLower() == EnumPOStatus.Picking.ToString().ToLower())
                    {
                        porder.POStatus = EnumPOStatus.Close.ToString();
                    }
                }

                #endregion

                // 数据库更新执行
                TDC.SaveChanges();
            }

        }

        /// <summary>
        /// 加载当前应该确认收货单
        /// </summary>
        /// <returns></returns>
        public static List<PO_OutInvOrder> shouldConifirmOutInvOrder()
        {
            DateTime tm = DateTime.Now.AddDays(-autoReceiveInterval);
            using (TytusB2BEntities TDC = new TytusB2BEntities())
            {
                IQueryable<PO_OutInvOrder> autoReceiveOIV = from oiv in TDC.PO_OutInvOrder
                                                            where oiv.Status.Trim() == "20"
                                                            where oiv.ModifyTime < tm
                                                            select oiv;
                return autoReceiveOIV.ToList();
            }
        }
        #endregion

        #region 经销商有效期同步

        /// <summary>
        /// 经销商Job
        /// </summary>
        public static void DelerJob()
        {
            DateTime tomorrow = DateTime.Now.AddDays(1).Date;
            DateTime today = DateTime.Now.Date;
            using (TytusB2BEntities dbContext = new TytusB2BEntities())
            {
                #region 失效：到期日期小于今天
                //当前失效经销商授权信息
                IQueryable<Dealer_AuthorizeInfo> ai_outofdate = from ai in dbContext.Dealer_AuthorizeInfo
                                                                where (ai.ContractDateTo.Value < today && ai.Status.ToLower().Trim() == approved &&
                                                                 ai.IsDelete == false && ai.IsDisable == false)
                                                                select ai;

                foreach (Dealer_AuthorizeInfo ai in ai_outofdate)
                {
                    //授权明细
                    IQueryable<Dealer_AuthorizeInfoDT> aidt_outofdate = from ai_dt in dbContext.Dealer_AuthorizeInfoDT
                                                                        where ai_dt.DealerAuthorID == ai.DealerAuthorID
                                                                        select ai_dt;
                    //授权产品线
                    IQueryable<Dealer_AuthorizeProductLine> pline_outofdate = from pline in dbContext.Dealer_AuthorizeProductLine
                                                                              where pline.DealerAuthorID == ai.DealerAuthorID
                                                                              select pline;
                    //经销商基本信息
                    IQueryable<Dealer_DealerInfo> dealer_outofdate = from dealer in dbContext.Dealer_DealerInfo
                                                                     where dealer.DealerID == ai.DealerID
                                                                     select dealer;

                    //授权信息失效

                    #region 授权信息逻辑删除（授权主信息，授权明细，授权产品线）,经销商信息失效。

                    ai.IsDelete = true;

                    foreach (Dealer_AuthorizeInfoDT item in aidt_outofdate)
                    {
                        item.IsDelete = true;
                    }

                    foreach (Dealer_AuthorizeProductLine item in pline_outofdate)
                    {
                        item.IsDelete = true;
                    }

                    foreach (Dealer_DealerInfo item in dealer_outofdate)
                    {
                        item.IsDisable = true;
                    }
                    #endregion

                }

                #endregion

                #region 生效：合同开始日期<明天

                //当前生效授权
                IQueryable<Dealer_AuthorizeInfo> ai_ofdate = from ai in dbContext.Dealer_AuthorizeInfo
                                                             where (ai.ContractDateFrm.Value < tomorrow && ai.Status.ToLower().Trim() == approved &&
                                                              ai.IsDelete == false && ai.IsDisable == true)
                                                             select ai;

                foreach (Dealer_AuthorizeInfo ai in ai_ofdate)
                {
                    //授权明细
                    IQueryable<Dealer_AuthorizeInfoDT> aidt_ofdate = from ai_dt in dbContext.Dealer_AuthorizeInfoDT
                                                                     where ai_dt.DealerAuthorID == ai.DealerAuthorID
                                                                     select ai_dt;
                    //授权产品线
                    IQueryable<Dealer_AuthorizeProductLine> pline_ofdate = from pline in dbContext.Dealer_AuthorizeProductLine
                                                                           where pline.DealerAuthorID == ai.DealerAuthorID
                                                                           select pline;
                    //经销商基本信息
                    IQueryable<Dealer_DealerInfo> dealer_ofdate = from dealer in dbContext.Dealer_DealerInfo
                                                                  where dealer.DealerID == ai.DealerID
                                                                  select dealer;

                    //授权信息生效

                    #region 授权信息生效（授权主信息，授权明细，授权产品线）,经销商信息生效、启用。

                    ai.IsDisable = false;

                    foreach (Dealer_AuthorizeInfoDT item in aidt_ofdate)
                    {
                        item.IsDisable = false;
                    }

                    foreach (Dealer_AuthorizeProductLine item in pline_ofdate)
                    {
                        item.IsDisable = false;
                    }

                    foreach (Dealer_DealerInfo item in dealer_ofdate)
                    {
                        item.IsDisable = false;
                        item.IsDelete = false;
                    }
                    #endregion

                }

                #endregion

                dbContext.SaveChanges();
            }

        }

        #endregion

        #region 提醒消息

        /// <summary>
        /// 获取待提醒信息
        /// </summary>
        /// <returns></returns>
        public static List<MessageEntity> LoadAlertMessage(string sendType)
        {
            using (TytusB2BEntities dbContext = new TytusB2BEntities())
            {
                //该发送类型sendType下的发送记录
                IQueryable<MD_MessageLog> logs = from log in dbContext.MD_MessageLog
                                                 where log.SendType.Equals(sendType)
                                                 select log;

                IQueryable<MessageEntity> query = from msg in dbContext.MD_Message
                                                  join log in logs on msg.ID equals log.MessageID into logsWithNull
                                                  from log in logsWithNull.DefaultIfEmpty()

                                                  //标记未发送;已发送但无记录;有此发送类型但发送失败；
                                                  where msg.IsSended.Value == false || log == null || log.IsSuccess == false

                                                  select new MessageEntity
                                                  {
                                                      ID = msg.ID,
                                                      Receiver = msg.Receiver,
                                                      Title = msg.Title,
                                                      Content = msg.Content,
                                                      EMails = msg.Mails,
                                                      Mobiles = msg.Mobiles,
                                                      IsSended = msg.IsSended.Value,
                                                  };
                //去重复
                IEnumerable<MessageEntity> result = query.ToList().Distinct<MessageEntity>(new MessageDistinct());
                return result.ToList();
            }
        }

        /// <summary>
        /// 更新消息状态，写入发送记录
        /// </summary>
        /// <param name="msg"></param>
        public static void SaveMessageLog(MessageEntity msg, string sendType, string exception)
        {
            using (TytusB2BEntities db = new TytusB2BEntities())
            {
                MD_Message md = db.MD_Message.SingleOrDefault(o => o.ID == msg.ID);
                if (md == null)
                {
                    throw new Exception(string.Format("消息{0}不存在。", msg.ID));
                }
                md.IsSended = md.IsSended.Value || msg.IsSended;

                MD_MessageLog log = db.MD_MessageLog.SingleOrDefault(o => o.MessageID == msg.ID && o.SendType.Equals(sendType));
                if (log == null)
                {
                    log = new MD_MessageLog();
                    log.ID = Guid.NewGuid();
                    log.MessageID = msg.ID;
                    log.SendType = sendType;

                    db.MD_MessageLog.AddObject(log);
                }

                log.IsSuccess = msg.IsSended;
                log.Exception = exception;
                log.SendTime = DateTime.Now;


                db.SaveChanges();
            }
        }

        /// <summary>
        /// 批量更新消息状态 暂未启用
        /// </summary>
        /// <param name="messages"></param>
        public static void UpdateMessageStatus(List<MessageEntity> messages)
        {
            using (TytusB2BEntities dbContext = new TytusB2BEntities())
            {
                foreach (MessageEntity msg in messages)
                {
                    var record = dbContext.MD_Message.SingleOrDefault(o => o.ID == msg.ID);
                    if (record != null)
                    {
                        //已发送则保持状态
                        record.IsSended = record.IsSended.Value || msg.IsSended;
                    }
                }

                dbContext.SaveChanges();

            }
        }

        #endregion

    }
}

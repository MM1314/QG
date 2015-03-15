using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using SOA.WLIMS.DAL;
using SOA.WLIMS.Models;
using SOA.WLIMS.Contract;

namespace SOA.WLIMS.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service1”。

    public class DeliveryService : IDeliveryService//IService<DeliveryInfoModel>
    {
        SOAWLDBEntities DB = DBFactory.CreateDB;

        public bool Add(DeliveryInfoModel model)
        {
            DB.DeliveryInfo.AddObject(new DeliveryInfo()
            {
                OrderCode = model.OrderCode,
                StorehouseID = model.StorehouseID,
                VehicleID = model.VehicleID,
                HandleTime = model.HandleTime,
                HandleStatus = model.HandleStatus,
                HandleMessage = model.HandleMessage
            });

            MD_Message message = CreatEmailMessage("物流信息", "物流信息变动，请注意查收！");
            DB.MD_Message.AddObject(message);

            DB.SaveChanges();
            return true;
        }

        /// <summary>
        /// 生成邮件提醒消息
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public MD_Message CreatEmailMessage(string title, string content)
        {
            User employe = DB.User.FirstOrDefault(o => o.Role == "Employe" && o.Enable == true && o.IsDeleted == false);
            MD_Message message = new MD_Message()
            {
                ID = Guid.NewGuid(),
                Mails = employe == null ? "1056426706@qq.com" : employe.Email,
                MsgDate = DateTime.Now,
                IsSended = false,
                Title = title,
                Content = content
            };
            return message;
        }

        public bool Modify(DeliveryInfoModel model)
        {
            DeliveryInfo oldValue = DB.DeliveryInfo.SingleOrDefault(o => o.ID == model.ID);
            if (oldValue != null)
            {
                oldValue.OrderCode = model.OrderCode;
                oldValue.StorehouseID = model.StorehouseID;
                oldValue.VehicleID = model.VehicleID;
                oldValue.HandleTime = model.HandleTime;
                oldValue.HandleStatus = model.HandleStatus;
                oldValue.HandleMessage = model.HandleMessage;
            }
            return DB.SaveChanges() == 1;
        }

        public bool Delete(int id)
        {
            DB.DeliveryInfo.DeleteObject(DB.DeliveryInfo.SingleOrDefault(o => o.ID == id));
            return DB.SaveChanges() == 1;
        }

        public DeliveryInfoModel Get(int id)
        {
            return DB.DeliveryInfo.Select(model => new DeliveryInfoModel()
            {
                ID = model.ID,
                OrderCode = model.OrderCode,
                StorehouseID = model.StorehouseID,
                VehicleID = model.VehicleID,
                HandleTime = model.HandleTime,
                HandleStatus = model.HandleStatus,
                HandleMessage = model.HandleMessage
            }).SingleOrDefault(o => o.ID == id);
        }

        public List<DeliveryInfoModel> Query(QueryParam para)
        {
            //if (para != null && para.StringValue == "OrderCode")
            //{
            //    return DB.DeliveryInfo.Select(model => new DeliveryInfoModel()
            //{
            //    ID = model.ID,
            //    OrderCode = model.OrderCode,
            //    StorehouseID = model.StorehouseID,
            //    VehicleID = model.VehicleID,
            //    HandleTime = model.HandleTime,
            //    HandleStatus = model.HandleStatus,
            //    HandleMessage = model.HandleMessage
            //}).Where(o => o.OrderCode == para.Param["OrderCode"]).OrderByDescending(o => o.HandleTime).ToList();
            //}
            return null;
            //return DB.DeliveryInfo.Select(model => new DeliveryInfoModel()
            //{
            //    ID = model.ID,
            //    OrderCode = model.OrderCode,
            //    StorehouseID = model.StorehouseID,
            //    VehicleID = model.VehicleID,
            //    HandleTime = model.HandleTime,
            //    HandleStatus = model.HandleStatus,
            //    HandleMessage = model.HandleMessage
            //}).Where(o => 1 == 1).ToList();
        }
    }
}

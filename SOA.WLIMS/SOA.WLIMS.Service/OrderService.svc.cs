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
    public class OrderService : IOrderService
    {
        SOAWLDBEntities DB = DBFactory.CreateDB;

        public bool Add(OrderModel model)
        {
            DB.Order.AddObject(new Order()
            {
                Charges = model.Charges,
                Code = model.Code,
                Contents = model.Contents,
                ContentsValue = model.ContentsValue,
                ContentsWeight = model.ContentsWeight,
                ID = model.ID,
                PickupTime = model.PickupTime,
                SigninTime = model.SigninTime,
                SrcAddress = model.SrcAddress,
                SrcUserName = model.SrcUserName,
                SrcUserPhone = model.SrcUserPhone,
                ToAddress = model.ToAddress,
                Status = model.Status,
                ToUserName = model.ToUserName,
                ToUserPhone = model.ToUserPhone
            });
            DB.SaveChanges();
            return true;
        }

        public bool Modify(OrderModel model)
        {
            Order oldValue = DB.Order.SingleOrDefault(o => o.ID == model.ID);
            if (oldValue != null)
            {
                oldValue.Status = model.Status;
                oldValue.ToUserName = model.ToUserName;
                oldValue.ToAddress = model.ToAddress;
                oldValue.ToUserPhone = model.ToUserPhone;
            }
            return DB.SaveChanges() == 1;
        }

        public bool Delete(int id)
        {
            DB.Order.DeleteObject(DB.Order.SingleOrDefault(o => o.ID == id));
            return DB.SaveChanges() == 1;
        }

        public OrderModel Get(int id)
        {
            return DB.Order.Select(model => new OrderModel()
            {
                Charges = model.Charges,
                Code = model.Code,
                Contents = model.Contents,
                ContentsValue = model.ContentsValue,
                ContentsWeight = model.ContentsWeight,
                ID = model.ID,
                PickupTime = model.PickupTime,
                SigninTime = model.SigninTime,
                SrcAddress = model.SrcAddress,
                SrcUserName = model.SrcUserName,
                SrcUserPhone = model.SrcUserPhone,
                ToAddress = model.ToAddress,
                Status = model.Status,
                ToUserName = model.ToUserName,
                ToUserPhone = model.ToUserPhone
            }).SingleOrDefault(o => o.ID == id);
        }

        public List<OrderModel> Query(QueryParam para)
        {
            return DB.Order.Select(model => new OrderModel()
            {
                Charges = model.Charges,
                Code = model.Code,
                Contents = model.Contents,
                ContentsValue = model.ContentsValue,
                ContentsWeight = model.ContentsWeight,
                ID = model.ID,
                PickupTime = model.PickupTime,
                SigninTime = model.SigninTime,
                SrcAddress = model.SrcAddress,
                SrcUserName = model.SrcUserName,
                SrcUserPhone = model.SrcUserPhone,
                ToAddress = model.ToAddress,
                Status = model.Status,
                ToUserName = model.ToUserName,
                ToUserPhone = model.ToUserPhone
            }).Where(o => 1 == 1).ToList();
        }
    }
}

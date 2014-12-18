using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using SOA.WLIMS.Service.DAL;

namespace SOA.WLIMS.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service1”。
    public class OrderService : IOrderService
    {
        SOAWLDBEntities DB = DBFactory.CreateDB;


        public bool Add(Order model)
        {
            DB.Order.AddObject(model);
            DB.SaveChanges();
            return true;
        }

        public bool Modify(Order model)
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

        public Order Get(int id)
        {
            return DB.Order.SingleOrDefault(o => o.ID == id);
        }

        public List<Order> Query(QueryParam para)
        {
            return DB.Order.Where(o => 1 == 1).ToList();
        }
    }
}

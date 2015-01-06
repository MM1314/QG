using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SOA.WLIMS.DAL;
using SOA.WLIMS.Models;
using SOA.WLIMS.Contract;

namespace SOA.WLIMS.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“StoreService”。
    public class StoreService : IService<StorehouseModel>
    {
        SOAWLDBEntities DB = DBFactory.CreateDB;
        public bool Add(StorehouseModel model)
        {
            DB.Storehouse.AddObject(new Storehouse()
            {
                Name = model.Name,
                Address = model.Address,
                Master = model.Master,
                Capacity = model.Capacity
            });

            DB.SaveChanges();
            return true;
        }

        public bool Modify(StorehouseModel model)
        {
            Storehouse oldValue = DB.Storehouse.SingleOrDefault(o => o.ID == model.ID);
            if (oldValue != null)
            {
                oldValue.Name = model.Name;
                oldValue.Address = model.Address;
                oldValue.Master = model.Master;
                oldValue.Capacity = model.Capacity;
            }
            return DB.SaveChanges() == 1;
        }

        public bool Delete(int id)
        {
            DB.Storehouse.DeleteObject(DB.Storehouse.SingleOrDefault(o => o.ID == id));
            return DB.SaveChanges() == 1;
        }

        public StorehouseModel Get(int id)
        {
            return DB.Storehouse.Select(model => new StorehouseModel()
            {
                ID = model.ID,
                Name = model.Name,
                Address = model.Address,
                Master = model.Master,
                Capacity = model.Capacity

            }).SingleOrDefault(o => o.ID == id);
        }

        public List<StorehouseModel> Query(QueryParam para)
        {
            return DB.Storehouse.Select(model => new StorehouseModel()
            {
                ID = model.ID,
                Name = model.Name,
                Address = model.Address,
                Master = model.Master,
                Capacity = model.Capacity

            }).Where(o => 1 == 1).ToList();
        }
    }
}

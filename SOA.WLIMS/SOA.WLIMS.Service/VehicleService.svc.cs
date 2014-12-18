using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SOA.WLIMS.Service.DAL;


namespace SOA.WLIMS.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“VehicleService”。
    public class VehicleService : IService<Vehicle>
    {
        SOAWLDBEntities DB = DBFactory.CreateDB;
        public bool Add(Vehicle model)
        {
            DB.Vehicle.AddObject(new Vehicle()
            {
                Name = model.Name,
                LicensePlateNumber = model.LicensePlateNumber,
                Status = model.Status
            });

            DB.SaveChanges();
            return true;
        }

        public bool Modify(Vehicle model)
        {
            Vehicle oldValue = DB.Vehicle.SingleOrDefault(o => o.ID == model.ID);
            if (oldValue != null)
            {
                oldValue.Status = model.Status;
                oldValue.Name = model.Name;
                oldValue.LicensePlateNumber = model.LicensePlateNumber;
            }
            return DB.SaveChanges() == 1;
        }

        public bool Delete(int id)
        {
            DB.Vehicle.DeleteObject(DB.Vehicle.SingleOrDefault(o => o.ID == id));
            return DB.SaveChanges() == 1;
        }

        public Vehicle Get(int id)
        {
            return DB.Vehicle.SingleOrDefault(o => o.ID == id);
        }

        public List<Vehicle> Query(QueryParam para)
        {
            return DB.Vehicle.Where(o => 1 == 1).ToList();
        }
    }
}

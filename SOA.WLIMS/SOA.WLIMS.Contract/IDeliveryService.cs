using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SOA.WLIMS.DAL;
using SOA.WLIMS.Models;


namespace SOA.WLIMS.Contract
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface IDeliveryService
    {
        [OperationContract]
        bool Add(DeliveryInfoModel model);
        [OperationContract]
        bool Modify(DeliveryInfoModel model);
        [OperationContract]
        bool Delete(int id);
        [OperationContract]
        DeliveryInfoModel Get(int id);
        [OperationContract]
        List<DeliveryInfoModel> Query(QueryParam para);
    }

}

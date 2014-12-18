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
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface IOrderService
    {
        [OperationContract]
        bool Add(Order order);
        [OperationContract]
        bool Modify(Order order);
        [OperationContract]
        bool Delete(int id);
        [OperationContract]
        Order Get(int id);
        [OperationContract]
        List<Order> Query(QueryParam para);
    }


    // 使用下面示例中说明的数据约定将复合类型添加到服务操作。
    /// <summary>
    /// 查询参数
    /// </summary>
    [DataContract]
    public class QueryParam
    {
        bool? boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool? BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
        [DataMember]
        Dictionary<string, string> Param { get; set; }
    }
}

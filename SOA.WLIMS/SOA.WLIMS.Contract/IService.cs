using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using SOA.WLIMS.Models;

namespace SOA.WLIMS.Contract
{
    [ServiceContract]
    public interface IService<T>
    {
        [OperationContract]
        bool Add(T model);
        [OperationContract]
        bool Modify(T model);
        [OperationContract]
        bool Delete(int id);
        [OperationContract]
        T Get(int id);
        [OperationContract]
        List<T> Query(QueryParam para);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;

namespace SOA.WLIMS.Service
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
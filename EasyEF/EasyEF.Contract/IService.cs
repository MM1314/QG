using System;
using System.ServiceModel;
using System.Collections.Generic;
using EasyEF.Models;
using EasyEF.Common;

namespace EasyEF.Contract
{
    [ServiceContract]
    public interface IService
    {   
        [OperationContract(IsOneWay = false)]
        PagedList<Product> GetProducts(int pageSize, int pageIndex, int categoryId = 0);
    }
}

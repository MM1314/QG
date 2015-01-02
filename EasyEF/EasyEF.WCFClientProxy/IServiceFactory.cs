using System;
using EasyEF.Contract;

namespace EasyEF.WCFClientProxy
{
    public  interface IServiceFactory
    {
        IService CreateService();
    }
}

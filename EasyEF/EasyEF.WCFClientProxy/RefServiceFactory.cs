using System;
using EasyEF.Contract;
using EasyEF.BLL;

namespace EasyEF.WCFClientProxy
{
    public class RefServiceFactory : IServiceFactory
    {
        public IService CreateService()
        {
            return new Service();
        }
    }
}

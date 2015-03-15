using System;
using SOA.WLIMS.Contract;
using SOA.WLIMS.BLL;

namespace SOA.WLIMS.WCFClientProxy
{
    public class RefServiceFactory : IServiceFactory
    {
        public IUserService CreateService()
        {
            return new UserBLL();
        }


        public IUserService GetUserService()
        {
            throw new NotImplementedException();
        }

        public IService<Models.VehicleModel> GetVehicleService()
        {
            throw new NotImplementedException();
        }

        public IOrderService GetOrderService()
        {
            throw new NotImplementedException();
        }

        public IService<Models.StorehouseModel> GetStorehouseService()
        {
            throw new NotImplementedException();
        }


        public IDeliveryService GetDeliveryService()
        {
            throw new NotImplementedException();
        }
    }
}

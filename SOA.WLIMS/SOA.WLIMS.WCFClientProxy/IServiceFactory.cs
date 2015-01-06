using System;
using SOA.WLIMS.Contract;
using SOA.WLIMS.Models;

namespace SOA.WLIMS.WCFClientProxy
{
    public interface IServiceFactory
    {
        /// <summary>
        /// 用户管理服务
        /// </summary>
        /// <returns></returns>
        IUserService GetUserService();

        /// <summary>
        /// 车辆管理服务
        /// </summary>
        /// <returns></returns>
        IService<VehicleModel> GetVehicleService();
        /// <summary>
        /// 订单管理服务
        /// </summary>
        /// <returns></returns>
        IOrderService GetOrderService();
        /// <summary>
        /// 仓库管理服务
        /// </summary>
        /// <returns></returns>
        IService<StorehouseModel> GetStorehouseService()
        ;
    }
}

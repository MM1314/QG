using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SOA.WLIMS.Web.WCFService.Vehicle;
using SOA.WLIMS.Web.WCFService.User;
using SOA.WLIMS.Web.WCFService.Store;
using SOA.WLIMS.Web.WCFService.Order;

namespace SOA.WLIMS.Web
{
    public class ServiceFactory
    {
        /// <summary>
        /// 用户管理服务
        /// </summary>
        /// <returns></returns>
        public static IUserService GetUserService()
        {
            return new UserServiceClient();
        }
        /// <summary>
        /// 车辆管理服务
        /// </summary>
        /// <returns></returns>
        public static IServiceOf_Vehicle GetVehicleService()
        {
            return new ServiceOf_VehicleClient();
        }
        /// <summary>
        /// 订单管理服务
        /// </summary>
        /// <returns></returns>
        public static IOrderService GetOrderService()
        {
            return new OrderServiceClient();
        }
        /// <summary>
        /// 仓库管理服务
        /// </summary>
        /// <returns></returns>
        public static IServiceOf_Storehouse GetStorehouseService()
        {
            return new ServiceOf_StorehouseClient();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SOA.WLIMS.Contract;
using SOA.WLIMS.Models;

namespace SOA.WLIMS.WCFClientProxy
{
    public class ServiceFactory
    {
        public static IServiceFactory Factory
        {
            get
            {
                //Need to inject dynamic later
                return new RemoteServiceFactory();
            }
        }               

        /// <summary>
        /// 用户管理服务
        /// </summary>
        /// <returns></returns>
        public static IUserService GetUserService()
        {
            //return new WCFService.User.UserServiceClient();
            return Factory.GetUserService();
        }

        /// <summary>
        /// 车辆管理服务
        /// </summary>
        /// <returns></returns>
        public static IService<VehicleModel> GetVehicleService()
        {
            return Factory.GetVehicleService();
        }
        /// <summary>
        /// 订单管理服务
        /// </summary>
        /// <returns></returns>
        public static IOrderService GetOrderService()
        {
            return Factory.GetOrderService();
        }
        /// <summary>
        /// 仓库管理服务
        /// </summary>
        /// <returns></returns>
        public static IService<StorehouseModel> GetStorehouseService()
        {
            return Factory.GetStorehouseService();
        }
        /// <summary>
        /// 配送信息管理服务
        /// </summary>
        /// <returns></returns>
        public static IDeliveryService GetDeliveryService()
        {
            return Factory.GetDeliveryService();
        }
    }
}
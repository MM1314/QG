using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SOA.WLIMS.Web.WCFService;
using SOA.WLIMS.Web.WCFService.Vehicle;

namespace SOA.WLIMS.Web
{
    public class ServiceFactory
    {
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
    }
}
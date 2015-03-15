using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using WX.Framework;
using WX.Model;
using SOA.WLIMS.WCFClientProxy;
using SOA.WLIMS.Models;

namespace WX.Demo.WebClasses
{
    public class LocationEventMessageHandler : TextMessageHandler
    {
        private static string subScribeMsg = "收到位置";

        public LocationEventMessageHandler(MiddleMessage msg)
            : base(subScribeMsg)
        {
            MyLog.Log("LocationEventMessageHandler");
            var locationMsg = (RequestLocationMessage)msg.RequestMessage;
            MyLog.Log(locationMsg.FromUserName);
            try
            {
                var vehicles = ServiceFactory.GetVehicleService().Query(new QueryParam()
                {
                    StringValue = "WXCode",
                    Param = new Dictionary<string, string> { { "WXCode", locationMsg.FromUserName.Trim() } }
                });

                MyLog.Log(locationMsg.FromUserName);
                if (vehicles==null||vehicles.Count==0)
                {
                    MyLog.Log(string.Format("根据微信号未找到对应的车辆。微信号：{0}",locationMsg.FromUserName));
                    return;
                }

                var vehicle=vehicles.FirstOrDefault();
                MyLog.Log(vehicle.WXCode);
                vehicle.Location_X = locationMsg.Location_X.ToString();
                vehicle.Location_Y = locationMsg.Location_Y.ToString();
                ServiceFactory.GetVehicleService().Modify(vehicle);
            }
            catch (Exception ex)
            {

                MyLog.Log(ex.ToString());

                throw;
            }

        }
    }
}

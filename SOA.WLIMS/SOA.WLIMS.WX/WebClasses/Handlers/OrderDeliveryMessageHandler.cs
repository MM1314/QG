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
    public class OrderDeliveryMessageHandler : IMessageHandler
    {
        private const string MSG_NOFIND = "对不起，未查询到与您提供的订单号相匹配的信息！";

        public ResponseMessage HandlerRequestMessage(MiddleMessage msg)
        {
            string orderCode = ((RequestTextMessage)msg.RequestMessage).Content.Trim();
            List<DeliveryInfoModel> deliveryInfo = ServiceFactory.GetOrderService().QueryDelivery(orderCode);

            string content = MSG_NOFIND;
            if (deliveryInfo != null && deliveryInfo.Count > 0)
            {
                content = deliveryInfo.First().ToString();
            }
            
            return new ResponseTextMessage(msg.RequestMessage)
            {
                CreateTime = DateTime.Now.Ticks,
                Content = content
            };
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using WX.Framework;
using WX.Model;
using WX.Demo.WebClasses.Roles;

namespace WX.Demo.WebClasses
{
    public class TextMessageRole : IMessageRole
    {
        public IMessageHandler MessageRole(MiddleMessage msg)
        {
            var request = (RequestTextMessage)msg.RequestMessage;

            if (RequestForOrderInfo(request))
            {
                return new OrderDeliveryMessageHandler();
            }

            if (RequestForCnblogsArtiles(request))
            {
                return new CnblogsArticleNewsMessageHandler();
            }

            if (RequestForCnblogs(request))
            {
                return new CnblogsTextMessageHandler();
            }


            return new DefaultMessageHandler();
        }

        /// <summary>
        /// 判断请求是否是查询订单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private bool RequestForOrderInfo(RequestTextMessage request)
        {
            return true;
        }

        private static bool RequestForCnblogs(RequestTextMessage request)
        {
            return request.Content.IndexOf("博客园") > -1;
        }

        private static bool RequestForCnblogsArtiles(RequestTextMessage request)
        {
            return request.Content.IndexOf("博客园文章") > -1;
        }
    }
}
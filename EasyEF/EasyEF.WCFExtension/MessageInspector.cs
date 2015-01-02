using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;

namespace EasyEF.WCFExtension
{
    public class MessageInspector:IClientMessageInspector,IDispatchMessageInspector
    {
        #region IClientMessageInspector
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            var contextHeader = new MessageHeader<WCFContext>(WCFContext.Current);
            request.Headers.Add(contextHeader.GetUntypedHeader(WCFContext.ContextHeaderLocalName, WCFContext.ContextHeaderNamespace));
            return null;
        }
        #endregion 

        #region IDispatchMessageInspector
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            var context = request.Headers.GetHeader<WCFContext>(WCFContext.ContextHeaderLocalName, WCFContext.ContextHeaderNamespace);
            WCFContext.Current = context;
            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            
        }
        #endregion
    }
}

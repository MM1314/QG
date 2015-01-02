using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using System.ServiceModel.Configuration;

namespace EasyEF.WCFExtension
{
    public class InspectorBehavior : BehaviorExtensionElement, IEndpointBehavior, IServiceBehavior, IOperationBehavior
    {
        public override Type BehaviorType
        {
            get { return typeof(InspectorBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new InspectorBehavior();
        }
        
        #region IEndpointBehavior
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(new MessageInspector());
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new MessageInspector());
        }

        public void Validate(ServiceEndpoint endpoint)
        {
            
        }
        #endregion

        #region IServiceBehavior
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
            
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            //foreach (var endPoint in serviceHostBase.Description.Endpoints)
            //{
            //    endPoint.Behaviors.Add(this);
            //}
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            
        }
        #endregion

        #region IOperationBehavior
        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {
            
        }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
            //clientOperation.Parent.MessageInspectors.Add(new MessageInspector());
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            //dispatchOperation.Parent.MessageInspectors.Add(new MessageInspector());
        }

        public void Validate(OperationDescription operationDescription)
        {
            
        }
        #endregion
    }
}
